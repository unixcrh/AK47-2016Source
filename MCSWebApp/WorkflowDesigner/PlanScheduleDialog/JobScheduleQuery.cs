using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WorkflowDesigner.PlanScheduleDialog
{
	[DataObject]
	public class JobScheduleQuery : ObjectDataSourceQueryAdapterBase<JobSchedule, JobScheduleCollection>
	{
		protected override string GetConnectionName()
		{
			return WorkflowSettings.GetConfig().ConnectionName;
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.OrderByClause = "SCHEDULE_NAME";
			qc.SelectFields = "SCHEDULE_ID,SCHEDULE_NAME,START_TIME,END_TIME,ENABLED";
		}
	}
}