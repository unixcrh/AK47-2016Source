<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPager.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.TestPager" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
	TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DIV1" runat="server">
    <asp:Button ID="query" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <cc1:DeluxePager ID="logListPager" runat="server" PageSize="10" Width="100%" BorderWidth="0" BorderColor="gray" 

OnCommonPageIndexChanged="LogListPager_CommonPageIndexChanged"

                   DataBoundControlID="gvLogList" IDataSource="False">

     <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PreviousPageText="上一页" />

</cc1:DeluxePager>        
        <asp:GridView ID="gvLogList" AllowPaging="true" Width="100%" AutoGenerateColumns="false" Visible="true" ShowFooter="false" runat="server" OnPageIndexChanged="gvLogList_PageIndexChanged" OnPageIndexChanging="gvLogList_PageIndexChanging" PageSize="1">
            <PagerSettings Visible="False" />
            <Columns>
                    <asp:BoundField DataField="ORDER_ID" HeaderText="ORDER_ID" SortExpression="ORDER_ID" />
                    <asp:BoundField DataField="SORT_ID" HeaderText="SORT_ID" InsertVisible="False" ReadOnly="True"
                        SortExpression="SORT_ID" />
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" SortExpression="CUSTOMER_NAME" />
                    <asp:BoundField DataField="PRIORITY" HeaderText="PRIORITY" SortExpression="PRIORITY" />
                    <asp:BoundField DataField="CREATE_USER" HeaderText="CREATE_USER" SortExpression="CREATE_USER" />
                    <asp:BoundField DataField="CREATE_TIME" HeaderText="CREATE_TIME" SortExpression="CREATE_TIME" />
                    <asp:BoundField DataField="UPDATE_TAG" HeaderText="UPDATE_TAG" SortExpression="UPDATE_TAG" />
                </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
