<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DeluxeGridForObjectDataSourceControl.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeGrid.DeluxeGridForObjectDataSourceControl" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
	TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>DeluxeGridForObjectDataSourceControl数据源类型展示</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:DropDownList ID="prioritySelector" runat="server" AutoPostBack="true">
				<asp:ListItem Value="" Text="-" Selected="True" />
				<asp:ListItem Value="-1" Text="低" />
				<asp:ListItem Value="0" Text="中" />
				<asp:ListItem Value="1" Text="高" />
			</asp:DropDownList>
		</div>
		<div>
			<cc1:DeluxeGrid ID="DeluxeGrid1" runat="server" CellPadding="4" Width="100%" ForeColor="Blue"
				GridLines="None" PagerStyle-BackColor="blue" AllowPaging="True"
				BackColor="RoyalBlue" BorderColor="Crimson" AutoGenerateColumns="False"
				UseAccessibleHeader="False" CellSpacing="1" CaptionAlign="Top" 
				DataSourceID="ObjectDataSource1" DataSourceMaxRow="0"
				SelectedKeysValue="" TitleColor="141, 143, 149" TitleFontSize="Large" ShowExportControl="False"
				OnExportData="DeluxeGrid1_ExportClick" DataKeyNames="Sort_ID" AllowSorting="True">
				<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
				<RowStyle BackColor="#EFF3FB" />
				<EditRowStyle BackColor="#2461BF" />
				<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
				<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
				<AlternatingRowStyle BackColor="White" />
				<Columns>
					<asp:BoundField DataField="ORDER_ID" visible="False">
						<itemstyle horizontalalign="Center" />
					</asp:BoundField>
					<asp:HyperLinkField DataNavigateUrlFields="ORDER_ID" DataNavigateUrlFormatString="CallServiceOrderFormView.aspx?orderID={0}"
						DataTextField="CUSTOMER_NAME" DataTextFormatString="{0}" HeaderText="CUSTOMER_NAME"
						Target="_blank" />
					<asp:BoundField DataField="PRIORITY" HeaderText="Priority" SortExpression="PRIORITY">
						<itemstyle horizontalalign="Center" />
					</asp:BoundField>
					<asp:BoundField DataField="CREATE_TIME" HeaderText="CreateTime" SortExpression="CREATE_TIME">
						<itemstyle horizontalalign="Center" />
					</asp:BoundField>
					<asp:BoundField DataField="CREATE_USER" HeaderText="CreateUser" SortExpression="CREATE_USER">
						<itemstyle horizontalalign="Center" />
					</asp:BoundField>
				</Columns>
				<PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
			</cc1:DeluxeGrid>
			<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectCountMethod="GetFilteredDataCount"
				SelectMethod="GetFilteredData" TypeName="MCS.Web.WebControls.Test.OrdersDataViewAdapter"
				EnablePaging="True" SortParameterName="sortExpression">
				<SelectParameters>
					<asp:ControlParameter ControlID="prioritySelector" PropertyName="SelectedValue" Name="priority"
						Type="String" />
				</SelectParameters>
			</asp:ObjectDataSource>
			<asp:Button ID="btnPostBack" runat="server" OnClick="btnPostBack_Click" Text="清除全部选中的checkbox" /></div>
	</form>
</body>
</html>
