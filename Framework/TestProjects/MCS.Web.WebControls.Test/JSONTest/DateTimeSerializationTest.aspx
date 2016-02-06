<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateTimeSerializationTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.JSONTest.DateTimeSerializationTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>DateTime SerializationTest</title>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		The first serialization:
		<asp:Label runat="server" ID="firstSerializationLabel" />
	</div>
	<div>
		The second serialization:
		<asp:Label runat="server" ID="secondSerializationLabel" />
	</div>
	</form>
</body>
</html>
