<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessCheckPageTest.aspx.cs" Inherits="ResponsivePassportService.TestPages.AccessCheckPageTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <mcs:SignInLogoControl ID="signInLogo" runat="server" AutoRedirect="false" />
        </div>
        <div>
            <h1>您有权限访问此页面</h1>
        </div>
    </form>
</body>
</html>
