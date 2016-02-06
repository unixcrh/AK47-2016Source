using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Contract;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Facade;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Web.Library;
namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class OuterETLEntityMapping : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
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
            ETLEntityMapping mapping = new ETLEntityMapping();
            if (!TimePointContext.Current.UseCurrentTime)
            {
                this.okButton.Visible = false;
                txt_OuterEntityName.Enabled = false;
                grid.ReadOnly = true;
            }
            if (!IsPostBack)
            {
                ExceptionHelper.TrueThrow(string.IsNullOrEmpty(Request.QueryString["EntityID"]), "EntityID不能为空");

                ETLEntity innerEntity = DESchemaObjectAdapter.Instance.Load(Request.QueryString["EntityID"].Trim()) as ETLEntity;
                mapping.InnerEntity = innerEntity;

                OuterETLEntity outerEntity = new OuterETLEntity();
                outerEntity.ID = Guid.NewGuid().ToString();

               // mapping.OuterEntityID = outerEntity.ID;
               
                //外部实体
                if (Request.QueryString["OuterEntityID"].IsNotEmpty())
                {
                    outerEntity = DESchemaObjectAdapter.Instance.Load(Request.QueryString["OuterEntityID"].Trim()) as OuterETLEntity;

                    mapping.OuterEntityID = outerEntity.ID;

                    mapping.OuterEntityName = outerEntity.Name;

                 
                }

                //绑定下拉框
               // this.ddl_InType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(InType)), "EnumValue", "Description");
            }

            this.bindingControl.Data = mapping;
        }

        //保存事件
        protected void btn_save_OnClick(object sender, EventArgs e)
        {
            if (!TimePointContext.Current.UseCurrentTime)
            {
                HttpContext.Current.Response.Write(string.Format("<script>alert('{0}')</script>", "历史数据不能修改"));
                return;
            }
            bindingControl.CollectData();

            ETLEntityMapping mapping = bindingControl.Data as ETLEntityMapping;
            ETLEntity etnEntity = DESchemaObjectAdapter.Instance.Load(Request.QueryString["EntityID"]) as ETLEntity;
            mapping.InnerEntity = etnEntity;

            mapping.ETLFieldsMapping.Where(p => p.FieldTypeName.Equals(FieldTypeEnum.Collection.ToString())).ForEach(p =>
            {
                //这里需要验证输入的外部实体定义名称的有效性
            });

            ETLDEObjectOperations.Instance.AddETLEntityMapping(mapping);
            if (mapping.InnerEntity != null) mapping.InnerEntity.ClearCacheData();
            //etnEntity

            HttpContext.Current.Response.Write("<script>window.returnValue=true;window.close()</script>");

        }
    }
}