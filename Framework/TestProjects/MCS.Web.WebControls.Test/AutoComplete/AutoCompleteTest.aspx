<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AutoCompleteTest.aspx.cs"
    Inherits="MCS.Web.WebControls.Test.AutoComplete.AutoCompleteTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AutoCompleteExtenderTest</title>
    <link rel="stylesheet" href="AutoComplete.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr style="vertical-align: top">
                    <td>
                        <asp:CheckBox ID="ckbAutoCallBack" runat="server" Text="AutoCallBack" TextAlign="left" /><br />
                        <label style="font-size: small; font-style: italic">
                            是否自动回调</label><br />
                        <asp:CheckBox ID="ckbIsAutoComplete" runat="server" Text="IsAutoComplete" TextAlign="left"
                            Checked="True" /><br />
                        <label style="font-size: small; font-style: italic">
                            是否打开自动完成功能</label><br />
                        <asp:CheckBox ID="ckbRequireValidation" runat="server" Text="RequireValidation" TextAlign="left" /><br />
                        <label style="font-size: small; font-style: italic">
                            控件是否启用验证，默认false，即不启用</label><br />
                        <asp:CheckBox ID="ckbEnableCaching" runat="server" Text="EnableCaching" TextAlign="left"
                            Checked="True" /><br />
                        <label style="font-size: small; font-style: italic">
                            启用客户端缓存，默认true，即启用</label><br />
                        MinimumPrefixLength
                        <asp:TextBox ID="txbMinimumPrefixLength" runat="server">3</asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            输入多少个字符后开始自动完成，默认为3</label><br />
                        CompletionInterval
                        <asp:TextBox ID="txbCompletionInterval" runat="server">1000</asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            自动完成间隔，单位毫秒，默认1000毫秒</label><br />
                        MaxCompletionRecordCount
                        <asp:TextBox ID="txbMaxCompletionRecordCount" runat="server">-1</asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            控件自动完成出来的列表中显示的最大记录数量，默认-1，表示显示全部数据</label><br />
                        MaxPopupWindowHeight
                        <asp:TextBox ID="txbMaxPopupWindowHeight" runat="server">260</asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            控件自动完成弹出的选择窗口的最大高度，默认为260px</label><br />
                    </td>
                    <td>
                        CompletionBodyCssClass
                        <asp:DropDownList ID="ddlCompletionBodyCssClass" runat="server">
                            <asp:ListItem>Default</asp:ListItem>
                            <asp:ListItem>CompletionBodyCssClass_Demo1</asp:ListItem>
                            <asp:ListItem>CompletionBodyCssClass_Demo2</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            自动完成部分的主体区域边框样式</label>
                        <br />
                        <br />
                        ItemCssClass
                        <asp:DropDownList ID="ddlItemCssClass" runat="server">
                            <asp:ListItem>Default</asp:ListItem>
                            <asp:ListItem>ItemCssClass_Demo1</asp:ListItem>
                            <asp:ListItem>ItemCssClass_Demo2</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            自动完成的项目CssClass，默认为空</label>
                        <br />
                        <br />
                        ItemHoverCssClass
                        <asp:DropDownList ID="ddlItemHoverCssClass" runat="server">
                            <asp:ListItem>Default</asp:ListItem>
                            <asp:ListItem>ItemHoverCssClass_Demo1</asp:ListItem>
                            <asp:ListItem>ItemHoverCssClass_Demo2</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            鼠标移动到自动完成的项目上的CssClass，默认为空</label>
                        <br />
                        <br />
                        ErrorCssClass
                        <asp:DropDownList ID="ddlErrorCssClass" runat="server">
                            <asp:ListItem>Default</asp:ListItem>
                            <asp:ListItem>ErrorCssClass_Demo1</asp:ListItem>
                            <asp:ListItem>ErrorCssClass_Demo2</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            当启用输入验证，并且输入的内容无法完全匹配到数据源中的某一项后的CssClass，默认为空</label>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr style="vertical-align: top">
                    <td style="width: 400px">
                        DataSource<br />
                        
                        <label style="font-size: small; font-style: italic">
                            离线数据源</label><br />
                        <table style="text-align: center; border-style: double; font-size: small">
                            <tr>
                                <td style="width: 100px; border-style: double; font-weight: bold">
                                    ID</td>
                                <td style="width: 100px; border-style: double; font-weight: bold">
                                    Text</td>
                                <td style="width: 100px; border-style: double; font-weight: bold">
                                    Value</td>
                            </tr>
                            <tr>
                                <td style="border-style: double">
                                    1</td>
                                <td style="border-style: double">
                                    Text1</td>
                                <td style="border-style: double">
                                    Value1</td>
                            </tr>
                            <tr>
                                <td style="border-style: double">
                                    2</td>
                                <td style="border-style: double">
                                    Text2</td>
                                <td style="border-style: double">
                                    Value2</td>
                            </tr>
                            <tr>
                                <td style="border-style: double">
                                    ...</td>
                                <td style="border-style: double">
                                    ...</td>
                                <td style="border-style: double">
                                    ...</td>
                            </tr>
                        </table>
                        <label style="font-size: small; font-style: italic">
                            回调页面方法中创建的数据源</label><br />
                        <table style="text-align: center; border-style: double; font-size: small">
                            <tr>
                                <td style="width: 100px; border-style: double; font-weight: bold">
                                    ID</td>
                                <td style="width: 100px; border-style: double; font-weight: bold">
                                    Text</td>
                                <td style="width: 100px; border-style: double; font-weight: bold">
                                    Value</td>
                            </tr>
                            <tr>
                                <td style="border-style: double">
                                    1</td>
                                <td style="border-style: double">
                                    'sPrefix'a</td>
                                <td style="border-style: double">
                                    Value_1</td>
                            </tr>
                            <tr>
                                <td style="border-style: double">
                                    2</td>
                                <td style="border-style: double">
                                    'sPrefix'b</td>
                                <td style="border-style: double">
                                    Value_2</td>
                            </tr>
                            <tr>
                                <td style="border-style: double">
                                    ...</td>
                                <td style="border-style: double">
                                    ...</td>
                                <td style="border-style: double">
                                    ...</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        DataTextFieldList
                        <asp:DropDownList ID="ddlDataTextFieldList" runat="server">
                            <asp:ListItem>Text</asp:ListItem>
                            <asp:ListItem>Text,ID</asp:ListItem>
                            <asp:ListItem>Text,Value</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            提供文本显示的数据源属性集合(必备条件)</label><br />
                        DataTextFormatString
                        <asp:DropDownList ID="ddlDataTextFormatString" runat="server">
                            <asp:ListItem>Default</asp:ListItem>
                            <asp:ListItem>{0}<{1}></asp:ListItem>
                            <asp:ListItem>《{0}》-{1}</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            指定显示格式的字符串，对应DataTextFieldList中的可用值，默认顺序显示所有值</label><br />
                        DataValueField
                        <asp:DropDownList ID="ddlDataValueField" runat="server">
                            <asp:ListItem>Default</asp:ListItem>
                            <asp:ListItem>Value</asp:ListItem>
                            <asp:ListItem>ID</asp:ListItem>
                        </asp:DropDownList><br />
                        <label style="font-size: small; font-style: italic">
                            指定项目的Value值的字段用来唯一标识项目，默认不设置</label><br />
                        CompareFieldName
                        <asp:DropDownList ID="ddlCompareFieldName" runat="server">
                            <asp:ListItem>Text</asp:ListItem>
                            <asp:ListItem>Text,ID</asp:ListItem>
                            <asp:ListItem>Text,ID,Value</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <label style="font-size: small; font-style: italic">
                            用于验证的字段集合(必备条件)，任一项完全匹配成功即为匹配成功</label>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr style="vertical-align: top">
                    <td>
                        <asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" />
                        <asp:Button ID="Button1" runat="server" Text="Button" /><br />
                        <br />
                        <asp:TextBox ID="txbTarget" runat="server" Width="200px" onTypeError="alert('onTypeError');"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="ctrlAutoCompleteExtender" AutoCallBack="true" runat="server" TargetControlID="txbTarget"
                            OnGetDataSource="ctrlAutoCompleteExtender_GetDataSource" />
                        <br />
                        <br />
                        <asp:TextBox ID="txbTarget2" runat="server" Width="200px" onTypeError="alert('onTypeError');"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="ctrlAutoCompleteExtender2" runat="server" TargetControlID="txbTarget2"
                            OnGetDataSource="ctrlAutoCompleteExtender2_GetDataSource" />
                        <br />
                        <br />
                        <asp:Button ID="btnGetData" runat="server" Text="GetData" OnClick="btnGetData_Click" /><br />
                        <label>
                            .Text
                        </label>
                        <asp:TextBox ID="txbGetText" runat="server" Enabled="false" Width="160px"></asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            绑定控件所显示的值</label>
                        <br />
                        <label>
                            .SelectValue&nbsp;
                        </label>
                        &nbsp;<asp:TextBox ID="txbGetSelectValue" runat="server" Enabled="false" Width="160px"></asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            对应DataValueField设置的值</label><br />
                        <br />
                        <asp:Button ID="btnSetData" runat="server" Text="SetData" OnClick="btnSetData_Click" /><br />
                        <label>
                            .Text
                        </label>
                        <asp:TextBox ID="txbSetText" runat="server" Width="160px"></asp:TextBox><br />
                        <label style="font-size: small; font-style: italic">
                            设置绑定控件的文本值</label>
                    </td>
                    <td>
                        <textarea id="ctrlAutoCompleteExtenderHtmlShow" runat="server" style="width: 600px;
                            height: 310px;" enableviewstate="true"></textarea>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
