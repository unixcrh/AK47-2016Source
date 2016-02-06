<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaskedEditTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.MaskedEdit.MaskedEditTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="stylesheet" href="maskedEdit.css" type="text/css" />
	<title>无标题页</title>
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<div style="text-align: center">
		<asp:CheckBox ID="chbIsValidValue" runat="server" TextAlign="Left" Text="IsValidValue"
			Checked="True" />
		<br />
		<br />
		<asp:CheckBox ID="chbAutoComplete" runat="server" TextAlign="Left" Text="AutoComplete"
			Checked="True" />
		<br />
		<br />
		<asp:CheckBox ID="chbIsShowBtn" runat="server" TextAlign="Left" Text="IsShowBtn" />
		<br />
		<br />
		ButtonText
		<asp:DropDownList ID="ddlButtonText" runat="server">
			<asp:ListItem>ButtonTextA</asp:ListItem>
			<asp:ListItem>ButtonTextB</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		ButtonCss
		<asp:DropDownList ID="ddlButtonCss" runat="server">
			<asp:ListItem>Default</asp:ListItem>
			<asp:ListItem>ButtonCss_Demo1</asp:ListItem>
			<asp:ListItem>ButtonCss_Demo2</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		Mask
		<asp:DropDownList ID="ddlMask" runat="server">
			<asp:ListItem>99:99</asp:ListItem>
			<asp:ListItem>99:99:99</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		CurrentMessageError
		<asp:DropDownList ID="ddlCurrentMessageError" runat="server">
			<asp:ListItem>输入时间的格式不符合要求</asp:ListItem>
			<asp:ListItem>输入的时间不符合格式要求</asp:ListItem>
			<asp:ListItem>不符合格式要求的时间输入</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		TextStyle
		<asp:DropDownList ID="ddlTextStyle" runat="server">
			<asp:ListItem>Default</asp:ListItem>
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
		OnFocusCssClass
		<asp:DropDownList ID="ddlOnFocusCssClass" runat="server">
			<asp:ListItem>Default</asp:ListItem>
			<asp:ListItem>OnFocusCssClass_Demo1</asp:ListItem>
			<asp:ListItem>OnFocusCssClass_Demo2</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		OnInvalidCssClass
		<asp:DropDownList ID="ddlOnInvalidCssClass" runat="server">
			<asp:ListItem>Default</asp:ListItem>
			<asp:ListItem>OnInvalidCssClass_Demo1</asp:ListItem>
			<asp:ListItem>OnInvalidCssClass_Demo2</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		DataArrayList
		<asp:DropDownList ID="ddlDataArrayList" runat="server">
			<asp:ListItem>"11:11:11", "22:22:22","12:34:56"</asp:ListItem>
			<asp:ListItem>"08:30:00","12:00:00","14:00:00","18:00:00","19:00:00","21:00:00"</asp:ListItem>
		</asp:DropDownList>
		<br />
		<br />
		<asp:Button ID="btnRun" runat="server" Text="Set Properties" OnClick="run_Click" /><br />
		<br />
		<br />
		<cc1:DeluxeTime ID="maskedEdit" runat="server" />
		<br />
		<br />
		<asp:TextBox ID="txbGetValue" runat="server" Enabled="False" Width="200px"></asp:TextBox>
		<asp:Button ID="btnGetValue" runat="server" Text="Get Value" OnClick="btnGetValue_Click" />&nbsp;
		<br />
		<br />
		<asp:TextBox ID="txbSetValue" runat="server" Width="200px"></asp:TextBox>
		<asp:Button ID="btnSetValue" runat="server" Text="Set Value" OnClick="btnSetValue_Click" />&nbsp;
		<br />
		<br />
		<textarea id="maskedEditHtmlShow" runat="server" style="width: 800px; height: 80px;"
			enableviewstate="true"></textarea>
	</div>
	</form>
</body>
</html>
