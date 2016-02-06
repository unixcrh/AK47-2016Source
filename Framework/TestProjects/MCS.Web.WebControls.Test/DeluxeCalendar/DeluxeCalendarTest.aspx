<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeCalendarTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeCalendar.DeluxeCalendarTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title>日期测试</title>
	<script type="text/javascript">
	    function onClientValueChanged(e) {
	        alert(1);
			$get("log").innerText = e.get_value() + "\n" + $get("log").innerText;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<cc1:DeluxeCalendar runat="server" ID="calendar" Value="2010/03/03" OnClientValueChanged="onClientValueChanged">
		</cc1:DeluxeCalendar>
		至
		<cc1:DeluxeCalendar Value="2010/03/03" runat="server" ID="DeluxeCalendar1">
		</cc1:DeluxeCalendar>
	</div>
	<textarea id="log" style="width: 400px; height: 400px"></textarea>
	</form>
</body>
</html>
