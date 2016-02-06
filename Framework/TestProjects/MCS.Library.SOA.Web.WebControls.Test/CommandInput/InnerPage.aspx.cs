using MCS.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Library.SOA.Web.WebControls.Test.CommandInput
{
    public partial class InnerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebUtility.RefreshParentWindow("refresh2", RefreshParentWindowName.Parent);
        }
    }
}