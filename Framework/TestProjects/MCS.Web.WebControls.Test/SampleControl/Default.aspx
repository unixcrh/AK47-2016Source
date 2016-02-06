<%@ Page Language="C#" AutoEventWireup="true" Inherits="SampleControl_Default" CodeBehind="Default.aspx.cs" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
</head>
<body>
	<form id="form1" runat="server">
	<div>
	</div>
	<cc1:SampleControl ID="SampleControl1" InputCssClass="" runat="server" OnBtnClick="OnSampleControlClick"
		Text="123456" ClientCallbackCompleteEventHanlder="onSampleControlCallbackComplete"
		OnUnload="SampleControl1_Unload" InvokeWithoutViewState="true">
	</cc1:SampleControl>
	<cc1:SampleControl ID="SampleControl2" InputCssClass="" runat="server" Text="123456"
		ClientCallbackCompleteEventHanlder="onSampleControlCallbackComplete" ShowingMode="Dialog">
	</cc1:SampleControl>
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
	<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>&nbsp;&nbsp; &nbsp;&nbsp;
	<asp:Button runat="server" ID="Button2" Text="Button" OnClick="Button2_Click" />
	<input type="button" value="findComponent" onclick="findComponent()" />
	<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></form>
	<script type="text/javascript">
		function onSampleControlCallbackComplete(sender, e) {
			alert(sender.get_text());
			alert(e.result);
		}

		function findComponent() {
			var sampleControl = Sys.Application.findComponent("SampleControl1");
			alert(sampleControl.get_alertImgUrl());
		}

		function pageLoad() {
			//            if ($HGClientMsg.confirm("a", "b", "c")) 
			//                alert("true") ;
			//            else 
			//                alert("false");
			//                debugger;
			//            var a = {a:"a", b:"b", c:{c1:"c1", c2:"c2"}};
			//            var b = Object.clone(a, true);
		}

		function onOK() {
			alert("ok");
		}

		function onCancel() {
			alert("cancel");
		}
	</script>
</body>
</html>
