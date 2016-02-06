<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntityMapping.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.EntityMapping.EntityMapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #menu_ul > li
        {
            float: left;
            margin-left: 5px;
            line-height: 30px;
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
        body
        {
            padding: 10px;
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
    <link href="../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../css/form.css" type="text/css" rel="stylesheet" />
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/Entity/EntityDetails.js" type="text/javascript"></script>
    <script src="../../scripts/pc.js" type="text/javascript"></script>
    <script src="../../scripts/EntityCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        var urllocation = "../../dialogs/OuterEntityMapping.aspx";
        $(document).ready(function () {
            Request.IntalData();
            urllocation += "?EntityID=" + Request.QueryString("ID");
            $("a.aspNetDisabled").attr("disabled", true);
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
    </div>
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <soa:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true"
        AutoBinding="True" ValidateUnbindProperties="false" AllowClientCollectData="true"
        ShowCheckBoxes="True">
        <ItemBindings>
            <soa:DataBindingItem ControlID="lbName" DataPropertyName="Name" />
            <soa:DataBindingItem ControlID="lbDesc" DataPropertyName="Description" />
        </ItemBindings>
    </soa:DataBindingControl>
    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td valign="top" style="height: 640px;">
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <div class="dialogTitle">
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../../Images/icon_01.gif" />
                                        实体信息<span class="pc-timepointmark">
                                            <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
                                        </span>
                                    </div>
                                </div>
                                <div>
                                    <table class="tb_form_grid" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    实体名
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbName" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    实体描述
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbDesc" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <!--表单开始-->
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../../Images/icon_01.gif" />
                                    实体映射关系</div>
                                <div class="dialogContent">
                                    <div runat="server" id="div_add">
                                        <ul id="menu_ul">
                                            <li>
                                                <asp:LinkButton ID="btn_add" OnClientClick="return view.call(this)" runat="server">添加关联</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btn_del_Mapping" runat="server" OnClientClick="return deleteEntity.call(this)"
                                                    OnClick="btn_del_Mapping_Click">删除关联</asp:LinkButton></li>
                                        </ul>
                                    </div>
                                    <mcs:DeluxeGrid ID="ProcessDescInfoDeluxeGrid" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="dataSourceMain" DataSourceMaxRow="0" AllowPaging="true" PageSize="10"
                                        Width="100%" DataKeyNames="ContainerName" ExportingDeluxeGrid="False" GridTitle="实体映射列表"
                                        CssClass="dataList" ShowExportControl="False" ShowCheckBoxes="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="关联表">
                                                <ItemTemplate>
                                                    <a href="javascript:void(0)" onclick="view('<%#Eval("MemberID") %>')">
                                                        <%#Eval("MemberName")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Description" HeaderText="实体描述" />--%>
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <a href="javascript:void(0)" onclick="view('<%#Eval("MemberID") %>')">编辑实体关联</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="process_id" ItemStyle-CssClass="process_id" />
                                        </Columns>
                                        <PagerStyle CssClass="pager" />
                                        <RowStyle CssClass="item" />
                                        <CheckBoxTemplateItemStyle CssClass="checkbox" />
                                        <CheckBoxTemplateHeaderStyle CssClass="checkbox" />
                                        <HeaderStyle CssClass="head" />
                                        <AlternatingRowStyle CssClass="aitem" />
                                        <EmptyDataTemplate>
                                            暂时没有您需要的数据
                                        </EmptyDataTemplate>
                                        <PagerSettings FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" Mode="NextPreviousFirstLast"
                                            NextPageText="下一页" Position="Bottom" PreviousPageText="上一页"></PagerSettings>
                                    </mcs:DeluxeGrid>
                                    <soa:DeluxeObjectDataSource ID="dataSourceMain" runat="server" EnablePaging="True"
                                        TypeName="MCS.Dynamics.Web.DataSource.EntityMappingDataSource" EnableViewState="false"
                                        OnSelecting="dataSourceMain_Selecting">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="whereCondition" Name="where" PropertyName="Value"
                                                Type="String" />
                                        </SelectParameters>
                                    </soa:DeluxeObjectDataSource>
                                    <asp:HiddenField ID="whereCondition" runat="server" />
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
                        <%-- <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
                            accesskey="S" onclick="return ($pc.getEnabled(this) && onSaveClick());" />--%>
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                        <div style="display: none;">
                            <soa:SubmitButton runat="server" Text="保存..." PopupCaption="正在操作..." ID="btn_save"
                                OnClick="btn_Save_Click" />
                            <soa:HBDropDownList ID="ddl_FieldType" runat="server">
                            </soa:HBDropDownList>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="display: none">
        <iframe name="innerFrame"></iframe>
    </div>
    <input type="hidden" id="properties" runat="server" />
    <input type="hidden" id="currentSchemaType" runat="server" />
    <asp:HiddenField ID="hd_entityID" runat="server" />
    </form>
</body>
</html>
