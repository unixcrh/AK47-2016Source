<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CascadePagedDeluxeGrid.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeGrid.CascadePagedDeluxeGrid" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>联动的DeluxeGrid</title>
</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True"
			ProviderName="System.Data.SqlClient" SelectCommand="SELECT  * FROM ORDERS ">
		</asp:SqlDataSource>
	</div>
	<div>
		<cc1:DeluxeGrid ID="firstGrid" runat="server" CellPadding="4" Width="100%" ForeColor="Blue" CascadeControlID="secondGrid"
			GridLines="None" PagerStyle-BackColor="blue" CssClassMouseOver="OverRow" AllowPaging="True"
			SetGridViewTitle="测试列表" BackColor="RoyalBlue" BorderColor="Crimson" AutoGenerateColumns="False"
			UseAccessibleHeader="False" CellSpacing="1" CaptionAlign="Top" RecordCount="0"
			PagerGotoMode="true" DataSourceID="SqlDataSource1" DataSourceMaxRow="0" IDataSource="True"
			SelectedKeysValue="" TitleColor="141, 143, 149" TitleFontSize="Large" DataKeyNames="Sort_ID">
			<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<RowStyle BackColor="#EFF3FB" />
			<EditRowStyle BackColor="#2461BF" />
			<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
			<PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" />
			<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<AlternatingRowStyle BackColor="White" />
			<Columns>
				<asp:BoundField DataField="ORDER_ID" Visible="False">
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:HyperLinkField DataNavigateUrlFields="ORDER_ID" DataNavigateUrlFormatString="CallServiceOrderFormView.aspx?orderID={0}"
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
			<PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
		</cc1:DeluxeGrid>
	</div>
	</div>
	<div>
		<cc1:DeluxeGrid ID="secondGrid" runat="server" CellPadding="4" Width="100%" ForeColor="Blue"
			GridLines="None" PagerStyle-BackColor="blue" CssClassMouseOver="OverRow" AllowPaging="True"
			SetGridViewTitle="测试列表" BackColor="RoyalBlue" BorderColor="Crimson" AutoGenerateColumns="False"
			UseAccessibleHeader="False" CellSpacing="1" CaptionAlign="Top" RecordCount="0"
			PagerGotoMode="true" DataSourceID="SqlDataSource1" DataSourceMaxRow="0" IDataSource="True"
			SelectedKeysValue="" TitleColor="141, 143, 149" TitleFontSize="Large" DataKeyNames="Sort_ID">
			<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<RowStyle BackColor="#EFF3FB" />
			<EditRowStyle BackColor="#2461BF" />
			<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
			<PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" />
			<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<AlternatingRowStyle BackColor="White" />
			<Columns>
				<asp:BoundField DataField="ORDER_ID" Visible="False">
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:HyperLinkField DataNavigateUrlFields="ORDER_ID" DataNavigateUrlFormatString="CallServiceOrderFormView.aspx?orderID={0}"
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
			<PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
		</cc1:DeluxeGrid>
	</div>
	</form>
</body>
</html>
