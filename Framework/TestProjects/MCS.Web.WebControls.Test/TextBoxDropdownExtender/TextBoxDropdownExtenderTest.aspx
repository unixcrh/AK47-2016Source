<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBoxDropdownExtenderTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.TextBoxDropdownExtender.TextBoxDropdownExtenderTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>测试TextBoxDropdownExtender</title>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
		</asp:ScriptManager>
	</div>
	<div>
		<asp:TextBox runat="server" ID="inputText"></asp:TextBox><MCS:TextBoxDropdownExtender
			ID="extender" runat="server" TargetControlID="inputText" Height="120px" />
	</div>
	<div>
		<asp:Button runat="server" ID="postBackBtn" Text="Postback" 
            onclick="postBackBtn_Click" />
	</div>
	</form>
</body>
</html>
