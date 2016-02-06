<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cloneAutoCompleteExtender.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.AutoComplete.cloneAutoCompleteExtender" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title>Clone Auto Complete Extender</title>
	<script type="text/javascript">
		function onCloneComponent() {
			var parent = $get("container");

			var template = $find("extenderTemplate");

			template.cloneAndAppendToContainer(parent);
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<div>
			<asp:TextBox ID="txbTarget" runat="server" Width="200px" onTypeError="alert('onTypeError');"></asp:TextBox>
			<MCS:AutoCompleteExtender ID="extenderTemplate" runat="server" TargetControlID="txbTarget"
				AutoCallBack="true" OnGetDataSource="ctrlAutoCompleteExtender_GetDataSource" />
		</div>
		<div>
			<input type="button" onclick="onCloneComponent();" value="Clone Component" />
		</div>
		<div id="container">
		</div>
	</div>
	</form>
</body>
</html>
