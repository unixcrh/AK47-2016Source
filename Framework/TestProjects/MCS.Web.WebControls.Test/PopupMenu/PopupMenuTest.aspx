<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMenuTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.PopupMenu.PopupMenuTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<title>PopupMenuTest</title>
	<link href="PopupMenu.css" type="text/css" rel="stylesheet" />
	<script type="text/javascript" language="javascript" type="text/javascript">
		function alertJS() {
			alert("There is no such NodeID");
		}
		function contextMenu() {
			Sys.Application.findComponent("ctrlPopupMenu").showPopupMenu(event.x, event.y);
			event.returnValue = false;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<div>
		<table>
			<tr style="vertical-align: top">
				<td style="width: 400px; height: 100px;">
					<cc1:DeluxeMenu ID="ctrlPopupMenu" runat="server" Orientation="Vertical">
						<Items>
							<cc1:MenuItem ImageUrl="cdrom.gif" Text="RootA" ToolTip="This is RootA">
								<cc1:MenuItem ImageUrl="cdrom.gif" Text="ItemAA" ToolTip="This is ItemAA" Enable="False">
									<cc1:MenuItem Text="ItemAAA" ToolTip="This is ItemAAA">
										<cc1:MenuItem Text="ItemAAAA">
										</cc1:MenuItem>
									</cc1:MenuItem>
									<cc1:MenuItem Text="ItemAAB">
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="ItemAB">
									<cc1:MenuItem ImageUrl="cdrom.gif" Text="ItemABA">
									</cc1:MenuItem>
								</cc1:MenuItem>
							</cc1:MenuItem>
							<cc1:MenuItem NavigateUrl="http://www.google.com" StaticPopOutImageUrl="computer.gif"
								Text="RootB">
								<cc1:MenuItem ImageUrl="cdrom.gif" Text="ItemBA">
									<cc1:MenuItem Text="ItemBAA">
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="ItemBB">
									<cc1:MenuItem Text="ItemBBA">
									</cc1:MenuItem>
									<cc1:MenuItem Text="ItemBBB">
										<cc1:MenuItem Text="ItemBBBA">
										</cc1:MenuItem>
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="ItemBC">
									<cc1:MenuItem Text="ItemBCA">
									</cc1:MenuItem>
								</cc1:MenuItem>
							</cc1:MenuItem>
							<cc1:MenuItem Text="TestSeparator">
								<cc1:MenuItem Text="Item1(3,1)" Enable="False">
									<cc1:MenuItem Text="Item11(3,1,1)">
									</cc1:MenuItem>
									<cc1:MenuItem Text="Item12(3,1,2)">
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="Item2(3,2)">
									<cc1:MenuItem Text="Item21(3,2,1)" Visible="False">
									</cc1:MenuItem>
									<cc1:MenuItem Text="Item22(3,2,2)">
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="Item3(3,3)" IsSeparator="True">
								</cc1:MenuItem>
								<cc1:MenuItem Text="Item4(3,4)">
								</cc1:MenuItem>
								<cc1:MenuItem Text="Item5(3,5)" IsSeparator="True">
								</cc1:MenuItem>
								<cc1:MenuItem Text="Item6(3,6)">
									<cc1:MenuItem Text="Item61(3,6,1)">
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="Item7(3,7)">
									<cc1:MenuItem Text="Item71(3,7,1)">
									</cc1:MenuItem>
									<cc1:MenuItem Text="Item72(3,7,2)">
									</cc1:MenuItem>
									<cc1:MenuItem Text="Item73(3,7,3)">
									</cc1:MenuItem>
								</cc1:MenuItem>
							</cc1:MenuItem>
						</Items>
					</cc1:DeluxeMenu>
				</td>
				<td style="width: 400px; height: 100px;">
					<cc1:DeluxeMenu ID="ctrlPopupMenu2" runat="server" Orientation="Vertical">
						<Items>
							<cc1:MenuItem Text="New Root">
								<cc1:MenuItem Text="New Item">
									<cc1:MenuItem Text="New Item">
										<cc1:MenuItem Text="New Item">
											<cc1:MenuItem Text="New Item">
												<cc1:MenuItem Text="New Item">
													<cc1:MenuItem Text="New Item">
														<cc1:MenuItem Text="New Item">
															<cc1:MenuItem Text="New Item">
															</cc1:MenuItem>
														</cc1:MenuItem>
														<cc1:MenuItem Text="New Item">
														</cc1:MenuItem>
													</cc1:MenuItem>
													<cc1:MenuItem Text="New Item">
													</cc1:MenuItem>
												</cc1:MenuItem>
												<cc1:MenuItem Text="New Item">
												</cc1:MenuItem>
											</cc1:MenuItem>
											<cc1:MenuItem Text="New Item">
											</cc1:MenuItem>
										</cc1:MenuItem>
										<cc1:MenuItem Text="New Item">
										</cc1:MenuItem>
									</cc1:MenuItem>
									<cc1:MenuItem Text="New Item">
									</cc1:MenuItem>
								</cc1:MenuItem>
								<cc1:MenuItem Text="New Item">
								</cc1:MenuItem>
							</cc1:MenuItem>
							<cc1:MenuItem Text="New Root">
							</cc1:MenuItem>
						</Items>
					</cc1:DeluxeMenu>
				</td>
			</tr>
		</table>
		<asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" />
		<asp:Button ID="Button1" runat="server" Text="postback" />
		<asp:TextBox ID="TextBox1" runat="server" oncontextmenu="contextMenu()" Text="将StaticDisplayLevels设为0，然后右键点击我"
			Width="280px"></asp:TextBox>
		<br />
		<br />
		<hr />
		主控件
		<table>
			<tr style="vertical-align: top">
				<td style="width: 380px">
					Orientation
					<asp:DropDownList ID="ddlOrientation" runat="server">
						<asp:ListItem>Vertical</asp:ListItem>
						<asp:ListItem>Horizontal</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						一级菜单排列方向，默认值为Vertical</label>
					<br />
					<asp:CheckBox ID="ckbIsImageIndent" runat="server" Text="IsImageIndent" TextAlign="left" /><br />
					<label style="font-size: small; font-style: italic">
						菜单条目前的图标是否缩进，默认为否</label>
					<br />
					<asp:CheckBox ID="ckbMultiSelect" runat="server" Text="MultiSelect" TextAlign="left" /><br />
					<label style="font-size: small; font-style: italic">
						是否是多选，默认为false</label>
					<br />
					<asp:CheckBox ID="ckbHasControlSeparator" runat="server" Text="HasControlSeparator"
						TextAlign="left" /><br />
					<label style="font-size: small; font-style: italic">
						是否处理分割线，默认为false</label>
					<br />
					StaticDisplayLevels
					<asp:TextBox ID="txbStaticDisplayLevels" runat="server" Width="100px">1</asp:TextBox><br />
					<label style="font-size: small; font-style: italic">
						静态菜单级别，默认为1</label>
					<br />
					SubMenuIndent
					<asp:TextBox ID="txbSubMenuIndent" runat="server" Width="100px">10</asp:TextBox><br />
					<label style="font-size: small; font-style: italic">
						静态菜单中，子菜单缩进长度，默认值为10</label>
					<br />
					TextHeadWidth
					<asp:TextBox ID="txbTextHeadWidth" runat="server" Width="100px">5</asp:TextBox><br />
					<label style="font-size: small; font-style: italic">
						菜单文本前的空格宽度，默认为5</label>
					<br />
					ItemFontWidth
					<asp:TextBox ID="txbItemFontWidth" runat="server" Width="100px">150</asp:TextBox><br />
					<label style="font-size: small; font-style: italic">
						菜单项文字宽度，默认值为150</label>
				</td>
				<td style="width: 380px">
					Target
					<asp:DropDownList ID="ddlTarget" runat="server">
						<asp:ListItem>_blank</asp:ListItem>
						<asp:ListItem>_parent</asp:ListItem>
						<asp:ListItem>_search</asp:ListItem>
						<asp:ListItem>_self</asp:ListItem>
						<asp:ListItem>_top</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						打开链接目标,打开目标的不同方式</label>
					<br />
					<br />
					StaticPopOutImageUrl
					<asp:DropDownList ID="ddlStaticPopOutImageUrl" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>/PopupMenu/computer.gif</asp:ListItem>
						<asp:ListItem>~/PopupMenu/computer.gif</asp:ListItem>
						<asp:ListItem>.\component.gif</asp:ListItem>
						<asp:ListItem>component.gif</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						弹出静态菜单图标</label>
					<br />
					DynamicPopOutImageUrl
					<asp:DropDownList ID="ddlDynamicPopOutImageUrl" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>/PopupMenu/computer.gif</asp:ListItem>
						<asp:ListItem>~/PopupMenu/computer.gif</asp:ListItem>
						<asp:ListItem>.\component.gif</asp:ListItem>
						<asp:ListItem>component.gif</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						弹出动态菜单图标</label>
					<br />
					<br />
					ItemCssClass
					<asp:DropDownList ID="ddlItemCssClass" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>ItemCssClass_Demo1</asp:ListItem>
						<asp:ListItem>ItemCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						菜单条目的CssClass</label>
					<br />
					HoverItemCssClass
					<asp:DropDownList ID="ddlHoverItemCssClass" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>HoverItemCssClass_Demo1</asp:ListItem>
						<asp:ListItem>HoverItemCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						鼠标移到菜单条目的CssClass</label>
					<br />
					SelectedItemCssClass
					<asp:DropDownList ID="ddlSelectedItemCssClass" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>SelectedItemCssClass_Demo1</asp:ListItem>
						<asp:ListItem>SelectedItemCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						菜单条目选择后的CssClass</label>
					<br />
					ImageColCssClass
					<asp:DropDownList ID="ddlImageColCssClass" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>ImageColCssClass_Demo1</asp:ListItem>
						<asp:ListItem>ImageColCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<label style="font-size: small; font-style: italic">
						菜单条目前的图标表格CssClass</label>
				</td>
			</tr>
		</table>
		<hr />
		菜单项
		<br />
		NodeID
		<asp:TextBox ID="txbItemNodeID" runat="server" Width="100px">2,2,1</asp:TextBox>
		<asp:Button ID="btnGetMenuItem" runat="server" Text="GetMenuItem" OnClick="btnGetMenuItem_Click" />
		<asp:Button ID="btnSetMenuItem" runat="server" Text="SetMenuItem" OnClick="btnSetMenuItem_Click"
			Enabled="False" />
		<br />
		<label style="font-size: small; font-style: italic">
			节点ID，'2,2,1'表示第一级菜单第二项，第二级菜单第二项，第三级菜单第一项，请勿超过5级菜单</label><br />
		<table>
			<tr style="vertical-align: top">
				<td style="width: 30px">
				</td>
				<td>
					<hr />
					<table>
						<tr style="vertical-align: top">
							<td style="width: 330px">
								<asp:CheckBox ID="ckbItemEnable" runat="server" Text="Enable" TextAlign="left" /><br />
								<label style="font-size: small; font-style: italic">
									该菜单项是否可用</label>
								<br />
								<asp:CheckBox ID="ckbItemIsSeparator" runat="server" Text="IsSeparator" TextAlign="left" /><br />
								<label style="font-size: small; font-style: italic">
									是否是分隔线</label>
								<br />
								<asp:CheckBox ID="ckbItemVisible" runat="server" Text="Visible" TextAlign="left" /><br />
								<label style="font-size: small; font-style: italic">
									该菜单项是否可见</label>
								<br />
								<asp:CheckBox ID="ckbItemSelected" runat="server" Text="Selected" TextAlign="left" /><br />
								<label style="font-size: small; font-style: italic">
									菜单条目是否被选中</label>
								<br />
								Text
								<asp:TextBox ID="txbItemText" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单条目文本</label>
								<br />
								Value
								<asp:TextBox ID="txbItemValue" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单条目值</label>
								<br />
								Target
								<asp:TextBox ID="txbItemTarget" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									打开链接目标，打开目标的不同方式</label>
							</td>
							<td style="width: 330px">
								NavigateUrl
								<asp:TextBox ID="txbItemNavigateUrl" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单条目链接Url</label>
								<br />
								ImageUrl
								<asp:TextBox ID="txbItemImageUrl" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单条目前图标(不设置则使用默认图标)，'cdrom.gif'，'color.gif'</label>
								<br />
								StaticPopOutImageUrl
								<asp:TextBox ID="txbItemStaticPopOutImageUrl" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									静态弹出菜单图标路径，'computer.gif'，'component.gif'</label>
								<br />
								DynamicPopOutImageUrl
								<asp:TextBox ID="txbItemDynamicPopOutImageUrl" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									动态弹出菜单图标路径，'computer.gif'，'component.gif'</label>
								<br />
								ToolTip
								<asp:TextBox ID="txbItemToolTip" runat="server" Width="100px"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单条目提示</label>
							</td>
							<td>
								Previous
								<asp:TextBox ID="txbItemPrevious" runat="server" Width="100px" Enabled="false"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单项的前一项</label>
								<br />
								Next
								<asp:TextBox ID="txbItemNext" runat="server" Width="100px" Enabled="false"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单项的后一项</label>
								<br />
								Parent
								<asp:TextBox ID="txbItemParent" runat="server" Width="100px" Enabled="false"></asp:TextBox><br />
								<label style="font-size: small; font-style: italic">
									菜单项的父项</label>
								<br />
								Children
								<br />
								<label style="font-size: small; font-style: italic">
									菜单项的子项集</label><br />
								&nbsp; &nbsp; &nbsp;&nbsp;
								<textarea id="textAreaChildren" runat="server" style="width: 160px; height: 100px;"
									readonly="readOnly"></textarea>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<hr />
	</div>
	<asp:Literal runat="server" ID="LiteralJS"></asp:Literal>
	</form>
</body>
</html>
