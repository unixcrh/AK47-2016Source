using MCS.Web.Library.Script;
using MCS.Web.WebControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#region 嵌入资源
[assembly: WebResource("MCS.Web.WebControls.ValidatorSelector.ValidatorSelectorControl.js", "application/x-javascript")]
[assembly: WebResource("MCS.Web.WebControls.ValidatorSelector.ou.gif", "image/gif")]
#endregion

namespace MCS.Web.WebControls
{
    /// <summary>
    /// 动态实体属性定义验证器选择控件
    /// </summary>
    [RequiredScript(typeof(ControlBaseScript))]
    [RequiredScript(typeof(HBCommonScript))]
    [ToolboxData("<{0}:ValidatorSelectorControl runat=server></{0}:ValidatorSelectorControl>")]
    [ClientScriptResource("MCS.Web.WebControls.ValidatorSelectorControl", "MCS.Web.WebControls.ValidatorSelector.ValidatorSelectorControl.js")]
    public class ValidatorSelectorControl : ScriptControlBase
    {
        #region 控件
        private HtmlGenericControl ctlHtmlSpan = new HtmlGenericControl("SPAN");
        /// <summary>
        /// 显示选中的校验器名称的控件
        /// </summary>
        private HtmlInputText inputText = new HtmlInputText();
        /// <summary>
        /// 选择校验器的图片按钮控件
        /// </summary>
        private HtmlImage selectorImage = new HtmlImage();
        /// <summary>
        /// 存储选中的校验器的JSON数据的隐藏控件
        /// </summary>
        private HtmlInputHidden inputHiddenValue = new HtmlInputHidden();
        #endregion

        public ValidatorSelectorControl()
            : base(true, HtmlTextWriterTag.Div)
        { }
        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        /// <summary>
        /// Render
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            Controls.Clear();
            inputText.ID = "txtName";
            inputText.Attributes.Add("readonly", "readonly");
            Controls.Add(inputText);
            selectorImage.ID = "imgSelector";
            selectorImage.Style["cursor"] = "hand";
            selectorImage.Src = SelectorImageUrl;
            Controls.Add(selectorImage);
            inputHiddenValue.ID = "hidValue";
            Controls.Add(inputHiddenValue);
            base.Render(writer);
        }
        /// <summary>
        /// 控件是否可用
        /// </summary>
        /// <remarks>
        [ScriptControlProperty]//设置此属性要输出到客户端
        [ClientPropertyName("enabled")]//对应的客户端属性
        public override bool Enabled
        {
            get { return GetPropertyValue<bool>("enabled", true); }
            set { SetPropertyValue<bool>("enabled", value); }
        }
        /// <summary>
        /// 选择验证器的图标地址
        /// </summary>
        [ScriptControlProperty]
        [ClientPropertyName("selectorImageUrl")]
        public string SelectorImageUrl
        {
            get
            {
                return GetPropertyValue<string>("SelectorImageUrl",
                    Page.ClientScript.GetWebResourceUrl(typeof(ValidatorSelectorControl),
                        "MCS.Web.WebControls.ValidatorSelector.ou.gif"));
            }
            set { SetPropertyValue<string>("SelectorImageUrl", value); }
        }
        /// <summary>
        /// 选中的Validator的JSON数据
        /// </summary>
        [ScriptControlProperty]
        [ClientPropertyName("value")]
        public string Value
        {
            get
            {
                return GetPropertyValue<string>("value", string.Empty);
            }
            set { SetPropertyValue<string>("value", value); }
        }

        /// <summary>
        /// 选中的Validator的在标签中显示的内容
        /// </summary>
        [ScriptControlProperty]
        [ClientPropertyName("text")]
        public string Text
        {
            get
            {
                return GetPropertyValue<string>("Text", string.Empty);
            }
            set { SetPropertyValue<string>("Text", value); }
        }

        /// <summary>
        /// 弹出选择Validator页面的URL
        /// </summary>
        [ScriptControlProperty]
        [ClientPropertyName("selectorDialogUrl")]
        public string SelectorDialogUrl
        {
            get
            {
                return GetPropertyValue<string>("SelectorDialogUrl", "../Dialogs/ValidatorSelectorDialog.aspx");
            }

            set { SetPropertyValue<string>("SelectorDialogUrl", value); }
        }

        /// <summary>
        /// 显示选中的校验器名称的控件的客户端ID
        /// </summary>
        [Browsable(false)]
        [ScriptControlProperty]
        [ClientPropertyName("inputTextClientID")]
        protected string InputTextClientID
        {
            get
            {
                return this.inputText.ClientID;
            }
        }

        /// <summary>
        /// 选择校验器的图片按钮控件的客户端ID
        /// </summary>
        [Browsable(false)]
        [ScriptControlProperty]
        [ClientPropertyName("selectorImageClientID")]
        protected string SelectorImageClientID
        {
            get
            {
                return selectorImage.ClientID;
            }
        }

        /// <summary>
        /// 存储选中的校验器的JSON数据的隐藏控件的客户端ID
        /// </summary>
        [Browsable(false)]
        [ScriptControlProperty]
        [ClientPropertyName("inputHiddenValueClientID")]
        protected string InputHiddenValueClientID
        {
            get
            {
                return this.inputHiddenValue.ClientID;
            }
        }

    }
}