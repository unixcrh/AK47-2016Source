<%@ Page Language="C#" AutoEventWireup="true" Codebehind="WordPrintTest.aspx.cs"
	Inherits="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls.Test.WordPrint.WordPrintTest" %>

<%@ Register Assembly="DeluxeWorks.Web.WebControls" Namespace="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls"
	TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>WordPrintTest</title>
	<link href="WordPrint.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<table>
				<tr style="vertical-align: top">
					<td style="width: 500px">
						Text
						<asp:TextBox ID="txbText" runat="server" Text="打印"></asp:TextBox><br />
						<label style="font-size: small; font-style: italic">
							控件显示的文本，默认为“打印”</label><br />
						Type
						<asp:DropDownList ID="ddlType" runat="server">
						    <asp:ListItem>Default</asp:ListItem>
							<asp:ListItem>InputButton</asp:ListItem>
							<asp:ListItem>ImageButton</asp:ListItem>
							<asp:ListItem>LinkButton</asp:ListItem>
						</asp:DropDownList><br />
						<label style="font-size: small; font-style: italic">
							控件类型，InputButton/ImageButton/LinkButton，默认为InputButton</label><br />
						CssClass
						<asp:DropDownList ID="ddlCssClass" runat="server">
							<asp:ListItem>Default</asp:ListItem>
							<asp:ListItem>CssClass_Demo1</asp:ListItem>
							<asp:ListItem>CssClass_Demo2</asp:ListItem>
						</asp:DropDownList><br />
						<label style="font-size: small; font-style: italic">
							控件的样式</label><br />
						ImageUrl
						<asp:DropDownList ID="ddlImageUrl" runat="server">
						    <asp:ListItem>Default</asp:ListItem>
							<asp:ListItem>Print.JPG</asp:ListItem>
							<asp:ListItem>/WordPrint/Print.JPG</asp:ListItem>
							<asp:ListItem>~/WordPrint/Print.JPG</asp:ListItem>
							<asp:ListItem>.\PrintArrow.JPG</asp:ListItem>
							<asp:ListItem>http://10.1.1.96/wordprint/PrintArrow.JPG</asp:ListItem>
						</asp:DropDownList><br />
						<label style="font-size: small; font-style: italic">
							如果为ImageButton，则为指定的图片路径</label><br />
						TempleteUrl
						<asp:DropDownList ID="ddlTempleteUrl" runat="server">
						    <asp:ListItem>Default</asp:ListItem>
							<asp:ListItem>Templete1.dot</asp:ListItem>
							<asp:ListItem>/WordPrint/Templete1.dot</asp:ListItem>
							<asp:ListItem>~/WordPrint/Templete2.dot</asp:ListItem>
							<asp:ListItem>.\Templete3.dot</asp:ListItem>
							<asp:ListItem>http://10.1.1.96/wordprint/Templete3.dot</asp:ListItem>
						</asp:DropDownList><br />
						<label style="font-size: small; font-style: italic">
							设置Word文档模板路径</label><br />
						AccessKey
						<asp:TextBox ID="txbAccessKey" runat="server"></asp:TextBox><br />
						<label style="font-size: small; font-style: italic">
							快捷键，设置为P则响应Alt+P</label><br />
					</td>
					<td>
						<asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" /><br />
						<br />
						<cc1:WordPrint ID="ctrlWordPrint" runat="server" OnPrint="ctrlWordPrint_Print" />
						<br />
                        <cc1:WordPrint ID="ctrlWordPrint2" runat="server" OnPrint="ctrlWordPrint2_Print" />
						<br />
						<textarea id="ctrlWordPrintHtmlShow" runat="server" style="width: 550px; height: 130px;"
							enableviewstate="true"></textarea>
					</td>
				</tr>
			</table>
			<hr />
			<table>
				<tr>
					<td>
						DataSourceList
						<label style="font-size: small; font-style: italic">
							生成Word文档的数据源集合</label>
					</td>
				</tr>
				<tr>
					<td>
						<table>
							<tr style="vertical-align: top">
								<td style="width: 20px">
								</td>
								<td style="width: 270px">
									文本数据<br />
									<hr />
									COLUMN1<asp:TextBox ID="txbDsTextCol1" runat="server" Width="180px"></asp:TextBox><br />
									COLUMN2<asp:TextBox ID="txbDsTextCol2" runat="server" Width="180px"></asp:TextBox><br />
									COLUMN3<asp:TextBox ID="txbDsTextCol3" runat="server" Width="180px"></asp:TextBox><br />
									<asp:Button ID="btnSetDefaultText" runat="server" Text="UseDefault" OnClick="btnSetDefaultText_Click" />
									<asp:Button ID="btnSetDsText" runat="server" Text="Set" OnClick="btnSetDsText_Click" />
								</td>
								<td style="width: 270px">
									表格数据<br />
									<hr />
									<label style="font-weight: bold; width: 270px">
										COLUMN1&nbsp; &nbsp;COLUMN2&nbsp; &nbsp;COLUMN3</label>
									<br />
									<div style="height: 100px; border-style: double">
										<asp:DataList ID="dlDsTable" runat="server" CellPadding="4" ForeColor="#333333">
											<ItemTemplate>
												<asp:Label ID="Label1" runat="server" Width="80px" Text='<%# Eval("COLUMN1") %>'></asp:Label>
												<asp:Label ID="Label2" runat="server" Width="80px" Text='<%# Eval("COLUMN2") %>'></asp:Label>
												<asp:Label ID="Label3" runat="server" Width="80px" Text='<%# Eval("COLUMN3") %>'></asp:Label>
											</ItemTemplate>
											<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
											<SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
											<AlternatingItemStyle BackColor="White" />
											<ItemStyle BackColor="#EFF3FB" />
											<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
										</asp:DataList>
									</div>
									COLUMN1<asp:TextBox ID="txbDsTableCol1" runat="server"></asp:TextBox><br />
									COLUMN2<asp:TextBox ID="txbDsTableCol2" runat="server"></asp:TextBox><br />
									COLUMN3<asp:TextBox ID="txbDsTableCol3" runat="server"></asp:TextBox><br />
									<asp:Button ID="btnSetDefaultTable" runat="server" Text="UseDefault" OnClick="btnSetDefaultTable_Click" />
									<asp:Button ID="btnSetDsTable" runat="Server" Text="Add" OnClick="btnSetDsTable_Click" />
								</td>
								<td style="width: 270px">
									文件数据<br />
									<hr />
									DocumentUrl1<br />
									<asp:TextBox ID="txbDsFileCol1" runat="server" Width="260px"></asp:TextBox><br />
									DocumentUrl2<br />
									<asp:TextBox ID="txbDsFileCol2" runat="server" Width="260px"></asp:TextBox><br />
									DocumentUrl3<br />
									<asp:TextBox ID="txbDsFileCol3" runat="server" Width="260px"></asp:TextBox><br />
									<asp:Button ID="btnSetDefaultFile" runat="server" Text="UseDefault" OnClick="btnSetDefaultFile_Click" />
									<asp:Button ID="btnSetDsFile" runat="Server" Text="Set" OnClick="btnSetDsFile_Click" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</form>
</body>
</html>
