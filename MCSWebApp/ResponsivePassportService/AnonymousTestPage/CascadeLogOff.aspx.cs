using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResponsivePassportService.AnonymousTestPage
{
    public partial class CascadeLogOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer != null)
                this.Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}