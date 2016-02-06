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
    public partial class DateTimeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                
            }
        }
        protected override void OnInit(EventArgs e)
        {

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShowing", @"function funcOnClientShowing(){alert('Event OnClientShowing');} ",true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientShown", @"function funcOnClientShown(){alert('Event OnClientShown');} ",true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHiding", @"function funcOnClientHiding(){alert('Event OnClientHiding');}",true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientHidden", @"function funcOnClientHidden(){alert('Event OnClientHidden');} ",true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "funcOnClientDateSelectionChanged", @"function funcOnClientDateSelectionChanged(){alert('Event OnClientDateSelectionChanged');} ",true);
            //dateTime.OnClientShowing = "funcOnClientShowing";
            //dateTime.OnClientShown = "funcOnClientShown";
            //dateTime.OnClientHiding = "funcOnClientHiding";
            //dateTime.OnClientHidden = "funcOnClientHidden";
            //dateTime.OnClientDateSelectionChanged = "funcOnClientDateSelectionChanged";
            base.OnInit(e);
        }

        private string BuildControlInfo()
        {
            StringBuilder strbInfo = new StringBuilder(512);

            strbInfo.Append("<cc1:DateTimeControl ID=\"dateTime\" runat=\"server\"\n");

            strbInfo.Append(" Enabled=\"" + dateTime.Enabled.ToString() + "\"");
            strbInfo.Append(" ReadOnly=\"" + dateTime.ReadOnly.ToString() + "\"\n");

            //日期部分
            strbInfo.Append(" IsValidDateValue=\"" + dateTime.IsValidDateValue.ToString() + "\"\n");
            strbInfo.Append(" DateAutoComplete=\"" + dateTime.DateAutoComplete.ToString() + "\"\n");
            if (ddlDateAutoCompleteValue.Text != "SystemDate")
            {
                strbInfo.Append(" DateAutoCompleteValue=\"" + dateTime.DateAutoCompleteValue.ToString() + "\"\n");
            }
            strbInfo.Append(" DatePromptCharacter=\"" + dateTime.DatePromptCharacter.ToString() + "\"\n");
            strbInfo.Append(" DateCurrentMessageError=\"" + dateTime.DateCurrentMessageError.ToString() + "\"\n");
            if (ddlDateTextStyle.Text != "Default")
            {
                //strbInfo.Append(" DateTextStyle=\"" + dateTime.DateTextStyle.ToString() + "\"\n");
            }
            if (ddlDateTextCssClass.Text != "Default")
            {
                strbInfo.Append(" DateTextCssClass=\"" + dateTime.DateTextCssClass.ToString() + "\"\n");
            }
            if (ddlOnFocusDateCssClass.Text != "Default")
            {
                strbInfo.Append(" OnFocusDateCssClass=\"" + dateTime.OnFocusDateCssClass.ToString() + "\"\n");
            }
            if (ddlOnInvalidDateCssClass.Text != "Default")
            {
                strbInfo.Append(" OnInvalidDateCssClass=\"" + dateTime.OnInvalidDateCssClass.ToString() + "\"\n");
            }
            //-----------------------------------------------------------------
            strbInfo.Append(" EnabledOnClient=\"" + dateTime.EnabledOnClient.ToString() + "\"\n");
            strbInfo.Append(" IsComplexHeader=\"" + dateTime.IsComplexHeader.ToString() + "\"\n");
            strbInfo.Append(" IsOnlyCurrentMonth=\"" + dateTime.IsOnlyCurrentMonth.ToString() + "\"\n");
            strbInfo.Append(" FirstDayOfWeek=\"" + dateTime.FirstDayOfWeek.ToString() + "\"\n");
            if (ddlPopupCalendarCssClass.Text != "Default")
            {
                strbInfo.Append(" PopupCalendarCssClass=\"" + dateTime.PopupCalendarCssClass.ToString() + "\"\n");
            }
            if (ddlDateImageCssClass.Text != "Default")
            {
                strbInfo.Append(" DateImageCssClass=\"" + dateTime.DateImageCssClass.ToString() + "\"\n");
            }
            if (ddlDateImageStyle.Text != "Default")
            {
                strbInfo.Append(" DateImageStyle=\"" + dateTime.DateImageStyle.ToString() + "\"\n");
            }
            if (ddlDateImageUrl.Text != "Default")
            {
                strbInfo.Append(" DateImageUrl=\"" + dateTime.DateImageUrl.ToString() + "\"\n");
            }
            
            //时间部分
            strbInfo.Append(" IsValidTimeValue=\"" + dateTime.IsValidTimeValue.ToString() + "\"\n");
            strbInfo.Append(" TimeAutoComplete=\"" + dateTime.TimeAutoComplete.ToString() + "\"\n");
            if (ddlTimeAutoCompleteValue.Text != "SystemTime")
            {
                strbInfo.Append(" TimeAutoCompleteValue=\"" + dateTime.TimeAutoCompleteValue.ToString() + "\"\n");
            }
            strbInfo.Append(" TimePromptCharacter=\"" + dateTime.TimePromptCharacter.ToString() + "\"\n");
            strbInfo.Append(" TimeCurrentMessageError=\"" + dateTime.TimeCurrentMessageError + "\"\n");
            if (ddlTimeTextStyle.Text != "Default")
            {
                //strbInfo.Append(" TimeTextStyle=\"" + dateTime.TimeTextStyle.ToString() + "\"\n");
            }
            if (ddlTimeTextCss.Text != "Default")
            {
                //strbInfo.Append(" TimeTextCss=\"" + dateTime.TimeTextCss.ToString() + "\"\n");
            }
            if (ddlOnTimeFocusCssClass.Text != "Default")
            {
                strbInfo.Append(" OnTimeFocusCssClass=\"" + dateTime.OnTimeFocusCssClass.ToString() + "\"\n");
            }
            if (ddlOnTimeInvalidCssClass.Text != "Default")
            {
                strbInfo.Append(" OnTimeInvalidCssClass=\"" + dateTime.OnTimeInvalidCssClass.ToString() + "\"\n");
            }
            //-----------------------------------------------------------------
            strbInfo.Append(" ShowButton=\"" + dateTime.ShowButton.ToString() + "\"\n");
            strbInfo.Append(" TimeMask=\"" + dateTime.TimeMask.ToString() + "\"");            

            strbInfo.Append("/>");

            return strbInfo.ToString();
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            //dateTime.ReadOnlyFormat;                      ->      //dateTime.ReadOnly;
            //dateTime.EnabledOnClient;
            //dateTime.IsComplexHeader;
            //dateTime.IsOnlyCurrentMonth;
            //dateTime.FirstDayOfWeek;
            //dateTime.Format;                              ->delete
            //dateTime.CssClass;                            ->      //dateTime.PopupCalendarCssClass;
            //dateTime.DateTextStyle;
            //dateTime.DateTextCss;                         ->      //dateTime.DateTextCssClass;
            //dateTime.IsValidValue;                        ->      //dateTime.IsValidTimeValue;
            //dateTime.AutoComplete;                        ->      //dateTime.TimeAutoComplete;
            //dateTime.IsShowBtn;                           ->      //dateTime.ShowButton;
            //dateTime.ButtonText;                          ->delete
            //dateTime.ButtonCss;                           ->delete
            //dateTime.Mask;                                ->      //dateTime.TimeMask;
            //dateTime.CurrentMessageError;                 ->      //dateTime.TimeCurrentMessageError;
            //dateTime.TimeTextStyle;
            //dateTime.TimeTextClass;                       ->      //dateTime.TimeTextCss;
            //dateTime.OnFocusCssClass;                     ->      //dateTime.OnTimeFocusCssClass;
            //dateTime.OnInvalidCssClass;                   ->      //dateTime.OnTimeInvalidCssClass;
            //dateTime.DataArrayList;                       ->      //dateTime.DataSource;
            //dateTime.OnClientDateSelectionChanged;
            //dateTime.OnClientHidden;
            //dateTime.OnClientHiding;
            //dateTime.OnClientShowing;
            //dateTime.OnClientShown;
            //dateTime.Value;
            
            //-------------------------------------------------------------------------------//
            //dateTime.ReadOnly;

            //dateTime.IsValidDateValue;                new
            //dateTime.DateAutoComplete;                new
            //dateTime.DateAutoCompleteValue;           new
            //dateTime.DatePromptCharacter;             new
            //dateTime.DateCurrentMessageError;         new
            //dateTime.DateTextStyle;
            //dateTime.DateTextCssClass;
            //dateTime.OnFocusDateCssClass;             new
            //dateTime.OnInvalidDateCssClass;           new
            //--------
            //dateTime.EnabledOnClient;
            //dateTime.IsComplexHeader;
            //dateTime.IsOnlyCurrentMonth;
            //dateTime.FirstDayOfWeek;
            //dateTime.PopupCalendarCssClass;
            //dateTime.DateImageStyle;                  new
            //dateTime.DateImageCssClass;               new
            //dateTime.DateImageUrl;                    new
            
            //dateTime.IsValidTimeValue;
            //dateTime.TimeAutoComplete;
            //dateTime.TimeAutoCompleteValue;           new
            //dateTime.TimePromptCharacter;             new
            //dateTime.TimeCurrentMessageError;
            //dateTime.TimeTextStyle;
            //dateTime.TimeTextCss;
            //dateTime.OnTimeFocusCssClass;
            //dateTime.OnTimeInvalidCssClass;
            //--------
            //dateTime.ShowButton;
            //dateTime.TimeMask;
            //dateTime.DataSource;
                        
            //dateTime.Value;

            //dateTime.OnClientDateSelectionChanged;
            //dateTime.OnClientHidden;
            //dateTime.OnClientHiding;
            //dateTime.OnClientShowing;
            //dateTime.OnClientShown;
            //-------------------------------------------------------------------------------//

            dateTime.Enabled = ckbEnabled.Checked;
            dateTime.ReadOnly = ckbReadOnly.Checked;

            //日期部分属性赋值
            dateTime.IsValidDateValue = ckbIsValidDateValue.Checked;
            dateTime.DateAutoComplete = ckbDateAutoComplete.Checked;
            if (ddlDateAutoCompleteValue.Text != "SystemDate")
            {
                dateTime.DateAutoCompleteValue = ddlDateAutoCompleteValue.Text;
            }
            dateTime.DatePromptCharacter = txbDatePromptCharacter.Text;
            dateTime.DateCurrentMessageError = ddlDateCurrentMessageError.Text;
            if (ddlDateTextStyle.Text != "Default")
            {
                //dateTime.DateTextStyle = ddlDateTextStyle.Text;
            }
            if (ddlDateTextCssClass.Text != "Default")
            {
                dateTime.DateTextCssClass = ddlDateTextCssClass.Text;
            }
            if (ddlOnFocusDateCssClass.Text != "Default")
            {
                dateTime.OnFocusDateCssClass = ddlOnFocusDateCssClass.Text;
            }
            if (ddlOnInvalidDateCssClass.Text != "Default")
            {
                dateTime.OnInvalidDateCssClass = ddlOnInvalidDateCssClass.Text;
            }
            //-----------------------------------------------------------------
            dateTime.EnabledOnClient = ckbEnabledOnClient.Checked;
            dateTime.IsComplexHeader = ckbIsComplexHeader.Checked;
            dateTime.IsOnlyCurrentMonth = ckbIsOnlyCurrentMonth.Checked;
            dateTime.FirstDayOfWeek = (FirstDayOfWeek)Enum.Parse(typeof(FirstDayOfWeek), this.ddlFirstDayOfWeek.Text);
            if (ddlPopupCalendarCssClass.Text != "Default")
            {
                dateTime.PopupCalendarCssClass = ddlPopupCalendarCssClass.Text;
            }
            if (ddlDateImageStyle.Text != "Default")
            {
                dateTime.DateImageStyle = ddlDateImageStyle.Text;
            }if (ddlDateImageCssClass.Text != "Default")
            {
                dateTime.DateImageCssClass = ddlDateImageCssClass.Text;
            }

            if (ddlDateImageUrl.Text == "Default")
            {
            //    //dateTime.DateImageUrl = "mvwres:1-MCS.Web.WebControls.Calendar.Images.datePicker.gif,MCS.Web.WebControls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
            //    dateTime.DateImageUrl = "MCS.Web.WebControls/Calendar/Images/arrow-left.gif";
            //}
            //else
            //{
            //    dateTime.DateImageUrl = "mvwres:1-MCS.Web.WebControls.Calendar.Images." + ddlDateImageUrl.Text
            //        + ".gif,MCS.Web.WebControls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
            }
            //以上是日期部分属性赋值

            //时间部分属性赋值
            dateTime.IsValidTimeValue = ckbIsValidTimeValue.Checked;
            dateTime.TimeAutoComplete = ckbTimeAutoComplete.Checked;
            if (ddlTimeAutoCompleteValue.Text != "SystemTime")
            {
                dateTime.TimeAutoCompleteValue = ddlTimeAutoCompleteValue.Text;
            }
            dateTime.TimePromptCharacter = txbTimePromptCharacter.Text;
            dateTime.TimeCurrentMessageError = ddlTimeCurrentMessageError.Text;
            if (ddlTimeTextStyle.Text != "Default")
            {
                //dateTime.TimeTextStyle = ddlTimeTextStyle.Text;
            }
            if (ddlTimeTextCss.Text != "Default")
            {
                //dateTime.TimeTextCss = ddlTimeTextCss.Text;
            }
            if (ddlOnTimeFocusCssClass.Text != "Default")
            {
                dateTime.OnTimeFocusCssClass = ddlOnTimeFocusCssClass.Text;
            }
            if (ddlOnTimeInvalidCssClass.Text != "Default")
            {
                dateTime.OnTimeInvalidCssClass = ddlOnTimeInvalidCssClass.Text;
            }
            //-----------------------------------------------------------------
            dateTime.ShowButton = ckbShowButton.Checked;
            dateTime.TimeMask = ddlTimeMask.SelectedItem.Text;
            //以上是时间部分属性赋值

            dateTimeHtmlShow.Value = this.BuildControlInfo();

        }
        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            txbGetValue.Text = dateTime.Value.ToString();
            txbSetValue.Text = txbGetValue.Text;
        }
        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            dateTime.Value = System.DateTime.Parse(txbSetValue.Text);
        }

        protected void btnSetDataSource_Click(object sender, EventArgs e)
        {
            ListItem litmTrans = new ListItem();
            litmTrans.Text = txbText.Text;
            litmTrans.Value = txbValue.Text;
            dateTime.DataSource.Add(litmTrans);
        }
    }
}
