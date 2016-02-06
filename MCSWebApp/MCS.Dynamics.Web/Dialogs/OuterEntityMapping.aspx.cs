using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Web.Library;
using MCS.Dynamics.Web.Saplocalhost;

namespace MCS.Dynamics.Web.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class OuterEntityMapping : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
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
            EntityMapping mapping = new EntityMapping();
            if (!TimePointContext.Current.UseCurrentTime)
            {
                this.okButton.Visible = false;
                txt_OuterEntityName.Enabled = false;
                ddl_InType.Enabled = false;
                grid.ReadOnly = true;
            }
            if (!IsPostBack)
            {
                ExceptionHelper.TrueThrow(string.IsNullOrEmpty(Request.QueryString["EntityID"]), "EntityID不能为空");

                DynamicEntity innerEntity = DESchemaObjectAdapter.Instance.Load(Request.QueryString["EntityID"].Trim()) as DynamicEntity;
                mapping.InnerEntity = innerEntity;

                OuterEntity outerEntity = new OuterEntity();
                outerEntity.ID = Guid.NewGuid().ToString();

                mapping.OuterEntityID = outerEntity.ID;
                //外部实体
                if (Request.QueryString["OuterEntityID"].IsNotEmpty())
                {
                    outerEntity = DESchemaObjectAdapter.Instance.Load(Request.QueryString["OuterEntityID"].Trim()) as OuterEntity;

                    mapping.OuterEntityID = outerEntity.ID;

                    mapping.OuterEntityName = outerEntity.Name;

                    mapping.OuterEntityInType = outerEntity.CustomType;
                }

                //绑定下拉框
                this.ddl_InType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(InType)), "EnumValue", "Description");
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

            EntityMapping mapping = bindingControl.Data as EntityMapping;

            mapping.InnerEntity = DESchemaObjectAdapter.Instance.Load(Request.QueryString["EntityID"]) as DynamicEntity;

            mapping.EntityFieldMappingCollection.Where(p => p.FieldTypeName.Equals(FieldTypeEnum.Collection.ToString())).ForEach(p =>
            {
                //这里需要验证输入的外部实体定义名称的有效性
            });

            DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(mapping);
            if (mapping.InnerEntity != null) 
                mapping.InnerEntity.ClearCacheData();
            
            HttpContext.Current.Response.Write("<script>window.returnValue=true;window.close()</script>");
        }
    }
}