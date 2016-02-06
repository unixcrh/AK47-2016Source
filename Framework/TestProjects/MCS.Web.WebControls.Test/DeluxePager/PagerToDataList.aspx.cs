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
    public partial class PagerToDataList1 : System.Web.UI.Page
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
                if (ppc != null && !ppc.IsDataSourceControl)
                {
                    DataList2.Visible = false;
                    this.InitializePage();
                }
                else
                {
                    DataList3.Visible = false;
                    int recordCount = ObjData.GetOrdersCount();

                    DeluxePager1.RecordCount = recordCount;
                }
            }
        }

        private void InitializePage()
        { 
            DeluxePager1.PageIndex = 0;
            DataSet ds = ObjData.GetPagerList(DeluxePager1.PageSize, DeluxePager1.PageIndex);

            int recordCount = ObjData.GetOrdersCount(); 

            DeluxePager1.RecordCount = recordCount;

            DataList3.DataSource = ds;

            DataList3.DataBind();
        }

        protected void DeluxePager1_CommonPageIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = ObjData.GetPagerList(DeluxePager1.PageSize, DeluxePager1.PageIndex);

            int recordCount = ObjData.GetOrdersCount(); 

            DeluxePager1.RecordCount = recordCount;

            DataList3.DataSource = ds;

            DataList3.DataBind();
        }        

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            e.Command.Connection.Open();

            e.Command.Parameters.Clear();
            e.Command.CommandText = "select count(*) from Orders";
            object obj = e.Command.ExecuteScalar();
            if (obj != null)
                DeluxePager1.RecordCount = (int)obj;

            e.Command.Connection.Close();
        }
    }
}
