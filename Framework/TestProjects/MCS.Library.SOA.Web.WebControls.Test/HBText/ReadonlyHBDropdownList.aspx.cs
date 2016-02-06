using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Library.SOA.Web.WebControls.Test.HBText
{
    public partial class ReadonlyHBDropdownList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void selectThreeBtn_Click(object sender, EventArgs e)
        {
            dropdownList.SelectedValue = "3";
        }

        protected void selectDefaultBtn_Click(object sender, EventArgs e)
        {
            dropdownList.SelectedValue = string.Empty;
        }
    }
}