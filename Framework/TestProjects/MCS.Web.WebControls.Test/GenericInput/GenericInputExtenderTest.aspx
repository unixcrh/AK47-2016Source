<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenericInputExtenderTest.aspx.cs" Inherits="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls.Test.GenericInput.GenericInputExtenderTest" %>

<%@ Register Assembly="DeluxeWorks.Web.WebControls" Namespace="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>GenericInputExtenderTest</title>
    <link type="text/css" href="GenericInput.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr style="vertical-align:top">
                <td>
                    HighlightBorderColor
                    <asp:DropDownList ID="ddlHighlightBorderColor" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>Red</asp:ListItem>
                        <asp:ListItem>Blue</asp:ListItem>
                        <asp:ListItem>Yellow</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　控件边框的颜色，默认值为　#2353B2</label><br />
                    DropArrowBackgroundColor
                    <asp:DropDownList ID="ddlDropArrowBackgroundColor" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>Red</asp:ListItem>
                        <asp:ListItem>Blue</asp:ListItem>
                        <asp:ListItem>Yellow</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　下拉箭头区域的背景色，默认值为　#C6E1FF</label><br />
                    ItemCssClass
                    <asp:DropDownList ID="ddlItemCssClass" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>ItemCssClass_Demo1</asp:ListItem>
                        <asp:ListItem>ItemCssClass_Demo2</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　选择项目默认字体颜色，默认值为　#003399</label><br />
                    ItemHoverCssClass
                    <asp:DropDownList ID="ddlItemHoverCssClass" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>ItemHoverCssClass_Demo1</asp:ListItem>
                        <asp:ListItem>ItemHoverCssClass_Demo2</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　鼠标移动到选项项目上时的字体颜色，默认值为　#FFE6A0</label><br />
                    Items<br />
                    <label style="font-size:small; font-style:italic">　　控件中的选择项目集合，ListItem类型值</label><br />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    .Text<asp:TextBox ID="txbLitmText" Width="90px" runat="server" Text="ListItem Text"></asp:TextBox>
                    .Value<asp:TextBox ID="txbLitmValue" Width="90px" runat="server" Text="ListItem Value"></asp:TextBox>
                    <asp:Button ID="btnSetListitems" runat="server" Text="AddToList" OnClick="btnSetListitems_Click"/>
                    <asp:Button ID="btnUseDefault" runat="server" Text="UseDefault" OnClick="btnUseDefault_Click" />
                </td>
                <td>
                    onSelectItem
                    <br />
                    <label style="font-size:small; font-style:italic">　　客户端，在选择一个项目并改变当前控件值之前触发</label>
                    <br />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txbOnSelectItem" runat="server" Width="220px" Text="alert('Event onSelectItem')"></asp:TextBox>
                    <asp:Button ID="btnSetOnSelectItem" runat="server" Text="Set" OnClick="btnSetOnSelectItem_Click"/>
                    <asp:Button ID="btnClearOnSelectItem" runat="server" Text="Clear" OnClick="btnClearOnSelectItem_Click"/><br />
                    onSelectedItem
                    <br />
                    <label style="font-size:small; font-style:italic">　　客户端，在选择一个项目并改变当前控件值之后触发</label>
                    <br />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txbOnSelectedItem" runat="server" Width="220px" Text="alert('Event onSelectedItem')"></asp:TextBox>
                    <asp:Button ID="btnSetOnSelectedItem" runat="server" Text="Set" OnClick="btnSetOnSelectedItem_Click"/>
                    <asp:Button ID="btnClearOnSelectedItem" runat="server" Text="Clear" OnClick="btnClearOnSelectedItem_Click"/><br />
                    onChange
                    <br />
                    <label style="font-size:small; font-style:italic">　　客户端，在手工输入文本变化后触发</label>
                    <br />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txbOnChange" runat="server" Width="220px" Text="alert('Event onChange')"></asp:TextBox>
                    <asp:Button ID="btnSetOntChange" runat="server" Text="Set" OnClick="btnSetOnChange_Click"/>
                    <asp:Button ID="btnClearOnChange" runat="server" Text="Clear" OnClick="btnClearOnChange_Click"/>
                </td>
            </tr>
        </table>        
        <hr />
        <table>
            <tr style="vertical-align:top">
                <td style="width:270px">
                    <asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" />
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                    <br />
                    <br />
                    <asp:TextBox ID="txbTarget" runat="server" Width="140px"></asp:TextBox>
                    <cc1:GenericInputExtender ID="ctrlGenericInputExtender" runat="server" TargetControlID="txbTarget"/>
                    <br />
                    <br />
                    <asp:TextBox ID="txbTarget2" runat="server" Width="140px"></asp:TextBox>
                    <cc1:GenericInputExtender ID="ctrlGenericInputExtender2" runat="server" TargetControlID="txbTarget2"/>
                </td>
                <td>
                    <textarea id="ctrlGenericInputExtenderHtmlShow" runat="server" style="width:600px; height: 120px;" enableviewstate="true"></textarea>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnGetValue" runat ="server" Text="Get Value" OnClick="btnGetValue_Click"/>
        <asp:TextBox ID="txbGetValue" runat="server" Width="520px"></asp:TextBox><br />
        <label style="font-size:small; font-style:italic">　　取值，此处引用方式：</label>
        <label style="font-weight:bold">ctrlGenericInputExtender.Text , ctrlGenericInputExtender.SelectedIndex</label><br />
        <br />
        <asp:Button ID="btnSetValue" runat="server" Text="Set Value" OnClick="btnSetValue_Click" />
        <asp:TextBox ID="txbSetValue" runat="server" Width="200px"></asp:TextBox><br />
         <label style="font-size:small; font-style:italic">　　设值，此处引用方式：</label>
        <label style="font-weight:bold">ctrlGenericInputExtender.Text = txbSetValue.Text</label>
    </div>
    </form>
</body>
</html>
