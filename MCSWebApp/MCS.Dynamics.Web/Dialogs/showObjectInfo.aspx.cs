using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using MCS.Dynamics.Web;
using MCS.Dynamics.Web.Dialogs;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Web.Library;
using MCS.Web.Library.MVC;
using MCS.Web.Library.Script;
using MCS.Web.WebControls;

namespace MCS.Dynamics.Web.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class ShowObjectInfo : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
        private bool sceneDirty = true;
        private bool enabled = false;
        private PropertyEditorSceneAdapter sceneAdapter = null;

        string ITimeSceneDescriptor.NormalSceneName
        {
            get { return this.EditEnabled ? "Normal" : "ReadOnly"; }
        }

        string ITimeSceneDescriptor.ReadOnlySceneName
        {
            get { return "ReadOnly"; }
        }


        public void AfterNormalSceneApplied()
        {
            this.okButton.Visible = enabled && this.Data != null && this.Data.Status == SchemaObjectStatus.Normal;
        }

        #region 受保护的属性
        protected bool EditEnabled
        {
            get
            {
                if (this.sceneDirty)
                {
                    this.enabled = TimePointContext.Current.UseCurrentTime;

                    if (this.enabled && Util.SuperVisiorMode == false && this.sceneAdapter != null)
                    {
                        this.enabled = this.sceneAdapter.IsEditable();
                    }

                    this.sceneDirty = false;
                }

                return this.enabled;
            }
        }
        #endregion

        #region 私有的属性


        private DESchemaObjectBase Data
        {
            get;
            set;
        }

        #endregion

        #region 受保护的方法

        protected void Page_Load(object sender, EventArgs e)
        {
            //Util.InitSecurityContext(this.notice);

            if (this.IsPostBack == false && this.IsCallback == false)
            {
                //根据请求的参数，匹配要执行的ControllerMethod
                ControllerHelper.ExecuteMethodByRequest(this);
            }

            this.PropertyEditorRegister();
        }

        [ControllerMethod(true)]
        protected void CreateNewObject()
        {
            this.CreateNewObject("DynamicEntityField");
        }

        [ControllerMethod]
        protected void CreateNewObject(string schemaType)
        {
            if (this.sceneAdapter == null)
            {
                this.sceneAdapter = PropertyEditorSceneAdapter.Create(schemaType);
                this.sceneAdapter.Mode = SCObjectOperationMode.Add;
            }

            this.CreateNewObjectBySchemaType(schemaType);
        }

        [ControllerMethod]
        protected void LoadObject(string id)
        {
            this.Data = DESchemaObjectAdapter.Instance.Load(id);

            this.sceneAdapter = PropertyEditorSceneAdapter.Create(this.Data.SchemaType);
            this.sceneAdapter.ObjectID = this.Data.ID;
            this.sceneAdapter.Mode = SCObjectOperationMode.Update;
            this.currentSchemaType.Value = this.Data.SchemaType;
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            PropertyEditorHelper.AttachToPage(this);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            this.CreateNewObjectBySchemaType(Request.Form.GetValue("currentSchemaType", string.Empty));

            var pvc = JSONSerializerExecute.Deserialize<PropertyValueCollection>(Request.Form.GetValue("properties", string.Empty));

            this.Data.Properties.FromPropertyValues(pvc);
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (this.IsPostBack == false && this.IsCallback == false)
            {
                this.InitPropertyForm(this.Data, this.EditEnabled == false);
            }

            base.OnPreRender(e);

            if (this.Data.Status != SchemaObjectStatus.Normal)
                this.okButton.Visible = false;
        }

        protected void Save_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 私有的方法

        private void CreateNewObjectBySchemaType(string schemaType)
        {

            this.Data = SchemaExtensions.CreateObject(schemaType);

            if (DESchemaObjectAdapter.Instance.Exist(this.Data.ID, DateTime.Now.SimulateTime()))
            {
                Data = DESchemaObjectAdapter.Instance.Load(this.Data.ID);
            }
            else
            {
                this.Data.Properties.ForEach(p =>
                {
                    if (Request.QueryString[p.Definition.Name].IsNotEmpty())
                    {
                        p.StringValue = Request.QueryString[p.Definition.Name].Trim();
                    }
                });

                this.currentSchemaType.Value = schemaType;
                this.Data.ID = UuidHelper.NewUuidString();
            }
        }

        private void InitPropertyForm(DESchemaObjectBase data, bool readOnly)
        {
            this.propertyForm.ReadOnly = readOnly;

            data.Properties.ForEach(p =>
            {
                if (Request.QueryString[p.Definition.Name].IsNotEmpty())
                {
                    p.StringValue = Request.QueryString[p.Definition.Name].Trim();
                }
            });

            this.propertyForm.Properties.CopyFrom(data.Properties.ToPropertyValues());


            var layouts = new PropertyLayoutSectionCollection();
            layouts.LoadLayoutSectionFromConfiguration("DefalutLayout");

            this.propertyForm.Layouts.InitFromLayoutSectionCollection(layouts);

            this.propertyForm.Style["width"] = "100%";
            this.propertyForm.Style["height"] = "400";
        }

        #region "将来添加自定义PropertyEditor时需要在此注册"
        private void PropertyEditorRegister()
        {
            PropertyEditorHelper.RegisterEditor(new StandardPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new BooleanPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new EnumPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new ObjectPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new DatePropertyEditor());
            PropertyEditorHelper.RegisterEditor(new DateTimePropertyEditor());
            PropertyEditorHelper.RegisterEditor(new CodeNameUniqueEditor());
            PropertyEditorHelper.RegisterEditor(new GetPinYinEditor());
            PropertyEditorHelper.RegisterEditor(new ImageUploaderPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new PObjectNameEditor());
            PropertyEditorHelper.RegisterEditor(new ReferenceEntityCodeNameEditor());
            PropertyEditorHelper.RegisterEditor(new ValidatorsPropertyEditor());
        }
        #endregion

        #endregion
    }
}