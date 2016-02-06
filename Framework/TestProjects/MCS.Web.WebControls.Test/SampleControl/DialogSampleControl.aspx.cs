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


namespace MCS.Web.WebControls.Test.SampleGrid
{
    public partial class DialogSampleControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			//Server.Transfer("UpdatePannel.aspx");
			//Response.Write("test");
            //Response.ClearContent();
            //Response.Write("Page_Load");
            //Response.End();
        }

        public int SetSampleObject(MCS.Web.WebControls.SampleObject[] o)
        {
            return o.Length;
        }
    }
}
