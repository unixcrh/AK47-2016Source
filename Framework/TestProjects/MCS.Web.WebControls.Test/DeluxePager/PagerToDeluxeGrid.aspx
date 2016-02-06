<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PagerToDeluxeGrid.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerToGridView1" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>无标题页</title>
</head>
<body>
	<form id="form1" runat="server">
	<div align="center">
		<font size="10" color="blue">DeluxeGrid</font> :见DeluxeGrid测试案例</div>
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<cc1:DeluxeGrid ID="DeluxeGrid1" runat="server" CellPadding="4" OnPageIndexChanging="ordersGridView_SelectedIndexChanging"
		Width="100%" ForeColor="Blue" GridLines="None" PagerStyle-BackColor="blue" CssClassMouseOver="OverRow"
		AllowPaging="True" SetGridViewTitle="测试列表" BackColor="RoyalBlue" BorderColor="Crimson"
		AutoGenerateColumns="False" UseAccessibleHeader="False" CellSpacing="1" CaptionAlign="Top"
		RecordCount="0" PagerGotoMode="true" CheckBoxAdd="True" PagerExportMode="true"
		ExportCommandArgument="ExcelFileName;0,1,2" OnDataBound="ordersGridView_DataBound"
		OnExportOnClick="ordersGridView_ExportOnClick" DataKeyNames="ORDER_ID">
		<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
		<RowStyle BackColor="#EFF3FB" />
		<EditRowStyle BackColor="#2461BF" />
		<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
		<PagerSettings Position="TopAndBottom" />
	</cc1:DeluxeGrid>
	<cc1:DeluxePager ID="DeluxePager1" runat="server" Width="100%" DataBoundControlID="ordersGridView"
		IDataSource="False" OnCommonPageIndexChanged="DeluxePager1_CommonPageIndexChanged">
		<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" LastPageText="尾页"
			NextPageText="下一页" PreviousPageText="上一页" />
	</cc1:DeluxePager>
	&nbsp;&nbsp;&nbsp;&nbsp;<br />
	<cc1:DeluxeGrid ID="DeluxeGrid2" runat="server" CellPadding="4" OnPageIndexChanging="ordersGridView_SelectedIndexChanging"
		Width="100%" ForeColor="Blue" GridLines="None" PagerStyle-BackColor="blue" CssClassMouseOver="OverRow"
		AllowPaging="True" SetGridViewTitle="测试列表" BackColor="RoyalBlue" BorderColor="Crimson"
		AutoGenerateColumns="False" UseAccessibleHeader="False" CellSpacing="1" CaptionAlign="Top"
		RecordCount="0" PagerGotoMode="true" CheckBoxAdd="True" PagerExportMode="true"
		ExportCommandArgument="ExcelFileName;0,1,2" OnDataBound="ordersGridView_DataBound"
		OnExportOnClick="ordersGridView_ExportOnClick" DataKeyNames="ORDER_ID">
		<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
		<RowStyle BackColor="#EFF3FB" />
		<EditRowStyle BackColor="#2461BF" />
		<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
		<PagerSettings Position="TopAndBottom" />
	</cc1:DeluxeGrid>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True"
		OnSelected="SqlDataSource1_Selected" ProviderName="System.Data.SqlClient" SelectCommand="SELECT  * FROM ORDERS ">
	</asp:SqlDataSource>
	<cc1:DeluxeGrid ID="DeluxeGrid3" runat="server" CellPadding="4" OnPageIndexChanging="ordersGridView_SelectedIndexChanging"
		Width="100%" ForeColor="Blue" GridLines="None" PagerStyle-BackColor="blue" CssClassMouseOver="OverRow"
		AllowPaging="True" SetGridViewTitle="测试列表" BackColor="RoyalBlue" BorderColor="Crimson"
		AutoGenerateColumns="False" UseAccessibleHeader="False" CellSpacing="1" CaptionAlign="Top"
		RecordCount="0" PagerGotoMode="true" CheckBoxAdd="True" PagerExportMode="true"
		ExportCommandArgument="ExcelFileName;0,1,2" OnDataBound="ordersGridView_DataBound"
		OnExportOnClick="ordersGridView_ExportOnClick" DataKeyNames="ORDER_ID">
		<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
		<RowStyle BackColor="#EFF3FB" />
		<EditRowStyle BackColor="#2461BF" />
		<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
		<PagerSettings Position="TopAndBottom" />
	</cc1:DeluxeGrid>
	</form>
</body>
</html>
