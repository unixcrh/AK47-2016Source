<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpreadSheetTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.SpreadSheet.SpreadSheetTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Excel 导出</title>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:Button runat="server" ID="exportDataSetBtn" Text="Export DataSet" Width="160px"
			onclick="exportDataSetBtn_Click"/>
		<asp:Button runat="server" ID="exportDataSetByTemplateBtn" 
			Text="Export To Template" Width="160px" 
			onclick="exportDataSetByTemplateBtn_Click" />
	</div>
	</form>
</body>
</html>
