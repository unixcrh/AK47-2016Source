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
    public partial class PagerToGridView1 : System.Web.UI.Page
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
            DeluxeGrid2.DataSourceID = "SqlDataSource1";
            base.OnPreInit(e);
        } 
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
				if (ppc != null && !ppc.IsDataSourceControl && ppc.IsPagedControl)
                {
                    DeluxeGrid2.Visible = false;
                    DeluxeGrid1.Visible = true;
                    this.InitializePage();
                }
                else if (ppc.IsPagedControl)
                {
                    DeluxeGrid2.Visible = true;
                    DeluxeGrid1.Visible = false;
                }
                else if (!ppc.IsPagedControl)
                {
                    DeluxeGrid3.Visible = true;
                    DeluxeGrid2.Visible = false;
                    DeluxeGrid1.Visible = false;
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

            DeluxeGrid3.DataSource = ds;

            DeluxeGrid3.DataBind();
        }
        private void InitializePage()
        { 
            DataSet ds = ObjData.Getlist();

            int recordCount = ObjData.GetOrdersCount();
            DeluxeGrid1.DataSource = ds;
            DeluxePager1.RecordCount = recordCount;

            DeluxeGrid1.DataBind();
        }

        protected void ordersGridView_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {

            DeluxeGrid1.PageIndex = e.NewPageIndex;
            DataSet ds = ObjData.Getlist();
            int recordCount = ObjData.GetOrdersCount();
            DeluxePager1.RecordCount = recordCount;
            DeluxeGrid1.DataSource = ds;
            DeluxeGrid1.DataBind();
        }

        protected void ordersGridView_DataBound(object sender, EventArgs e)
        { 
            DeluxeGrid1.DataSourceMaxRow = 10;
        }


        protected void ordersGridView_ExportOnClick(object sender, EventArgs e)
        { 
            DeluxeGrid1.DataSourceMaxRow = 10;
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

            DeluxeGrid3.DataSource = ds;

            DeluxeGrid3.DataBind();
        }
    }
}
