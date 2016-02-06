using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library.Script;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters
{
    /// <summary>
    /// 最基本的对象适配器。从SchemaObjectAdapterBase派生，是权限中心所有对象的读写器。
    /// 基类SchemaObjectAdapterBase中主要实现了更新操作，而SchemaObjectAdapter主要实现了对象的读（Load）操作。
    /// </summary>
    public class DEInstanceAdapter : DEInstanceAdapterBase<DEEntityInstanceBase>
    {
        /// <summary>
        /// <see cref="DEInstanceAdapter"/>的实例，此字段为只读
        /// </summary>
        public static readonly DEInstanceAdapter Instance = new DEInstanceAdapter();

        private DEInstanceAdapter()
        {
        }

        /// <summary>
        /// 根据ID和时间载入对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns><see cref="DESchemaObjectBase"/>的派生类型的实例</returns>
        public DEEntityInstanceBase Load(string id)
        {
            id.NullCheck("id");

            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("ID");

            inBuilder.AppendItem(id);

            DEEntityInstanceBaseCollection objs = Load(inBuilder);

            if (!objs.Any())
            {
                throw new Exception(string.Format("不能找到编码为{0}的实例", id));
            }

            return objs.Single();
        }

        //public 

        public bool Exist(string id)
        {
            id.NullCheck("id");

            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("ID");

            inBuilder.AppendItem(id);

            DEEntityInstanceBaseCollection objs = Load(inBuilder);

            return objs.Any();
        }

        /// <summary>
        /// 根据<see cref="IConnectiveSqlClause"/>指定的条件和时间点载入对象
        /// </summary>
        /// <param name="condition">表示条件的<see cref="IConnectiveSqlClause"/></param>
        /// <returns>一个<see cref="DESchemaObjectCollection"/>，包含条件指定的对象。</returns>
        public DEEntityInstanceBaseCollection Load(IConnectiveSqlClause condition)
        {
            //var timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            DEEntityInstanceBaseCollection result = new DEEntityInstanceBaseCollection();

            if (condition.IsEmpty == false)
            {
                ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(condition);

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

        /// <summary>
        /// 按照SchemaType加载对象
        /// </summary>
        /// <param name="schemaTypes"></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DEEntityInstanceBaseCollection LoadByEntityID(params string[] EntityIDs)
        {
            EntityIDs.NullCheck("EntityIDs");

            InSqlClauseBuilder builder = new InSqlClauseBuilder("EntityCode");

            builder.AppendItem(EntityIDs);

            return Load(builder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DEEntityInstanceBaseCollection LoadByEntityID(string entityID, int pageIndex, int pageSize, ref int totalNum)
        {
            DataView dataView = InnerQuery(entityID, pageIndex, pageSize, "CreateDate", ref totalNum);

            DataTable table = dataView.ToTable();

            Dictionary<string, DynamicEntity> schemaElements = new Dictionary<string, DynamicEntity>(StringComparer.OrdinalIgnoreCase);

            DEEntityInstanceBaseCollection instanceCollection = new DEEntityInstanceBaseCollection();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DynamicEntity dynamicEntity = null;

                string entityCode = table.Rows[i]["EntityCode"].ToString();
                string data = table.Rows[i]["Data"].ToString();

                if (schemaElements.TryGetValue(entityCode, out dynamicEntity) == false)
                {
                    dynamicEntity = DESchemaObjectAdapter.Instance.Load(entityCode) as DynamicEntity;
                    schemaElements.Add(entityCode, dynamicEntity);
                }

                if (dynamicEntity != null)
                {
                    DEEntityInstance obj = (DEEntityInstance)dynamicEntity.CreateInstance();

                    obj.FromString(data);

                    ORMapping.DataRowToObject(table.Rows[i], obj);

                    if (instanceCollection.ContainsKey(obj.ID) == false)
                        instanceCollection.Add(obj);
                }
            }

            return instanceCollection;


        }

        /// <summary>
        /// 按照实例名查询实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DEEntityInstanceBaseCollection LoadByEntityInstanceBaseName(string name)
        {
            name.NullCheck("name");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
            builder.AppendItem("name",name);

            return Load(builder);
        }

        private DataView InnerQuery(string entityID, int startRowIndex, int pageSize, string orderBy, ref int totalCount)
        {
            WhereSqlClauseBuilder wsc = new WhereSqlClauseBuilder();
            wsc.AppendItem("status", (int)SchemaObjectStatus.Normal);
            wsc.AppendItem("EntityCode", entityID);

            QueryCondition qc = new QueryCondition(startRowIndex, pageSize, "*", ORMapping.GetMappingInfo<DEEntityInstanceBase>().TableName, orderBy, wsc.ToSqlString(TSqlBuilder.Instance));

            TSqlCommonAdapter adapter = new TSqlCommonAdapter(GetConnectionName());

            DataSet ds = adapter.SplitPageQuery(qc, totalCount <= 0);

            DataView result = ds.Tables[0].DefaultView;

            if (ds.Tables.Count > 1)
                totalCount = (int)ds.Tables[1].Rows[0][0];

            //当页码超出索引的，返回最大页
            if (result.Count == 0 && totalCount > 0)
            {
                int newStartRowIndex = (totalCount - 1) / pageSize * pageSize;

                totalCount = -1;

                result = this.Query(entityID, newStartRowIndex, pageSize, orderBy, ref totalCount);
            }

            return result;
        }

        public DataView Query(string entityID, int startRowIndex, int maximumRows, string orderBy, ref int totalCount)
        {
            DataView result = InnerQuery(entityID, startRowIndex, maximumRows, orderBy, ref totalCount);

            return result;
        }

        public void Update(DEEntityInstanceBaseCollection collection)
        {
            collection.NullCheck<ArgumentNullException>("实例集合collection不能为空");

            collection.ForEach(this.Update);
        }

    }
}
