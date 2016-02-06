<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.Converter.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实体实例JSON转换测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_json" Height="500px" Width="100%"></asp:TextBox>
        <br />
        <asp:Button runat="server" Text="转换" ID="btn_Convert" OnClick="btn_Convert_Click" /><asp:Label
            runat="server" ID="lbl_msg" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
