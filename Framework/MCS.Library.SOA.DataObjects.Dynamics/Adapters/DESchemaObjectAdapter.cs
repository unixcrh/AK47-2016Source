using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library.Script;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Caching;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// 最基本的对象适配器。从SchemaObjectAdapterBase派生，是权限中心所有对象的读写器。
    /// 基类SchemaObjectAdapterBase中主要实现了更新操作，而SchemaObjectAdapter主要实现了对象的读（Load）操作。
    /// </summary>
    public class DESchemaObjectAdapter : DESchemaObjectAdapterBase<DESchemaObjectBase>
    {
        /// <summary>
        /// <see cref="DESchemaObjectAdapter"/>的实例，此字段为只读
        /// </summary>
        public static readonly DESchemaObjectAdapter Instance = new DESchemaObjectAdapter();

        private DESchemaObjectAdapter()
        {
        }

        /// <summary>
        /// 带缓存的，根据ID载入数据。缓存的Key为租户+ID+Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loadNormalStatus"></param>
        /// <returns></returns>
        public DESchemaObjectBase Get(string id)
        {
            string cacheKey = CalculateIDCacheKey(id);

            return DESchemaObjectByIDCache.Instance.GetOrAddNewValue(cacheKey, (cache, key) =>
            {
                DESchemaObjectBase result = this.Load(id);

                MixedDependency dependency = new MixedDependency(new UdpNotifierCacheDependency(), new MemoryMappedFileNotifierCacheDependency());

                cache.Add(key, result, dependency);

                return result;
            });
        }

        /// <summary>
        /// 根据ID载入数据
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <param name="loadNormalStatus">对象状态为Normal</param>
        /// <returns><see cref="DESchemaObjectBase"/>的派生类型的实例</returns>
        public DESchemaObjectBase Load(string id, bool loadNormalStatus = true)
        {
            id.CheckStringIsNullOrEmpty<ArgumentNullException>("id");

            DateTime dtMin = TimePointContext.Current.UseCurrentTime ? DateTime.MinValue : TimePointContext.Current.SimulatedTime;

            DESchemaObjectBase result = this.Load(id, dtMin, loadNormalStatus);

            (loadNormalStatus && result.Status != SchemaObjectStatus.Normal).TrueThrow(String.Format("ID为[{0}]的对象状态无效", id));

            return result;
        }

        /// <summary>
        /// 更新数据，更新之后清除缓存
        /// </summary>
        /// <param name="data">需要更新的数据</param>
        public new void Update(DESchemaObjectBase data)
        {
            base.Update(data);

            UpdateDESchemaObjectByIDCache(data.ID);
        }

        /// <summary>
        /// 更新数据的状态，更新之后清除缓存
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        public new void UpdateStatus(DESchemaObjectBase data, SchemaObjectStatus status)
        {
            base.UpdateStatus(data, status);

            UpdateDESchemaObjectByIDCache(data.ID);
        }

        /// <summary>
        /// 根据ID和时间载入对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <param name="timePoint">表示时间点的<see cref="DateTime"/> 或 <see cref="DateTime.MinValue"/>表示当前时间</param>
        /// <returns><see cref="DESchemaObjectBase"/>的派生类型的实例</returns>
        public DESchemaObjectBase Load(string id, DateTime timePoint, bool loadNormalStatus = true)
        {
            id.NullCheck("id");

            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("ID");

            inBuilder.AppendItem(id);

            DESchemaObjectCollection objs = Load(inBuilder, timePoint, loadNormalStatus);

            if (!objs.Any())
            {
                throw new Exception(string.Format("{0}不能找到ID为{1}的对象", (timePoint == DateTime.MinValue ? string.Empty : string.Format("在{0}时间下", timePoint.ToString("yyyy-MM-dd HH:mm:ss"))), id));
            }

            return objs.Single();
        }

        public bool Exist(string id, DateTime timePoint)
        {
            id.NullCheck("id");

            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("ID");

            inBuilder.AppendItem(id);

            DESchemaObjectCollection objs = Load(inBuilder, timePoint, false);

            return objs.Any();
        }

        /// <summary>
        /// 根据<see cref="IConnectiveSqlClause"/>指定的条件载入对象
        /// </summary>
        /// <param name="condition">表示条件的<see cref="IConnectiveSqlClause"/></param>
        /// <returns>一个<see cref="DESchemaObjectCollection"/>，包含条件指定的对象。</returns>
        public DESchemaObjectCollection Load(IConnectiveSqlClause condition, bool loadNormalStatus = true)
        {
            return Load(condition, DateTime.MinValue, loadNormalStatus);
        }

        /// <summary>
        /// 根据<see cref="IConnectiveSqlClause"/>指定的条件和时间点载入对象
        /// </summary>
        /// <param name="condition">表示条件的<see cref="IConnectiveSqlClause"/></param>
        /// <param name="timePoint">时间点 - 或 - <see cref="DateTime.MinValue"/>表示当前时间</param>
        /// <returns>一个<see cref="DESchemaObjectCollection"/>，包含条件指定的对象。</returns>
        public DESchemaObjectCollection Load(IConnectiveSqlClause condition, DateTime timePoint, bool loadNormalStatus = true)
        {
            var timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            DESchemaObjectCollection result = new DESchemaObjectCollection();

            if (condition.IsEmpty == false)
            {
                ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(condition, timePointBuilder);

                if (loadNormalStatus)
                {
                    WhereSqlClauseBuilder statusBuilder = new WhereSqlClauseBuilder();
                    statusBuilder.AppendItem("Status", (int)SchemaObjectStatus.Normal);
                    connectiveBuilder.Add(statusBuilder);
                }

                using (DbContext context = DbContext.GetContext(this.GetConnectionName()))
                {
                    VersionedObjectAdapterHelper.Instance.FillData(GetMappingInfo().TableName, connectiveBuilder, this.GetConnectionName(),
                        reader =>
                        {
                            result.LoadFromDataReader(reader);
                        });
                }
            }

            return result;
        }

        //private DESchemaObjectBase LoadByCodeNameInner(string codeName, DateTime timePoint)
        //{
        //    string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

        //    InSqlClauseBuilder inbuilder = new InSqlClauseBuilder("CodeName");
        //    inbuilder.AppendItem(codeName);

        //    WhereSqlClauseBuilder wherebuilder = new WhereSqlClauseBuilder();
        //    wherebuilder.AppendItem("Status", 1);

        //    ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And);

        //    connectiveBuilder.Add(inbuilder);
        //    connectiveBuilder.Add(wherebuilder);


        //    var timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);
        //    connectiveBuilder.Add(timePointBuilder);

        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendFormat(@"SELECT TOP 1 ID FROM {0} WHERE {1}",
        //                        "DE.SchemaObjectSnapshot",
        //                        connectiveBuilder.ToSqlString(TSqlBuilder.Instance)
        //                    );


        //    string _EntityID = Convert.ToString(DbHelper.RunSqlReturnScalar(sql.ToString(), this.GetConnectionName()));

        //    DESchemaObjectBase entity = null;
        //    if (_EntityID.IsNotEmpty())
        //        entity = this.Load(_EntityID);

        //    return entity;
        //}

        //public DESchemaObjectBase LoadByCodeName(string codeName, DateTime timePoint)
        //{
        //    string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

        //    string key = codeName.Replace("/", "") + timePoint.Ticks.ToString();

        //    DESchemaObjectBase entity = null;

        //    if (HttpRuntime.Cache.Get(key) == null)
        //    {
        //        entity = this.LoadByCodeNameInner(codeName, timePoint);

        //        entity.NullCheck<ArgumentNullException>(string.Format("不能找到CodeName为[{0}]的对象", codeName));

        //        HttpRuntime.Cache.Insert(key, entity, null, SNTPClient.AdjustedTime.SimulateTime().AddHours(1), TimeSpan.Zero);
        //    }
        //    else
        //    {
        //        entity = HttpRuntime.Cache.Get(key) as DESchemaObjectBase;
        //    }

        //    return entity;
        //}

        //public DESchemaObjectBase LoadByCodeName(string codeName)
        //{
        //    string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

        //    return this.LoadByCodeName(codeName, DateTime.MinValue);
        //}

        /// <summary>
        /// 生成CodeName
        /// </summary>
        /// <param name="categoryID">分类编码</param>
        /// <param name="entityName">实体名称</param>
        /// <returns></returns>
        public string BuildCodeName(string categoryID, string entityName)
        {
            categoryID.CheckStringIsNullOrEmpty<ArgumentNullException>("categoryID");
            entityName.CheckStringIsNullOrEmpty<ArgumentNullException>("entityName");

            DECategory category = CategoryAdapter.Instance.GetByID(categoryID);
            string codeName = string.Format("{0}/{1}", category.FullPath, entityName);

            ExceptionHelper.TrueThrow(DEDynamicEntityAdapter.Instance.ExistByCodeName(codeName, SNTPClient.AdjustedTime.SimulateTime()), "已存在" + codeName);

            return codeName;
        }

        /// <summary>
        /// 检查CodeName有唯一性
        /// </summary>
        /// <param name="categoryID">分类编码</param>
        /// <param name="entityName">实体名称</param>
        /// <returns></returns>
        public bool CheckCodeNameExist(string categoryID, string entityName)
        {
            categoryID.CheckStringIsNullOrEmpty<ArgumentNullException>("categoryID");
            entityName.CheckStringIsNullOrEmpty<ArgumentNullException>("entityName");
            bool result = false;

            try
            {
                this.BuildCodeName(categoryID, entityName);
                result = false;
            }
            catch (Exception)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 按照SchemaType加载对象
        /// </summary>
        /// <param name="schemaTypes"></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DESchemaObjectCollection LoadBySchemaType(string[] schemaTypes, DateTime timePoint)
        {
            schemaTypes.NullCheck("schemaTypes");

            InSqlClauseBuilder builder = new InSqlClauseBuilder("SchemaType");

            builder.AppendItem(schemaTypes);

            return Load(builder, timePoint);
        }

        /// <summary>
        /// 清除所有数据。慎用，仅测试使用
        /// </summary>
        public void ClearAllData()
        {
            DbHelper.RunSql("EXEC DE.ClearAllData", this.GetConnectionName());
        }

        public void InitAllData()
        {
            DbHelper.RunSql("EXEC DE.InitData", this.GetConnectionName());
        }

        private static string CalculateIDCacheKey(string id)
        {
            string result = id;

            if (TenantContext.Current.Enabled)
                result = string.Format("{0}-{1}", TenantContext.Current.TenantCode, id);

            return result;
        }

        private static void UpdateDESchemaObjectByIDCache(string id)
        {
            CacheNotifyData notifyData = new CacheNotifyData(typeof(DESchemaObjectByIDCache), CalculateIDCacheKey(id), CacheNotifyType.Invalid);

            UdpCacheNotifier.Instance.SendNotifyAsync(notifyData);
            MmfCacheNotifier.Instance.SendNotify(notifyData);
        }

        ///// <summary>
        ///// 根据指定的CodeName和Schema类型载入对象
        ///// </summary>
        ///// <param name="schemas">指定包含的schemas</param>
        ///// <param name="codeNames">指定包含的codeName</param>
        ///// <param name="normalOnly">表示是否查找所有状态的对象</param>
        ///// <param name="ignoreVersions">表示是否忽略时间版本因素</param>
        ///// <param name="timePoint">当<paramref name="ignoreVersions"/>为<see langword="true"/>时，表示指定的时间点版本</param>
        ///// <returns>如果为<see langword="true"/>表示存在，或<see langword="true"/>表示不存在</returns>
        //public DESchemaObjectCollection LoadByCodeNameAndSchema(string schemas, string codeNames, bool normalOnly, bool ignoreVersions, DateTime timePoint)
        //{
        //    if (schemas == null || schemas.Length == 0)
        //        throw new ArgumentNullException("schemas");
        //    if (codeNames == null || codeNames.Length == 0)
        //        throw new ArgumentNullException("codeNames");

        //    WhereSqlClauseBuilder where = new WhereSqlClauseBuilder();

        //    where.AppendItem("1", (int)1);

        //    if (normalOnly)
        //    {
        //        where.AppendItem("O.Status", (int)SchemaObjectStatus.Normal);
        //    }

        //    ConnectiveSqlClauseCollection conditions = new ConnectiveSqlClauseCollection(where);

        //    if (ignoreVersions == false)
        //    {
        //        conditions.Add(VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint, "S."));
        //        conditions.Add(VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint, "O."));
        //    }

        //    if (schemas.Length > 1)
        //    {
        //        var inSchemas = new InSqlClauseBuilder("S.SchemaType");
        //        inSchemas.AppendItem(schemas);
        //    }
        //    else
        //    {
        //        where.AppendItem("S.SchemaType", schemas[0]);
        //    }

        //    if (codeNames.Length > 1)
        //    {
        //        var inCodeName = new InSqlClauseBuilder("S.CodeName");
        //        inCodeName.AppendItem(codeNames);
        //        conditions.Add(inCodeName);
        //    }
        //    else
        //    {
        //        where.AppendItem("S.CodeName", codeNames[0]);
        //    }

        //    return this.LoadByCodeNameInner(conditions);
        //}

        //public bool CheckExistCodeName(string schemas, string codeNames)
        //{
        //    DESchemaObjectCollection result = this.LoadByCodeNameAndSchema(schemas, codeNames, true, false, SNTPClient.AdjustedTime.SimulateTime());
        //    if (result.Any())
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 清除时间点之后的数据，并恢复之前的数据（慎用，仅限测试）
        ///// </summary>
        ///// <param name="time"></param>
        //public void HistoryMoveBack(DateTime time)
        //{
        //    using (HistoryMoveBackAdapter adapter = new HistoryMoveBackAdapter(this.GetConnectionName()))
        //    {
        //        adapter.MoveBack("DE.Acl", new string[] { "ContainerID", "MemberID", "ContainerPermission" }, time);
        //        adapter.MoveBack("DE.Acl_Current", new string[] { "ContainerID", "MemberID", "ContainerPermission" }, time);
        //        adapter.MoveBack("DE.Conditions", new string[] { "OwnerID", "Type", "SortID" }, time);
        //        adapter.MoveBack("DE.Conditions_Current", new string[] { "OwnerID", "Type", "SortID" }, time);
        //        adapter.MoveBack("DE.SchemaApplicationSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaApplicationSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaGroupSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaGroupSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaMembers", new string[] { "ContainerID", "MemberID" }, time);
        //        adapter.MoveBack("DE.SchemaMembersSnapshot", new string[] { "ContainerID", "MemberID" }, time);
        //        adapter.MoveBack("DE.SchemaMembersSnapshot_Current", new string[] { "ContainerID", "MemberID" }, time);
        //        adapter.MoveBack("DE.SchemaObject", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaObjectSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaObjectSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaOrganizationSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaOrganizationSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaPermissionSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaPermissionSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaRelationObjects", new string[] { "ParentID", "ObjectID" }, time);
        //        adapter.MoveBack("DE.SchemaRelationObjectsSnapshot", new string[] { "ParentID", "ObjectID" }, time);
        //        adapter.MoveBack("DE.SchemaRelationObjectsSnapshot_Current", new string[] { "ParentID", "ObjectID" }, time);
        //        adapter.MoveBack("DE.SchemaRoleSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaRoleSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaUserSnapshot", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.SchemaUserSnapshot_Current", new string[] { "ID" }, time);
        //        adapter.MoveBack("DE.UserAndContainerSnapshot", new string[] { "ContainerID", "UserID" }, time);
        //        adapter.MoveBack("DE.UserAndContainerSnapshot_Current", new string[] { "ContainerID", "UserID" }, time);
        //    }
        //}

        //class HistoryMoveBackAdapter : IDisposable
        //{
        //    private Database db;
        //    private DateTime timeEnd = new DateTime(9999, 9, 9);
        //    private DbContext dbContext;

        //    public HistoryMoveBackAdapter(string connectionName)
        //    {
        //        this.dbContext = DbContext.GetContext(connectionName);
        //        this.db = DatabaseFactory.Create(this.dbContext);
        //    }

        //    internal void MoveBack(string tableName, string[] keyColumns, DateTime timeToMove)
        //    {
        //        if (this.db == null || this.dbContext == null)
        //            throw new ObjectDisposedException(this.ToString());

        //        var cmd = this.db.GetSqlStringCommand("DELETE FROM " + tableName + " WHERE VersionStartTime>@time");
        //        this.db.AddInParameter(cmd, "time", DbType.DateTime, timeToMove);
        //        this.db.ExecuteNonQuery(cmd);

        //        string columns = string.Empty;
        //        for (int i = 0; i < keyColumns.Length - 1; i++)
        //        {
        //            columns += keyColumns[i] + ",";
        //        }

        //        columns += keyColumns[keyColumns.Length - 1];

        //        string conditions = string.Empty;

        //        for (int i = 0; i < keyColumns.Length - 1; i++)
        //        {
        //            conditions += "DEST_TBL." + keyColumns[i] + " = SRC_TBL." + keyColumns[i] + " AND ";
        //        }

        //        conditions += " DEST_TBL." + keyColumns[keyColumns.Length - 1] + " = SRC_TBL." + keyColumns[keyColumns.Length - 1] + "";

        //        string sql = string.Format("UPDATE {0} SET VersionEndTime = @endtime FROM {1} DEST_TBL INNER JOIN (SELECT max(VersionStartTime) AS VST,{2} FROM {3} GROUP BY {4}) SRC_TBL ON {5}", tableName, tableName, columns, tableName, columns, conditions);

        //        cmd = this.db.GetSqlStringCommand(sql);

        //        this.db.AddInParameter(cmd, "endtime", DbType.DateTime, this.timeEnd);

        //        this.db.ExecuteNonQuery(cmd);
        //    }

        //    public void Dispose()
        //    {
        //        if (this.dbContext != null)
        //        {
        //            this.dbContext.Dispose();
        //            this.dbContext = null;
        //            this.db = null;
        //        }
        //    }
        //}
    }
}
