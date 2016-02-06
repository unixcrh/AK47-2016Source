<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dynamicDynamicInput.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.GenericInput.dynamicDynamicInput" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>动态创建GenericInputExtender</title>
	<script type="text/javascript">
		function onBindExtender() {
			var input = new $HGRootNS.GenericInput($get("nameText"));

			input.set_items([{ Value: "SZ", Text: "SZ"}]);
			input.initialize();
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:ScriptManager runat="server" EnableScriptGlobalization="true" />
	</div>
	<div>
		<input type="text" id="nameText" />
	</div>
	<div>
		<input type="button" id="bindExtender" value="Bind" onclick="onBindExtender()" />
	</div>
	</form>
</body>
</html>
