using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UEP.DataObjects.UserPool.DataObjects;
using UEP.DataObjects.UserPool.Adapters;
using MCS.Web.Library;
using MCS.Library.Caching;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class AllSapUserCompareChoose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string clientID = string.Empty;
                if (!string.IsNullOrEmpty(this.Request.QueryString["ClientID"]))
                    clientID = this.Request.QueryString["ClientID"].Trim();

                if (!string.IsNullOrEmpty(clientID))
                    this.whereCondition.Value = " ClientID = " + "'" + clientID + "'";
            }
        }

        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["totalCount"] = LastQueryRowCount;
        }

        protected void objectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            LastQueryRowCount = (int)e.OutputParameters["totalCount"];
        }

        protected void gridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string sortExpression = this.gridUser.SortExpression;
            SortDirection sortDirection = this.gridUser.SortDirection;
            SetDeluxeGridSortInfo(sortExpression, sortDirection);
        }

        private void SetDeluxeGridSortInfo(string sortExp, SortDirection sortDirection)
        {
            string strSortDirection = "desc";
            ObjectContextCache.Instance.Add("deluxeGridSort", sortDirection + " " + strSortDirection);
        }

        private int LastQueryRowCount
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "LastQueryRowCount", -1);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "LastQueryRowCount", value);
            }
        }
    }
}