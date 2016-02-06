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
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;

namespace MCS.Dynamics.Web.Pages.ETL
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class ETLEntityList : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
        #region
        string ITimeSceneDescriptor.NormalSceneName
        {
            get { return this.EditEnabled ? "Normal" : "ReadOnly"; }
        }

        string ITimeSceneDescriptor.ReadOnlySceneName
        {
            get { return "ReadOnly"; }
        }

        protected bool EditEnabled
        {
            get
            {
                var enabled = TimePointContext.Current.UseCurrentTime && Util.SuperVisiorMode;
                return enabled;
            }
        }

        public void AfterNormalSceneApplied()
        {
            //此处注释掉了   验证不成功时  页面还可以继续更改
            //this.okButton.Visible = this.Data != null && this.Data.Status == SchemaObjectStatus.Normal;
        }

        #endregion
        public static readonly string ThisPageSearchResourceKey = "6EADF811-9825-4410-A648-552BB5ABCDEF";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!TimePointContext.Current.UseCurrentTime)
            {
                this.btn_delEntity.Enabled = false;
            }
            if (Page.IsPostBack == false && Page.IsCallback == false)
            {
                this.CurrentAdvancedSearchCondition = new ETLPageAdvancedSearchCondition();

                string categoryID = Request.QueryString["categoryID"];

                if (string.IsNullOrEmpty(categoryID))
                {
                    this.btn_addEtlEntity.Visible = false;
                }

                string islast = Request.QueryString["islast"];

                if (string.IsNullOrEmpty(islast))
                {
                    btn_Import.Visible = false;
                    this.btn_addEtlEntity.Visible = false;
                }

                this.DeluxeSearch.UserCustomSearchConditions = DbUtil.LoadSearchCondition(ThisPageSearchResourceKey, "Default");

            }

            this.searchBinding.Data = this.CurrentAdvancedSearchCondition;


        }
        [SerializableAttribute]
        internal class ETLPageAdvancedSearchCondition
        {
            [ConditionMapping("Name")]
            public string CodeName { get; set; }
            [ConditionMapping("Description")]
            public string Description { get; set; }
        }

        private ETLPageAdvancedSearchCondition CurrentAdvancedSearchCondition
        {
            get { return this.ViewState["AdvSearchCondition"] as ETLPageAdvancedSearchCondition; }

            set { this.ViewState["AdvSearchCondition"] = value; }
        }

        protected void SearchButtonClick(object sender, MCS.Web.WebControls.SearchEventArgs e)
        {

            this.ProcessDescInfoDeluxeGrid.PageIndex = 0;

            this.searchBinding.CollectData();

            var bindData = searchBinding.Data as ETLPageAdvancedSearchCondition;

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
                //entity.Fields[0].OuterEntityFields[0].Name
                //entity.DynamicEntityMappingCollection[0].DestinationName;
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

        //导出
        protected void btn_Export_Click(object sender, EventArgs e)
        {
            var req = Request;
            if (req.IsAuthenticated)
            {
                //var entityIDs = hd_entityID.Value.Split(',').Where(p => p.IsNotEmpty()).ToList();
                var entityIDs = ProcessDescInfoDeluxeGrid.SelectedKeys;

                if (!entityIDs.Any())
                    return;

                ETLEntityCollection collection = new ETLEntityCollection();
                try
                {
                    entityIDs.ForEach(id => collection.Add(DESchemaObjectAdapter.Instance.Load(id, DateTime.Now.SimulateTime()) as ETLEntity));
                    //验证导出数据的完整性
                    string validResult = CheckEntityChildren.CheckSelectEntities(collection.Select(p => p.ID).ToArray());
                    validResult.IsNotEmpty().TrueThrow(validResult);

                    string fileName = "ETLEntity" + "_" + DateTime.Now.SimulateTime().ToString("yyyyMMdd_HHmmss") + ".xml";
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

        protected void uploadProgress_DoUploadProgress(HttpPostedFile file, UploadProgressResult result)
        {
            ExceptionHelper.FalseThrow(Path.GetExtension(file.FileName).ToLower() == ".xml",
                "'{0}' must be a xml file.", file.FileName);

            StreamReader reader = new StreamReader(file.InputStream);

            XElement element = XElement.Parse(reader.ReadToEnd());

            string msg = string.Empty;
            ETLEntityImportAdapter.Instance.Import(element, Request.QueryString["CategoryID"].Trim(), out msg);

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

        protected void btn_moveEntity_Click(object sender, EventArgs e)
        {

            InnerRefreshList();
        }
    }
}
