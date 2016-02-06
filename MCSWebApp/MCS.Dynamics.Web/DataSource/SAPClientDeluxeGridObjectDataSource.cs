using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using UEP.DataObjects.UserPool.DataObjects;

namespace MCS.Dynamics.Web.DataSource
{
    public class SAPClientDeluxeGridObjectDataSource : ObjectDataSourceQueryAdapterBase<SAPClient, SAPClientCollection>
    {
        protected override string GetConnectionName()
        {
            return "UserPool";
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = "*";
            qc.FromClause = "[Config].[SAPClients]";
            qc.OrderByClause = "ClientID desc";
        }

        protected override void OnDataRowToObject(SAPClientCollection noticeCollection, System.Data.DataRow row)
        {
            base.OnDataRowToObject(noticeCollection, row);
        }
    }
}