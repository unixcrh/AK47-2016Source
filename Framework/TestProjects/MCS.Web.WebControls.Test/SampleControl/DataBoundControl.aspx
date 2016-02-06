<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBoundControl.aspx.cs" Inherits="MCS.Web.WebControls.Test.SampleControl.DataBoundControl" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DeluxeSelect ID="DeluxeSelect1" runat="server" DataSourseTextField="Displayname" Width="1005" DataSourseValueField="Guid">
        </cc1:DeluxeSelect>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OAConnectionString %>"
            SelectCommand="SELECT TOP 20 * FROM USER_INFO"></asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Button" />
    </form>
</body>
</html>
