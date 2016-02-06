<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerToDataGrid.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerToDataGrid1" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"><font size ="10" color="blue">DataGrid</font> </div>
    <div>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" OnPageIndexChanged="DataGrid1_PageIndexChanged" AutoGenerateColumns="False" Width="80%">
            <Columns>
                <asp:BoundColumn DataField="CREATE_USER" HeaderText="CREATE_USER"></asp:BoundColumn>
                <asp:BoundColumn DataField="PRIORITY" HeaderText="PRIORITY"></asp:BoundColumn>
                <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="CREATE_TIME" HeaderText="CREATE_TIME"></asp:BoundColumn> 
                
            </Columns>
            <PagerStyle Mode="NumericPages" NextPageText="下一页" PrevPageText="上一页" />
        
        </asp:DataGrid> 
        <cc1:deluxepager id="DeluxePager1" runat="server" databoundcontrolid="DataGrid1"
            isidatasouce="False" width="80%" OnCommonPageIndexChanged="DeluxePager1_CommonPageIndexChanged">
        <PagerSettings PreviousPageText="上一页" Mode="NextPreviousFirstLast" LastPageText="尾页" FirstPageText="首页" NextPageText="下一页"></PagerSettings>
        </cc1:deluxepager>
        <asp:DataGrid ID="DataGrid2" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" DataKeyField="Sort_ID" AutoGenerateColumns="False" OnPageIndexChanged="DataGrid2_PageIndexChanged">
             <Columns>   
                    <asp:BoundColumn DataField="ORDER_ID" HeaderText="ORDER_ID" SortExpression="ORDER_ID" />
                    <asp:BoundColumn DataField="SORT_ID" HeaderText="SORT_ID"  ReadOnly="True"
                        SortExpression="SORT_ID" />
                    <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" SortExpression="CUSTOMER_NAME" />
                    <asp:BoundColumn DataField="PRIORITY" HeaderText="PRIORITY" SortExpression="PRIORITY" />
                    <asp:BoundColumn DataField="CREATE_USER" HeaderText="CREATE_USER" SortExpression="CREATE_USER" />
                    <asp:BoundColumn DataField="CREATE_TIME" HeaderText="CREATE_TIME" SortExpression="CREATE_TIME" />
                    <asp:BoundColumn DataField="UPDATE_TAG" HeaderText="UPDATE_TAG" SortExpression="UPDATE_TAG" />
        </Columns>
            <PagerStyle Mode="NumericPages" />
       
        </asp:DataGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True" OnSelected="SqlDataSource1_Selected" ProviderName="System.Data.SqlClient" SelectCommand="SELECT  * FROM ORDERS "></asp:SqlDataSource>
        <br />
        <asp:DataGrid ID="DataGrid3" runat="server" AllowPaging="True" OnPageIndexChanged="DataGrid1_PageIndexChanged" AutoGenerateColumns="False" Width="80%">
            <Columns>
                <asp:BoundColumn DataField="CREATE_USER" HeaderText="CREATE_USER"></asp:BoundColumn>
                <asp:BoundColumn DataField="PRIORITY" HeaderText="PRIORITY"></asp:BoundColumn>
                <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="CREATE_TIME" HeaderText="CREATE_TIME"></asp:BoundColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" NextPageText="下一页" PrevPageText="上一页" />
        </asp:DataGrid>
    </div>
    </form>
</body>
</html>
