<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.ModuleTest.ExceptionTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Exception Test</title>
	<script type="text/javascript">
		function onShowDialog() {
			window.showModalDialog("staticDialog.htm");
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<div>
		<asp:Button runat="server" ID="throwException" Text="Throw Exception" OnClick="throwException_Click" />
		<asp:Button runat="server" ID="throwMsgBoxException" Text="Throw Message Box Exception"
			OnClick="throwMsgBoxException_Click" />
		<input type="button" value="Show dialog..." onclick="onShowDialog()" />
	</div>
	<div>
		<asp:UpdatePanel runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button runat="server" ID="throwMsgBoxInUpdatePanelException" Text="Throw Message Box Exception In UpdatePanel"
					OnClick="throwMsgBoxExceptionInUpdataPanel_Click" />
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
	</form>
</body>
</html>
