<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OuterEntityMapping.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.OuterEntityMapping" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>实体映射关系</title>
    <style type="text/css">
        #menu_ul li
        {
            float: left;
            margin-left: 5px;
            line-height: 30px;
        }
        body
        {
            width: 100%;
            margin: 0 auto;
        }
        #menu_ul
        {
            width: 100%;
            background: #F4F4F4;
            height: 30px;
        }
        .process_id
        {
            display: none;
        }
        .entityInfo
        {
            margin: 0 auto;
        }
        .entityDiv
        {
            width: 100%;
        }
        .tb_form_grid
        {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }
        
        .tb_form_grid td
        {
            text-align: left;
            border: 1px solid #f0f0f0;
            line-height: 25px;
            height: 25px;
        }
    </style>
    <base target="_self" />
    <link href="../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../Css/form.css" rel="stylesheet" type="text/css" />
    <link href="../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../scripts/pc.js" type="text/javascript"></script>
    <script src="../scripts/Entity/EntityMappingDetail.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                var heigthResult = $(window).height() - $(".dialogTitle").height() - $(".entityDiv").height() - $(".dialogTitle").height() - 55;
                $(".dialogContent").height(heigthResult);
            }, 300);

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
        <pc:SceneControl ID="SceneControl1" runat="server" />
    </div>
    <div>
        <soa:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true"
            AutoBinding="True" ValidateUnbindProperties="false" AllowClientCollectData="true">
            <ItemBindings>
                <soa:DataBindingItem ControlID="lbl_entityName" DataPropertyName="InnerEntity.Name"
                    ControlPropertyName="Text" Direction="DataToControl" />
                <soa:DataBindingItem ControlID="lbl_entityDesc" DataPropertyName="InnerEntity.Description"
                    ControlPropertyName="Text" Direction="DataToControl" />
                <soa:DataBindingItem ControlID="txt_OuterEntityName" DataPropertyName="OuterEntityName"
                    ControlPropertyName="Text" Direction="Both" />
                <soa:DataBindingItem ControlID="lbl_OuterEntityID" DataPropertyName="OuterEntityID"
                    ControlPropertyName="Text" Direction="Both" />
                <soa:DataBindingItem ControlID="ddl_InType" DataPropertyName="OuterEntityInType"
                    ControlPropertyName="SelectedValue" Direction="Both" />
                <soa:DataBindingItem ControlID="grid" ControlPropertyName="InitialData" DataPropertyName="EntityFieldMappingCollection"
                    Direction="Both">
                </soa:DataBindingItem>
            </ItemBindings>
        </soa:DataBindingControl>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td valign="top">
                        <div>
                            <div class="dialogTitle">
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../Images/icon_01.gif" />
                                    实体信息<span class="pc-timepointmark">
                                        <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
                                    </span>
                                </div>
                            </div>
                            <div class="entityDiv">
                                <div class="entityInfo">
                                    <table class="tb_form_grid" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    实体名称:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_entityName" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    实体描述:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_entityDesc" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    映射表:
                                                </td>
                                                <td>
                                                    <soa:HBTextBox ID="txt_OuterEntityName" runat="server"></soa:HBTextBox>
                                                    <asp:Label ID="lbl_OuterEntityID" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    外部结构类型:
                                                </td>
                                                <td>
                                                    <soa:HBDropDownList runat="server" ID="ddl_InType" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="dialogTitle">
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../Images/icon_01.gif" />
                                    编辑实体字段关联</div>
                            </div>
                            <div class="dialogContent" style="overflow-y: auto; height: 100%;">
                                <soa:ClientGrid runat="server" ID="grid" ShowEditBar="false" AllowPaging="false"
                                    AutoPaging="false" ShowCheckBoxColumn="false" Width="100%" OnCellCreatingEditor="clientGrid.OnCellCreatingEditor"
                                    OnDataFormatting="clientGrid.OnDataFormatting">
                                    <Columns>
                                        <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'left',fontWeight:'bold'}"
                                            ItemStyle="{width:'30px',textAlign:'left'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="rowIndex" HeaderText="序号" DataType="Integer" ItemStyle="{width:'30px',textAlign:'center'}"
                                            HeaderStyle="{width:'30px',textAlign:'center'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="FieldName" HeaderText="字段名" DataType="String" HeaderStyle="{textAlign:'left'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="FieldDesc" HeaderText="字段描述" DataType="String" HeaderStyle="{textAlign:'left'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="FieldTypeName" HeaderText="字段类型" DataType="String"
                                            HeaderStyle="{textAlign:'left'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="FieldLength" HeaderText="字段长度" DataType="String"
                                            HeaderStyle="{textAlign:'left'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="FieldDefaultValue" HeaderText="字段默认值" DataType="String"
                                            HeaderStyle="{textAlign:'left'}">
                                        </soa:ClientGridColumn>
                                        <soa:ClientGridColumn DataField="OuterFieldName" HeaderText="关联字段/结构" HeaderStyle="{textAlign:'left'}">
                                            <EditTemplate EditMode="TextBox" />
                                        </soa:ClientGridColumn>
                                    </Columns>
                                </soa:ClientGrid>
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
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                        <div style="display: none;">
                            <soa:SubmitButton runat="server" Text="保存..." PopupCaption="正在操作..." ID="btn_save"
                                OnClick="btn_save_OnClick" />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
