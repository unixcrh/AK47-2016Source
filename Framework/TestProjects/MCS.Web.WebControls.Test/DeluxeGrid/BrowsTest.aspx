<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowsTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxeGrid.BrowsTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>
        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" />
        <br />
        <br />
        <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyField="Sort_ID" DataSourceID="SqlDataSource1" OnPageIndexChanged="DataGrid2_PageIndexChanged">
            <Columns>
                <asp:BoundColumn DataField="ORDER_ID" HeaderText="ORDER_ID" SortExpression="ORDER_ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SORT_ID" HeaderText="SORT_ID" ReadOnly="True" SortExpression="SORT_ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" SortExpression="CUSTOMER_NAME">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PRIORITY" HeaderText="PRIORITY" SortExpression="PRIORITY">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CREATE_USER" HeaderText="CREATE_USER" SortExpression="CREATE_USER">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CREATE_TIME" HeaderText="CREATE_TIME" SortExpression="CREATE_TIME">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UPDATE_TAG" HeaderText="UPDATE_TAG" SortExpression="UPDATE_TAG">
                </asp:BoundColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" />
        </asp:DataGrid><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT  * FROM ORDERS ">
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" DataKeyField="Sort_ID" DataSourceID="SqlDataSource1" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>    
                    
                    <asp:BoundField DataField="ORDER_ID" visible="False" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="ORDER_ID" DataNavigateUrlFormatString="CallServiceOrderFormView.aspx?orderID={0}"
                        DataTextField="CUSTOMER_NAME" DataTextFormatString="{0}" HeaderText="CUSTOMER_NAME"
                        Target="_blank" />
                    <asp:BoundField DataField="PRIORITY" HeaderText="Priority"  SortExpression="PRIORITY">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CREATE_TIME" HeaderText="CreateTime"  SortExpression="CREATE_TIME">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CREATE_USER" HeaderText="CreateUser" SortExpression="CREATE_USER">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>     
            <PagerTemplate>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <cc1:DeluxePager ID="DeluxePager1" runat="server" DataBoundControlID="GridView1"
                    IDataSource="true" PageSize="10" Width="100%">
                    <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
                        NextPageText="下一页" PreviousPageText="上一页" />
                </cc1:DeluxePager> 
            </PagerTemplate>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </form>
</body>
</html>
