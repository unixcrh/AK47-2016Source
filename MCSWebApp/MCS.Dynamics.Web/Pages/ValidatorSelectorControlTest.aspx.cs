using MCS.Library.SOA.DataObjects;
using MCS.Web.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Dynamics.Web
{
    public partial class ValidatorSelectorControlTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack == false)
            {

                PropertyDefineCollection propeties = new PropertyDefineCollection();
                propeties.LoadPropertiesFromConfiguration("StringFieldTestProperties");
                propertyGrid.Properties.InitFromPropertyDefineCollection(propeties);

            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            PropertyEditorRegister();
            base.OnPreInit(e);
        }

        private void PropertyEditorRegister()
        {
            PropertyEditorHelper.RegisterEditor(new StandardPropertyEditor());
            //注册ValidatorPropertyEditor
            PropertyEditorHelper.RegisterEditor(new ValidatorsPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new BooleanPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new EnumPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new ObjectPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new DatePropertyEditor());
            PropertyEditorHelper.RegisterEditor(new DateTimePropertyEditor());
            PropertyEditorHelper.RegisterEditor(new ImageUploaderPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new ImageUploaderPropertyEditorForGrid());
            PropertyEditorHelper.RegisterEditor(new OUUserInputPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new RoleGraphPropertyEditor());
        }
    }
}