using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.WebControls;

namespace MCS.Dynamics.Web.Dialogs
{
    public partial class EntityFieldDetails : System.Web.UI.Page
    {
        private bool sceneDirty = true;
        private bool enabled = false;
        //private DEPropertyEditorSceneAdapter sceneAdapter = null;
        private DESchemaObjectBase Data
        {
            get;
            set;
        }

        protected bool EditEnabled
        {
            get
            {
                if (this.sceneDirty)
                {
                    //this.enabled = TimePointContext.Current.UseCurrentTime;

                    //if (this.enabled && Util.SuperVisiorMode == false && this.sceneAdapter != null)
                    //{
                    //    this.enabled = this.sceneAdapter.IsEditable();
                    //}

                    this.sceneDirty = false;
                }

                return this.enabled;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (this.IsPostBack == false && this.IsCallback == false)
            {
                this.RenderAllTabs(this.Data, this.EditEnabled == false);
            }

            base.OnPreRender(e);

            //if (this.Data.Status != SchemaObjectStatus.Normal)
                //this.okButton.Visible = false;
        }

        private void RenderAllTabs(DESchemaObjectBase data, bool readOnly)
        {
            string defaultKey = this.tabStrip.SelectedKey;

            Dictionary<string, SchemaPropertyValueCollection> tabGroup = data.Properties.GroupByTab();

            this.tabStrip.TabStrips.Clear();

            foreach (SchemaTabDefine tab in data.Schema.Tabs)
            {
                SchemaPropertyValueCollection properties = null;

                if (tabGroup.TryGetValue(tab.Name, out properties) == false)
                    properties = new SchemaPropertyValueCollection();

                Control panel = this.RenderOnePanel(tab, this.panelContainer, properties, readOnly);

                var item = new TabStripItem() { Key = tab.Name, Text = tab.Description, ControlID = panel.ClientID, Tag = panel.Controls[0].ClientID };

                this.tabStrip.TabStrips.Add(item);

                if (defaultKey.IsNullOrEmpty())
                    defaultKey = item.Key;
            }

            if (defaultKey.IsNotEmpty())
                this.tabStrip.SelectedKey = defaultKey;
        }

        private Control RenderOnePanel(SchemaTabDefine tab, Control container, SchemaPropertyValueCollection properties, bool readOnly)
        {
            var panel = new HtmlGenericControl("div");

            panel.ID = tab.Name;
            panel.Style["width"] = "100%";
            panel.Style["height"] = "100%";

            this.panelContainer.Controls.Add(panel);

            var pForm = new PropertyForm() { AutoSaveClientState = false };
            pForm.ID = tab.Name + "_Form";
            pForm.ReadOnly = readOnly;

            //// if (currentScene.Items[this.tabStrip.ID].Recursive == true)
            ////    pForm.ReadOnly = currentScene.Items[this.tabStrip.ID].ReadOnly;

            pForm.Properties.CopyFrom(properties.ToPropertyValues());

            var layouts = new PropertyLayoutSectionCollection();
            layouts.LoadLayoutSectionFromConfiguration("DefalutLayout");

            pForm.Layouts.InitFromLayoutSectionCollection(layouts);

            pForm.Style["width"] = "100%";
            pForm.Style["height"] = "400";

            panel.Controls.Add(pForm);

            return panel;
        }
    }
}