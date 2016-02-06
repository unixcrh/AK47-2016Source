using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls.Test.PopupCalendar
{
    public partial class CalendarTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void run_Click(object sender, EventArgs e)
        {
            //calendar.CssClass;
            //calendar.CValue;
            //calendar.EnabledOnClient;
            //calendar.FirstDayOfWeek;
            //calendar.Format;
            //calendar.IsComplexHeader;
            //calendar.IsOnlyCurrentMonth;
            //calendar.OnClientDateSelectionChanged;
            //calendar.OnClientHidden;
            //calendar.OnClientHiding;
            //calendar.OnClientShowing;
            //calendar.OnClientShown;
            //calendar.TextClass;
            //calendar.TextStyle;

            calendar.EnabledOnClient = ckb_EnabledOnClient.Checked;
            calendar.IsComplexHeader = ckb_IsComplexHeader.Checked;
            calendar.IsOnlyCurrentMonth = ckbIsOnlyCurrentMonth.Checked;
            calendar.FirstDayOfWeek = (FirstDayOfWeek)Enum.Parse(typeof(FirstDayOfWeek), ddl_FirstDayOfWeek.Text);
            //calendar.Format = ddl_Format.Text;
            if (ddl_CssClass.Text != "Default")
            {
                calendar.CssClass = ddl_CssClass.Text;
            }
            //calendar.TextStyle = ddlTextStyle.Text;
            if (ddlTextCss.Text != "Default")
            {
                //calendar.TextClass = ddlTextCss.Text;
                calendar.TextCssClass = ddlTextCss.Text;
            }

            StringBuilder calendrHtml = new StringBuilder(1024);
            calendrHtml.Append(@"<cc1:PopupCalendarControl ID='calendar' runat='server' ");
            if (!ckb_IsComplexHeader.Checked)
            {
                calendrHtml.Append("IsComplexHeader='" + ckb_IsComplexHeader.Checked.ToString() + "' ");
            }
            if (!ckb_EnabledOnClient.Checked)
            {
                calendrHtml.Append("EnabledOnClient='" + ckb_EnabledOnClient.Checked.ToString() + "' ");
            }
            calendrHtml.Append("FirstDayOfWeek='" + ddl_FirstDayOfWeek.Text + "' Format='" + ddl_Format.Text + "' ");
            if (ddl_CssClass.Text != "Default")
            {
                calendrHtml.Append("CssClass='" + ddl_CssClass.Text + "' ");
            }
            calendrHtml.Append(@" />");

            calendarHtmlShow.Value = calendrHtml.ToString();
        }


        protected void getValue_Click(object sender, EventArgs e)
        {
            txbGetValue.Text = calendar.CValue;
        }
        protected void setValue_Click(object sender, EventArgs e)
        {
            calendar.CValue = txbSetValue.Text;
        }
    }
}
