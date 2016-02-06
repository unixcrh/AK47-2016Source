using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MCS.Dynamics.Web.DataSource
{
    /// <summary>
    /// 查询实体列表
    /// </summary>
    public class EntitySearchDataSource : DataViewDataSourceQueryAdapterBase
    {
        public EntitySearchDataSource()
            : base("DE.EntitySnapshot_Current")
        {
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.FromClause = TimePointContext.Current.UseCurrentTime ? "DE.EntitySnapshot_Current" : "DE.EntitySnapshot";
            qc.SelectFields = "*";
            var timeCondition1 = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder().ToSqlString(TSqlBuilder.Instance);
            qc.WhereClause.IsNotEmpty((s) => qc.WhereClause += " AND ");
            WhereSqlClauseBuilder wsc = new WhereSqlClauseBuilder();
            qc.WhereClause += timeCondition1;
            wsc.AppendItem("SchemaType", DEStandardObjectSchemaType.DynamicEntity.ToString());
            if (!TimePointContext.Current.UseCurrentTime)
            {
                wsc.AppendItem("status", (int)SchemaObjectStatus.Normal);


            }
            qc.WhereClause.IsNotEmpty((s) => qc.WhereClause += " AND ");
            qc.WhereClause += wsc.ToSqlString(TSqlBuilder.Instance);
            qc.OrderByClause = "CreateDate DESC";

        }

        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }

        public DataView Query(int startRowIndex, int maximumRows, string ou, string where, string orderBy, ref int totalCount)
        {
            return base.Query(startRowIndex, maximumRows, where, orderBy, ref totalCount);
        }

        public int GetQueryCount(string ou, string where, ref int totalCount)
        {
            return base.GetQueryCount(where, ref totalCount);
        }
    }
}