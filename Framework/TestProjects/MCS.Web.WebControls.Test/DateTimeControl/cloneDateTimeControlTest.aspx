<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cloneDateTimeControlTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DateTimeControl.cloneDateTimeControlTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title>Clone date time control</title>
	<script type="text/javascript">
		function onCloneComponent() {
			var parent = $get("container");

			var template = $find("timeInput");

			template.cloneAndAppendToContainer(parent);
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<div id="container">
		<MCS:DeluxeDateTime ID="timeInput" runat="server" TimeAutoComplete="true" TimeMask="99:99:99"
			TimeAutoCompleteValue="00:00:00"></MCS:DeluxeDateTime>
	</div>
	<div>
		<input type="button" onclick="onCloneComponent();" value="Clone Component" />
	</div>
	</form>
</body>
</html>
