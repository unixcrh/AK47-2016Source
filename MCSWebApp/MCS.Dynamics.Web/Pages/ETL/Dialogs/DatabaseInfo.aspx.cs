using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library;
using MCS.Library.Caching;
using PetroChina.UEP.Web.DataObjects;
using MCS.Library.Data.Builder;
using System.Text;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class DatabaseInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["totalCount"] = LastQueryRowCount;
        }

        protected void objectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            LastQueryRowCount = (int)e.OutputParameters["totalCount"];
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

        protected void DeluxeGrid_Paging(object sender, GridViewPageEventArgs e)
        {
            string sortExpression = this.NoticeDeluxeGrid.SortExpression;
            SortDirection sortDirection = this.NoticeDeluxeGrid.SortDirection;
            SetDeluxeGridSortInfo(sortExpression, sortDirection);
        }

        protected void DeluxeGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            SetDeluxeGridSortInfo(e.SortExpression, e.SortDirection);
        }

        private void SetDeluxeGridSortInfo(string sortExp, SortDirection sortDirection)
        {
            string strSortDirection = "desc";
            ObjectContextCache.Instance.Add("deluxeGridSort", sortDirection + " " + strSortDirection);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private string GetStrWhere()
        {
            string strWhere = string.Empty;
            DBInfo condition = new DBInfo();
            condition.DBLoginID = txtLoginID.Value;
            condition.DBAddr = txtDBAddr.Value;
            condition.DBName = txtDBName.Value;



            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder(LogicOperatorDefine.And);


            //数据库登录账号
            if (!string.IsNullOrEmpty(condition.DBLoginID))
            {
                builder.AppendItem("DBLoginID", "'%" + condition.DBLoginID + "%'", "LIKE", true);
            }
            //数据库连接地址
            if (!string.IsNullOrEmpty(condition.DBAddr))
            {
                builder.AppendItem("DBAddr", "'%" + condition.DBAddr + "%'", "LIKE", true);
            }
            //数据库名
            if (!string.IsNullOrEmpty(condition.DBName))
            {
                builder.AppendItem("DBName", "'%" + condition.DBName + "%'", "LIKE", true);
            }
            strWhere = builder.ToSqlString(TSqlBuilder.Instance);
            return strWhere;
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsearch_Click(object sender, EventArgs e)
        {

            //查询方法
            string strWhere = GetStrWhere();
            whereCondition.Value = strWhere;
            //  Session["strWhere"] = strWhere;
            searchResult(strWhere);

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strWhere"></param>
        private void searchResult(string strWhere)
        {
            this.LastQueryRowCount = 0;
            this.NoticeDeluxeGrid.SelectedKeys.Clear();
            this.NoticeDeluxeGrid.PageIndex = 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoticeDeluxeGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            string ID = e.CommandArgument.ToString();

            if (commandName == "Delete")//删除
            {
                DBInfo condition = DBInfoAdapter.Instance.GetByID(ID);
                DBInfoAdapter.Instance.Delete(condition);
                Response.Redirect("../Pages/DatabaseInfo.aspx");
            }
        }

        //保存事件
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string code = NoticeDeluxeGrid.SelectedKeysValue;
            if (string.IsNullOrEmpty(code))
            {
                return;
            }
            DBInfo condition = DBInfoAdapter.Instance.GetByID(code);
            //创建传值JSON
            StringBuilder strJson = new StringBuilder();
            //选中行的编码值
            strJson.Append("{");
            strJson.Append("DBCode:'");
            strJson.Append(code);
            strJson.Append("',DBAddr:'");
            strJson.Append(condition.DBAddr);
            strJson.Append("',DBName:'");
            strJson.Append(condition.DBName);
            strJson.Append("',DBLoginID:'");
            strJson.Append(condition.DBLoginID);
            strJson.Append("',DBPassword:'");
            strJson.Append(condition.DBPassword);
            strJson.Append("'}");
            WebUtility.RegisterOnLoadScriptBlock(this, string.Format("window.returnValue={0};top.close();", strJson.ToString()));
        }
    }
}