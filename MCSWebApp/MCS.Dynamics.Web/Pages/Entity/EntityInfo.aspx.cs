using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using MCS.Library.Principal;
using MCS.Web.Library;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;
using MCS.Dynamics.Web.Validate;
using MCS.Web.WebControls;

namespace MCS.Dynamics.Web.Pages.Entity
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class EntityInfo : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
       
        #region 字段属性
        public static readonly string ThisPageSearchResourceKey = "6EADF811-9825-4410-A648-552BB5ETGD9C";
        /// <summary>
        /// 普通场景名称
        /// </summary>
        string ITimeSceneDescriptor.NormalSceneName
        {
            get { return this.EditEnabled ? "Normal" : "ReadOnly"; }
        }
        /// <summary>
        /// 只读场景名称
        /// </summary>
        string ITimeSceneDescriptor.ReadOnlySceneName
        {
            get { return "ReadOnly"; }
        }
        /// <summary>
        /// 是否可以编辑
        /// </summary>
        protected bool EditEnabled
        {
            get
            {
                var enabled = TimePointContext.Current.UseCurrentTime && Util.SuperVisiorMode;
                return enabled;
            }
        }
        /// <summary>
        /// 当前页面检索条件
        /// </summary>
        private PageAdvancedSearchCondition CurrentAdvancedSearchCondition
        {
            get { return this.ViewState["AdvSearchCondition"] as PageAdvancedSearchCondition; }

            set { this.ViewState["AdvSearchCondition"] = value; }
        }

        #endregion

        public void AfterNormalSceneApplied()
        {
            //此处注释掉了   验证不成功时  页面还可以继续更改
            //this.okButton.Visible = this.Data != null && this.Data.Status == SchemaObjectStatus.Normal;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!TimePointContext.Current.UseCurrentTime)
            {
                this.btn_addEntity.Enabled = this.btn_addEntityAndMapping.Enabled = this.btn_copyEntity.Enabled = this.btn_delEntity.Enabled = this.btn_moveEntity.Enabled = btn_Import.Enabled = false;
            }
            if (Page.IsPostBack == false && Page.IsCallback == false)
            {
                this.CurrentAdvancedSearchCondition = new PageAdvancedSearchCondition();

                string islast = Request.QueryString["islast"];

                if (string.IsNullOrEmpty(islast))
                {
                    btn_addEntity.Visible = false;
                    //btn_copyEntity.Visible = false;
                    //btn_moveEntity.Visible = false;
                    btn_addEntityAndMapping.Visible = false;
                    //btn_Export.Visible = false;
                    btn_Import.Visible = false;
                }
                this.DeluxeSearch.UserCustomSearchConditions = DbUtil.LoadSearchCondition(ThisPageSearchResourceKey, "Default");

            }
            this.searchBinding.Data = this.CurrentAdvancedSearchCondition;
        }

        [Serializable]
        internal class PageAdvancedSearchCondition
        {
            [ConditionMapping("Name")]
            public string CodeName { get; set; }
            [ConditionMapping("Description")]
            public string Description { get; set; }
        }
        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 删除实体定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_delEntity_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(hd_entityID.Value))
            {
                throw new Exception("");
            }
            var entityArray = hd_entityID.Value.Split(',');

            foreach (string str in entityArray)
            {
                DynamicEntity entity = (DynamicEntity)DESchemaObjectAdapter.Instance.Load(str);
                DEObjectOperations.InstanceWithoutPermissions.DeleteEntity(entity);
            }

            InnerRefreshList();
        }

        protected void dataSourceMain_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            var allConditions = new ConnectiveSqlClauseCollection(this.DeluxeSearch.GetCondition());

            var condition = this.CurrentAdvancedSearchCondition;

            WhereSqlClauseBuilder builder = ConditionMapping.GetWhereSqlClauseBuilder(condition);

            string categoryID = Request.QueryString["categoryID"];

            if (!string.IsNullOrEmpty(categoryID))
            {
                DECategory dcg = CategoryAdapter.Instance.GetByID(categoryID);

                dcg.FullPath += "%";

                builder.AppendItem("CodeName", dcg.FullPath, string.Empty, "${DataField}$ like ${Data}$");
            }

            allConditions.Add(builder);

            this.dataSourceMain.Condition = allConditions;
        }

        /// <summary>
        /// 复制实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_copyEntity_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 导出XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Export_Click(object sender, EventArgs e)
        {
            var req = Request;
            if (req.IsAuthenticated)
            {
                //var entityIDs = hd_entityID.Value.Split(',').Where(p => p.IsNotEmpty()).ToList();
                var entityIDs = ProcessDescInfoDeluxeGrid.SelectedKeys;
                if (!entityIDs.Any())
                    return;

                DynamicEntityCollection collection = new DynamicEntityCollection();
                try
                {
                    entityIDs.ForEach(id => collection.Add(DESchemaObjectAdapter.Instance.Load(id, DateTime.Now.SimulateTime()) as DynamicEntity));
                    //验证导出数据的完整性
                    string validResult = CheckEntityChildren.CheckSelectEntities(collection.Select(p => p.ID).ToArray());
                    validResult.IsNotEmpty().TrueThrow(validResult);

                    string fileName = "DynamicEntity" + "_" + DateTime.Now.SimulateTime().ToString("yyyyMMdd_HHmmss") + ".xml";
                    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + ResponseExtensions.EncodeFileNameInContentDisposition(Response, fileName) + "\"");

                    var aaa = collection.ToXElement();
                    Response.Write(aaa.ToString());
                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {
                    var exception = ex.GetRealException();
                    throw new HttpException("导出实体出错!\r\n" + exception.Message, ex.InnerException);
                }
            }
            else
            {
                throw new HttpException("请求的方式错误");
            }
        }
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="file"></param>
        /// <param name="result"></param>
        protected void uploadProgress_DoUploadProgress(HttpPostedFile file, UploadProgressResult result)
        {
            ExceptionHelper.FalseThrow(Path.GetExtension(file.FileName).ToLower() == ".xml",
                "'{0}' must be a xml file.", file.FileName);

            StreamReader reader = new StreamReader(file.InputStream);

            XElement element = XElement.Parse(reader.ReadToEnd());

            string msg = string.Empty;
            DEDynamicEntityImportAdapter.Instance.Import(element, Request.QueryString["CategoryID"].Trim(), out msg);

            UploadProgressStatus status = new UploadProgressStatus
            {
                CurrentStep = 1,
                MinStep = 0,
                MaxStep = 1,
                StatusText = "处理完成"
            };
            status.Response();

            result.DataChanged = true;
            result.CloseWindow = false;
            result.ProcessLog = msg;
        }

        protected void uploadProgress_BeforeNormalPreRender(object sender, EventArgs e)
        {
            uploadProgress.Tag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        /// <summary>
        /// 移动实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_moveEntity_Click(object sender, EventArgs e)
        {
            InnerRefreshList();
        }
    }
}