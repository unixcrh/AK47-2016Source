<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeDateTimeTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DateTimeControl.DeluxeDateTimeTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>DeluxeDataTime</title>
	<link type="text/css" href="DateTime.css" rel="stylesheet" />
	<script type="text/javascript">
		function onClientValueChanged(sender) {
			$get("dateValue").innerText = sender.get_value();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="clientEvent">
	</div>
	<div>
		<table>
			<tr>
				<td style="width: 400px">
					<cc1:DeluxeDateTime ID="ctrlDeluxeDateTime" runat="server" OnClientValueChanged="onClientValueChanged" />
					<br />
					<span id="dateValue" />
					<br />
					<asp:Button ID="btnGo" runat="server" Text="Set Properties" OnClick="btnGo_Click" />
					<br />
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
					<textarea id="ctrlDeluxeDateTimeAttributeShow" runat="server" style="width: 400px;
						height: 260px">
				</textarea>
				</td>
			</tr>
		</table>
		<asp:CheckBox ID="ckbReadOnly" runat="server" Text="ReadOnly" Checked="True" />
		<br />
		<asp:Label ID="Label7" runat="server" Text="是否只读"></asp:Label>
		<br />
		<br />
		<hr />
		<table>
			<tr>
				<td style="width: 400px">
					<asp:Label ID="lbName" runat="server" Text="日期部分"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbDateAutoComplete" runat="server" Text="DateAutoComplete" Checked="True" />
					<br />
					<asp:Label ID="Label1" runat="server" Text="是否自动补齐日期"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbIsValidDateValue" runat="server" Text="IsValidDateValue" Checked="True" />
					<br />
					<asp:Label ID="Label5" runat="server" Text="是否启用日期验证"></asp:Label>&nbsp;<br />
					<br />
					<asp:Label ID="Label43" runat="server" Text="DateTextCssClass"></asp:Label>
					<asp:DropDownList ID="ddlDateTextCssClass" runat="server" Width="151px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>TimeTextCss_Demo1</asp:ListItem>
						<asp:ListItem>TimeTextCss_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label10" runat="server" Text="日历输入框的Css"></asp:Label><br />
					<br />
					<asp:Label ID="lbDateAutoCompleteValue" runat="server" Text="DateAutoCompleteValue"></asp:Label>
					<asp:TextBox ID="txbDateAutoCompleteValue" runat="server" Text=""></asp:TextBox>
					<br />
					<asp:Label ID="lable10" runat="server" Text="提供自动补齐的时间串，不设置则取系统日期"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbDateCurrentMessageError" runat="server" Text="DateCurrentMessageError"></asp:Label>
					<asp:TextBox ID="txbDateCurrentMessageError" runat="server" Text="请检查输入日期"></asp:TextBox>
					<br />
					<asp:Label ID="Label16" runat="server" Text="验证日期的提示信息"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbDatePromptCharacter" runat="server" Text="DatePromptCharacter"></asp:Label>
					<asp:TextBox ID="txbDatePromptCharacter" runat="server" Text="_" Width="1px"></asp:TextBox>
					<br />
					<asp:Label ID="Label17" runat="server" Text="日期掩码字符"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label38" runat="server" Text="OnFocusDateCssClass"></asp:Label>&nbsp;
					<asp:DropDownList ID="ddlOnFocusDateCssClass" runat="server" Width="220px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnFocusDateCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnFocusDateCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label22" runat="server" Text="日期得到焦点时文本框的样式"></asp:Label><br />
					<br />
					<asp:Label ID="Label39" runat="server" Text="OnInvalidDateCssClass"></asp:Label>
					<asp:DropDownList ID="ddlOnInvalidDateCssClass" runat="server" Width="220px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnInvalidDateCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnInvalidDateCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label24" runat="server" Text="日期验证不通过时文本框的样式"></asp:Label><br />
				</td>
				<td style="width: 400px">
					<asp:Label ID="lbName2" runat="server" Text="时间部分"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbTimeAutoComplete" runat="server" Text="TimeAutoComplete" Checked="True" />
					<br />
					<asp:Label ID="Label9" runat="server" Text="是否自动补齐时间"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbIsValidTimeValue" runat="server" Text="IsValidTimeValue" Checked="True" />
					<br />
					<asp:Label ID="Label6" runat="server" Text="是否启用时间验证"></asp:Label>&nbsp;<br />
					<br />
					<asp:Label ID="Label44" runat="server" Text="TimeTextCssClass"></asp:Label>
					<asp:DropDownList ID="ddlTimeTextCssClass" runat="server" Width="150px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>DateTextCssClass_Demo1</asp:ListItem>
						<asp:ListItem>DateTextCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label11" runat="server" Text="时间输入框的Css"></asp:Label><br />
					<br />
					<asp:Label ID="lbTimeAutoCompleteValue" runat="server" Text="DateAutoCompleteValue"></asp:Label>
					<asp:TextBox ID="txbTimeAutoCompleteValue" runat="server" Text=""></asp:TextBox>
					<br />
					<asp:Label ID="lable11" runat="server" Text="提供自动补齐的时间串，不设置则取系统时间"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbTimeCurrentMessageError" runat="server" Text="TimeCurrentMessageError"></asp:Label>
					<asp:TextBox ID="txbTimeCurrentMessageError" runat="server" Text="请检查输入时间"></asp:TextBox>
					<br />
					<asp:Label ID="Label18" runat="server" Text="验证时间的提示信息"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbTimePromptCharacter" runat="server" Text="TimePromptCharacter"></asp:Label>
					<asp:TextBox ID="txbTimePromptCharacter" runat="server" Text="_" Width="1px"></asp:TextBox>
					<br />
					<asp:Label ID="Label20" runat="server" Text="掩码字符"></asp:Label>&nbsp;<br />
					<br />
					<asp:Label ID="Label40" runat="server" Text="OnTimeFocusCssClass"></asp:Label>
					<asp:DropDownList ID="ddlOnTimeFocusCssClass" runat="server" Width="220px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnTimeFocusCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnTimeFocusCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label26" runat="server" Text="得到焦点时时间文本框的样式"></asp:Label><br />
					<br />
					<asp:Label ID="Label41" runat="server" Text="OnTimeInvalidCssClass"></asp:Label>
					<asp:DropDownList ID="ddlOnTimeInvalidCssClass" runat="server" Width="220px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnTimeInvalidCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnTimeInvalidCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label28" runat="server" Text="验证不通过时时间文本框的样式"></asp:Label><br />
				</td>
			</tr>
		</table>
		<table>
			<tr style="vertical-align: top">
				<td style="width: 400px">
					<asp:CheckBox ID="ckbAnimated" runat="server" Text="Animated" Checked="True" />
					<br />
					<asp:Label ID="lable1" runat="server" Text="设置日历月份转换的动画效果"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbEnabledOnClient" runat="server" Text="EnabledOnClient" Checked="True" />
					<br />
					<asp:Label ID="Label2" runat="server" Text="是否启用日历功能"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbIsOnlyCurrentMonth" runat="server" Text="IsOnlyCurrentMonth"
						Checked="True" />
					<br />
					<asp:Label ID="Label4" runat="server" Text="是否只显示当月"></asp:Label>
					<br />
					<br />
					<asp:CheckBox ID="ckbIsComplexHeader" runat="server" Text="IsComplexHeader" Checked="True" />
					<br />
					<asp:Label ID="Label3" runat="server" Text="是否提供下拉框快捷选项"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label30" runat="server" Text="DateImageCssClass"></asp:Label>
					<asp:DropDownList ID="ddlDateImageCssClass" runat="server" Width="230px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>DateImageCssClass_Demo1</asp:ListItem>
						<asp:ListItem>DateImageCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label12" runat="server" Text="图片的CssClass"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label31" runat="server" Text="DateImageStyle"></asp:Label>
					<asp:DropDownList ID="ddlDateImageStyle" runat="server">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>width: 10px; color:yellow; text-align:left</asp:ListItem>
						<asp:ListItem>width: 20px; font-size:larger; text-align:center</asp:ListItem>
					</asp:DropDownList>
					<asp:Label ID="Label13" runat="server" Text="图片的Style"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label34" runat="server" Text="DateImageUrl"></asp:Label>
					<asp:DropDownList ID="ddlDateImageUrl" runat="server" Width="150px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>end.jpg</asp:ListItem>
						<asp:ListItem>/DateTime/end.jpg</asp:ListItem>
						<asp:ListItem>..\PopupCalendar\triangle.jpg</asp:ListItem>
						<asp:ListItem>~/PopupCalendar/heart.jpg</asp:ListItem>
						<asp:ListItem>http://10.1.1.98/DeluxeCalendar/stop.jpg</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label14" runat="server" Text="按钮图片的Src"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label35" runat="server" Text="TimeInvalidCssClass"></asp:Label>
					<asp:DropDownList ID="ddlTimeInvalidCssClass" runat="server" Width="150px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>OnTimeInvalidCssClass_Demo1</asp:ListItem>
						<asp:ListItem>OnTimeInvalidCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="Label29" runat="server" Text="验证不通过时时间文本框的样式"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label36" runat="server" Text="PopupCalendarCssClass"></asp:Label>
					<asp:DropDownList ID="ddlPopupCalendarCssClass" runat="server" Width="150px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>PopupCalendarCssClass_Demo1</asp:ListItem>
						<asp:ListItem>PopupCalendarCssClass_Demo2</asp:ListItem>
					</asp:DropDownList>
					<asp:Label ID="Label33" runat="server" Text="日历的样式，不填为默认样式"></asp:Label>
					<br />
					<br />
				</td>
				<td style="width: 400px">
					<asp:CheckBox ID="ckbShowButton" runat="server" Text="ShowButton" Checked="True" />
					<br />
					<asp:Label ID="Label8" runat="server" Text="是否提供按钮来选择自定义时间列表,若是则需设置数据源"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbTimeMask" runat="server" Text="TimeMask"></asp:Label>
					<asp:TextBox ID="txbTimeMask" runat="server" Text="99:99"></asp:TextBox>
					<br />
					<asp:Label ID="Label19" runat="server" Text="时间的分隔符"></asp:Label>
					<br />
					<br />
					<asp:Label ID="Label37" runat="server" Text="FirstDayOfWeek"></asp:Label>&nbsp;
					<asp:DropDownList ID="ddlFirstDayOfWeek" runat="server" Width="150px">
						<asp:ListItem>Default</asp:ListItem>
						<asp:ListItem>Monday</asp:ListItem>
						<asp:ListItem>Tuesday</asp:ListItem>
						<asp:ListItem>Wednesday</asp:ListItem>
						<asp:ListItem>Thursday</asp:ListItem>
						<asp:ListItem>Friday</asp:ListItem>
						<asp:ListItem>Saturday</asp:ListItem>
						<asp:ListItem>Sunday</asp:ListItem>
					</asp:DropDownList>
					<br />
					<asp:Label ID="lbFirstDayOfWeek" runat="server" Text="自定义第一列是从周几开始"></asp:Label>&nbsp;<br />
					<br />
					<asp:Label ID="Label42" runat="server" Text="DataSource"></asp:Label><br />
					<asp:Label ID="lbDataSource1" runat="server" Text=".Text"></asp:Label>
					<asp:TextBox ID="txbDataSource1" runat="server" Width="100px">11:00:00</asp:TextBox>
					&nbsp;
					<asp:Button ID="btnSetDataSource" runat="server" Text="Set" OnClick="btnSetDataSource_Click" /><br />
					<asp:Label ID="Label32" runat="server" Text="绑定时间的数据源"></asp:Label>
					<br />
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td style="width: 803px">
					<asp:Label ID="lbname3" runat="server" Text="触发事件"></asp:Label>
					<br />
					<asp:CheckBox ID="ckb_ClientScript" runat="server" /><br />
					<asp:Label ID="lbOnClientHiding" runat="server" Text="OnClientHiding"></asp:Label>
					<asp:Label ID="lable20" runat="server" Text="隐藏日历时触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientHidden" runat="server" Text="OnClientHidden"></asp:Label>
					<asp:Label ID="Label21" runat="server" Text="隐藏日历后触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientShowing" runat="server" Text="OnClientShowing"></asp:Label>
					<asp:Label ID="Label23" runat="server" Text="弹出日历时触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientShown" runat="server" Text="OnClientShown"></asp:Label>
					<asp:Label ID="Label25" runat="server" Text="弹出日历后触发的客户端事件"></asp:Label>
					<br />
					<br />
					<asp:Label ID="lbOnClientDateSelectionChanged" runat="server" Text="OnClientDateSelectionChanged"></asp:Label>
					<asp:Label ID="Label27" runat="server" Text="日期选择变化后触发的客户端事件"></asp:Label>
					<br />
					<br />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
