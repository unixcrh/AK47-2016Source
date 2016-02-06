#region
// -------------------------------------------------
// Assembly	��	MCS.Web.WebControls
// FileName	��	HBDropDownList.cs
// Remark	��	ѡ��ؼ�
// -------------------------------------------------
// VERSION		AUTHOR		     DATE			CONTENT
// 1.0			Ӣ�۲�����		20080325    	����15�� ���� 
// -------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Web.Library;
using MCS.Web.Library.Script;

namespace MCS.Web.WebControls
{
    [DefaultProperty("Text")]
    public class HBDropDownList : DropDownList
    {
        private string lastPostKey = string.Empty;
        private HtmlInputHidden selectedTextHidden = new HtmlInputHidden();
        private HtmlInputHidden selectedValueHidden = new HtmlInputHidden();

        [Browsable(true),
        Description("ֻ������"),
        DefaultValue(false),
        Category("��չ")]
        public bool ReadOnly
        {
            get
            {
                return WebControlUtility.GetViewStateValue<bool>(ViewState, "ReadOnly", false);
            }
            set
            {
                WebControlUtility.SetViewStateValue<bool>(ViewState, "ReadOnly", value);
            }
        }

        /// <summary>
        /// �ı������ʽ������TextBox�������ʽ������CssClass
        /// </summary>
        [CssClassProperty]
        [DefaultValue("")]
        public virtual string DropDownListCssClass
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "DropDownListCssClass", string.Empty);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "DropDownListCssClass", value);
            }
        }

        /// <summary>
        /// ��ֻ��ģʽʱ����������б�ѡ�����Ĭ������ʾ���ı�
        /// </summary>
        [DefaultValue("")]
        public virtual string ReadOnlyDefaultText
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "ReadOnlyDefaultText", string.Empty);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "ReadOnlyDefaultText", value);
            }
        }

        /// <summary>
        /// �Ƿ�����ֻ��ģʽ�������б�ѡ�����Ĭ������ʾ�ı�
        /// </summary>
        [DefaultValue(false)]
        public virtual bool EnableReadOnlyDefaultText
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "EnableReadOnlyDefaultText", false);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "EnableReadOnlyDefaultText", value);
            }
        }

        /// <summary>
        /// Label����ʽ������Label�������ʽ������CssClass
        /// </summary>
        [CssClassProperty]
        [DefaultValue("")]
        public virtual string LabelBoxCssClass
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "LabelBoxCssClass", string.Empty);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "LabelBoxCssClass", value);
            }
        }

        [DefaultValue(false)]
        public bool KeepControlWhenReadOnly
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "KeepControlWhenReadOnly", false);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "KeepControlWhenReadOnly", value);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            lastPostKey = postDataKey;
            return base.LoadPostData(postDataKey, postCollection);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }

        protected override void OnPreRender(EventArgs e)
        {
            string script = ResourceHelper.LoadStringFromResource(Assembly.GetExecutingAssembly(),
                    "MCS.Web.WebControls.HBDropDownList.HBDropDownList.js");

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "HBDropDownListClient", script, true);

            this.Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                this.ClientID,
                string.Format("attachHBDropDownListEvents(document.getElementById('{0}'));", this.ClientID),
                true);

            base.OnPreRender(e);
        }

        public override string SelectedValue
        {
            get
            {
                string result = base.SelectedValue;

                if (this.Page.IsPostBack || this.Page.IsCallback)
                {
                    string postedValue = this.Page.Request.Form[lastPostKey];

                    if (postedValue != null)
                        result = postedValue;
                }

                this.selectedValueHidden.Value = result;

                return result;
            }
            set
            {
                this.selectedValueHidden.Value = value;
                base.SelectedValue = value;
            }
        }

        public string SelectedText
        {
            get
            {
                if (this.Page.IsPostBack || this.Page.IsCallback)
                {
                    string postedText = this.Page.Request.Form[lastPostKey + "_SelectedText"];

                    if (postedText != null)
                        this.selectedTextHidden.Value = postedText;
                    else
                        this.selectedTextHidden.Value = (string)this.ViewState["SelectedText"];
                }
                else
                {
                    if (this.SelectedItem != null)
                        this.selectedTextHidden.Value = this.SelectedItem.Text;
                }

                return this.selectedTextHidden.Value;
            }
            set
            {
                this.selectedTextHidden.Value = value;
            }
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            base.RenderControl(writer);

            RenderHiddenFields(writer);
        }

        protected override object SaveViewState()
        {
            InitHiddenFieldsAttributes();

            return base.SaveViewState();
        }

        /// <summary>
        /// ����ؼ�ֻ������Ϊtrueʱ�򣬰�DropDownList��ΪLabel�ؼ�
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.ReadOnly && KeepControlWhenReadOnly == false)
            {
                RenderLabel(writer, this.ClientID);
            }
            else
            {
                string lableID = string.Empty;

                if (this.KeepControlWhenReadOnly && this.ReadOnly)
                {
                    lableID = this.ClientID + "_Label";

                    RenderLabel(writer, lableID);
                }

                if (string.IsNullOrEmpty(lableID) == false)
                {
                    writer.AddAttribute("relativeLabel", lableID);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                }

                if (this.DropDownListCssClass.IsNotEmpty())
                    this.CssClass = this.DropDownListCssClass;

                writer.AddAttribute("relativeHidden", this.selectedTextHidden.ID);
                writer.AddAttribute("relativeValueHidden", this.selectedValueHidden.ID);

                base.Render(writer);
            }
        }

        private void InitHiddenFieldsAttributes()
        {
            this.selectedTextHidden.ID = this.ClientID + "_SelectedText";
            this.selectedValueHidden.ID = this.ClientID + "_SelectedValue";

            if (this.SelectedItem != null)
            {
                this.selectedTextHidden.Value = this.SelectedItem.Text;
                this.selectedValueHidden.Value = this.SelectedItem.Value;
            }
            else
            {
                this.selectedTextHidden.Value = string.Empty;
                this.selectedValueHidden.Value = string.Empty;
            }

            this.ViewState["SelectedText"] = this.selectedTextHidden.Value;
        }

        private void RenderHiddenFields(HtmlTextWriter writer)
        {
            writer.Write(WebControlUtility.GetControlHtml(this.selectedTextHidden));
            writer.Write(WebControlUtility.GetControlHtml(this.selectedValueHidden));
        }

        private void RenderLabel(HtmlTextWriter writer, string id)
        {
            Label lb = new Label();
            lb.AccessKey = this.AccessKey;
            lb.AppRelativeTemplateSourceDirectory = this.AppRelativeTemplateSourceDirectory;

            foreach (string s in this.Attributes.Keys)
                lb.Attributes.Add(s, this.Attributes[s]);

            foreach (string s in this.Style.Keys)
                lb.Style.Add(s, this.Style[s]);

            lb.ForeColor = this.ForeColor;
            lb.Font.CopyFrom(this.Font);
            lb.BackColor = this.BackColor;
            lb.BorderColor = this.BorderColor;
            lb.BorderStyle = this.BorderStyle;
            lb.BorderWidth = this.BorderWidth; ;
            lb.ControlStyle.CopyFrom(this.ControlStyle);
            lb.Height = this.Height;
            lb.ID = id;
            lb.TabIndex = this.TabIndex;
            lb.TemplateControl = this.TemplateControl;

            if (this.EnableReadOnlyDefaultText && this.SelectedIndex <= 0)
                lb.Text = this.ReadOnlyDefaultText;
            else
            {
                if (this.SelectedItem != null)
                    lb.Text = HttpUtility.HtmlEncode(this.SelectedItem.Text);
                else
                    lb.Text = this.SelectedValue;
            }

            lb.ToolTip = this.ToolTip;
            lb.Visible = this.Visible;
            lb.Width = this.Width;

            if (this.LabelBoxCssClass.IsNotEmpty())
                lb.CssClass = this.LabelBoxCssClass;
            else
                lb.CssClass = this.CssClass;

            lb.RenderControl(writer);
        }
    }
}