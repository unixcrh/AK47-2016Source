<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cloneAutoCompleteControl.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.AutoComplete.cloneAutoCompleteControl" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title>Clone AutoComplete Test</title>
	<script type="text/javascript">
		function onValueChanged(ctrl, e) {
			var selectedObject = e.selectedObject;

			if (selectedObject.Value != "") {
				$get("valuesText").innerText =
					String.format("Value: {0}, Text: {1}", selectedObject.Value, selectedObject.Text);
			}
		}

		function onShowValueClick() {
			alert($find("dataInput").get_value());
		}

		function onCloneComponent() {
			var parent = $get("container");

			//这个目前还不支持
			var template = $find("dataInput");

			template.cloneAndAppendToContainer(parent);
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<input runat="server" id="dataInput" type="text" />
		<MCS:AutoCompleteControl runat="server" ID="autoCompleteTemplate" DataValueField="Code"
			DataTextFormatString="{0}-{1}" RequireValidation="true" AutoCallBack="true" AutoValidateOnChange="true"
			TargetControlID="dataInput" OnGetDataSource="autoComplete_GetDataSource" OnValueChanged="onValueChanged" />
	</div>
	<div>
		<input id="showValueBtn" type="button" value="Show Value..." onclick="onShowValueClick();" />
		<input type="button" onclick="onCloneComponent();" value="Clone Component" />
	</div>
	<div>
		<div>
			Values</div>
		<div runat="server" id="valuesText">
		</div>
		<div id="container">
		</div>
	</div>
	</form>
</body>
</html>
