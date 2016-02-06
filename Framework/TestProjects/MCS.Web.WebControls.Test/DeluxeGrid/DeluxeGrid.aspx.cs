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

namespace MCS.Web.WebControls.Test.DeluxeGrid
{
    public partial class DeluxeGrid : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeControls();
                //InitializePage();
            }
        }

        void InitializeControls()
        { 
            ddlShowExport.Items.Insert(0, new ListItem("ÊÇ", "1"));
            ddlShowExport.Items.Insert(0, new ListItem("·ñ", "0"));
			ddlShowExport.SelectedIndex = 1;

            ddlSelected.Items.Insert(0, new ListItem("ÊÇ", "1"));
            ddlSelected.Items.Insert(0, new ListItem("·ñ", "0"));

            ddlSelectedPosition.Items.Insert(0, new ListItem("×ó", "0"));
            ddlSelectedPosition.Items.Insert(0, new ListItem("ÓÒ", "1"));
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            DeluxeGridPropertiesCls dgpc = new DeluxeGridPropertiesCls();
            SetProperties(dgpc);
            string id = Guid.NewGuid().ToString();
            Session[id] = dgpc;

			switch (selectedDataSourceControl.SelectedValue)
			{
				case "0":
					Response.Write("<script type=\"text/javascript\" language=\"javascript\" >"
				+ "window.open('DeluxeGridIndex.aspx?id=" + id + "');</script>");
					break;
				case "1":
					Response.Write("<script type=\"text/javascript\" language=\"javascript\" >"
				+ "window.open('DeluxeGridForDataSourceControl.aspx?id=" + id + "');</script>");
					break;
				case "2":
					Response.Write("<script type=\"text/javascript\" language=\"javascript\" >"
				+ "window.open('DeluxeGridForObjectDataSourceControl.aspx?id=" + id + "');</script>");
					break;
			}			 
        }

        void SetProperties(DeluxeGridPropertiesCls dgpc)
        {
            if (txtMaxRows.Text != null && txtMaxRows.Text.Trim().Length > 0)
                dgpc.DataSourceMaxRow = Convert.ToInt32(txtMaxRows.Text);  

            dgpc.GridTitle = txtTitle.Text;
            dgpc.PagerExportMode = ddlShowExport.SelectedValue == "1" ? true : false;
            //dgpc.IDataSource = Convert.ToBoolean(selectedDataSourceControl.Value);
            
            dgpc.CheckBoxAdd = ddlSelected.SelectedValue == "1" ? true : false;

            if (ddlSelectedPosition.SelectedValue == "0")
                dgpc.CheckBoxPosition = RowPosition.Left;
            else
                dgpc.CheckBoxPosition = RowPosition.Right;

			dgpc.MultiSelect = ddlMultiSelect.SelectedValue == "True";
        }
    }
}
