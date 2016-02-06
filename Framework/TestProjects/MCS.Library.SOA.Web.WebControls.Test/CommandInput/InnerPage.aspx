<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InnerPage.aspx.cs" Inherits="MCS.Library.SOA.Web.WebControls.Test.CommandInput.InnerPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="serverForm" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="刷新父窗口" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
