using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Web.WebControls
{
	public class ProcessDescriptorInfoQuery : ObjectDataSourceQueryAdapterBase<WfProcessCurrentInfo, WfProcessCurrentInfoCollection>
	{
		protected override string GetConnectionName()
		{
			return WorkflowSettings.GetConfig().ConnectionName;
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.OrderByClause = "START_TIME";
			qc.SelectFields = "INSTANCE_ID, OWNER_ACTIVITY_ID, STATUS, PROCESS_NAME, DESCRIPTOR_KEY, START_TIME, END_TIME";
		}
	}
}
