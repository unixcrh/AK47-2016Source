using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls.Test.SampleControl
{
    public partial class DataBoundControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                DeluxeSelect1.DataSourceID = "SqlDataSource1";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //DeluxeSelect1.DataSourceID = "SqlDataSource1";
            Response.Write("Button1_Click");
        }
    }
}
