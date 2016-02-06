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

namespace MCS.Web.WebControls.Test.PopupCalendar
{
	public partial class DeluxeCalendarTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			//ctrlDeluxeCalendar.EnabledOnClient;//是否启用日历功能
			//ctrlDeluxeCalendar.AutoComplete;//是否自动补齐日期
			//ctrlDeluxeCalendar.IsComplexHeader;//是否提供下拉框快捷选项
			//ctrlDeluxeCalendar.IsOnlyCurrentMonth;//是否只显示当月
			//ctrlDeluxeCalendar.IsValidValue;//是否启用验证
			//ctrlDeluxeCalendar.Animated;//设置日历月份转换的动画效果


			//ctrlDeluxeCalendar.FirstDayOfWeek;//自定义第一列是从周几开始

			//ctrlDeluxeCalendar.AutoCompleteValue;//提供自动补齐的时间串，不设置则取系统日期
			//ctrlDeluxeCalendar.CurrentMessageError;//验证日期的提示信息
			//ctrlDeluxeCalendar.MaskedEditButtonID;//按钮的ID
			//ctrlDeluxeCalendar.PromptCharacter;//掩码字符

			//ctrlDeluxeCalendar.OnInvalidCssClass;//验证不通过时文本框的样式
			//ctrlDeluxeCalendar.OnFocusCssClass;//得到焦点时文本框的样式
			//ctrlDeluxeCalendar.ImageCssClass;//图片的CssClass
			//ctrlDeluxeCalendar.TextCssClass
			//ctrlDeluxeCalendar.ImageUrl;
			//ctrlDeluxeCalendar.ImageStyle;
			//ctrlDeluxeCalendar.CssClass;

			//ctrlDeluxeCalendar.CValue;

			//ctrlDeluxeCalendar.TextStyle;

			//ctrlDeluxeCalendar.OnClientDateSelectionChanged;//日期选择变化后触发的客户端事件
			//ctrlDeluxeCalendar.OnClientHidden;//隐藏日历后触发的客户端事件
			//ctrlDeluxeCalendar.OnClientHiding;//隐藏日历时触发的客户端事件
			//ctrlDeluxeCalendar.OnClientShowing;//弹出日历时触发的客户端事件
			//ctrlDeluxeCalendar.OnClientShown;//弹出日历时后触发的客户端事件


		}

		protected override void OnInit(EventArgs e)
		{
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientDateSelectionChanged", @"<script>function funcOnClientDateSelectionChanged(){alert('Event OnClientDateSelectionChanged');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHidden", @"<script>function funcOnClientHidden(){alert('Event OnClientHidden');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHiding", @"<script>function funcOnClientHiding(){alert('Event OnClientHiding');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShowing", @"<script>function funcOnClientShowing(){alert('Event OnClientShowing');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShown", @"<script>function funcOnClientShown(){alert('Event OnClientShown');} </script>", false);
			//ctrlDeluxeCalendar.OnClientDateSelectionChanged = "funcOnClientDateSelectionChanged";
			//ctrlDeluxeCalendar.OnClientHidden = "funcOnClientHidden";
			//ctrlDeluxeCalendar.OnClientHiding = "funcOnClientHiding";
			//ctrlDeluxeCalendar.OnClientShowing = "funcOnClientShowing";
			//ctrlDeluxeCalendar.OnClientShown = "funcOnClientShown";
			base.OnInit(e);
		}

		private string BuildControlInfo()
		{
			StringBuilder strbInfo = new StringBuilder(512);

			strbInfo.Append("<cc1:DeluxeCalendar ID=\"ctrlDeluxeCalendar\" runat=\"server\"");

			if (!ckbEnabledOnClient.Checked)
			{
				strbInfo.Append("\n EnabledOnClient=\"" + ctrlDeluxeCalendar.EnabledOnClient.ToString() + "\" ");
			}
			if (!ckbAutoComplete.Checked)
			{
				strbInfo.Append("\n AutoComplete=\"" + ctrlDeluxeCalendar.AutoComplete.ToString() + "\" ");
			}
			if (!ckbIsComplexHeader.Checked)
			{
				strbInfo.Append("\n IsComplexHeader=\"" + ctrlDeluxeCalendar.IsComplexHeader.ToString() + "\" ");
			}
			if (!ckbIsOnlyCurrentMonth.Checked)
			{
				strbInfo.Append("\n IsOnlyCurrentMonth=\"" + ctrlDeluxeCalendar.IsOnlyCurrentMonth.ToString() + "\" ");
			}
			if (!ckbIsValidValue.Checked)
			{
				strbInfo.Append("\n IsValidValue=\"" + ctrlDeluxeCalendar.IsValidValue.ToString() + "\" ");
			}
			if (!ckbAnimated.Checked)
			{
				strbInfo.Append("\n Animated=\"" + ctrlDeluxeCalendar.Animated.ToString() + "\" ");
			}

			strbInfo.Append("\n AutoCompleteValue=\"" + ctrlDeluxeCalendar.AutoCompleteValue.ToString() + "\" ");
			strbInfo.Append("\n CurrentMessageError=\"" + ctrlDeluxeCalendar.CurrentMessageError.ToString() + "\" ");
			strbInfo.Append("\n PromptCharacter=\"" + ctrlDeluxeCalendar.PromptCharacter.ToString() + "\" ");

			if (ddlFirstDayOfWeek.Text != "ChooseSomeDay")
			{
				strbInfo.Append("\n FirstDayOfWeek=\"" + ctrlDeluxeCalendar.FirstDayOfWeek.ToString() + "\" ");
			}

			if (ddlOnInvalidCssClass.Text != "Default")
			{
				strbInfo.Append("\n OnInvalidCssClass=\"" + ctrlDeluxeCalendar.OnInvalidCssClass.ToString() + "\" ");
			}
			if (ddlOnFocusCssClass.Text != "Default")
			{
				strbInfo.Append("\n OnFocusCssClass=\"" + ctrlDeluxeCalendar.OnFocusCssClass.ToString() + "\" ");
			}
			if (ddlImageCssClass.Text != "Default")
			{
				strbInfo.Append("\n ImageCssClass=\"" + ctrlDeluxeCalendar.ImageCssClass.ToString() + "\" ");
			}
			if (ddlTextCssClass.Text != "Default")
			{
				strbInfo.Append("\n TextCssClass=\"" + ctrlDeluxeCalendar.TextCssClass.ToString() + "\" ");
			}
			if (ddlImageUrl.Text != "Default")
			{
				strbInfo.Append("\n ImageUrl=\"" + ctrlDeluxeCalendar.ImageUrl.ToString() + "\" ");
			}
			if (ddlImageStyle.Text != "Default")
			{
				strbInfo.Append("\n ImageStyle=\"" + ctrlDeluxeCalendar.ImageStyle.ToString() + "\" ");
			}
			if (ddlCssClass.Text != "Default")
			{
				strbInfo.Append("\n CssClass=\"" + ctrlDeluxeCalendar.CssClass.ToString() + "\" ");
			}

			strbInfo.Append("\n/>");

			return strbInfo.ToString();
		}


		protected void btnSetProperties_Click(object sender, EventArgs e)
		{

			ctrlDeluxeCalendar.EnabledOnClient = ckbEnabledOnClient.Checked;
			ctrlDeluxeCalendar.AutoComplete = ckbAutoComplete.Checked;
			ctrlDeluxeCalendar.IsComplexHeader = ckbIsComplexHeader.Checked;
			ctrlDeluxeCalendar.IsOnlyCurrentMonth = ckbIsOnlyCurrentMonth.Checked;
			ctrlDeluxeCalendar.IsValidValue = ckbIsValidValue.Checked;
			ctrlDeluxeCalendar.Animated = ckbAnimated.Checked;

			ctrlDeluxeCalendar.AutoCompleteValue = txbAutoCompleteValue.Text;
			ctrlDeluxeCalendar.CurrentMessageError = txbCurrentMessageError.Text;
			ctrlDeluxeCalendar.PromptCharacter = txbPromptCharacter.Text;

			ctrlDeluxeCalendar.FirstDayOfWeek = (FirstDayOfWeek)Enum.Parse(typeof(FirstDayOfWeek), this.ddlFirstDayOfWeek.Text.Trim());
			//if (ddlFirstDayOfWeek.Text != "ChooseSomeDay")
			//{

			//    switch (ddlFirstDayOfWeek.Text)
			//    {
			//        case "Monday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Monday;
			//            break;
			//        case "Thursday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Thursday;
			//            break;
			//        case "Wednesday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Wednesday;
			//            break;
			//        case "Tuesday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Thursday;
			//            break;
			//        case "Friday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Friday;
			//            break;
			//        case "Saturday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Saturday;
			//            break;
			//        case "Sunday":
			//            ctrlDeluxeCalendar.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Sunday;
			//            break;
			//    }
			//}

			if (ddlOnInvalidCssClass.Text != "Default")
			{
				ctrlDeluxeCalendar.OnInvalidCssClass = ddlOnInvalidCssClass.Text;
			}
			if (ddlOnFocusCssClass.Text != "Default")
			{
				ctrlDeluxeCalendar.OnFocusCssClass = ddlOnFocusCssClass.Text;
			}
			if (ddlImageCssClass.Text != "Default")
			{
				ctrlDeluxeCalendar.ImageCssClass = ddlImageCssClass.Text;
			}
			if (ddlTextCssClass.Text != "Default")
			{
				ctrlDeluxeCalendar.TextCssClass = ddlTextCssClass.Text;
			}
			if (ddlImageUrl.Text != "Default")
			{
				ctrlDeluxeCalendar.ImageUrl = ddlImageUrl.Text;
			}
			else
			{
				
			}
			if (ddlImageStyle.Text != "Default")
			{
				ctrlDeluxeCalendar.ImageStyle = ddlImageStyle.Text;
			}
			if (ddlCssClass.Text != "Default")
			{
				ctrlDeluxeCalendar.CssClass = ddlCssClass.Text;
			}


			ctrlDeluxeCalendarAttributeShow.Value = this.BuildControlInfo();

			if(this.ckb_ClientScript.Checked)
			{
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientDateSelectionChanged", @"<script>function funcOnClientDateSelectionChanged(){$get('clientEvent').innerHTML += 'Event: OnClientDateSelectionChanged &nbsp;&nbsp;&nbsp;'} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHidden", @"<script>function funcOnClientHidden(){$get('clientEvent').innerHTML += 'Event: OnClientHidden &nbsp;&nbsp;&nbsp;';} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHiding", @"<script>function funcOnClientHiding(){$get('clientEvent').innerHTML += 'Event OnClientHiding &nbsp;&nbsp;&nbsp;';} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShowing", @"<script>function funcOnClientShowing(){$get('clientEvent').innerHTML += 'Event: OnClientShowing &nbsp;&nbsp;&nbsp;';} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShown", @"<script>function funcOnClientShown(){$get('clientEvent').innerHTML += 'Event: OnClientShown &nbsp;&nbsp;&nbsp;';} </script>", false);
				ctrlDeluxeCalendar.OnClientDateSelectionChanged = "funcOnClientDateSelectionChanged";
				ctrlDeluxeCalendar.OnClientHidden = "funcOnClientHidden";
				ctrlDeluxeCalendar.OnClientHiding = "funcOnClientHiding";
				ctrlDeluxeCalendar.OnClientShowing = "funcOnClientShowing";
				ctrlDeluxeCalendar.OnClientShown = "funcOnClientShown";
			}
		}

		protected void btnGetValue_Click(object sender, EventArgs e)
		{
			txbGetValue.Text = ctrlDeluxeCalendar.CValue.ToString();
			txbSetValue.Text = txbGetValue.Text;

		}

		protected void btnSetValue_Click(object sender, EventArgs e)
		{
			ctrlDeluxeCalendar.CValue = txbSetValue.Text;
		}



	}

}
