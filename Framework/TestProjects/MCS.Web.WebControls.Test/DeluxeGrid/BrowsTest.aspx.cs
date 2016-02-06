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
using System.IO;

namespace MCS.Web.WebControls.Test.DeluxeGrid
{
    public partial class BrowsTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

            GridViewRow pagerRow = GridView1.BottomPagerRow;
            TextBox tb = (TextBox)pagerRow.FindControl("TextBox1");
            if (tb != null)
                tb.Text = "3006";
            MCS.Web.WebControls.DeluxePager pager = (MCS.Web.WebControls.DeluxePager)pagerRow.FindControl("DeluxePager1");
            if (pager != null)
                pager.RecordCount = 3006; 

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            using (FileStream stream = new FileStream(@"c:\mafile.txt", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //ComStream cstream = new ComStream(stream);
                //IPersistStreamInit persistentStreamInit =
                //(IPersistStreamInit)axWebBrowser1.Document;
                //persistentStreamInit.Save(cstream, 0);
            }
        }

        protected void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid2.CurrentPageIndex = e.NewPageIndex;
            DataGrid2.DataBind();
        }
    }
}
