using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.Data.DataObjects;

namespace MCS.Dynamics.Web.DataSource
{
    public class ErrorLogSearchDataSourcecs : ObjectDataSourceQueryAdapterBase<ErrorLog, ErrorLogCollection>
    {
        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            if (string.IsNullOrEmpty(qc.OrderByClause))
                qc.OrderByClause = "CreateDate DESC";
            //qc.WhereClause += string.IsNullOrEmpty(qc.WhereClause) ? "IsDelete=0" : " and IsDelete=1 ";
            qc.WhereClause += string.IsNullOrEmpty(qc.WhereClause) ? "IsDelete=0" : " and IsDelete=0 ";
            qc.SelectFields = "Code,InsertSql,ExecutionTime,CreateDate,Creator,ErrorMessage,EntityCodes,ErrorType,IsDelete";
        }
    }
}