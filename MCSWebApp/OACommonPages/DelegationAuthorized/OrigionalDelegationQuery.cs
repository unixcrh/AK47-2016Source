using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.OA.CommonPages.DelegationAuthorized
{
    public class OrigionalDelegationQuery : ObjectDataSourceQueryAdapterBase<MCS.Library.SOA.DataObjects.Workflow.WfDelegation, MCS.Library.SOA.DataObjects.Workflow.WfDelegationCollection>
    {
        protected override string GetConnectionName()
        {
            return ConnectionNameMappingSettings.GetConfig().GetConnectionName(ConnectionDefine.DBConnectionName, ConnectionDefine.DBConnectionName);
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @"*";
            qc.OrderByClause = "[START_TIME] DESC";
            qc.FromClause = "";
            qc.FromClause = @"WF.DELEGATIONS(NOLOCK)";
            base.OnBuildQueryCondition(qc);
        }
    }
}