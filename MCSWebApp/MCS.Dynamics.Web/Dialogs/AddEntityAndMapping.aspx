<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEntityAndMapping.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.AddEntityAndMapping" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>根据T_Code创建实体</title>
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
    <link href="../css/form.css" type="text/css" rel="stylesheet" />
    <script src="../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../scripts/Entity/AddEntityAndMapping.js" type="text/javascript"></script>
    <script src="../scripts/pc.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <soa:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true"
            AutoBinding="True" ValidateUnbindProperties="false" AllowClientCollectData="true">
            <ItemBindings>
                <soa:DataBindingItem ControlID="txt_entityName" DataPropertyName="InnerEntity.Name"
                    ControlPropertyName="Text" Direction="DataToControl" />
                <soa:DataBindingItem ControlID="txt_entityDesc" DataPropertyName="InnerEntity.Description"
                    ControlPropertyName="Text" Direction="DataToControl" />
                <soa:DataBindingItem ControlID="txt_OuterEntityName" DataPropertyName="OuterEntityName"
                    ControlPropertyName="Text" Direction="Both" />
                <soa:DataBindingItem ControlID="lbl_OuterEntityID" DataPropertyName="OuterEntityID"
                    ControlPropertyName="Text" Direction="Both" />
                <soa:DataBindingItem ControlID="ddl_InType" DataPropertyName="OuterEntityInType"
                    ControlPropertyName="SelectedValue" Direction="Both" />
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
                                    根据T_Code创建实体</div>
                            </div>
                            <div class="entityDiv">
                                <div class="entityInfo">
                                    <table class="tb_form_grid" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    T_Code:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_Code" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" style="text-align: center;">
                                                    <asp:Button ID="btn_getEntityDefine" runat="server" Text="获取实体定义" OnClick="btn_getEntityDefine_Click">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div id="center_panel" runat="server" visible="false">
                            <div class="dialogTitle">
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../Images/icon_01.gif" />
                                    实体信息</div>
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
                                                    <asp:TextBox ID="txt_entityName" runat="server" Text="Label"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    实体描述:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_entityDesc" runat="server" Text="Label"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    映射表:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_OuterEntityName" runat="server"></asp:TextBox>
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
                            accesskey="S" onclick="onSaveClick()" />
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
