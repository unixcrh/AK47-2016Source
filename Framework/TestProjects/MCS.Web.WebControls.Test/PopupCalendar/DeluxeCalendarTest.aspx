<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeCalendarTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.PopupCalendar.DeluxeCalendarTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>DeluxeCalenderTest</title>
	<link type="text/css" rel="Stylesheet" href="CalendarControl.css" />
	<style type="text/css">
		.text
		{
			font-color: red;
		}
	</style>
	<script type="text/javascript">
		function onClientValueChanged(sender) {
			$get("dateValue").innerText = sender.get_value();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<div>
		<div id="clientEvent">
		</div>
		<table>
			<tr>
				<td style="width: 400px">
					<cc1:DeluxeCalendar ID="ctrlDeluxeCalendar" runat="server" AutoComplete="false" OnClientValueChanged="onClientValueChanged">
					</cc1:DeluxeCalendar>
					<br />
					<span id="dateValue" />
					<br />
					<asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" />
					<br />
					<asp:Button ID="btnGetValue" runat="server" Text="Get Value" OnClick="btnGetValue_Click" />
					<asp:TextBox ID="txbGetValue" runat="server" Enabled="False"></asp:TextBox>
					<br />
					<asp:Label ID="lblGetValue" runat="server" Text="　　取得日历时间控件的Value值，是DateTime类型，此处引用方式："></asp:Label>
					<asp:Label ID="Label46" runat="server" Text="dateTime.Value.ToString()"></asp:Label>
					<br />
					<br />
					<asp:Button ID="btnSetValue" runat="server" Text="Set Value" OnClick="btnSetValue_Click" />
					<asp:TextBox ID="txbSetValue" runat="server"></asp:TextBox>
					<br />
					<asp:Label ID="Label47" runat="server" Text="　　设置日历时间控件的Value值，必须是有效的Datetime类型，此处引用方式："></asp:Label>
					<asp:Label ID="Label48" runat="server" Text="System.DateTime.Parse(txbSetValue.Text)"></asp:Label>
					<br />
				</td>
				<td style="width: 400px">
					<textarea id="ctrlDeluxeCalendarAttributeShow" runat="server" style="width: 400px;
						height: 260px"></textarea>
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td style="width: 400px">
					<asp:Label ID="lbEnabledOnClient" runat="server" Text="EnabledOnClient" Width="111px"></asp:Label>
					<asp:CheckBox ID="ckbEnabledOnClient" runat="server" Text="EnabledOnClient" TextAlign="Right"
						Checked="true" />
					<br />
					<asp:Label ID="lb" runat="server" Text="是否启用日历功能" Width="130px"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbAutoComplete" runat="server" Text="AutoComplete" Width="111px"></asp:Label>
					<asp:CheckBox ID="ckbAutoComplete" runat="server" Text="AutoComplete" TextAlign="Right"
						Checked="true" />
					<br />
					<asp:Label ID="Label1" runat="server" Text="是否自动补齐日期" Width="130px"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbIsComplexHeader" runat="server" Text="IsComplexHeader" Width="111px"></asp:Label>
					<asp:CheckBox ID="ckbIsComplexHeader" runat="server" Text="IsComplexHeader" TextAlign="Right"
						Checked="true" />
					<br />
					<asp:Label ID="Label2" runat="server" Text="是否提供下拉框快捷选项" Width="187px"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbIsOnlyCurrentMonth" runat="server" Text="IsOnlyCurrentMonth" Width="111px"></asp:Label>
					<asp:CheckBox ID="ckbIsOnlyCurrentMonth" runat="server" Text="IsOnlyCurrentMonth"
						TextAlign="Right" Checked="true" />
					<br />
					<asp:Label ID="Label3" runat="server" Text="是否只显示当月" Width="130px"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbIsValidValue" runat="server" Text="IsValidValue" Width="111px"></asp:Label>
					<asp:CheckBox ID="ckbIsValidValue" runat="server" Text="IsValidValue" TextAlign="Right"
						Checked="true" />
					<br />
					<asp:Label ID="Label4" runat="server" Text="是否启用验证" Width="130px"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbAnimated" runat="server" Text="Animated" Width="111px"></asp:Label>
					<asp:CheckBox ID="ckbAnimated" runat="server" Text="Animated" TextAlign="Right" Checked="true" />
					<br />
					<asp:Label ID="Label7" runat="server" Text="是否设置日历月份转换的动画效果" Width="242px"></asp:Label>
					<br />
				</td>
				<td style="width: 400px">
					<asp:Label ID="Label6" runat="server" Text="AutoCompleteValue" Width="150px"></asp:Label>
					<br />
					<asp:TextBox ID="txbAutoCompleteValue" runat="server"></asp:TextBox>
					<br />
					<asp:Label ID="Label17" runat="server" Text="提供自动补齐的时间串，不设置则取系统日期" Width="325px"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbCurrentMessageError" runat="server" Text="CurrentMessageError" Width="150px"></asp:Label>
					<br />
					<asp:TextBox ID="txbCurrentMessageError" runat="server" Text="请检查输入信息" Width="150px"></asp:TextBox>
					<br />
					<asp:Label ID="Label18" runat="server" Text="验证日期的提示信息" Width="150px"></asp:Label>&nbsp;<br />
					<br />
					&nbsp;<br />
					<br />
					<asp:Label ID="lbPromptCharacter" runat="server" Text="PromptCharacter" Width="150px"></asp:Label>
					<br />
					<asp:TextBox ID="txbPromptCharacter" runat="server" Text="_" Width="150px"></asp:TextBox>
					<br />
					<asp:Label ID="Label20" runat="server" Text="掩码字符" Width="150px"></asp:Label>
					<br />
					<br />
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td style="width: 400px">
					<br />
					<asp:Label ID="lbFirstDayOfWeek" runat="server" Text="FirstDayOfWeek" Width="114px"></asp:Label>
					<asp:DropDownList ID="ddlFirstDayOfWeek" runat="server" Width="130px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>Monday</asp:ListItem>
						<asp:ListItem>Tuesday</asp:ListItem>
						<asp:ListItem>Wednesday</asp:ListItem>
						<asp:ListItem>Tursday</asp:ListItem>
						<asp:ListItem>Friday</asp:ListItem>
						<asp:ListItem>Saturday</asp:ListItem>
						<asp:ListItem>Sunday</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label5" runat="server" Text="自定义第一列是从周几开始" Width="194px"></asp:Label>
					<br />
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td style="width: 400px">
					<asp:Label ID="lbOnInvalidCssClass" runat="server" Text="OnInvalidCssClass"></asp:Label>
					<asp:DropDownList ID="ddlOnInvalidCssClass" runat="server" Width="180px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnInvalidCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnInvalidCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label8" runat="server" Text="验证不通过时文本框的样式"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnFocusCssClass" runat="server" Text="OnFocusCssClass"></asp:Label>
					<asp:DropDownList ID="ddlOnFocusCssClass" runat="server" Width="180px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnInvalidCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnInvalidCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label10" runat="server" Text="得到焦点时文本框的样式"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbImageCssClass" runat="server" Text="ImageCssClass"></asp:Label>
					<asp:DropDownList ID="ddlImageCssClass" runat="server" Width="180px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>ImageCssClass_Demo1</asp:ListItem>
						<asp:ListItem>ImageCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label11" runat="server" Text="图片的CssClass"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbTextCssClass" runat="server" Text="TextCssClass"></asp:Label>
					<asp:DropDownList ID="ddlTextCssClass" runat="server" Width="180px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>TextCssClass_Demo1</asp:ListItem>
						<asp:ListItem>TextCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<br />
					<asp:Label ID="lbImageUrl" runat="server" Text="ImageUrl"></asp:Label>
					<asp:DropDownList ID="ddlImageUrl" runat="server" Width="200px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>..\WordPrint\end.jpg</asp:ListItem>
						<asp:ListItem>~/PopupCalendar/heart.jpg</asp:ListItem>
						<asp:ListItem>.\trianger.jpg</asp:ListItem>
						<asp:ListItem>http://10.1.1.98/DeluxeCalendar/stop.jpg</asp:ListItem>
					</asp:DropDownList>
					<br />
					<br />
					<asp:Label ID="lbImageStyle" runat="server" Text="ImageStyle"></asp:Label>
					<asp:DropDownList ID="ddlImageStyle" runat="server" Width="180px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>CssClass_Demo1</asp:ListItem>
						<asp:ListItem>CssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<br />
					<asp:Label ID="lbCssClass" runat="server" Text="CssClass"></asp:Label>
					<asp:DropDownList ID="ddlCssClass" runat="server" Width="180px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>CssClass_Demo1</asp:ListItem>
						<asp:ListItem>CssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
				</td>
				<td style="width: 400px">
					<asp:CheckBox ID="ckb_ClientScript" runat="server" Text="注册客户断脚本" /><br />
					<asp:Label ID="lbOnClientDateSelectionChanged" runat="server" Text="OnClientDateSelectionChanged"></asp:Label>
					<br />
					<asp:Label ID="Label12" runat="server" Text="日期选择变化后触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientHidden" runat="server" Text="OnClientHidden"></asp:Label>
					<br />
					<asp:Label ID="Label13" runat="server" Text="隐藏日历后触发的客户端事件"></asp:Label>&nbsp;<br />
					<br />
					<asp:Label ID="lbOnClientHiding" runat="server" Text="OnClientHiding"></asp:Label>
					<br />
					<asp:Label ID="Label14" runat="server" Text="隐藏日历时触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientShowing" runat="server" Text="OnClientShowing"></asp:Label>
					<br />
					<asp:Label ID="Label15" runat="server" Text="弹出日历时触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientShown" runat="server" Text="OnClientShown"></asp:Label>
					<br />
					<asp:Label ID="Label16" runat="server" Text="弹出日历时后触发的客户端事件"></asp:Label>
					<br />
					<br />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
