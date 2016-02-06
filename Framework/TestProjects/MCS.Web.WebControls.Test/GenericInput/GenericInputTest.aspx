<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenericInputTest.aspx.cs" Inherits="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls.Test.GenericInput.GenericInputTest" %>

<%@ Register Assembly="DeluxeWorks.Web.WebControls" Namespace="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>GenericInputTest</title>
    <link type="text/css" href="GenericInput.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <table>
            <tr valign="top">
                <td style="width:490px">
                    <asp:CheckBox ID="ckbAutoPostBack" runat="server" Text="AutoPostBack" TextAlign="left" /><br />
                    <label style="font-size:small; font-style:italic">　　设置控件是否自动PostBack,如果为True则在Text改变的时候和选择项目的时候PostBack</label><br />
                    HighlightBorderColor
                    <asp:DropDownList ID="ddlHighlightBorderColor" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>Red</asp:ListItem>
                        <asp:ListItem>Blue</asp:ListItem>
                        <asp:ListItem>Yellow</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　控件边框的颜色，默认值为　#2353B2</label>
                    <br />
                    DropArrowBackgroundColor
                    <asp:DropDownList ID="ddlDropArrowBackgroundColor" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>Red</asp:ListItem>
                        <asp:ListItem>Blue</asp:ListItem>
                        <asp:ListItem>Yellow</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　下拉箭头区域的背景色，默认值为　#C6E1FF</label>
                    <br />
                    Height
                    <asp:TextBox ID="txbHeight" runat="server"></asp:TextBox><br />
                    <label style="font-size:small; font-style:italic">　　控件的高度</label>
                    <br />
                    CssClass
                    <asp:DropDownList ID="ddlCssClass" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>CssClass_Demo1</asp:ListItem>
                        <asp:ListItem>CssClass_Demo2</asp:ListItem>
                    </asp:DropDownList><br />
                     <label style="font-size:small; font-style:italic">　　控件样式</label>
                    <br />
                    ItemCssClass
                    <asp:DropDownList ID="ddlItemCssClass" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>ItemCssClass_Demo1</asp:ListItem>
                        <asp:ListItem>ItemCssClass_Demo2</asp:ListItem>
                    </asp:DropDownList><br />
                     <label style="font-size:small; font-style:italic">　　选择项目默认样式</label>
                    <br />
                    ItemHoverCssClass
                    <asp:DropDownList ID="ddlItemHoverCssClass" runat="server">
                        <asp:ListItem>Default</asp:ListItem>
                        <asp:ListItem>ItemHoverCssClass_Demo1</asp:ListItem>
                        <asp:ListItem>ItemHoverCssClass_Demo2</asp:ListItem>
                    </asp:DropDownList><br />
                    <label style="font-size:small; font-style:italic">　　鼠标移动到选项项目上时的样式</label>
                    <br />
                    AccessKey
                    <asp:TextBox ID="txbAccessKey" runat="server">f</asp:TextBox><br />
                    <label style="font-size:small; font-style:italic">　　快捷键，控件取到焦点，全选文本值高亮</label>
                    <br />
                    Font
                    <br />
                    &nbsp; &nbsp; &nbsp;
                    <asp:CheckBox id="ckbBold" runat="server" Text="Bold"></asp:CheckBox>
                    <asp:CheckBox id="ckbItalic" runat="server" Text="Italic"></asp:CheckBox>
                    <asp:CheckBox id="ckbUnderline" runat="server" Text="Underline"></asp:CheckBox>
                    ......
                    <br />
                    <label style="font-size:small; font-style:italic">　　文本字体</label>
                 </td>
                 <td style="width:600px">
                    SelectedIndexChanged<br />
                    <label style="font-size:small; font-style:italic">　　服务器端，设置AutoPostBack后，每选择一个项目则自动PostBack，触发这个事件</label>
                    <br />
                    <label>　　SelectedIndex</label>
                    <asp:TextBox ID="txbSelectedIndex" runat="server" Enabled="false"></asp:TextBox><br />
                    <label>　　SelectedItem.Text</label>
                    <asp:TextBox ID="txbSelectedText" runat="server" Enabled="false"></asp:TextBox><br />
                    <label>　　SelectedItem.Value</label>
                    <asp:TextBox ID="txbSelectedValue" runat="server" Enabled="false"></asp:TextBox><br />
                     <br />
                    TextChanged<br />
                    <label style="font-size:small; font-style:italic">　　服务器端，在手工输入文本变化后触发这个事件</label>
                    <br />
                    <label>　　ctrlGenericInput.Text</label>
                    <asp:TextBox ID="txbTextInput" runat="server"></asp:TextBox><br />
                     <br />
                    onSelectItem
                    <label style="font-size:small; font-style:italic">　　客户端，在选择一个项目并改变当前控件值之前触发</label>
                    <br />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txbOnSelectItem" runat="server" Width="220px" Text="alert('Event onSelectItem')"></asp:TextBox>
                    <asp:Button ID="btnSetOnSelectItem" runat="server" Text="Set" OnClick="btnSetOnSelectItem_Click"/>
                    <asp:Button ID="btnClearOnSelectItem" runat="server" Text="Clear" OnClick="btnClearOnSelectItem_Click"/><br />
                    onSelectedItem
                    <label style="font-size:small; font-style:italic">　　客户端，在选择一个项目并改变当前控件值之后触发</label>
                    <br />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txbOnSelectedItem" runat="server" Width="220px" Text="alert('Event onSelectedItem')"></asp:TextBox>
                    <asp:Button ID="btnSetOnSelectedItem" runat="server" Text="Set" OnClick="btnSetOnSelectedItem_Click"/>
                    <asp:Button ID="btnClearOnSelectedItem" runat="server" Text="Clear" OnClick="btnClearOnSelectedItem_Click"/><br />
                    onChange
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
            <tr valign="top">
                <td style="width:520px">
                    设置可选列表，ListItem类型值&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    .Text<asp:TextBox ID="txbLitmText" Width="90px" runat="server" Text="ListItem Text"></asp:TextBox>
                    .Value<asp:TextBox ID="txbLitmValue" Width="90px" runat="server" Text="ListItem Value"></asp:TextBox>
                    <asp:Button ID="btnSetListitems" runat="server" Text="AddToList" OnClick="btnSetListitems_Click" />
                    <asp:Button ID="btnUseDefault" runat="server" Text="UseDefault" OnClick="btnUseDefault_Click" />
                    <br />
                    <br />
                    <asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" />
                    <asp:Button ID="Button1" runat="server" Text="Button" /><br />
                    <br />
                    <cc1:GenericInput ID="ctrlGenericInput" runat="server" Width="200px" OnSelectedIndexChanged="ctrlGenericInput_SelectedIndexChanged" OnTextChanged="ctrlGenericInput_TextChanged"></cc1:GenericInput>
                    <br />
                    <br />
                    <cc1:GenericInput ID="ctrlGenericInput2" runat="server" Width="200px"></cc1:GenericInput>
                    <br />
                    <hr />
                    <asp:Button ID="btnGetValue" runat ="server" Text="Get Value" OnClick="btnGetValue_Click"/><textarea id="txtaGetValue" runat="server" rows="2" style="width:390px; vertical-align:top" enableviewstate="true"></textarea>
                    <br />
                    <label style="font-size:small; font-style:italic">　　取值，此处引用方式：</label>
                    <label style="font-weight:bold">ctrlGenericInput.Text</label><br />
                    <label style="font-weight:bold">　　　　　　　　　 ctrlGenericInput.SelectedItem.Text</label>
                    <br />
                    <asp:Button ID="btnSetValue" runat="server" Text="Set Value" OnClick="btnSetValue_Click" />
                    <asp:TextBox ID="txbSetValue" runat="server" Width="200px"></asp:TextBox><br />
                    <label style="font-size:small; font-style:italic">　　设值，此处引用方式：</label>
                    <label style="font-weight:bold">ctrlGenericInput.Text = txbSetValue.Text;</label>
                    <br />
                    <br />
                </td>
                <td>
                    <textarea id="ctrlGenericInputHtmlShow" runat="server" style="width:380px; height: 300px;" enableviewstate="true"></textarea>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
