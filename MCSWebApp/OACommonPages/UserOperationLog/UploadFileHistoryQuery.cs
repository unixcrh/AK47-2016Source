using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;

namespace MCS.OA.CommonPages
{
	public class UploadFileHistoryQuery : ObjectDataSourceQueryAdapterBase<UploadFileHistory, UploadFileHistoryCollection>
	{

		public UploadFileHistoryQuery()
		{
			//UploadFileHistory
		}

		protected override string GetConnectionName()
		{
			return AppLogSettings.GetConfig().ConnectionName;
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.OrderByClause = "CREATE_TIME DESC";
			base.OnBuildQueryCondition(qc);
		}

	}
}