<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectETLEntity.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.SelectETLEntity" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../scripts/pc.js" type="text/javascript"></script>
    <script src="../../../scripts/Entity/EntityInfo.js" type="text/javascript"></script>
    <script type="text/javascript">
        function onSaveClick() {
            var $check = $("#ProcessDescInfoDeluxeGrid td").find("input:checked");
            if ($check.length == 0) {
                alert("请选择要复制的数据");
                return false;
            }
            var result = "{\"Description\":\"选择的实体\",\"ETLs\":[ ";
            $check.each(function () {
                //ID
                var ETLID = $(this).parent().siblings(".process_id").text();
                //实体标识
                var ETLCodeNmae = $(this).parent().siblings(".process_CodeName").text();
                //实体名称
                var ETLName = $(this).parent().siblings(".process_ETLEntityname").text();
                //描述
                var ETLDescription = $(this).parent().siblings(".process_Description").text();

                // alert(ETLID + ETLCodeNmae + ETLName + ETLDescription);

                result += "{\"ID\":\"" + ETLID + "\",\"CodeName\":\"" + ETLCodeNmae + "\",\"Name\":\"" + ETLName + "\",\"Description\":\"" + ETLDescription + "\"}" + ",";
            });

            result = result.substring(0, result.length - 1) + " ]} ";

            window.returnValue = result;

            window.close();
        }

    </script>
    <title>实体列表</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pc-search-box-wrapper">
            <soa:DeluxeSearch ID="DeluxeSearch" runat="server" CssClass="deluxe-search deluxe-left"
                HasCategory="False" SearchFieldTemplate="CONTAINS(${DataField}$, ${Data}$)" SearchField="SearchContent"
                OnSearching="SearchButtonClick" OnConditionClick="onconditionClick" CustomSearchContainerControlID="advSearchPanel"
                HasAdvanced="true">
            </soa:DeluxeSearch>
            <soa:DataBindingControl runat="server" ID="searchBinding" AllowClientCollectData="True">
                <ItemBindings>
                    <soa:DataBindingItem ControlID="sfCodeName" DataPropertyName="CodeName" />
                    <soa:DataBindingItem ControlID="sfDescription" DataPropertyName="Description" />
                </ItemBindings>
            </soa:DataBindingControl>
            <div id="advSearchPanel" runat="server" style="display: none" class="pc-search-advpan">
                <asp:HiddenField runat="server" ID="sfAdvanced" Value="False" />
                <table border="0" cellpadding="0" cellspacing="0" class="pc-search-grid-duo">
                    <tr>
                        <td>
                            <label for="sfCodeName" class="pc-label">
                                实体名称</label><asp:TextBox runat="server" ID="sfCodeName" MaxLength="56" CssClass="pc-textbox" />
                        </td>
                        <td>
                            <label for="sfCodeDic" class="pc-label">
                                实体描述</label><asp:TextBox runat="server" ID="sfDescription" MaxLength="56" CssClass="pc-textbox" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="lefttitle">
            <img src="../../../Images/icon_01.gif" />
            实体列表 <span class="pc-timepointmark">
                <MCS:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
            </span>
        </div>
        <ul id="menu_ul">
            <li>
                <asp:LinkButton ID="btn_addEntity" OnClientClick="return view.call(this);" runat="server">添加实体</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btn_delEntity" runat="server" OnClick="btn_delEntity_Click" OnClientClick="javascript:return deleteEntity.call(this);">删除实体</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btn_moveEntity" OnClientClick="return copyEntities.call(this,'true');"
                    runat="server" OnClick="btn_moveEntity_Click">移动实体</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btn_copyEntity" OnClientClick="return copyEntities.call(this);"
                    runat="server" OnClick="btn_copyEntity_Click">复制实体</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btn_Export" OnClientClick="return handler.Export();" runat="server"
                    OnClick="btn_Export_Click">导出</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btn_Import" OnClientClick="" runat="server">导入</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btn_addEntityAndMapping" OnClientClick="return addEntity.call(this);"
                    runat="server">生成实体</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btn_addEtlEntity" OnClientClick="return addETLEntity.call(this);"
                    runat="server">创建ETL实体</asp:LinkButton>
            </li>
        </ul>
        <MCS:DeluxeGrid ID="ProcessDescInfoDeluxeGrid" runat="server" AutoGenerateColumns="False"
            DataSourceID="dataSourceMain" DataSourceMaxRow="0" AllowPaging="True" PageSize="10"
            Width="100%" DataKeyNames="Name" ExportingDeluxeGrid="False" GridTitle="实体列表"
            CssClass="dataList" ShowExportControl="False" ShowCheckBoxes="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="实体ID" HeaderStyle-CssClass="process_id"
                    ItemStyle-CssClass="process_id" />
                <asp:BoundField DataField="CodeName" HeaderText="实体标识" ItemStyle-CssClass="process_CodeName" />
                <asp:BoundField DataField="Name" HeaderText="实体名称" ItemStyle-CssClass="process_ETLEntityname" />
                <asp:BoundField DataField="Description" HeaderText="实体描述" ItemStyle-CssClass="process_Description" />
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
        </MCS:DeluxeGrid>
        <soa:DeluxeObjectDataSource ID="dataSourceMain" runat="server" EnablePaging="True"
            TypeName="MCS.Dynamics.Web.DataSource.ETLEntitySearchDataSource" EnableViewState="false"
            OnSelecting="dataSourceMain_Selecting">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="ou" Type="String" Name="ou" DefaultValue="773BA455-6A2E-4A71-BDC7-AFE689789390" />
            </SelectParameters>
        </soa:DeluxeObjectDataSource>
        <input runat="server" type="hidden" id="whereCondition" />
    </div>
    <div style="height: 40px; vertical-align: middle; text-align: center">
        <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
            accesskey="S" onclick="return ($pc.getEnabled(this) && onSaveClick());" />
        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
    </div>
    <asp:HiddenField ID="hd_entityID" runat="server" />
    <asp:HiddenField ID="moveTargetCategory" runat="server" />
    <soa:UploadProgressControl runat="server" ID="uploadProgress" DialogTitle="导入实体"
        OnClientBeforeStart="onPrepareData" ControlIDToShowDialog="btn_Import" OnDoUploadProgress="uploadProgress_DoUploadProgress"
        OnBeforeNormalPreRender="uploadProgress_BeforeNormalPreRender" OnClientCompleted="onCompleted" />
    </form>
</body>
</html>
