<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericInput.aspx.cs" Inherits="GenericInput" %>

<%@ Register Assembly="DeluxeWorks.Web.WebControls" Namespace="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script language="javascript">
    function BeforeItemSelect()
    {
        alert(this._selectIndex);
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:GenericInput ID="GenericInput1" runat="server" Width="145px">
        </cc1:GenericInput>
        &nbsp; &nbsp;
        <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="btnDataBind" runat="server" OnClick="btnDataBind_Click" Text="数据绑定" /><br />
        <br />
        <asp:CheckBox ID="chkReadOnly" runat="server" AutoPostBack="True" OnCheckedChanged="chkReadOnly_CheckedChanged"
            Text="只读" />
        &nbsp; &nbsp;&nbsp;
        <asp:CheckBox ID="chkAutoPostBack" runat="server" AutoPostBack="True" OnCheckedChanged="chkAutoPostBack_CheckedChanged"
            Text="自动PostBack" /><br />
        <br />
        输入框背景色：<asp:TextBox ID="txtInputBgColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnSetInputBgColor" runat="server" OnClick="btnSetInputBgColor_Click"
            Text="设置" Width="54px" /><br />
        输入框文本颜色：<asp:TextBox ID="txtForeColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnSetForeColor" runat="server" OnClick="btnSetForeColor_Click"
            Text="设置" Width="54px" /><br />
        <br />
        项目文本：<asp:TextBox ID="txtItemText" runat="server"></asp:TextBox>
        <asp:CheckBox ID="chkSelected" runat="server" Text="默认选中" /><br />
        项目值：<asp:TextBox ID="txtItemValue" runat="server"></asp:TextBox>
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Width="54px" />
        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="清空项目" /><br />
        <br />
        边框颜色：<asp:TextBox ID="txtBorderColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnSetBorderColor" runat="server" OnClick="btnSetBorderColor_Click"
            Text="设置" Width="54px" /><br />
        项目字体颜色：<asp:TextBox ID="txtFontColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnSetFontColor" runat="server" OnClick="btnSetFontColor_Click" Text="设置"
            Width="54px" /><br />
        <br />
        鼠标移动到项目字体颜色：<asp:TextBox ID="txtSelectItemFontColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnSelectItemFontColor" runat="server" OnClick="btnSelectItemFontColor_Click"
            Text="设置" Width="54px" /><br />
        鼠标移动到项目背景颜色：<asp:TextBox ID="txtHoveBGColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnHoveBGColor" runat="server" OnClick="btnHoveBGColor_Click" Text="设置"
            Width="54px" /><br />
        <br />
        下拉箭头区域背景色：<asp:TextBox ID="txtBgColor" runat="server"></asp:TextBox>
        <asp:Button ID="btnSetBgColor" runat="server" OnClick="btnSetBgColor_Click" Text="设置"
            Width="54px" /><br />
        <br />
        选择项目的脚本(选择完成前)：onSelectItem<br />
        <asp:TextBox ID="txtItemSelectScript" runat="server" Height="45px" TextMode="MultiLine"
            Width="484px"></asp:TextBox>
        <asp:Button ID="btnItemSelectScript" runat="server" OnClick="btnItemSelectScript_Click"
            Text="设置" Width="54px" /><br />
        选择项目的脚本(选择完成后)：onSelectedItem<br />
        <asp:TextBox ID="txtItemSelectedScript" runat="server" Height="49px" TextMode="MultiLine"
            Width="484px"></asp:TextBox>
        <asp:Button ID="btnItemSelectedScript" runat="server" OnClick="btnItemSelectedScript_Click"
            Text="设置" Width="54px" /><br />
        文本变更后的脚本：onTextChange<br />
        <asp:TextBox ID="txtOnTextChange" runat="server" Height="52px" TextMode="MultiLine"
            Width="484px"></asp:TextBox>
        <asp:Button ID="btnOnTextChange" runat="server" OnClick="btnOnTextChange_Click" Text="设置"
            Width="54px" />
    </form>
</body>
</html>
l