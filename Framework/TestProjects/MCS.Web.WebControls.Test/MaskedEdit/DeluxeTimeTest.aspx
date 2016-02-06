<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeTimeTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.MaskedEdit.DeluxeTimeTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="stylesheet" href="maskedEdit.css" type="text/css" />
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="clientEvent"></div>
    <div>
        <table>
            <tr>
                <td style="width:351px">
                    <cc1:DeluxeTime ID="ctrlDeluxeTime" runat="server">
                        <TextStyle BorderColor="White" />
                    </cc1:DeluxeTime>
                    <br />
                    <br />
                    <asp:Button ID="btnGo" runat="server" Text="Set Properties" OnClick="btnGo_Click" />
                    <asp:Button ID="Button1" runat="server" Text="PostBack" /><br />
                    <br />
                    <asp:Button ID="btnGetValue" runat="server" Text="Get Value" OnClick="btnGetValue_Click"   />
                    <asp:TextBox ID="txbGetValue" runat="server" Enabled="False"   ></asp:TextBox>
                    <br />
                    <asp:Label ID="lblGetValue" runat="server"  Text="　　取得日历时间控件的Value值，是DateTime类型，此处引用方式："   ></asp:Label>
                    <asp:Label ID="Label46" runat="server" Text="dateTime.Value.ToString()"   ></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnSetValue" runat="server" Text="Set Value" OnClick="btnSetValue_Click"   />
                    <asp:TextBox ID="txbSetValue" runat="server"  ></asp:TextBox>
                    <br />
                    <asp:Label ID="Label47" runat="server"  Text="　　设置日历时间控件的Value值，必须是有效的Datetime类型，此处引用方式："   ></asp:Label>
                    <asp:Label ID="Label48" runat="server" Text="System.DateTime.Parse(txbSetValue.Text)"    ></asp:Label>
                    <br />
                </td>
                <td>
                    <textarea id="ctrlDeluxeTimeAttributeShow" runat="server" style="width:350px; height:271px"></textarea>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width:350px; height: 433px;">
                <asp:Label ID="lbAutoComplete" runat="server" Text="AutoComplete"  ></asp:Label>
                <asp:CheckBox ID="ckbAutoComplete" runat="server" Text="AutoComplete"   Checked="True" />
                <br />
                <asp:Label ID="lable1" runat="server" Text="是否自动补齐时间"  ></asp:Label>
                <br />
                <br />
                <asp:Label ID="lbIsValidValue" runat="server" Text="IsValidValue"  ></asp:Label>
                <asp:CheckBox ID="ckbIsValidValue" runat="server" Text="IsValidValue"   Checked="True" />
                <br />
                <asp:Label ID="Label2" runat="server" Text="是否启用验证"  ></asp:Label>
                <br />
                <br />
                <asp:Label ID="lbShowButton" runat="server" Text="ShowButton"  ></asp:Label>
                <asp:CheckBox ID="ckbShowButton" runat="server" Text="ShowButton"   Checked="True" />
                <br />
                <asp:Label ID="Label4" runat="server" Text="否提供按钮来选择自定义时间列表,若是则需设置数据源"  ></asp:Label>
                <br />
                    <br />
                    <asp:Label ID="Label42" runat="server" Text="DataSource"  ></asp:Label><br />
                    <asp:Label ID="lbDataSource1" runat="server" Text=".Text"  ></asp:Label>
                    <asp:TextBox ID="txbDataSource1" runat="server" Width="100px"  >11:00:00</asp:TextBox>
					&nbsp;
                    <asp:Button ID="btnSetDataSource" runat="server" Text="Set" OnClick="btnSetDataSource_Click"    /><br />
                    <asp:Label ID="Label32" runat="server"  Text="绑定时间的数据源"  ></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="width:350px; height: 433px;">
                    <asp:Label ID="lbAutoCompleteValue" runat="server" Text="AutoCompleteValue"  ></asp:Label>
                    <asp:TextBox ID="txbAutoCompleteValue" runat="server"  >11:34:56</asp:TextBox>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="提供自动补齐的时间串，不设置则取系统时间"  ></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lbCurrentMessageError" runat="server" Text="CurrentMessageError"  ></asp:Label>
                    <asp:TextBox ID="txbCurrentMessageError" runat="server"  ></asp:TextBox>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="验证时间的提示信息"  ></asp:Label>
                    <br />
                    <br />
                     <asp:Label ID="lbMask" runat="server" Text="Mask"  ></asp:Label>
                    <asp:DropDownList ID="ddlMask" runat="server" Width="150px"  >
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>99:99</asp:ListItem>
                        <asp:ListItem>99:99:99</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="时间格式串"  ></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lbOnFocusCssClass" runat="server" Text="OnFocusCssClass"  ></asp:Label>
                    <asp:DropDownList ID="ddlOnFocusCssClass" runat="server" Width="160px"  >
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>OnFocusCssClass_Demo1</asp:ListItem>
                        <asp:ListItem>OnFocusCssClass_Demo2</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label13" runat="server" Text="得到焦点时文本框的样式"  ></asp:Label>
                    <br />
                    <br />
                     <asp:Label ID="lbOnInvalidCssClass" runat="server" Text="OnInvalidCssClass"  ></asp:Label>
                    <asp:DropDownList ID="ddlOnInvalidCssClass" runat="server" Width="160px"  >
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>OnInvalidCssClass_Demo1</asp:ListItem>
                        <asp:ListItem>OnInvalidCssClass_Demo2</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label15" runat="server" Text="验证不通过时文本框的样式"  ></asp:Label>
                    <br />
                    <br />
                     <asp:Label ID="lbTextCssClass" runat="server" Text="TextCssClass"  ></asp:Label>
                    <asp:DropDownList ID="ddlTextCssClass" runat="server" Width="150px"  >
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>TextCss_Demo1</asp:ListItem>
                        <asp:ListItem>TextCss_Demo2</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label17" runat="server" Text="设置文本框的样式"  ></asp:Label>
                    <br />
                    <br />
                     <asp:Label ID="lbPromptCharacter" runat="server" Text="PromptCharacter"  ></asp:Label>
                    <asp:TextBox ID="txbPromptCharacter" runat="server" Text="*" Width="4px"  ></asp:TextBox>
                    <br />
                    <asp:Label ID="Label19" runat="server" Text="掩码字符"  ></asp:Label><br />
					<br />
					<asp:CheckBox ID="ckb_ClientScript" runat="server" Text="注册客户端事件" /><br />
					<asp:Label ID="Label1" runat="server" Text="输入的值发生变化时出发的客户端事件: OnClientValueChanged"></asp:Label></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
