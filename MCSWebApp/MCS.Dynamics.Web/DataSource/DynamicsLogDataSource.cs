using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MCS.Dynamics.Web.DataSource
{
    /// <summary>
    /// 查询日志列表
    /// </summary>
    public class DynamicsLogDataSource : DataViewDataSourceQueryAdapterBase
    {
        public DynamicsLogDataSource()
            : base("[DE].[OperationLog]")
        { 
          
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = "*";
            qc.WhereClause = qc.WhereClause;
            qc.OrderByClause = "CreateTime DESC";
        }

        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }

        public DataView Query(int startRowIndex, int maximumRows, string ou, string where, string orderBy, ref int totalCount)
        {
            return base.Query(startRowIndex, maximumRows, where, orderBy, ref totalCount);
        }

        public int GetQueryCount(string ou, string where, ref int totalCount)
        {
            //this._SearchOuID = ou;
            //this.schemaTypes = null;
            return base.GetQueryCount(where, ref totalCount);
        }
    }
}