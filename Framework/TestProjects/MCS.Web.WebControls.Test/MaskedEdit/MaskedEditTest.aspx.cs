using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls.Test.MaskedEdit
{
    public partial class MaskedEditTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void run_Click(object sender, EventArgs e)
        {
            maskedEdit.IsValidValue = chbIsValidValue.Checked;
            maskedEdit.AutoComplete = chbAutoComplete.Checked;
            //maskedEdit.IsShowBtn = chbIsShowBtn.Checked;
            maskedEdit.ShowButton = chbIsShowBtn.Checked;
            //maskedEdit.ButtonText = ddlButtonText.Text;
            //if (ddlButtonCss.Text != "Default")
            //{
            //    maskedEdit.ButtonCss = ddlButtonCss.Text;
            //}
            maskedEdit.Mask = ddlMask.Text;
            maskedEdit.CurrentMessageError = ddlCurrentMessageError.Text;
            if (ddlTextStyle.Text != "Default")
            {
                //maskedEdit.TextStyle = ddlTextStyle.Text;
            }
            if (ddlTextCss.Text != "Default")
            {
                //maskedEdit.TextCss = ddlTextCss.Text;
                //maskedEdit.TextCssClass = ddlTextCss.Text;
            }
            if (ddlOnFocusCssClass.Text != "Default")
            {
                maskedEdit.OnFocusCssClass = ddlOnFocusCssClass.Text;
            }
            if (ddlOnInvalidCssClass.Text != "Default")
            {
                maskedEdit.OnInvalidCssClass = ddlOnInvalidCssClass.Text;
            }
            //if (ddlDataArrayList.Text == "\"11:11:11\", \"22:22:22\",\"12:34:56\"")
            //{
            //    maskedEdit.DataArrayList = new string[] { "11:11:11", "22:22:22","12:34:56" };
            //}
            //else if (ddlDataArrayList.Text == "\"08:30:00\",\"12:00:00\",\"14:00:00\",\"18:00:00\",\"19:00:00\",\"21:00:00\"")
            //{
            //    maskedEdit.DataArrayList = new string[] { "08:30:00","12:00:00","14:00:00","18:00:00","19:00:00","21:00:00" };
            //}
            //maskedEdit.AutoComplete;
            //maskedEdit.ButtonCss;
            //maskedEdit.ButtonText;
            //maskedEdit.CurrentMessageError;
            //maskedEdit.DataArrayList;
            //maskedEdit.IsShowBtn;
            //maskedEdit.IsValidValue;
            //maskedEdit.Mask;
            //maskedEdit.MaskedEditButtonID;
            //maskedEdit.MaskedEditTextBoxID;
            //maskedEdit.MValue;
            //maskedEdit.OnFocusCssClass;
            //maskedEdit.OnInvalidCssClass;
            //maskedEdit.TextCss;
            //maskedEdit.TextStyle;

            StringBuilder strbInfo = new StringBuilder(512);

            strbInfo.Append("<cc1:MaskedEditControl ID=\"maskedEdit\" runat=\"server\" ");

            strbInfo.Append("IsValidValue=\"" + maskedEdit.IsValidValue.ToString() + "\" ");

            strbInfo.Append("AutoComplete=\"" + maskedEdit.AutoComplete.ToString() + "\" ");

            //strbInfo.Append("IsShowBtn=\"" + maskedEdit.IsShowBtn.ToString() + "\" ");
            strbInfo.Append("IsShowBtn=\"" + maskedEdit.ShowButton.ToString() + "\" ");

            //strbInfo.Append("ButtonText=\"" + maskedEdit.ButtonText.ToString() + "\" ");

            //if (ddlButtonCss.Text != "Default")
            //{
            //    strbInfo.Append("ButtonCss=\"" + maskedEdit.ButtonCss.ToString() + "\" ");
            //}

            strbInfo.Append("Mask=\"" + maskedEdit.Mask.ToString() + "\" ");

            strbInfo.Append("CurrentMessageError=\"" + maskedEdit.CurrentMessageError.ToString() + "\" ");

            if (ddlTextStyle.Text != "Default")
            {
                strbInfo.Append("TextStyle=\"" + maskedEdit.TextStyle.ToString() + "\" ");
            }

            if (ddlTextCss.Text != "Default")
            {
                //strbInfo.Append("TextCss=\"" + maskedEdit.TextCss.ToString() + "\" ");
                //strbInfo.Append("TextCss=\"" + maskedEdit.TextCssClass.ToString() + "\" ");
            }

            if (ddlOnFocusCssClass.Text != "Default")
            {
                strbInfo.Append("OnFocusCssClass=\"" + maskedEdit.OnFocusCssClass.ToString() + "\" ");
            }

            if (ddlOnInvalidCssClass.Text != "Default")
            {
                strbInfo.Append("OnInvalidCssClass=\"" + maskedEdit.OnInvalidCssClass.ToString() + "\" ");
            }

            strbInfo.Append("/>");

            maskedEditHtmlShow.Value = strbInfo.ToString();
        }

        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            txbGetValue.Text = maskedEdit.MValue;
        }

        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            maskedEdit.MValue = txbSetValue.Text;
        }
    }
}
