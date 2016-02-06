<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DeluxeGrid.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeGrid.DeluxeGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Grid测试</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table width="80%">
			<tr>
				<td>
					指定GridView上显示标题内容
					<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
				</td>
				<td>
					否显示导出部分
					<asp:DropDownList ID="ddlShowExport" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td>
					选择数据源类型
					<asp:DropDownList ID="selectedDataSourceControl" runat="server">
						<asp:ListItem Value="0">普通数据源</asp:ListItem>
						<asp:ListItem Value="1">SqlDataSourceControl</asp:ListItem>
						<asp:ListItem Value="2">ObjectDataSourceControl</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>
					（无效）导出数据源的最大行数：<asp:TextBox ID="txtMaxRows" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					是否增加选择列
					<asp:DropDownList ID="ddlSelected" runat="server">
					</asp:DropDownList>
				</td>
				<td>
					选择列的位置
					<asp:DropDownList ID="ddlSelectedPosition" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td>
					是否多选
					<asp:DropDownList ID="ddlMultiSelect" runat="server">
						<asp:ListItem Value="True" Text="多选"></asp:ListItem>
						<asp:ListItem Value="False" Text="单选"></asp:ListItem>
					</asp:DropDownList>
				</td>
				<td style="height: 26px">
					SetProperties:
					<asp:Button ID="btnSet" runat="server" Text="设 置" OnClick="btnSet_Click" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
