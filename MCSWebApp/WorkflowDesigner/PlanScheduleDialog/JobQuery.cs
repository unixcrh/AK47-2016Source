using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkflowDesigner.PlanScheduleDialog
{
    public class JobQuery : ObjectDataSourceQueryAdapterBase<JobBase, JobCollection>
    {
        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            if (string.IsNullOrEmpty(qc.OrderByClause))
                qc.OrderByClause = "JOB_NAME DESC";

            if (qc.WhereClause.IsNullOrEmpty())
                qc.WhereClause = new WhereSqlClauseBuilder().AppendTenantCode().ToSqlString(TSqlBuilder.Instance);

            qc.SelectFields = "JOB_ID,JOB_NAME,DESCRIPTION,ENABLED,LAST_START_EXE_TIME,JOB_TYPE";
        }
    }
}