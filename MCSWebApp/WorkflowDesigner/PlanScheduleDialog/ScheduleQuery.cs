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
    public class ScheduleQuery : ObjectDataSourceQueryAdapterBase<JobSchedule, JobScheduleCollection>
    {
        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }
        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            if (qc.WhereClause.IsNullOrEmpty())
                qc.WhereClause = new WhereSqlClauseBuilder().AppendTenantCode().ToSqlString(TSqlBuilder.Instance);

            qc.OrderByClause = "SCHEDULE_NAME";
            qc.SelectFields = "SCHEDULE_ID,SCHEDULE_NAME,START_TIME,END_TIME,ENABLED";
        }
    }
}