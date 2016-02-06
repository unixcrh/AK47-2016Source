using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System.Data;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics;

namespace MCS.Dynamics.Web.DataSource
{
    public class ETLEntitySearchDataSource : DataViewDataSourceQueryAdapterBase
    {
        public ETLEntitySearchDataSource()
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
            wsc.AppendItem("SchemaType", DEStandardObjectSchemaType.ETLEntity.ToString());
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