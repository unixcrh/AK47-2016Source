using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Security.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Security.DataSources
{
	public class SchemaGlobalObjectDataSource : DataViewDataSourceQueryAdapterBase
	{
		public SchemaGlobalObjectDataSource()
			: base(TimePointContext.Current.UseCurrentTime ? "SC.SchemaObjectSnapshot_Current" : "SC.SchemaObjectSnapshot")
		{
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.SelectFields = "*";
			if (string.IsNullOrEmpty(qc.OrderByClause))
				qc.OrderByClause = "VersionStartTime DESC";

			qc.WhereClause.IsNotEmpty((s) => qc.WhereClause += " AND ");

			var builder = new WhereSqlClauseBuilder();
			builder.AppendItem("Status", (int)SchemaObjectStatus.Normal);
			var timeCondition = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder();
			qc.WhereClause += new ConnectiveSqlClauseCollection(builder, timeCondition).ToSqlString(TSqlBuilder.Instance);
		}

		protected override string GetConnectionName()
		{
			return SCConnectionDefine.DBConnectionName;
		}
	}
}
