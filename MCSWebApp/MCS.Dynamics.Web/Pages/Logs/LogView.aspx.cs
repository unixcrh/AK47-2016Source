using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

namespace MCS.Dynamics.Web.Pages.Logs
{
    public partial class LogView : System.Web.UI.Page
    {
        public static readonly string ThisPageSearchResourceKey = "6EADF811-9825-4410-A648-552BB341GD9C";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false && Page.IsCallback == false)
            {
                this.CurrentAdvancedSearchCondition = new PageAdvancedSearchCondition();

                this.DeluxeSearch.UserCustomSearchConditions = DbUtil.LoadSearchCondition(ThisPageSearchResourceKey, "Default");

            }
            this.searchBinding.Data = this.CurrentAdvancedSearchCondition;
        }


        protected void dataSourceMain_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {


            var allConditions = new ConnectiveSqlClauseCollection(this.DeluxeSearch.GetCondition());

            var condition = this.CurrentAdvancedSearchCondition;

            WhereSqlClauseBuilder builder = ConditionMapping.GetWhereSqlClauseBuilder(condition);

            string categoryID = Request.QueryString["categoryID"];

            if (!string.IsNullOrEmpty(categoryID))
            {
                //DECategory dcg = CategoryAdapter.Instance.GetByID(categoryID);

                //dcg.FullPath += "%";
                if (categoryID != "undefined")
                {
                    builder.AppendItem("SchemaType", categoryID);
                }

            }

            allConditions.Add(builder);

            this.dataSourceMain.Condition = allConditions;
        }


        [Serializable]
        internal class PageAdvancedSearchCondition
        {
            /// <summary>
            /// 姓名
            /// </summary>
            [ConditionMapping("OperatorName")]
            public string OperatorName { get; set; }
            /// <summary>
            /// 真实姓名
            /// </summary>
            [ConditionMapping("RealOperatorName")]
            public string RealOperatorName { get; set; }
        }

        private PageAdvancedSearchCondition CurrentAdvancedSearchCondition
        {
            get { return this.ViewState["AdvSearchCondition"] as PageAdvancedSearchCondition; }

            set { this.ViewState["AdvSearchCondition"] = value; }
        }


        protected void SearchButtonClick(object sender, MCS.Web.WebControls.SearchEventArgs e)
        {

            this.ProcessDescInfoDeluxeGrid.PageIndex = 0;

            this.searchBinding.CollectData();

            var bindData = searchBinding.Data as PageAdvancedSearchCondition;

            Util.SaveSearchCondition(e, this.DeluxeSearch, ThisPageSearchResourceKey, this.searchBinding.Data);

            this.InnerRefreshList();
        }

        private void InnerRefreshList()
        {
            // 重新刷新列表
            this.dataSourceMain.LastQueryRowCount = -1;

            this.ProcessDescInfoDeluxeGrid.SelectedKeys.Clear();

            this.Page.PreRender += new EventHandler(this.DelayRefreshList);
        }
        private void DelayRefreshList(object sender, EventArgs e)
        {
            this.ProcessDescInfoDeluxeGrid.DataBind();
        }


    }
}