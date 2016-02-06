<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgBoxTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.MsgBox.MsgBoxTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>MessageBoxTest</title>
	<script type="text/javascript">
		function onShowError() {
		    var error = Error.create("Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException");
		    error.description = "Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException";
		    $showError(error);

		    //$HGClientMsg.alert("Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException", "Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException Hello ExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionExceptionException", "Info");
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:ScriptManager runat="server" EnableScriptGlobalization="true">
		</asp:ScriptManager>
	</div>
	<div>
		<input type="button" value="Show Error" onclick="onShowError();" />
	</div>
	</form>
</body>
</html>
