<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultoTabTest.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.Demo.ResultoTabTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Height="285px" TextMode="MultiLine" Width="1410px"></asp:TextBox>
        <br />
        JobID:<asp:TextBox runat="server" ID="txt_JobID"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="生成创建表语句" OnClick="Button1_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="执行任务" OnClick="Button2_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="测试sql特殊字符过滤" 
            onclick="Button4_Click" />
    </div>
    <asp:LinkButton ID="LinkButton1" runat="server">测试按钮</asp:LinkButton>
    <asp:Button ID="Button3" runat="server" Text="测试新方法获取SAP连接串" 
        onclick="Button3_Click" />
    </form>
</body>
</html>
