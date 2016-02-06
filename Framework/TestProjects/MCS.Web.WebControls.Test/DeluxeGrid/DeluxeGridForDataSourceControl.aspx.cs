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
	public partial class DeluxeGridForDataSourceControl : System.Web.UI.Page
	{
		protected override void OnPreInit(EventArgs e)
		{
			SetProperties();
			base.OnPreInit(e);
		} 

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		private void InitializePagers()
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

		protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
		{
			//DeluxeGrid1.RecordCount = e.AffectedRows;
		}
 
	}
}
