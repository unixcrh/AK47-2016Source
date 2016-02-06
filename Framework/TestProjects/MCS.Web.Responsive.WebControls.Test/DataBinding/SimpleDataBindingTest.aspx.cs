using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Web.Responsive.WebControls.Test.DataBinding
{
    public partial class SimpleDataBindingTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.bindingControl.Data = SimpleDataObject.PrepareData();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            this.bindingControl.CollectData(true);

            StringBuilder strB = new StringBuilder();

            StringWriter writer = new StringWriter(strB);

            SimpleDataObject data = (SimpleDataObject)this.bindingControl.Data;

            writer.WriteLine("StringInput: {0}", data.StringInput);
            writer.WriteLine("DateInput: {0}", data.DateInput);

            serverInfo.Text = HttpUtility.HtmlEncode(strB.ToString());
        }
    }
}