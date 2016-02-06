<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog.aspx.cs" Inherits="MCS.Web.WebControls.Test.ModuleTest.dialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Dialog window</title>
	<base target="_self" />
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		This is id a dialog window.
		<asp:Literal runat="server" Mode="Encode" ID="dataLabel"></asp:Literal>
	</div>
	<div>
		<asp:Button runat="server" ID="postBackBtn" Text="Postback..." />
	</div>
	</form>
</body>
</html>
