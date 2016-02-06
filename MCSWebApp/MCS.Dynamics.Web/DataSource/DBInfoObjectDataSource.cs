using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Web.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MCS.Dynamics.Web.DataSource
{
    public class DBInfoObjectDataSource : DataViewDataSourceQueryAdapterBase
    {
        protected override string GetConnectionName()
        {
            return "UEPDemoConn";
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("utp.DBCODE,");
            sb.Append("utp.DBLoginID,");
            sb.Append("utp.DBAddr,");
            sb.Append("utp.DBName,");
            sb.Append("utp.DBPAssWord");
            qc.SelectFields = sb.ToString();
            qc.PrimaryKey = "DBLoginID";
            qc.FromClause = "[dbo].[DATABASE_INFO] utp";
            qc.OrderByClause = "utp.DBLoginID desc";
        }
    }
}