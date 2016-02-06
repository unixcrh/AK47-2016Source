<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAutoComp.aspx.cs" Inherits="MCS.Web.WebControls.Test.AutoComplete.TestAutoComp" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<div>
		<asp:TextBox ID="txbTarget" runat="server" Width="200px" onTypeError="alert('onTypeError');"></asp:TextBox>
		<cc1:AutoCompleteExtender ID="ctrlAutoCompleteExtender" runat="server" TargetControlID="txbTarget"
			AutoCallBack="true" OnGetDataSource="ctrlAutoCompleteExtender_GetDataSource" />
	</div>
	<asp:TextBox ID="txbTarget1" runat="server" Width="200px" onTypeError="alert('onTypeError');"></asp:TextBox>
	<div runat="server" id="dynamicContainer">
	</div>
	</form>
</body>
</html>
