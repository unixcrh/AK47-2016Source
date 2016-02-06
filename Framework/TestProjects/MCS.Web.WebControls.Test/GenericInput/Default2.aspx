<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default2" CodeBehind="Default2.aspx.cs" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>无标题页</title>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:TextBox ID="TextBox1" runat="server" onSelectedItem="alert(this.get_items()[this.get_selectIndex()].Value);alert(this.get_items()[this.get_selectIndex()].Text);"
			BorderStyle="None"></asp:TextBox>&nbsp;
		<cc1:GenericInputExtender ID="GenericInputExtender1" runat="server" TargetControlID="TextBox1" />
		<input type=button value="Show Text..." onclick="alert($get('TextBox1').value)" />
		<br />
		项目文本：<asp:TextBox ID="txtItemText" runat="server"></asp:TextBox>
		<asp:CheckBox ID="chkSelected" runat="server" Text="默认选中" /><br />
		项目值：<asp:TextBox ID="txtItemValue" runat="server"></asp:TextBox>
		<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Width="54px" />
		<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="清空项目" /><br />
		<br />
		边框颜色：<asp:TextBox ID="txtBorderColor" runat="server"></asp:TextBox>
		<asp:Button ID="btnSetBorderColor" runat="server" OnClick="btnSetBorderColor_Click"
			Text="设置" Width="54px" /><br />
		项目字体颜色：<asp:TextBox ID="txtFontColor" runat="server"></asp:TextBox>
		<asp:Button ID="btnSetFontColor" runat="server" OnClick="btnSetFontColor_Click" Text="设置"
			Width="54px" /><br />
		<br />
		鼠标移动到项目字体颜色：<asp:TextBox ID="txtSelectItemFontColor" runat="server"></asp:TextBox>
		<asp:Button ID="btnSelectItemFontColor" runat="server" OnClick="btnSelectItemFontColor_Click"
			Text="设置" Width="54px" /><br />
		鼠标移动到项目背景颜色：<asp:TextBox ID="txtHoveBGColor" runat="server"></asp:TextBox>
		<asp:Button ID="btnHoveBGColor" runat="server" OnClick="btnHoveBGColor_Click" Text="设置"
			Width="54px" /><br />
		<br />
		下拉箭头区域背景色：<asp:TextBox ID="txtBgColor" runat="server"></asp:TextBox>
		<asp:Button ID="btnSetBgColor" runat="server" OnClick="btnSetBgColor_Click" Text="设置"
			Width="54px" /><br />
	</div>
	</form>
</body>
</html>
