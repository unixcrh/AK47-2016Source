using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Web.Library.Script;
using MCS.Web.WebControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Dynamics.Web.ValidatorSelector
{
    public partial class ValidatorSelectorDialog : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            PropertyEditorRegister();
            ValidatorPropertisRegister();
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack == false)
            {
                BindValidators();
                //var layouts = new PropertyLayoutSectionCollection();
                //layouts.LoadLayoutSectionFromConfiguration("DefalutLayout");
                //this.propertyGrid.Layouts.InitFromLayoutSectionCollection(layouts);
                //this.propertyGrid.Style["width"] = "100%";
                //this.propertyGrid.Style["height"] = "400";
            }
        }

        private void PropertyEditorRegister()
        {
            PropertyEditorHelper.RegisterEditor(new StandardPropertyEditor());
        }

        public void ValidatorPropertisRegister()
        {
            PropertyGroupSettings settings = PropertyGroupSettings.GetConfig();
            PropertyGroupConfigurationElementCollection groups = settings.Groups;
            List<ValidatorDefine> validatorDefineList = new List<ValidatorDefine>();
            foreach (PropertyGroupConfigurationElement element in groups)
            {
                PropertyValueCollection pvc = new PropertyValueCollection();
                PropertyDefineCollection pdc = new PropertyDefineCollection();
                pdc.LoadPropertiesFromConfiguration(element);
                pvc.InitFromPropertyDefineCollection(pdc);
                validatorDefineList.Add(new ValidatorDefine { ValidatorName = element.Name, PropertyValues = pvc });
            }
            string script = string.Format("var arrValidatorDefine={0};", JSONSerializerExecute.Serialize(validatorDefineList));

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "RegisterValidatorDefineScript", script, true);
        }

        public void BindValidators()
        {
            ValidatorTypeConfigurationElementCollection validators = ValidatorSettings.GetConfig().Validators;
            foreach (ValidatorTypeConfigurationElement vt in validators)
            {
                this.ddlValidator.Items.Add(new ListItem(vt.Description, string.Format("{0}|{1}",vt.Name,vt.Type)));
            }
            this.ddlValidator.Items.Insert(0,new ListItem("请选择",""));
            this.ddlValidator.SelectedIndex = 0;
        }
    }
    [Serializable]
    public class ValidatorDefine
    {
        public string ValidatorName { get; set; }
        public PropertyValueCollection PropertyValues { get; set; }
    }
}