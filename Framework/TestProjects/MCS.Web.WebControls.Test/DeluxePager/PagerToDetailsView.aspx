<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerToDetailsView.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerToDetailsView1" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"><font size ="10" color="blue">DetailsView</font> </div>
    <div>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="80%" AutoGenerateRows="False" AllowPaging="True" OnPageIndexChanging="DetailsView1_PageIndexChanging">             
            <Fields>
                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" ReadOnly="True" SortExpression="CUSTOMER_NAME" />
                <asp:BoundField DataField="PRIORITY" HeaderText="PRIORITY" ReadOnly="True" SortExpression="PRIORITY" />
                <asp:BoundField DataField="CREATE_USER" HeaderText="CREATE_USER" ReadOnly="True" SortExpression="CREATE_USER" />
                <asp:BoundField DataField="CREATE_TIME" HeaderText="CREATE_TIME" ReadOnly="True" SortExpression="CREATE_TIME" />
            </Fields>
        </asp:DetailsView>
        <cc1:DeluxePager ID="DeluxePager1" runat="server" Width="80%" DataBoundControlID="DetailsView1" PageSize="1" OnCommonPageIndexChanged="DeluxePager1_CommonPageIndexChanged">
            <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页" />
        </cc1:DeluxePager><asp:DetailsView ID="DetailsView2" runat="server" Height="50px" Width="80%" AutoGenerateRows="False" AllowPaging="True" OnPageIndexChanging="DetailsView1_PageIndexChanging" DataKeyNames="Sort_ID" DataSourceID="SqlDataSource1">
            <Fields>
                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" ReadOnly="True" SortExpression="CUSTOMER_NAME" />
                <asp:BoundField DataField="PRIORITY" HeaderText="PRIORITY" ReadOnly="True" SortExpression="PRIORITY" />
                <asp:BoundField DataField="CREATE_USER" HeaderText="CREATE_USER" ReadOnly="True" SortExpression="CREATE_USER" />
                <asp:BoundField DataField="CREATE_TIME" HeaderText="CREATE_TIME" ReadOnly="True" SortExpression="CREATE_TIME" />
            </Fields>
        </asp:DetailsView>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True"
            OnSelected="SqlDataSource1_Selected" ProviderName="System.Data.SqlClient" SelectCommand="SELECT  *  FROM ORDERS">
            <FilterParameters>
                <asp:Parameter DefaultValue="10" Name="@PageSize" />
            </FilterParameters>
        </asp:SqlDataSource><asp:DetailsView ID="DetailsView3" runat="server" Height="50px" Width="80%" AutoGenerateRows="False" AllowPaging="True" OnPageIndexChanging="DetailsView1_PageIndexChanging">
            <Fields>
                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" ReadOnly="True" SortExpression="CUSTOMER_NAME" />
                <asp:BoundField DataField="PRIORITY" HeaderText="PRIORITY" ReadOnly="True" SortExpression="PRIORITY" />
                <asp:BoundField DataField="CREATE_USER" HeaderText="CREATE_USER" ReadOnly="True" SortExpression="CREATE_USER" />
                <asp:BoundField DataField="CREATE_TIME" HeaderText="CREATE_TIME" ReadOnly="True" SortExpression="CREATE_TIME" />
            </Fields>
        </asp:DetailsView>
    </form>
</body>
</html>
