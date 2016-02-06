using MCS.Library.Core;
using MCS.Web.Library;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Web.WebControls
{
    [DefaultProperty("Text")]
    public class HBTextBox : TextBox
    {
        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string text = postCollection[postDataKey];

            if (text != null)
                this.Text = text;

            return text != null;
        }

        /// <summary>
        /// 文本框的样式，对于TextBox，这个样式优先于CssClass
        /// </summary>
        [CssClassProperty]
        [DefaultValue("")]
        public virtual string TextBoxCssClass
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "TextBoxCssClass", string.Empty);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "TextBoxCssClass", value);
            }
        }

        /// <summary>
        /// Label的样式，对于Label，这个样式优先于CssClass
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

        /// <summary>
        /// 输入控件只读属性为true时候，把TextBox变为Label控件
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.ReadOnly && this.KeepControlWhenReadOnly == false)
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

                if (lableID.IsNotEmpty())
                {
                    writer.AddAttribute("relativeLabel", lableID);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");

                    string script = "if (event.propertyName=='value' || event.propertyName=='innerText') {var textValue; if (event.srcElement.tagName=='INPUT') {textValue=event.srcElement.value;} else {textValue=event.srcElement.innerText;}\ndocument.getElementById('{0}').innerText=textValue;}";

                    script = script.Replace("{0}", lableID);
                    writer.AddAttribute("onpropertychange", script);
                }

                if (this.TextBoxCssClass.IsNotEmpty())
                    this.CssClass = this.TextBoxCssClass;

                if (this.TextMode == TextBoxMode.MultiLine && this.ReadOnly == false)
                {
                    writer.AddAttribute("onpropertychange",
                        "this.style.posHeight = (this.scrollHeight > parseInt(this.style.minHeight.replace('px', '')))" +
                        " ? this.scrollHeight : parseInt(this.style.minHeight.replace('px', ''));");

                    if (this.CssClass.IsNullOrEmpty() && this.Height == Unit.Empty)
                        this.Height = Unit.Pixel(20);

                    if (this.Height != Unit.Empty)
                    {
                        //if (string.IsNullOrEmpty(this.Height.ToString()))
                        //    this.Height = Unit.Pixel(20);

                        writer.AddStyleAttribute("min-height", this.Height.ToString());
                    }

                    base.Render(writer);

                    writer.WriteLine("<script type='text/javascript'>");

                    string adjustScript = string.Format("{0}.style.posHeight = ({0}.scrollHeight > parseInt({0}.style.minHeight.replace('px', ''))) ? {0}.scrollHeight : parseInt({0}.style.minHeight.replace('px', ''));",
                        "document.getElementById('" + this.ClientID + "')");

                    string loadScript = "function(){" + adjustScript + "}";
                    writer.WriteLine("if (window.attachEvent){window.attachEvent('onload', " + loadScript + ")}else{window.addEventListener('load', " + loadScript + ", false)}");
                    writer.WriteLine("</script>");
                }
                else
                    base.Render(writer);
            }
        }

        private void RenderLabel(HtmlTextWriter writer, string id)
        {
            Label lb = new Label();

            lb.ID = id;
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
            lb.BorderWidth = this.BorderWidth;
            lb.ControlStyle.CopyFrom(this.ControlStyle);

            if (this.Height != Unit.Empty)
            {
                lb.Style["min-height"] = this.Height.ToString();
                lb.Style["height"] = string.Empty;
            }

            lb.TabIndex = this.TabIndex;
            lb.TemplateControl = this.TemplateControl;

            string txt = HttpUtility.HtmlEncode(this.Text);

            txt = txt.Replace("\r\n", "<br>");
            lb.Text = txt;
            lb.ToolTip = this.ToolTip;
            lb.Visible = this.Visible;
            lb.Width = this.Width;

            if (this.LabelBoxCssClass.IsNotEmpty())
                lb.CssClass = this.LabelBoxCssClass;
            else
                lb.CssClass = this.CssClass;

            lb.Style.Add("word-wrap", "break-word");

            lb.RenderControl(writer);
        }
    }
}
