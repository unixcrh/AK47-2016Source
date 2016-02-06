using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System.Data;

namespace MCS.Dynamics.Web
{
    public partial class defaultView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            DataColumn dc = new DataColumn("col1",typeof(string));
            dt.Columns.Add(dc);
            dr[0] = "ddsjkwelkfjwefl;";
            dt.Rows.Add(dr);
            //grid.DataSourceID = "SqlDataSource1";
            //grid.DataBind();
            //var entitys = DESchemaObjectAdapter.Instance.Load("1");

            //grid.DataSource = entitys;
            grid.DataSource = dt;
            grid.DataBind();
        }
    }
}