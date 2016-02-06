<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DeluxeGridIndex.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeGrid.DeluxeGridIndex" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>普通数据类型的DeluxeGrid展示</title>

	<script type="text/javascript" language="javascript">
		function alertSelectValue() {
			document.getElementById("txtSelectValue").value = $find("DeluxeGrid1").get_clientSelectedKeys().join(",");
		}

		function onClear() {
			document.getElementById("txtSelectValue").value = "";
		}
	</script>

</head>
<body>
	<form id="serverForm" runat="server">
	<div>
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
		</asp:ScriptManager>
		<cc1:DeluxeGrid ID="DeluxeGrid1" runat="server" CellPadding="4" OnPageIndexChanging="DeluxeGrid1_PageIndexChanging"
			Width="100%" ForeColor="Blue" GridLines="None" PagerStyle-BackColor="blue" AllowPaging="True"
			BackColor="RoyalBlue" BorderColor="Crimson" AutoGenerateColumns="False" UseAccessibleHeader="False"
			CellSpacing="1" CaptionAlign="Top" PagerExportMode="true" CheckBoxAdd="True"
			TitleColor="Cyan" OnRowDataBound="DeluxeGrid1_RowDataBound" DataKeyNames="ORDER_ID"
			OnExportClick="DeluxeGrid1_ExportClick">
			<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<RowStyle BackColor="#EFF3FB" />
			<EditRowStyle BackColor="#2461BF" />
			<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
			<PagerStyle BackColor="white" HorizontalAlign="Center" />
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
			<PagerSettings Position="Top" Mode="NextPrevious" PreviousPageText="上一页" NextPageText="下一页" />
		</cc1:DeluxeGrid>
		&nbsp;&nbsp;&nbsp;
		<textarea id="txtSelectValue" cols="60" rows="20" runat="server"></textarea>
		<asp:Button ID="btnServer" runat="server" OnClick="btnServer_Click" Text="获取服务器端的选中项内容" />&nbsp;
		<input id="btnClient" type="button" onclick="alertSelectValue();" value="获取选中项客户端内容" />
		<input id="btnClear" type="button" onclick="onClear();" value="清除显示内容" />&nbsp;
	</div>
	<asp:Button ID="Button1" runat="server" Text="Button" />
	</form>
</body>
</html>
