<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmptyGrid.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxeGrid.EmptyGrid" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Empty Grid Test</title>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:ScriptManager runat="server" EnableScriptGlobalization="true" />
		<MCS:DeluxeGrid ID="DeluxeGrid1" runat="server" AllowPaging="true" />
	</div>
	</form>
</body>
</html>
