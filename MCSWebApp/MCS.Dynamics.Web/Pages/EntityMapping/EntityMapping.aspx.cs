using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;

namespace MCS.Dynamics.Web.Pages.EntityMapping
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class EntityMapping : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!TimePointContext.Current.UseCurrentTime)
            {
                this.btn_add.Enabled = this.btn_del_Mapping.Enabled = false;
            }
            if (!IsPostBack)
            {
                string dataSourId = Request.QueryString["ID"];
                dataSourId.NullCheck("参数");
                this.bindingControl.Data = LoadEntity(Request.QueryString["ID"]);
            }
        }

        private DynamicEntity LoadEntity(string entityID)
        {
            entityID.CheckStringIsNullOrEmpty("[实体ID]");
            var loadEntity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            loadEntity.NullCheck("[获取实体]");
            return loadEntity;
        }

        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }

        protected void objectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

        }

        protected void dataSourceMain_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            ConnectiveSqlClauseCollection condition = new ConnectiveSqlClauseCollection();
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
            builder.AppendItem(" r.ContainerID ", Request.QueryString["ID"].Trim());
            condition.Add(builder);
            this.dataSourceMain.Condition = condition;
        }

        protected void btn_del_Mapping_Click(object sender, EventArgs e)
        {

            string[] memberID = hd_entityID.Value.Split(',');
            string dataSourId = Request.QueryString["ID"];
            dataSourId.NullCheck("参数");
            DEObjectOperations.InstanceWithoutPermissions.DeleteEntityMapping(dataSourId, memberID);
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