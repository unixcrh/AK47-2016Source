<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeCalendar.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeCalendar.DeluxeCalendar" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title>Calendar Test</title>
	<script type="text/javascript">
		function onCloneComponent() {
			var parent = $get("container");

			var template = $find("calendarTemplate");

			var newComponent = template.cloneAndAppendToContainer(parent);

			newComponent.add_clientValueChanged(Function.createDelegate(this, onDateChanged));
		}

		function onDateChanged(sender) {
			addMessage(sender.get_element().id + ":" + sender.get_value());
		}

		function addMessage(msg) {
			result.innerHTML += "<p style='margin:0'>" + msg + "</p>";
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<div id="container">
		<cc1:DeluxeCalendar Value="2010/03/03" runat="server" ReadOnly="false" ID="calendarTemplate">
		</cc1:DeluxeCalendar>
	</div>
	<div>
		<input type="button" onclick="onCloneComponent();" value="Clone Component" />
	</div>
	<div id="resultContainer">
		<div id="result" contenteditable="true" style="overflow: auto; border: 1px silver solid;
			width: 100%; height: 160px">
		</div>
	</div>
	</form>
</body>
</html>
