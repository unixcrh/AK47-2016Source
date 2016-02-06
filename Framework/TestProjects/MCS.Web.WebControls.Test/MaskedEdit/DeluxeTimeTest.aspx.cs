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
    public partial class DeluxeTimeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ctrDeluxeTime.AutoComplete;   //是否自动补齐时间
            // ctrDeluxeTime.IsValidValue;   //是否启用验证
            //ctrDeluxeTime.ShowButton; //是否提供按钮来选择自定义时间列表,若是则需设置数据源

            //ctrDeluxeTime.DataSource; //Data

            //ctrDeluxeTime.TextStyle;  //输入框样式

            //ctrDeluxeTime.AutoCompleteValue;  //提供自动补齐的时间串，不设置则取系统时间
            //ctrDeluxeTime.CurrentMessageError;    //验证时间的提示信息
            //ctrDeluxeTime.Mask;   //时间格式串
            //ctrDeluxeTime.MValue; //设置或是获取时间的值
            //ctrDeluxeTime.OnFocusCssClass;    //得到焦点时文本框的样式
            //ctrDeluxeTime.OnInvalidCssClass;  //验证不通过时文本框的样式
            //ctrDeluxeTime.TextCssClass;   //设置文本框的样式
            //ctrDeluxeTime.PromptCharacter;    //掩码字符

        }


        private string BuildInfo()
        {
            StringBuilder strBuildInfo = new StringBuilder(512);

            strBuildInfo.Append("<cc1:DeluxeTime ID=\"ctrDeluxeTime\" \n runat=\"server\"");

            if (!ckbAutoComplete.Checked)
            {
                strBuildInfo.Append("\n AutoComplete=\"" + ctrlDeluxeTime.AutoComplete.ToString() + "\" ");
            }
            if (!ckbIsValidValue.Checked)
            {
                strBuildInfo.Append("\n IsValidValue=\"" + ctrlDeluxeTime.IsValidValue.ToString() + "\" ");
            }
            if (!ckbShowButton.Checked)
            {
                strBuildInfo.Append("\n ShowButton=\"" + ctrlDeluxeTime.ShowButton.ToString() + "\" ");
            }


            strBuildInfo.Append("\n AutoCompleteValue=\"" + ctrlDeluxeTime.AutoCompleteValue.ToString() + "\" ");
            strBuildInfo.Append("\n CurrentMessageError=\"" + ctrlDeluxeTime.CurrentMessageError.ToString() + "\" ");
           
            
            if (ddlMask.Text != "Default")
            {
                strBuildInfo.Append("\n Mask=\"" + ctrlDeluxeTime.Mask.ToString() + "\" ");
            }
            if (ddlOnFocusCssClass.Text != "Default")
            {
                strBuildInfo.Append("\n OnFocusCssClass=\"" + ctrlDeluxeTime.OnFocusCssClass.ToString() + "\" ");
            } 
            if (ddlOnInvalidCssClass.Text != "Default")
            {
                strBuildInfo.Append("\n OnInvalidCssClass=\"" + ctrlDeluxeTime.OnInvalidCssClass.ToString() + "\" ");
            } 
            if (ddlTextCssClass.Text != "Default")
            {
                strBuildInfo.Append("\n TextCssClass=\"" + ctrlDeluxeTime.TextCssClass.ToString() + "\" ");
            }
           

            strBuildInfo.Append("\n PromptCharacter=\"" + ctrlDeluxeTime.PromptCharacter.ToString() + "\" ");
           
            strBuildInfo.Append("/>");

            return strBuildInfo.ToString();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            ctrlDeluxeTime.AutoComplete = ckbAutoComplete.Checked;
            ctrlDeluxeTime.IsValidValue = ckbIsValidValue.Checked;
            ctrlDeluxeTime.ShowButton = ckbShowButton.Checked;

            //if (txbAutoCompleteValue.Text != "")
            //{
                ctrlDeluxeTime.AutoCompleteValue = txbAutoCompleteValue.Text;
            //}
            //if (txbCurrentMessageError.Text != "")
            //{
                ctrlDeluxeTime.CurrentMessageError = txbCurrentMessageError.Text;
            //}
            //if (txbCurrentMessageError.Text != "")
            //{
                ctrlDeluxeTime.PromptCharacter = txbPromptCharacter.Text;
            //}

            if (ddlMask.Text != "Default")
            {
                ctrlDeluxeTime.Mask = ddlMask.Text;
            }
            if (ddlOnFocusCssClass.Text != "Default")
            {
                ctrlDeluxeTime.OnFocusCssClass = ddlOnFocusCssClass.Text;
            }
            if (ddlOnInvalidCssClass.Text != "Default")
            {
                ctrlDeluxeTime.OnInvalidCssClass = ddlOnInvalidCssClass.Text;
            }
            if (ddlTextCssClass.Text != "Default")
            {
                ctrlDeluxeTime.TextCssClass = ddlTextCssClass.Text;
            }

            ctrlDeluxeTimeAttributeShow.Value = this.BuildInfo();
			if (this.ckb_ClientScript.Checked)
			{
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ValueChanged", @"<script>function ValueChanged(){alert('Event: ValueChanged ')} </script>", false);
				this.ctrlDeluxeTime.OnClientValueChanged = "ValueChanged";
			}
        }

        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            txbGetValue.Text = ctrlDeluxeTime.MValue.ToString();
            txbSetValue.Text = txbGetValue.Text;
        }

        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            ctrlDeluxeTime.MValue = txbSetValue.Text;
        }

        protected void btnSetDataSource_Click(object sender, EventArgs e)
        {
            this.DateBind();
        }

        private void DateBind()
        {
            ListItem lsitOption = new ListItem();
            lsitOption.Text = txbDataSource1.Text;
            ctrlDeluxeTime.DataSource.Add(lsitOption);
        }
    }
}
