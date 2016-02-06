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

namespace MCS.Web.WebControls.Test.DateTimeControl
{
    public partial class DeluxeDateTimeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ctrlDeluxeDateTime.Animated;    //设置日历月份转换的动画效果
            //ctrlDeluxeDateTime.DateAutoComplete;    //是否自动补齐日期
            //ctrlDeluxeDateTime.EnabledOnClient;     //是否启用日历功能
            //ctrlDeluxeDateTime.IsComplexHeader;     //是否提供下拉框快捷选项
            //ctrlDeluxeDateTime.IsOnlyCurrentMonth;    //是否只显示当月
            //ctrlDeluxeDateTime.IsValidDateValue;    //是否启用日期验证
            //ctrlDeluxeDateTime.IsValidTimeValue;    //是否启用时间验证
            //ctrlDeluxeDateTime.ReadOnly;    //是否只读
            //ctrlDeluxeDateTime.ShowButton;      //是否提供按钮来选择自定义时间列表,若是则需设置数据源
            //ctrlDeluxeDateTime.TimeAutoComplete;    //是否自动补齐时间


            //ctrlDeluxeDateTime.DateImageCssClass;   //图片的CssClass
            //ctrlDeluxeDateTime.DateImageStyle;  //图片的Style
            //ctrlDeluxeDateTime.DateImageUrl;    //按钮图片的Src
            //ctrlDeluxeDateTime.DateTextCssClass;    //日历输入框的Css
            //ctrlDeluxeDateTime.TimeTextCssClass;    //时间输入框的Css

            //ctrlDeluxeDateTime.DateAutoCompleteValue;   //提供自动补齐的时间串，不设置则取系统日期
            //ctrlDeluxeDateTime.DateCurrentMessageError;     //验证日期的提示信息
            //ctrlDeluxeDateTime.DatePromptCharacter;     //日期掩码字符
            //ctrlDeluxeDateTime.TimePromptCharacter;     //掩码字符
            //ctrlDeluxeDateTime.TimeMask;    //时间的分隔符
            //ctrlDeluxeDateTime.TimeAutoCompleteValue;   //提供自动补齐的时间串，不设置则取系统时间
            //ctrlDeluxeDateTime.TimeCurrentMessageError; //验证时间的提示信息

            //ctrlDeluxeDateTime.OnClientDateSelectionChanged;    //日期选择变化后触发的客户端事件
            //ctrlDeluxeDateTime.OnClientHidden;  //隐藏日历后触发的客户端事件
            //ctrlDeluxeDateTime.OnClientHiding;  //隐藏日历时触发的客户端事件
            //ctrlDeluxeDateTime.OnClientShowing;     //弹出日历时触发的客户端事件
            //ctrlDeluxeDateTime.OnClientShown;   //弹出日历后触发的客户端事件

            //ctrlDeluxeDateTime.Value;   //日期数据

            //ctrlDeluxeDateTime.OnFocusDateCssClass;     //日期得到焦点时文本框的样式
            //ctrlDeluxeDateTime.OnInvalidDateCssClass;   //日期验证不通过时文本框的样式
            //ctrlDeluxeDateTime.OnTimeFocusCssClass;     //得到焦点时时间文本框的样式
            //ctrlDeluxeDateTime.OnTimeInvalidCssClass;   //验证不通过时时间文本框的样式
            //ctrlDeluxeDateTime.PopupCalendarCssClass;   //日历的样式，不填为默认样式


            //ctrlDeluxeDateTime.DataSource;      //绑定时间的数据源

            //ctrlDeluxeDateTime.DateTextStyle;   //日历的Style    //已经被灭了
            //ctrlDeluxeDateTime.TimeTextStyle;   //时间输入框的Style   //也被灭了

            //ctrlDeluxeDateTime.FirstDayOfWeek;  //自定义第一列是从周几开始

            
        }

        protected override void OnInit(EventArgs e)
        {
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientDateSelectionChanged", @"<script>function funcOnClientDateSelectionChanged(){alert('Event OnClientDateSelectionChanged');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHidden", @"<script>function funcOnClientHidden(){alert('Event OnClientHidden');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHiding", @"<script>function funcOnClientHiding(){alert('Event OnClientHiding');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShowing", @"<script>function funcOnClientShowing(){alert('Event OnClientShowing');} </script>", false);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShown", @"<script>function funcOnClientShown(){alert('Event OnClientShown');} </script>", false);
            //ctrlDeluxeDateTime.OnClientDateSelectionChanged = "funcOnClientDateSelectionChanged";
            //ctrlDeluxeDateTime.OnClientHidden = "funcOnClientHidden";
            //ctrlDeluxeDateTime.OnClientHiding = "funcOnClientHiding";
            //ctrlDeluxeDateTime.OnClientShowing = "funcOnClientShowing";
            //ctrlDeluxeDateTime.OnClientShown = "funcOnClientShown";
            base.OnInit(e);
        }

        protected void btnSetDataSource_Click(object sender, EventArgs e)
        {
            ListItem lsitOption=new ListItem();
            lsitOption.Text = txbDataSource1.Text;
            ctrlDeluxeDateTime.DataSource.Add(lsitOption);
        }


        private string BuildControlInfo()
        {
            StringBuilder strbInfo = new StringBuilder(512);

            strbInfo.Append("<cc1:DeluxeDateTime ID=\"ctrlDeluxeDateTime\"\n runat=\"server\"");

            strbInfo.Append("\n ReadOnly=\"" + ctrlDeluxeDateTime.ReadOnly.ToString() + "\"");
            //strbInfo.Append(" Value=\"" + ctrlDeluxeDateTime.Value.ToString() + "\"\n");

            //日期部分
            strbInfo.Append("\n DateAutoComplete=\"" + ctrlDeluxeDateTime.DateAutoComplete.ToString() + "\"");
            strbInfo.Append("\n IsValidDateValue=\"" + ctrlDeluxeDateTime.IsValidDateValue.ToString() + "\"");

            if (ddlDateTextCssClass.Text != "Default")
            {
                strbInfo.Append("\n DateTextCssClass=\"" + ctrlDeluxeDateTime.DateTextCssClass.ToString() + "\"");
            }
            if (txbDateAutoCompleteValue.Text != string.Empty)
            {
                strbInfo.Append("\n DateAutoCompleteValue=\"" + ctrlDeluxeDateTime.DateAutoCompleteValue.ToString() + "\"");
            }
            strbInfo.Append("\n DateCurrentMessageError=\"" + ctrlDeluxeDateTime.DateCurrentMessageError.ToString() + "\"");
            strbInfo.Append("\n DatePromptCharacter=\"" + ctrlDeluxeDateTime.DatePromptCharacter.ToString() + "\"");

            if (ddlOnFocusDateCssClass.Text != "Default")
            {
                strbInfo.Append("\n OnFocusDateCssClass=\"" + ctrlDeluxeDateTime.OnFocusDateCssClass.ToString() + "\"");
            }
            if (ddlOnInvalidDateCssClass.Text != "Default")
            {
                strbInfo.Append("\n OnInvalidDateCssClass=\"" + ctrlDeluxeDateTime.OnInvalidDateCssClass.ToString() + "\"");
            }
           
            //------------------------------------------------------------------------------------------------------

            strbInfo.Append("\n Animated=\"" + ctrlDeluxeDateTime.Animated.ToString() + "\"");
            strbInfo.Append("\n EnabledOnClient=\"" + ctrlDeluxeDateTime.EnabledOnClient.ToString() + "\"");
            strbInfo.Append("\n IsOnlyCurrentMonth=\"" + ctrlDeluxeDateTime.IsOnlyCurrentMonth.ToString() + "\"");
            strbInfo.Append("\n IsComplexHeader=\"" + ctrlDeluxeDateTime.IsComplexHeader.ToString() + "\"");

            if (ddlDateTextCssClass.Text != "Default")
            {
                strbInfo.Append("\n DateImageCssClass=\"" + ctrlDeluxeDateTime.DateImageCssClass.ToString() + "\"");
            }
            if (ddlDateImageStyle.Text != "Default")
            {
                strbInfo.Append("\n DateImageStyle=\"" + ctrlDeluxeDateTime.DateImageStyle.ToString() + "\"");
            }
           if (ddlDateImageUrl.Text != "Default")
            {
                strbInfo.Append("\n DateImageUrl=\"" + ctrlDeluxeDateTime.DateImageUrl.ToString() + "\"");
            }
            if (ddlOnTimeInvalidCssClass.Text != "Default")
            {
                strbInfo.Append("\n OnTimeInvalidCssClass=\"" + ctrlDeluxeDateTime.OnTimeInvalidCssClass.ToString() + "\"");
            }
            if (ddlPopupCalendarCssClass.Text != "Default")
            {
                strbInfo.Append("\n PopupCalendarCssClass=\"" + ctrlDeluxeDateTime.PopupCalendarCssClass.ToString() + "\"");
            }
            if (ddlDateImageCssClass.Text != "Default")
            {
                strbInfo.Append("\n DateImageCssClass=\"" + ctrlDeluxeDateTime.DateImageCssClass.ToString() + "\"");
            }
           


            //时间部分
            strbInfo.Append("\n TimeAutoComplete=\"" + ctrlDeluxeDateTime.TimeAutoComplete.ToString() + "\"");
            strbInfo.Append("\n IsValidTimeValue=\"" + ctrlDeluxeDateTime.IsValidTimeValue.ToString() + "\"");

            if (ddlTimeTextCssClass.Text != "Default")
            {
                strbInfo.Append("\n TimeTextCssClass=\"" + ctrlDeluxeDateTime.TimeTextCssClass.ToString() + "\"");
            }
            if (txbTimeAutoCompleteValue.Text != string.Empty)
            {
                strbInfo.Append("\n TimeAutoCompleteValue=\"" + ctrlDeluxeDateTime.TimeAutoCompleteValue.ToString() + "\"");
            }
            strbInfo.Append("\n TimeCurrentMessageError=\"" + ctrlDeluxeDateTime.TimeCurrentMessageError.ToString() + "\"");
            strbInfo.Append("\n TimePromptCharacter=\"" + ctrlDeluxeDateTime.TimePromptCharacter.ToString() + "\"");

            if (ddlOnTimeFocusCssClass.Text != "Default")
            {
                strbInfo.Append("\n OnTimeFocusCssClass=\"" + ctrlDeluxeDateTime.OnTimeFocusCssClass.ToString() + "\"");
            }
            if (ddlOnTimeInvalidCssClass.Text != "Default")
            {
                strbInfo.Append("\n OnTimeInvalidCssClass=\"" + ctrlDeluxeDateTime.OnTimeInvalidCssClass.ToString() + "\"");
            }
           
            //-----------------------------------------------------------------------------------------------------

            strbInfo.Append("\n ShowButton=\"" + ctrlDeluxeDateTime.ShowButton.ToString() + "\"");

            strbInfo.Append("\n TimeMask=\"" + ctrlDeluxeDateTime.TimeMask.ToString() + "\"");

            ctrlDeluxeDateTime.FirstDayOfWeek = (FirstDayOfWeek)Enum.Parse(typeof(FirstDayOfWeek), this.ddlFirstDayOfWeek.Text.Trim());

            strbInfo.Append("\n>");

            return strbInfo.ToString();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
    
            ctrlDeluxeDateTime.ReadOnly = ckbReadOnly.Checked;
            //ctrlDeluxeDateTime.Value = Convert.ToDateTime(txbValue.Text.Trim());

            //日期部分赋值
            ctrlDeluxeDateTime.DateAutoComplete = ckbDateAutoComplete.Checked;
            ctrlDeluxeDateTime.IsValidDateValue = ckbIsValidDateValue.Checked;

            if (ddlDateTextCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.DateTextCssClass = ddlDateTextCssClass.Text;
            }

            ctrlDeluxeDateTime.DateAutoCompleteValue = txbDateAutoCompleteValue.Text;
            ctrlDeluxeDateTime.DateCurrentMessageError = txbDateCurrentMessageError.Text;
            ctrlDeluxeDateTime.DatePromptCharacter = txbDatePromptCharacter.Text;

            if (ddlOnFocusDateCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.OnFocusDateCssClass = ddlOnFocusDateCssClass.Text;
            }
            if (ddlOnInvalidDateCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.OnInvalidDateCssClass = ddlOnInvalidDateCssClass.Text;
            }

            //------------------------------------------------------------------------
            ctrlDeluxeDateTime.Animated = ckbAnimated.Checked;
            ctrlDeluxeDateTime.EnabledOnClient = ckbEnabledOnClient.Checked;
            ctrlDeluxeDateTime.IsOnlyCurrentMonth = ckbIsOnlyCurrentMonth.Checked;
            ctrlDeluxeDateTime.IsComplexHeader = ckbIsComplexHeader.Checked;

            if (ddlDateImageCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.DateImageCssClass = ddlDateImageCssClass.Text;
            }
            if (ddlDateImageStyle.Text != "Default")
            {
                ctrlDeluxeDateTime.DateImageStyle = ddlDateImageStyle.Text;
            }
            if (ddlDateImageUrl.Text != "Default")
            {
                ctrlDeluxeDateTime.DateImageUrl = ddlDateImageUrl.Text;
            }
            if (ddlOnTimeInvalidCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.OnTimeInvalidCssClass = ddlOnTimeInvalidCssClass.Text;
            }
            if (ddlPopupCalendarCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.PopupCalendarCssClass = ddlPopupCalendarCssClass.Text;
            }


            //时间部分赋值
            ctrlDeluxeDateTime.TimeAutoComplete = ckbTimeAutoComplete.Checked;
            ctrlDeluxeDateTime.IsValidTimeValue = ckbIsValidTimeValue.Checked;
            if (ddlTimeTextCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.TimeTextCssClass = ddlTimeTextCssClass.Text;
            }

            //if (txbTimeAutoCompleteValue.Text != string.Empty)
            //{
                ctrlDeluxeDateTime.TimeAutoCompleteValue = txbTimeAutoCompleteValue.Text;
            //}
            ctrlDeluxeDateTime.TimeCurrentMessageError = txbTimeCurrentMessageError.Text;
            ctrlDeluxeDateTime.TimePromptCharacter = txbTimePromptCharacter.Text;

            if (ddlOnTimeFocusCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.OnTimeFocusCssClass = ddlOnTimeFocusCssClass.Text;
            }
            if (ddlOnTimeInvalidCssClass.Text != "Default")
            {
                ctrlDeluxeDateTime.OnTimeInvalidCssClass = ddlOnTimeInvalidCssClass.Text;
            }

            //-------------------------------------------------------------------------
            ctrlDeluxeDateTime.ShowButton = ckbShowButton.Checked;

            ctrlDeluxeDateTime.TimeMask = txbTimeMask.Text;

            ctrlDeluxeDateTime.FirstDayOfWeek = (FirstDayOfWeek)Enum.Parse(typeof(FirstDayOfWeek), this.ddlFirstDayOfWeek.Text.Trim());
           
            ctrlDeluxeDateTimeAttributeShow.Value = this.BuildControlInfo();
			if(this.ckb_ClientScript.Checked)
			{
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientDateSelectionChanged", @"<script>function funcOnClientDateSelectionChanged(){$get('clientEvent').innerHTML += 'Event: OnClientDateSelectionChanged &nbsp;&nbsp;&nbsp;'} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHidden", @"<script>function funcOnClientHidden(){$get('clientEvent').innerHTML += 'Event: OnClientHidden &nbsp;&nbsp;&nbsp;';} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHiding", @"<script>function funcOnClientHiding(){$get('clientEvent').innerHTML += 'Event OnClientHiding &nbsp;&nbsp;&nbsp;';} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShowing", @"<script>function funcOnClientShowing(){$get('clientEvent').innerHTML += 'Event: OnClientShowing &nbsp;&nbsp;&nbsp;';} </script>", false);
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShown", @"<script>function funcOnClientShown(){$get('clientEvent').innerHTML += 'Event: OnClientShown &nbsp;&nbsp;&nbsp;';} </script>", false);

				ctrlDeluxeDateTime.OnClientDateSelectionChanged = "funcOnClientDateSelectionChanged";
				ctrlDeluxeDateTime.OnClientHidden = "funcOnClientHidden";
				ctrlDeluxeDateTime.OnClientHiding = "funcOnClientHiding";
				ctrlDeluxeDateTime.OnClientShowing = "funcOnClientShowing";
				ctrlDeluxeDateTime.OnClientShown = "funcOnClientShown";
				
			}
        }

        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            txbGetValue.Text = ctrlDeluxeDateTime.Value.ToString();
            txbSetValue.Text = txbGetValue.Text;
        }

        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            ctrlDeluxeDateTime.Value=System.DateTime.Parse(txbSetValue.Text);
        }

       
    


    }
}
