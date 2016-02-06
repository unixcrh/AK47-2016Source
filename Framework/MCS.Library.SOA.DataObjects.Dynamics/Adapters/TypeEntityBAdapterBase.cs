using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// 开放时间段版本控制Adapter基类
    /// </summary>
    public abstract class TypeEntityBAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection>
        where TCollection : EditableDataObjectCollectionBase<T>, new()
        where T : VersionedEntityBase, new()
    {
        #region 查询数据

        /// <summary>
        /// 按条件加载当前有效数据对象
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <returns></returns>
        public TCollection LoadCurrentData(Action<WhereSqlClauseBuilder> whereAction)
        {
            return LoadCurrentData(whereAction, null);
        }

        /// <summary>
        /// 按条件加载当前有效数据对象
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <param name="orderByAction">orderBy委托</param>
        /// <returns></returns>
        public TCollection LoadCurrentData(Action<WhereSqlClauseBuilder> whereAction, Action<OrderBySqlClauseBuilder> orderByAction)
        {
            string sql = GetSqlByWhereAction(whereAction, "*");

            if (orderByAction != null)
            {
                OrderBySqlClauseBuilder orderByBuilder = new OrderBySqlClauseBuilder();
                orderByAction(orderByBuilder);
                if (orderByBuilder.Count > 0)
                {
                    sql = sql + string.Format(" ORDER BY {0}", orderByBuilder.ToSqlString(TSqlBuilder.Instance));
                }
            }

            TCollection result = QueryData(sql);

            AfterLoad(result);

            return result;
        }

        /// <summary>
        /// 按条件检查当前有效数据对象是否存在
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <returns></returns>
        public bool ExistCurrentVersion(Action<WhereSqlClauseBuilder> whereAction)
        {
            string sql = GetSqlByWhereAction(whereAction, "COUNT(*)");

            return (int)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName()) > 0;
        }

        private string GetSqlByWhereAction(Action<WhereSqlClauseBuilder> whereAction, string fields)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
            if (whereAction != null)
            {
                whereAction(builder);
            }
            builder.AppendItem("VersionEndTime", DBNull.Value, "IS");

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = string.Format("SELECT {2} FROM {0} WHERE {1}",
                mappings.TableName, builder.ToSqlString(TSqlBuilder.Instance), fields);
            return sql;
        }

        /// <summary>
        /// 按条件加载指定有效时间点对象集合
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <param name="dateTime">有效时间点</param>
        /// <returns></returns>
        public TCollection LoadDataByTime(Action<WhereSqlClauseBuilder> whereAction, DateTime dateTime)
        {
            return LoadDataByTime(whereAction, null, dateTime);
        }

        /// <summary>
        /// 按条件加载指定有效时间点对象集合
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <param name="orderByAction">orderBy委托</param>
        /// <param name="dateTime">有效时间点</param>
        /// <returns></returns>
        public TCollection LoadDataByTime(Action<WhereSqlClauseBuilder> whereAction, Action<OrderBySqlClauseBuilder> orderByAction, DateTime dateTime)
        {
            string sql = GetSqlByWhereActionAndTime(whereAction, dateTime, "*");

            if (orderByAction != null)
            {
                OrderBySqlClauseBuilder orderByBuilder = new OrderBySqlClauseBuilder();

                orderByAction(orderByBuilder);

                if (orderByBuilder.Count > 0)
                    sql = sql + string.Format(" ORDER BY {0}", orderByBuilder.ToSqlString(TSqlBuilder.Instance));
            }

            TCollection result = QueryData(sql);

            AfterLoad(result);

            return result;
        }
        /// <summary>
        /// 按条件加载指定有效时间点对象集合
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <param name="orderByAction">orderBy委托</param>
        /// <param name="dateTime">有效时间点</param>
        /// <returns></returns>
        public TCollection LoadDataByTime(Action<ConnectiveSqlClauseCollection> whereAction, Action<OrderBySqlClauseBuilder> orderByAction, DateTime dateTime)
        {
            var qc = GetQueryConditionByTime(whereAction, dateTime);

            OrderBySqlClauseBuilder orderByBuilder = new OrderBySqlClauseBuilder();

            if (orderByAction != null)
            {
                orderByAction(orderByBuilder);
            }
            qc.OrderByClause = orderByBuilder.ToSqlString(TSqlBuilder.Instance);

            TCollection result = qc.GetData<T, TCollection>(GetConnectionName());

            AfterLoad(result);

            return result;
        }
        public QueryCondition GetQueryConditionByTime(Action<ConnectiveSqlClauseCollection> whereAction, DateTime dateTime)
        {
            ConnectiveSqlClauseCollection connectiveSqlClauses = new ConnectiveSqlClauseCollection();
            ORMappingItemCollection mappings = ORMapping.GetMappingInfo<T>();
            connectiveSqlClauses.GetVersionTimeCondion<QueryCondtionEntityBase>(dateTime, mappings.TableName, whereAction);
            QueryCondition qc = new QueryCondition
            {
                FromClause = mappings.TableName,
                WhereClause = connectiveSqlClauses.ToSqlString(TSqlBuilder.Instance)
            };
            return qc;
        }
        /// <summary>
        /// 按条件检查指定时间版本的数据对象是否存在
        /// </summary>
        /// <param name="whereAction">where委托</param>
        /// <param name="dateTime">有效时间点</param>
        /// <returns></returns>
        public bool ExistVersionByTime(Action<WhereSqlClauseBuilder> whereAction, DateTime dateTime)
        {
            string sql = GetSqlByWhereActionAndTime(whereAction, dateTime, "COUNT(*)");

            return (int)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName()) > 0;
        }

        private string GetSqlByWhereActionAndTime(Action<WhereSqlClauseBuilder> whereAction, DateTime dateTime, string fields)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
            if (whereAction != null)
            {
                whereAction(builder);
            }

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = string.Format("SELECT {1} FROM {0}", mappings.TableName, fields);

            string where = TypeEntityBAdapterBase<T, TCollection>.GetValidTimeWhere(dateTime);
            where = string.Format(" WHERE {0}", where);
            if (builder.Count > 0)
            {
                where += string.Format(" AND {0}", builder.ToSqlString(TSqlBuilder.Instance));
            }

            sql = sql + where;
            return sql;
        }

        /// <summary>
        /// 获得有效时间点相关的where语句
        /// </summary>
        /// <param name="validTime">有效时间点</param>
        /// <returns>where子语句</returns>
        private static string GetValidTimeWhere(DateTime validTime)
        {
            return GetValidTimeWhere(validTime, null);
        }

        /// <summary>
        /// 获得有效时间点相关的where语句
        /// </summary>
        /// <param name="validTime">有效时间点</param>
        /// <param name="tblName">表前缀名称</param>
        /// <returns>where子语句</returns>
        private static string GetValidTimeWhere(DateTime validTime, string tblName)
        {
            StringBuilder strB = new StringBuilder();
            if (string.IsNullOrEmpty(tblName))
            {
                strB.AppendFormat(" (VersionStartTime <={0} AND ( VersionEndTime IS NULL OR  {0} < VersionEndTime ) )",
                    TSqlBuilder.Instance.FormatDateTime(validTime));
            }
            else
            {
                strB.AppendFormat(" ({1}VersionStartTime <={0} AND ( {1}VersionEndTime IS NULL OR {0} < {1}VersionEndTime ) )",
                    TSqlBuilder.Instance.FormatDateTime(validTime), tblName + ".");
            }
            return strB.ToString();
        }

        #endregion 查询数据

        #region 更新数据操作

        #region[更新给定的数据实体]

        public DateTime UpdateData(T data, DateTime versionTime, WhereSqlClauseBuilder SqlWhere)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            Dictionary<string, object> context = new Dictionary<string, object>();

            BeforeInnerUpdate(data, context);

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                //InnerUpdateCurrentData(data, versionTime, context);
                updateBusinessModel(data, versionTime, SqlWhere, context, SqlWhere);

                AfterInnerUpdate(data, context);

                scope.Complete();
            }

            return (DateTime)context["VersionTime"];
        }
        public void updateBusinessModel(T data, DateTime VersonTime, WhereSqlClauseBuilder sqlFengBan, Dictionary<string, object> context, WhereSqlClauseBuilder SqlWhere)
        {
            StringBuilder stringBulider = new StringBuilder();
            stringBulider = InnerUpdateSql(data, VersonTime, SqlWhere);
            context.Add("VersionTime", DbHelper.RunSqlReturnScalar(stringBulider.ToString(), GetConnectionName()));
        }

        public StringBuilder InnerUpdateSql(T data, DateTime versionTime, WhereSqlClauseBuilder SqlWhere)
        {
            StringBuilder sqlbuilder = new StringBuilder();
            ORMappingItemCollection mappings = GetMappingInfo(new Dictionary<string, object>());
            if (versionTime == DateTime.MinValue)
                versionTime = SNTPClient.AdjustedTime;

            sqlbuilder.Append("DECLARE @CURRENTTIME DATETIME");
            sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);

            sqlbuilder.AppendFormat(
                @" SET @CURRENTTIME = {0} {3} UPDATE {1}  SET VersionEndTime =  @CURRENTTIME WHERE {2}; ",
                TSqlBuilder.Instance.FormatDateTime(versionTime),
                mappings.TableName,
                SqlWhere.ToSqlString(TSqlBuilder.Instance),
                TSqlBuilder.Instance.DBStatementSeperator
                );

            InsertSqlClauseBuilder insertBuilder = ORMapping.GetInsertSqlClauseBuilder(data, mappings, "VersionStartTime");
            insertBuilder.AppendItem("VersionStartTime", "@CURRENTTIME", "=", true);

            string insertSql =
                string.Format("INSERT INTO {0} {1};",
                              mappings.TableName,
                              insertBuilder.ToSqlString(TSqlBuilder.Instance));

            sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            sqlbuilder.Append(insertSql);
            sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            sqlbuilder.Append("SELECT @CURRENTTIME");

            return sqlbuilder;
        }

        public void UpdateVersionEndTime(string companyCode, DateTime versionTime, TCollection data)
        {
            StringBuilder stringBulider = UpdateVersiTiem(companyCode, versionTime, data);
            DbHelper.RunSqlReturnScalar(stringBulider.ToString(), GetConnectionName());
        }

        public StringBuilder UpdateVersiTiem(string companyCode, DateTime versionTime, TCollection data)
        {
            StringBuilder sqlbuilder = new StringBuilder();
            ORMappingItemCollection mappings = GetMappingInfo(new Dictionary<string, object>());

            string sqlWhere = string.Format("CompanyCode = '{0}' AND VersionEndTime IS NULL", companyCode);
            if (versionTime == DateTime.MinValue)
                versionTime = SNTPClient.AdjustedTime;
            if (sqlbuilder.ToString().Contains("DECLARE @CURRENTTIME DATETIME") == false)
            {
                sqlbuilder.Append("DECLARE @CURRENTTIME DATETIME");
                sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            }
            sqlbuilder.AppendFormat(
                @" SET @CURRENTTIME = {0} {3} UPDATE {1}  SET VersionEndTime =  @CURRENTTIME WHERE {2}; ",
                TSqlBuilder.Instance.FormatDateTime(versionTime),
                mappings.TableName,
                sqlWhere,
                TSqlBuilder.Instance.DBStatementSeperator
                );

            return sqlbuilder;
        }

        /// <summary>
        /// 更新给定的数据实体
        /// </summary>
        /// <param name="data">实体对象</param>
        /// <param name="versionTime">版本时间，若为DateTime.MinValue则使用服务器时间</param>
        /// <returns>更新数据使用的时间</returns>
        public DateTime UpdateCurrentData(T data, DateTime versionTime)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            Dictionary<string, object> context = new Dictionary<string, object>();

            BeforeInnerUpdate(data, context);

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                InnerUpdateCurrentData(data, versionTime, context);

                AfterInnerUpdate(data, context);

                scope.Complete();
            }

            return (DateTime)context["VersionTime"];
        }
        #endregion



        protected virtual void InnerUpdateCurrentData(T data, DateTime versionTime, Dictionary<string, object> context)
        {
            var strB = GetInnerUpdateCurrentDataSql(data, versionTime);

            context.Add("VersionTime", DbHelper.RunSqlReturnScalar(strB, GetConnectionName()));
        }

        public string GetInnerUpdateCurrentDataSql(T data, DateTime versionTime)
        {
            StringBuilder strB = new StringBuilder();
            GetInnerUpdateCurrentDataSql(data, versionTime, strB);
            return strB.ToString();
        }

        public void GetInnerUpdateCurrentDataSql(T data, DateTime versionTime, StringBuilder sqlbuilder)
        {
            ORMappingItemCollection mappings = GetMappingInfo(new Dictionary<string, object>());

            WhereSqlClauseBuilder whereBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(data, mappings,
                                                                                                "VersionStartTime");
            whereBuilder.AppendItem("VersionEndTime", DBNull.Value, "IS");
            if (versionTime == DateTime.MinValue)
                versionTime = SNTPClient.AdjustedTime;
            if (sqlbuilder.ToString().Contains("DECLARE @CURRENTTIME DATETIME") == false)
            {
                sqlbuilder.Append("DECLARE @CURRENTTIME DATETIME");
                sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            }
            sqlbuilder.AppendFormat(
                @" SET @CURRENTTIME = {0} {3} UPDATE {1}  SET VersionEndTime =  @CURRENTTIME WHERE {2}; ",
                TSqlBuilder.Instance.FormatDateTime(versionTime),
                mappings.TableName,
                whereBuilder.ToSqlString(TSqlBuilder.Instance),
                TSqlBuilder.Instance.DBStatementSeperator
                );

            InsertSqlClauseBuilder insertBuilder = ORMapping.GetInsertSqlClauseBuilder(data, mappings, "VersionStartTime");
            insertBuilder.AppendItem("VersionStartTime", "@CURRENTTIME", "=", true);

            string insertSql =
                string.Format("INSERT INTO {0} {1};",
                              mappings.TableName,
                              insertBuilder.ToSqlString(TSqlBuilder.Instance));

            sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            sqlbuilder.Append(insertSql);
            sqlbuilder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            sqlbuilder.Append("SELECT @CURRENTTIME");
        }


        #region[ 更新给定的数据实体集合]
        /// <summary>
        /// 更新给定的数据实体集合
        /// </summary>
        /// <param name="whereAction">where委托，用来限定需要设置VersionEndTime的数据</param>
        /// <param name="data">数据实体集合，用来新增的数据集合</param>
        /// <param name="versionTime">版本时间</param>
        /// <returns>更新数据使用时间</returns>
        public DateTime UpdateCurrentData(Action<WhereSqlClauseBuilder> whereAction, TCollection data, DateTime versionTime)
        {
            StringBuilder strB = new StringBuilder();

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
            if (whereAction != null)
            {
                whereAction(builder);
            }
            builder.AppendItem("VersionEndTime", DBNull.Value, "IS");

            if (versionTime != DateTime.MinValue)
            {
                strB.AppendFormat(@"DECLARE @CURRENTTIME DATETIME
                                                SET @CURRENTTIME = {0}
                                                UPDATE {1}  SET VersionEndTime =  @CURRENTTIME WHERE {2}",
                           TSqlBuilder.Instance.FormatDateTime(versionTime),
                           GetMappingInfo(new Dictionary<string, object>()).TableName,
                           builder.ToSqlString(TSqlBuilder.Instance));
            }
            else
            {
                strB.AppendFormat(@"DECLARE @CURRENTTIME DATETIME
                                                SET @CURRENTTIME =GETDATE()
                                                UPDATE {0}  SET VersionEndTime =  @CURRENTTIME WHERE {1}",
                               GetMappingInfo(new Dictionary<string, object>()).TableName,
                               builder.ToSqlString(TSqlBuilder.Instance));
            }

            ORMappingItemCollection mappings = GetMappingInfo(new Dictionary<string, object>());

            foreach (var item in data)
            {
                strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
                InsertSqlClauseBuilder insertBuilder = ORMapping.GetInsertSqlClauseBuilder(item, mappings, "VersionStartTime");
                insertBuilder.AppendItem("VersionStartTime", "@CURRENTTIME", null, true);

                strB.Append(string.Format(" INSERT INTO {0} {1};", mappings.TableName,
                        insertBuilder.ToSqlString(TSqlBuilder.Instance))
                  );
            }

            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
            strB.Append("SELECT @CURRENTTIME");

            return (DateTime)DbHelper.RunSqlReturnScalar(strB.ToString(), GetConnectionName());
        }

        #endregion


        #region[更新给定的数据实体集合（此方法待评审，请匆使用）]
        /// <summary>
        /// 更新给定的数据实体集合（此方法待评审，请匆使用）
        /// </summary>
        /// <param name="data">数据实体集合，用来新增的数据集合</param>
        /// <param name="versionTime">版本时间</param>
        /// <returns>更新数据使用时间</returns>
        public DateTime UpdateCurrentData(TCollection data, DateTime versionTime)
        {
            StringBuilder strB = new StringBuilder();
            ORMappingItemCollection mappings = GetMappingInfo(new Dictionary<string, object>());

            if (versionTime != DateTime.MinValue)
            {
                strB.AppendFormat(@"DECLARE @CURRENTTIME DATETIME
                                                SET @CURRENTTIME = {0}",
                           TSqlBuilder.Instance.FormatDateTime(versionTime));
            }
            else
            {
                strB.AppendFormat(@"DECLARE @CURRENTTIME DATETIME
                                                SET @CURRENTTIME =GETDATE()");
            }

            foreach (var item in data)
            {
                strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
                builder.AppendItem("Code", item.Code);
                builder.AppendItem("VersionEndTime", DBNull.Value, "IS");

                strB.Append(string.Format("UPDATE {0}  SET VersionEndTime =  @CURRENTTIME WHERE {1}",
                        mappings.TableName,
                        builder.ToSqlString(TSqlBuilder.Instance))
                  );
            }

            foreach (var item in data)
            {
                strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
                InsertSqlClauseBuilder insertBuilder = ORMapping.GetInsertSqlClauseBuilder(item, mappings, "VersionStartTime");
                insertBuilder.AppendItem("VersionStartTime", "@CURRENTTIME", null, true);

                strB.Append(string.Format(" INSERT INTO {0} {1};", mappings.TableName,
                        insertBuilder.ToSqlString(TSqlBuilder.Instance))
                  );
            }

            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
            strB.Append("SELECT @CURRENTTIME");

            return (DateTime)DbHelper.RunSqlReturnScalar(strB.ToString(), GetConnectionName());
        }

        #endregion


        #endregion 更新数据
    }
}