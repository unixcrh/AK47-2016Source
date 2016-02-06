<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.Demo.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Demo</title>
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/home.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascript/pc.js" type="text/javascript"></script>
    <script src="../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Javascript/home.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
        <Services>
            <asp:ServiceReference Path="~/Services/CommonService.asmx" />
        </Services>
    </asp:ScriptManager>
    <div style="width:800px;margin:auto;">
    <soa:PropertyForm runat="server" ID="propertyForm" Width="100%" Height="100%"  AutoSaveClientState="True" />
    </div>
    <div style="width:100%;margin:auto;text-align:center;">
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
