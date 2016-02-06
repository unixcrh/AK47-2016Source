using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class SapUserCompareChoose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridUserBind(1, 1000);  //默认第一页  
            }
        }

        protected void gridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridUser.PageIndex = e.NewPageIndex;
            GridUserBind(e.NewPageIndex, 1000);
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        protected void GridUserBind(int pageIndex, int pageSize)
        {
            using (var sapUserCompareService = new SapUserCompare.SapUserCompareServiceSoapClient())
            {

                int total;
                string errorMessage;

                SapUserCompare.TB_SapUserCompare[] sapUser = sapUserCompareService.LoadEntityPage(out total,
                      out errorMessage, new SapUserCompare.SapUserCompareCustom() { PageIndex = pageIndex, PageSize = pageSize });
                //gridUser.PageCount = (total + pageSize - 1) / pageSize;

                gridUser.DataSource = sapUser;
                gridUser.DataBind();
            }



        }
    }
}