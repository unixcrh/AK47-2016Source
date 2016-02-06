using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkflowDesigner.MatrixModalDialog 
{
	public class MatrixDefinitionQuery :ObjectDataSourceQueryAdapterBase<WfMatrixDefinition,WfMatrixDefinitionCollection >
	{
		protected override string GetConnectionName()
		{
			return WorkflowSettings.GetConfig().ConnectionName;
		}
		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.OrderByClause = "Name";
			qc.SelectFields = "DEF_KEY,NAME,DESCRIPTION,ENABLED";
		}

	}
}