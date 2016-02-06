using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Web.WebControls.Test.DeluxeGrid
{
	public partial class EnumDropDownFieldTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void grid1_SelectedIndexChanged(object sender, EventArgs e)
		{
			grid1.EditIndex = grid1.SelectedIndex;
		}
	}
}