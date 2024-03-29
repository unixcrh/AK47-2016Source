﻿using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.OA.CommonPages.UserOperationLog
{
	public class OperationTasksLogDataSource : ObjectDataSourceQueryAdapterBase<MCS.Library.SOA.DataObjects.UserOperationTasksLog, UserOperationTasksLogCollection>
	{
		protected override string GetConnectionName()
		{
			return WfRuntime.ProcessContext.SimulationContext.GetConnectionName(AppLogSettings.GetConfig().ConnectionName);
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.OrderByClause = "SEND_TO_USER_NAME";
			qc.SelectFields = "distinct SEND_TO_USER_NAME, SEND_TO_USER_ID";
			qc.FromClause = "WF.USER_OPERATION_TASKS_LOG (NOLOCK)";
			base.OnBuildQueryCondition(qc);
		}
	}
}