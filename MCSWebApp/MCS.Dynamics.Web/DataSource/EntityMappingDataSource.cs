using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Dynamics.Web.DataSource
{
    /// <summary>
    /// 查询实体映射列表
    /// </summary>
    public class EntityMappingDataSource : DataViewDataSourceQueryAdapterBase
    {
        public EntityMappingDataSource()
            //: base("DE.EntityMappingSnapshot_Current")
            : base("DE.SchemaMembersSnapshot_Current")
        {
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            string fromClauseAddress1 = TimePointContext.Current.UseCurrentTime ? "[DE].SchemaMembersSnapshot_Current" : "[DE].[SchemaMembersSnapshot]";
            string fromClauseAddress2 = TimePointContext.Current.UseCurrentTime ? "de.SchemaObjectSnapshot_Current" : "[DE].[SchemaObjectSnapshot]";
   
            qc.FromClause =fromClauseAddress1+ @" as r
                                JOIN "+fromClauseAddress2+" AS m ON m.ID=r.MemberID";
            qc.SelectFields = "m.ID as ContainerID,m.Name as ContainerName,m.ID as MemberID, m.Name as MemberName";

            qc.WhereClause += " and r.SchemaType='DynamicEntityMapping' and";

            var timeCondition1 = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder("r.");
            var timeCondition2 = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder("m.");

            qc.WhereClause += new ConnectiveSqlClauseCollection(timeCondition1, timeCondition2).ToSqlString(TSqlBuilder.Instance);
            qc.OrderByClause = "r.CreateDate DESC";


        }

        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }
    }
}