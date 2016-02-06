<%@ Page Language="C#" AutoEventWireup="true" Codebehind="UpdatePannel.aspx.cs" Inherits="MCS.Web.WebControls.Test.SampleControl.UpdatePannel" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
	TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>

</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode=release>
			</asp:ScriptManager>
			&nbsp;</div>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<cc1:SampleControl ID="SampleControl1" InputCssClass="" runat="server" Text="123456">
				</cc1:SampleControl>
				<asp:Button ID="Button1" runat="server" Text="Button" />
			</ContentTemplate>
		</asp:UpdatePanel>
	</form>
	<script language="javascript" type="text/javascript">
	    function pageLoad()
	    {
	        var test2 = Sys.Serialization.JavaScriptSerializer.deserialize('{a:"\\/Date(1190996816539)\\/"}');
	        
	        debugger;
	    }
	</script>
</body>
</html>
