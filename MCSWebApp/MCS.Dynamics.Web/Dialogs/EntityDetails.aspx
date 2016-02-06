<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntityDetails.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.EntityDetails" EnableViewState="false"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑实体</title>
    <base target="_self" />
    <link href="../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../css/form.css" type="text/css" rel="stylesheet" />
    <link href="../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../scripts/Entity/EntityDetails.js" type="text/javascript"></script>
    <script src="../scripts/pc.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HF_EnumValueKey" runat="server" />
    <asp:HiddenField ID="HF_EnumKeyValue" runat="server" />
    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
    </div>
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <%--OnClientDataBinding="onClientDataBinding" OnClientCollectData="onClientCollectData"--%>
    <soa:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true"
        AutoBinding="True" ValidateUnbindProperties="false" AllowClientCollectData="true">
        <ItemBindings>
            <soa:DataBindingItem ControlID="grid" ControlPropertyName="InitialData" DataPropertyName="Fields" Direction="Both">
            </soa:DataBindingItem>
        </ItemBindings>
    </soa:DataBindingControl>
    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <div class="dialogTitle">
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../Images/icon_01.gif" />
                                        编辑实体 <span class="pc-timepointmark">
                                            <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
                                        </span>
                                    </div>
                                </div>
                                <div>
                                    <soa:PropertyForm runat="server" ID="propertyForm" Width="100%" Height="100%" AutoSaveClientState="False" />
                                </div>
                                <!--表单开始-->
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../Images/icon_01.gif" />
                                    编辑实体属性</div>
                                <div class="dialogContent">
                                    <soa:ClientGrid runat="server" ID="grid" ShowEditBar="true" AllowPaging="false" AutoPaging="false"
                                        ShowCheckBoxColumn="true" Width="100%" OnPreRowAdd="clientGrid.OnPreRowAdd" OnCellCreatingEditor="clientGrid.OnCellCreatingEditor"
                                        OnDataFormatting="clientGrid.OnDataFormatting">
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'center',fontWeight:'bold'}"
                                                ItemStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="rowIndex" HeaderText="序号" DataType="Integer" ItemStyle="{width:'30px',textAlign:'center'}"
                                                HeaderStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Name" HeaderText="字段名" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="FieldType" HeaderText="字段类型" DataType="String" EditorReadOnly="True"
                                                EditorEnabled="False">
                                                <EditTemplate EditMode="DropdownList" TemplateControlID="ddl_FieldType" />
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Description" HeaderText="字段描述" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Length" HeaderText="字段长度" DataType="Integer">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="DefaultValue" HeaderText="默认值" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="options" HeaderText="操作" DataType="String">
                                                <EditTemplate EditMode="A" />
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
                                </div>
                                <!--表单结束-->
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="gridfileBottom">
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px; vertical-align: middle; text-align: center">
                        <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
                            accesskey="S" onclick="return ($pc.getEnabled(this) && onSaveClick());" />
                        <input type="button" id="btnClose" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                        <div style="display: none;">
                            <soa:SubmitButton runat="server" Text="保存..." PopupCaption="正在操作..." ID="btn_save" OnClick="btn_Save_Click" />
                            <soa:HBDropDownList ID="ddl_FieldType" runat="server"></soa:HBDropDownList>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <input type="hidden" id="properties" runat="server" />
    <input type="hidden" id="currentSchemaType" runat="server" />
    <asp:HiddenField ID="HFEntityID" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="HFEntityName" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="HFOperationType" runat="server" ClientIDMode="Static"/>
    </form>
</body>
</html>
