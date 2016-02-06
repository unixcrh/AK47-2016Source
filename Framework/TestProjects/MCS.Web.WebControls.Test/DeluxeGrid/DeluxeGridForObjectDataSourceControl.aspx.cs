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
using MCS.Web.WebControls.Test;

namespace MCS.Web.WebControls.Test.DeluxeGrid
{
	public partial class DeluxeGridForObjectDataSourceControl : System.Web.UI.Page
	{
		protected override void OnPreInit(EventArgs e)
		{
			SetProperties();
			base.OnPreInit(e);
		} 

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		void SetProperties()
		{
			DeluxeGridPropertiesCls dgpc = (DeluxeGridPropertiesCls)Session[Request["id"]];
			if (dgpc == null)
				return;

			DeluxeGrid1.DataSourceMaxRow = dgpc.DataSourceMaxRow;
			DeluxeGrid1.ShowExportControl = dgpc.PagerExportMode;
			DeluxeGrid1.GridTitle = dgpc.GridTitle;


			DeluxeGrid1.ShowCheckBoxes = dgpc.CheckBoxAdd;
			DeluxeGrid1.CheckBoxPosition = dgpc.CheckBoxPosition;
			DeluxeGrid1.MultiSelect = dgpc.MultiSelect;
		}

		protected void DeluxeGrid1_ExportClick(object sender, EventArgs e)
		{
			//this.DeluxeGrid1.DataSource = OrdersDataViewAdapter.Instance.GetData();
			//prioritySelector.SelectedIndex = prioritySelector.SelectedIndex;
			//this.DeluxeGrid1.PageIndex = 0;
		}

		protected void btnPostBack_Click(object sender, EventArgs e)
		{ 
			this.DeluxeGrid1.SelectedKeys.Clear();
		}
	}
 
}
