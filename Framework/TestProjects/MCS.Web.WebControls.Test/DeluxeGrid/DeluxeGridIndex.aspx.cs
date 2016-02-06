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
using MCS.Web.WebControls;
using MCS.Web.Library;
namespace MCS.Web.WebControls.Test.DeluxeGrid
{
    public partial class DeluxeGridIndex : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            SetProperties();
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void DeluxeGrid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeluxeGrid1.PageIndex = e.NewPageIndex;
        }         

		protected override void OnPreRender(EventArgs e)
		{
			DataSet ds = ObjData.Getlist();
			DeluxeGrid1.DataSource = ds.Tables[0];

			DeluxeGrid1.DataBind();

			base.OnPreRender(e);
		}

        private void SetProperties()
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

        protected void DeluxeGrid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void btnServer_Click(object sender, EventArgs e)
        {             
            string selectedValue = "";
            foreach (string s in DeluxeGrid1.SelectedKeys)
            {
                selectedValue += s + ",";
            }
            selectedValue = selectedValue.TrimEnd(',');
            txtSelectValue.Value = selectedValue;
        }

		protected void DeluxeGrid1_ExportClick(object sender, EventArgs e)
		{
			this.DeluxeGrid1.DataSource = OrdersDataViewAdapter.Instance.GetData(0,20);
			this.DataBind();
		}
    }
}
