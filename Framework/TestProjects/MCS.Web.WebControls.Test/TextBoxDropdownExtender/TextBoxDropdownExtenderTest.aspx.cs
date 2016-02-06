using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library;

namespace MCS.Web.WebControls.Test.TextBoxDropdownExtender
{
	public partial class TextBoxDropdownExtenderTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.IsPostBack == false)
			{
				List<KeyValuePair<int, string>> dataSource = new List<KeyValuePair<int, string>>();

				for (int i = 0; i < 100; i++)
				{
					KeyValuePair<int, string> pair = new KeyValuePair<int, string>(i, string.Format("Work Item-{0}", i));
					dataSource.Add(pair);
				}

				extender.BindData(dataSource, "Key", "Value");
			}
		}

        protected void postBackBtn_Click(object sender, EventArgs e)
        {
            var value = this.extender.Text;
        }
	}
}