using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Web.WebControls.Test.TextBoxDropDown
{
    public partial class TextBoxDropdown : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.asd.DataSource = new string[] { "111", "222", "333" };
            //this.asd.DataBind();

            //this.TextBoxDropdownExtender1.DataSource = new string[] { "111", "222", "333" };
            //this.TextBoxDropdownExtender1.DataBind();

            if (this.IsPostBack == false)
            {
                for (int i = 0; i < 100; i++)
                {
                    TextBoxDropdownExtender1.Items.Add(string.Format("Work Item-{0}", i));
                    asd.Items.Add(string.Format("Work Item-{0}", i));
                }
            }

        }
    }
}