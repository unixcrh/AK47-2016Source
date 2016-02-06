using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WorkflowDesigner.PlanScheduleDialog
{
	public class TaskAccomplishedSource : DataViewDataSourceQueryAdapterBase
	{
		public TaskAccomplishedSource()
			: base("WF.SYS_ACCOMPLISHED_TASK")
		{
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			base.OnBuildQueryCondition(qc);
			if (string.IsNullOrEmpty(qc.OrderByClause))
			{
				qc.OrderByClause = "SORT_ID DESC";
			}
		}

		protected override string GetConnectionName()
		{
			return WorkflowSettings.GetConfig().ConnectionName;
		}
	}
}