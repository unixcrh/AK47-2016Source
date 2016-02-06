<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertFiledValueTest.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.Demo.ConvertFiledValueTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Int值&nbsp;&nbsp; <asp:TextBox ID="txtInt" runat="server"></asp:TextBox>
        <br />
        <br />
        Bool值&nbsp;&nbsp; <asp:TextBox ID="txtBool" runat="server"></asp:TextBox>
        <br />
        <br />
        DateTime值&nbsp;&nbsp; <asp:TextBox ID="txtDatetime" runat="server"></asp:TextBox>
        <br />
        <br />
        Decimal值&nbsp;&nbsp; <asp:TextBox ID="txtDecimal" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="ConvertValue" OnClick="Button1_Click" />
    </div>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    
    <br/>
    <asp:Button ID="Button2" runat="server" 
        Text="调用ETL实体取SAP账户信息-Code=624da4a3-ffc2-4379-bddd-3cc4543da1d6" 
        onclick="Button2_Click" />

    </form>
</body>
</html>
