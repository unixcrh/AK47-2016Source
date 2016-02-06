<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerForGridView.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerForGridView1" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
	TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PagerForGridView</title>
</head>
<body>
    <form id="form1" runat="server">
       <div align="center"><font size ="10" color="blue">GridView</font> </div>
        <div>
            <asp:GridView runat="server" ID="GridView1" AllowPaging="True" CellPadding="4"
                Width="100%" ForeColor="#333333" GridLines="None"  
                AutoGenerateColumns="False" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="Order_ID" DataNavigateUrlFormatString="CallServiceOrderFormView.aspx?orderID={0}"
                        DataTextField="CUSTOMER_NAME" DataTextFormatString="{0}" HeaderText="CUSTOMER_NAME"
                        Target="_blank" />
                    <asp:BoundField DataField="PRIORITY" HeaderText="Priority" SortExpression="PRIORITY">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CREATE_TIME" HeaderText="CreateTime" SortExpression="CREATE_TIME">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CREATE_USER" HeaderText="CreateUser" SortExpression="CREATE_USER">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
             <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页"
                 PreviousPageText="上一页" />
            </asp:GridView> 
        <cc1:DeluxePager ID="DeluxePager1" runat="server" DataBoundControlID="GridView1" IDataSource="False" OnCommonPageIndexChanged="DeluxePager1_CommonPageIndexChanged" >
            <PagerSettings Mode="Numeric" />
             
        </cc1:DeluxePager>
            &nbsp;&nbsp;&nbsp;&nbsp;
        </div>        
        <asp:GridView Width="100%" runat="server" ID="GridView2" DataSourceID="SqlDataSource1"
                AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="SORT_ID">
                <FooterStyle BackColor="#CCCC99" />
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
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True"
            ProviderName="System.Data.SqlClient"  SelectCommand="SELECT  * FROM ORDERS&#13;&#10;&#13;&#10;SELECT  COUNT(*)  FROM ORDERS" OnSelected="SqlDataSource1_Selected">
        </asp:SqlDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager><asp:GridView Width="100%" runat="server" ID="GridView3"
                AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="SORT_ID">
            <PagerSettings Mode="NumericFirstLast" />
            <FooterStyle BackColor="#CCCC99" />
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
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </form>
</body>
</html>
