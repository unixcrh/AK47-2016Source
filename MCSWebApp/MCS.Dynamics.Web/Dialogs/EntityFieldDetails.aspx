<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntityFieldDetails.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.EntityFieldDetails" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改实体属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pcdlg-content">
        <%--<pc:BannerNotice runat="server" ID="notice" />--%>
        <div>
            <soa:SimpleTabStrip ID="tabStrip" runat="server">
                <TabStrips />
            </soa:SimpleTabStrip>
        </div>
        <div id="panelContainer" style="display: none" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
