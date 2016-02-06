<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.PopupCalendar.CalendarTest"  %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Calendar Test</title>
 <link rel="stylesheet" type="text/css" id="css" href="CalendarControl.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
		</asp:ScriptManager>
    <div style="text-align:center">
		<asp:CheckBox ID="ckb_EnabledOnClient" runat="server" TextAlign="Left" Text="EnabledOnClient" Checked="True" />
        <br />
        <br />
        <asp:CheckBox ID="ckb_IsComplexHeader" runat="server" TextAlign="Left" Text="IsComplexHeader" Checked="True" />
        <br />
        <br />
        <asp:CheckBox ID="ckbIsOnlyCurrentMonth" runat="server" TextAlign="Left" Text="IsOnlyCurrentMonth" Checked="True" />
        <br />
        <br />
        FirstDayOfWeek:
        <asp:DropDownList ID="ddl_FirstDayOfWeek" runat="server">
            <asp:ListItem>Default</asp:ListItem>
            <asp:ListItem>Friday</asp:ListItem>
            <asp:ListItem>Monday</asp:ListItem>
            <asp:ListItem>Saturday</asp:ListItem>
            <asp:ListItem>Sunday</asp:ListItem>
            <asp:ListItem>Thursday</asp:ListItem>
            <asp:ListItem>Tuesday</asp:ListItem>
            <asp:ListItem>Wednesday</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        Format:
        <asp:DropDownList ID="ddl_Format" runat="server">
            <asp:ListItem>yyyy-MM-dd</asp:ListItem>
            <asp:ListItem>yyyy/MM/dd</asp:ListItem>
            <asp:ListItem>MMMM d, yyyy</asp:ListItem>                        
            <asp:ListItem>yyyyÄêMMÔÂdd</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        CssClass:
        <asp:DropDownList ID="ddl_CssClass" runat="server">
            <asp:ListItem>Default</asp:ListItem>
            <asp:ListItem>CssClass_Demo1</asp:ListItem>
            <asp:ListItem>CssClass_Demo2</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        TextStyle
        <asp:DropDownList ID="ddlTextStyle" runat="server">
            <asp:ListItem>width: 100px; background-color:Aqua; text-align:left</asp:ListItem>
            <asp:ListItem>width: 200px; font-size:larger; text-align:center</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        TextCss
        <asp:DropDownList ID="ddlTextCss" runat="server">
            <asp:ListItem>Default</asp:ListItem>
            <asp:ListItem>TextCss_Demo1</asp:ListItem>
            <asp:ListItem>TextCss_Demo2</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        
        <asp:Button ID="run" runat="server" Text="Run" OnClick="run_Click" />
        <br />
        <br />
        <cc1:DeluxeCalendar ID="calendar" runat="server" />
		<br />
        <br />
        <asp:TextBox ID="txbGetValue" runat="server" Enabled="false"></asp:TextBox>
        <asp:Button ID="getValue" runat="server" Text="get value" OnClick="getValue_Click" />
        <br />
        <br />
        <asp:TextBox ID="txbSetValue" runat="server"></asp:TextBox>
        <asp:Button ID="setValue" runat="server" Text="set value" OnClick="setValue_Click" />       
        <br />
        <br />
        <textarea id="calendarHtmlShow" runat="server" style="width: 800px; height: 80px;" enableviewstate="true"></textarea>
        </div>
    </form>
</body>
</html>
