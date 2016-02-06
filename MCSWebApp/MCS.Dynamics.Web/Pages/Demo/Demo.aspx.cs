using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Web.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Dynamics.Web.Pages.Demo
{
    public partial class Demo : System.Web.UI.Page
    {
        private string EntityID
        {
            get
            {
                //if (Request.QueryString["EntityID"] == null)
                //{
                //    return String.Empty;
                //}
                //else
                //{
                //    return Request.QueryString["EntityID"].ToString();
                //}

                return "f9174c26-c340-ad42-4209-d0d35f1cdb84";
            }

        }
        private string EntityInstenceID
        {
            get
            {
                //if (Request.QueryString["EntityInstenceID"] == null)
                //{
                //    return String.Empty;
                //}
                //else
                //{
                //    return Request.QueryString["EntityInstenceID"].ToString();
                //}
                return "c12c3932-a7fb-4300-882d-03573b800128";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PropertyEditorRegister();
                this.InitData();
            }
        }

        protected void InitData()
        {
            PropertyLayoutSectionCollection layouts = new PropertyLayoutSectionCollection();
            layouts.LoadLayoutSectionFromConfiguration("DefalutLayout");
            this.propertyForm.Layouts.InitFromLayoutSectionCollection(layouts);
            if (!string.IsNullOrEmpty(EntityInstenceID))
            {
                DEEntityInstanceBase entityInstence = DEInstanceAdapter.Instance.Load(EntityInstenceID);
                this.propertyForm.Properties.CopyFrom(entityInstence.Fields.ToPropertyValues());
            }
            else if (!string.IsNullOrEmpty(EntityID))
            {
                DynamicEntity entity = (DynamicEntity)DESchemaObjectAdapter.Instance.Load(EntityID);
                this.propertyForm.Properties.CopyFrom(entity.Fields.ToPropertyValues());

                //var propertiesInAppContext = this.ViewData.Data.Properties;
                //if (propertiesInAppContext != null)
                //{
                //    this.propertyForm.Properties.ReplaceExistedPropertyValues(propertiesInAppContext);
                //}
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PropertyValueCollection propterties = new PropertyValueCollection();
            this.propertyForm.Properties.CopyTo(propterties);
            if (propterties.Count > 0)
            {
                if (!string.IsNullOrEmpty(EntityInstenceID))
                {
                    DEEntityInstanceBase entityInstence = DEInstanceAdapter.Instance.Load(EntityInstenceID);
                    entityInstence.Fields.FromPropertyValues(propterties);
                    DEInstanceAdapter.Instance.Update(entityInstence);
                }
                else
                {
                    DynamicEntity entity = (DynamicEntity)DESchemaObjectAdapter.Instance.Load(EntityID);
                    DEEntityInstanceBase instence = entity.CreateInstance();
                    instence.Fields.FromPropertyValues(propterties);
                    DEInstanceAdapter.Instance.Update(instence);
                }
            }

        }

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
        }
    }
}