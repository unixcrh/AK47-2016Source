<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerForTable.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerForTable1" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>

<body>
    <form id="form1" runat="server">
    <div align="center"><font size ="10" color="blue">Table</font> </div>
    <div>
        &nbsp;<asp:Table ID="Table1" runat="server" Width="80%">
        </asp:Table>
    </div>
        <cc1:DeluxePager ID="DeluxePager1" runat="server" DataBoundControlID="Table1" IDataSource="False"
            onCommonPageIndexChanged="DeluxePager1_CommonPageIndexChanged" IsPagedControl="false"
            PageSize="5" Width="100%" Enabled="true" BackColor="White" ForeColor="Lime">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" PreviousPageText="上一页" />
        </cc1:DeluxePager>
    </form>
</body>
</html>
