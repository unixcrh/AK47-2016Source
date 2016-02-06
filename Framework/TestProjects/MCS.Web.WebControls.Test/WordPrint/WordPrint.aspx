<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordPrint.aspx.cs" Inherits="MCS.Web.WebControls.Test.WordPrint.WordPrint" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:WordPrint ID="WordPrint1" runat="server" AutoCallBack="True" OnOnPrint="WordPrint1_OnPrint"
            TempleteUrl="http://localhost:2032/WordPrint/Doc1.dot" />

    </form>
</body>
</html>
