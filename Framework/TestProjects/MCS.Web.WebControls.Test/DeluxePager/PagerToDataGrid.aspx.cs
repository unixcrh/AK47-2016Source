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
using System.Web.Caching;

namespace MCS.Web.WebControls.Test.DeluxePager
{
    public partial class PagerToDataGrid1 : System.Web.UI.Page
    {
        PagerPropertiesCls ppc = new PagerPropertiesCls();
        protected override void OnPreInit(EventArgs e)
        {
            if (PreviousPage != null)
            {
                if (PreviousPage.IsCrossPagePostBack == true)
                {
                    string pagerObj = (PreviousPage.FindControl("hidPagerObject") as HtmlInputHidden).Value;
                    Session["pagerClss"] = pagerObj;
                    Tools.GetDeluxePager(DeluxePager1, pagerObj, ref ppc);
                }                
            }
            if (Session["pagerClss"] != null)
            {
                Tools.GetDeluxePager(DeluxePager1, Session["pagerClss"].ToString(), ref ppc);
            }
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
            Session.Timeout = 30;
            if (!IsPostBack)
            { 
                if (ppc != null && !ppc.IsDataSourceControl && ppc.IsPagedControl)
                {
                    DataGrid3.Visible = false;
                    DataGrid2.Visible = false;
                    DataGrid1.Visible = true;
                    this.InitializePage();
                }
                else if(ppc.IsPagedControl)
                {
                    DataGrid3.Visible = false;
                    DataGrid2.Visible = true;
                    DataGrid1.Visible = false;
                }
                else if (!ppc.IsPagedControl)
                {
                    DataGrid3.Visible = true;
                    DataGrid2.Visible = false;
                    DataGrid1.Visible = false;
                    this.InitializePagers();
                }
            }
        }
        private void InitializePagers()
        {
            DeluxePager1.PageIndex = 0;
            DataSet ds = ObjData.GetPagerList(DeluxePager1.PageSize, DeluxePager1.PageIndex);

            int recordCount = ObjData.GetOrdersCount();

            DeluxePager1.RecordCount = recordCount;

            DataGrid3.DataSource = ds;

            DataGrid3.DataBind();
        }
        private void InitializePage()
        { 
            DataSet ds = ObjData.Getlist();
            int recordCount = ObjData.GetOrdersCount();
            DataGrid1.DataSource = ds;
            DeluxePager1.RecordCount = recordCount;
            DataGrid1.DataBind();
        }

        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataSet ds = ObjData.Getlist();
            int recordCount = ObjData.GetOrdersCount();
            DataGrid1.DataSource = ds;
            DeluxePager1.RecordCount = recordCount;
            DataGrid1.DataBind();
        }        

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            DeluxePager1.RecordCount = e.AffectedRows;
            
        }

        protected void DeluxePager1_CommonPageIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = ObjData.GetPagerList(DeluxePager1.PageSize, DeluxePager1.PageIndex);

            int recordCount = ObjData.GetOrdersCount();

            DeluxePager1.RecordCount = recordCount;

            DataGrid3.DataSource = ds;

            DataGrid3.DataBind();
        }

        protected void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid2.CurrentPageIndex = e.NewPageIndex;
            DataGrid2.DataBind();
        }
 
    }
}
