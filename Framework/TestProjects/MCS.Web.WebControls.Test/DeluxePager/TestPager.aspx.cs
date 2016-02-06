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
using System.IO;
using System.Web.Caching;

//新增测试文件 added by longmark 2007-11-13
namespace MCS.Web.WebControls.Test.DeluxePager
{
    public partial class TestPager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializePage();
        }

        private void InitializePage()
        {
            DataSet ds = ObjData.GetlistTop10();
            int recordCount =  10;// ObjData.GetOrdersCount();
            gvLogList.DataSource = ds;
            logListPager.RecordCount = recordCount;
            gvLogList.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataSet ds = ObjData.GetQuery(); 

            this.logListPager.RecordCount = ds.Tables[0].Rows.Count; 
        } 

        protected void LogListPager_CommonPageIndexChanged(object sender, EventArgs e)
        { 
            DataSet ds = ObjData.GetPagerQuery(logListPager.PageSize, logListPager.PageIndex);

            int recordCount = ObjData.GetOrdersQueryCount();

            gvLogList.DataSource = ds;

            logListPager.RecordCount = recordCount;

            gvLogList.DataBind();
        }

        protected void gvLogList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            logListPager.PageIndex = e.NewPageIndex;

            DataSet ds = ObjData.GetPagerQuery(logListPager.PageSize, logListPager.PageIndex);

            int recordCount = ObjData.GetOrdersQueryCount();

            gvLogList.DataSource = ds;

            logListPager.RecordCount = recordCount;

            gvLogList.DataBind();
        }

        protected void gvLogList_PageIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
