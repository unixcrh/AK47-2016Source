<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadOnlyHBDropdownList.aspx.cs" Inherits="MCS.Library.SOA.Web.WebControls.Test.HBText.ReadonlyHBDropdownList" %>

<%@ Register Assembly="MCS.Library.SOA.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="HB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>只读下拉列表测试</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <HB:HBDropDownList ID="dropdownList" runat="server" ReadOnly="true" EnableReadOnlyDefaultText="true" ReadOnlyDefaultText="这里没有值">
                <asp:ListItem Text="请选择..." Value=""></asp:ListItem>
                <asp:ListItem Text="One" Value="1"></asp:ListItem>
                <asp:ListItem Text="Two" Value="2"></asp:ListItem>
                <asp:ListItem Text="Three" Value="3"></asp:ListItem>
            </HB:HBDropDownList>
        </div>
        <div>
            <asp:Button runat="server" ID="selectThreeBtn" Text="Select Three" OnClick="selectThreeBtn_Click" />
            <asp:Button runat="server" ID="selectDefaultBtn" Text="Select Default" OnClick="selectDefaultBtn_Click" />
        </div>
    </form>
</body>
</html>
