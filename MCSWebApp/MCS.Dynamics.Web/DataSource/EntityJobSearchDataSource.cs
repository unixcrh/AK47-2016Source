using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Workflow;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Job;

namespace MCS.Dynamics.Web.DataSource
{
    public class EntityJobSearchDataSource : ObjectDataSourceQueryAdapterBase<ETLJob, ETLJobCollection>
    {
        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            if (string.IsNullOrEmpty(qc.OrderByClause))
                qc.OrderByClause = "JOB_NAME DESC";
            qc.WhereClause += string.IsNullOrEmpty(qc.WhereClause) ? "ENABLED=1" : " and ENABLED=1 ";
            qc.SelectFields = "JOB_ID,JOB_NAME,DESCRIPTION,ENABLED,LAST_START_EXE_TIME,JOB_TYPE,ISManual";
           // qc.FromClause = ("[WF].[JOBS] as A left join [DynamicsEntityDB].[DE].[ETL_JobAndAutoMapping] as B on A.JOB_ID=B.JobID");
        }

        public ETLJobCollection Query(int startRowIndex, int maximumRows, string ou, string where, string orderBy, ref int totalCount)
        {
            return base.Query(startRowIndex, maximumRows, where, orderBy, ref totalCount);
        }

        public int GetQueryCount(string ou, string where, ref int totalCount)
        {
            return base.GetQueryCount(where, ref totalCount);
        }


    }
}