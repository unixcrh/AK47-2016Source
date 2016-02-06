<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ETLEntityList.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.ETLEntityList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ETL实体列表</title>
    <script language="javascript" type="text/javascript">
        function SelectETL(ETLEntityCode) {
            var sFeature = "dialogWidth:1000px; dialogHeight:600px;center:yes;help:no;resizable:yes;scroll:no;status:no";
            var result;
            result = window.showModalDialog("../ETL/Dialogs/ViewCreateTableSql.aspx?Code=" + ETLEntityCode, null, sFeature);
        }
    </script>
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
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/pc.js" type="text/javascript"></script>
    <script src="../../scripts/Entity/EntityInfo.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <div>
        <div class="pc-search-box-wrapper">
            <soa:DeluxeSearch ID="DeluxeSearch" runat="server" CssClass="deluxe-search deluxe-left"
                HasCategory="False" SearchFieldTemplate="CONTAINS(${DataField}$, ${Data}$)" SearchField="SearchContent"
                OnSearching="SearchButtonClick" OnConditionClick="onconditionClick" CustomSearchContainerControlID="advSearchPanel"
                HasAdvanced="true">
            </soa:DeluxeSearch>
            <p style="display: none;">
                <soa:SubmitButton runat="server" Text="保存..." PopupCaption="正在查询..." ID="btn_save_seachLoading" />
            </p>
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
            <img src="../../Images/icon_01.gif" />
            ETL实体列表 <span class="pc-timepointmark">
                <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
            </span>
        </div>
        <ul id="menu_ul">
            <li>
                <asp:LinkButton ID="btn_delEntity" runat="server" OnClick="btn_delEntity_Click" OnClientClick="javascript:return deleteEntity.call(this);">删除实体</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btn_addEtlEntity" OnClientClick="return addETLEntity.call(this);"
                    runat="server">创建ETL实体</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btn_Export" OnClientClick="return handler.Export();" runat="server"
                    OnClick="btn_Export_Click">导出</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btn_Import" OnClientClick="" runat="server">导入</asp:LinkButton></li>
        </ul>
        <mcs:DeluxeGrid ID="ProcessDescInfoDeluxeGrid" runat="server" AutoGenerateColumns="False"
            DataSourceID="dataSourceMain" DataSourceMaxRow="0" AllowPaging="True" PageSize="10"
            Width="100%" DataKeyNames="ID" ExportingDeluxeGrid="False" GridTitle="实体列表" CssClass="dataList"
            ShowExportControl="False" ShowCheckBoxes="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="实体ID" HeaderStyle-CssClass="process_id"
                    ItemStyle-CssClass="process_id" />
                <asp:BoundField DataField="CodeName" HeaderText="实体标识" />
                <asp:TemplateField HeaderText="实体名称">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="editETLView('<%#Eval("ID") %>','<%#Eval("CategoryID") %>')">
                            <%#Eval("Name")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="实体描述" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="editETLView('<%#Eval("ID") %>','<%#Eval("CategoryID") %>')">
                            编辑ETL实体</a> &nbsp;<a href="javascript:void(0)" onclick="SelectETL('<%#Eval("ID") %>')">查看SQL</a>&nbsp;
                        <a href="javascript:void(0)" onclick="editETLEntitymapping('<%#Eval("ID") %>'),('<%#Eval("CategoryID") %>')">
                            查看ETL实体映射</a>
                    </ItemTemplate>
                </asp:TemplateField>
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
            TypeName="MCS.Dynamics.Web.DataSource.ETLEntitySearchDataSource" EnableViewState="false"
            OnSelecting="dataSourceMain_Selecting">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="ou" Type="String" Name="ou" DefaultValue="773BA455-6A2E-4A71-BDC7-AFE689789390" />
            </SelectParameters>
        </soa:DeluxeObjectDataSource>
        <input runat="server" type="hidden" id="whereCondition" />

    </div>
    <asp:HiddenField ID="hd_entityID" runat="server" />
    <asp:HiddenField ID="moveTargetCategory" runat="server" />
    <soa:UploadProgressControl runat="server" ID="uploadProgress" DialogTitle="导入实体"
        OnClientBeforeStart="onPrepareData" ControlIDToShowDialog="btn_Import" OnDoUploadProgress="uploadProgress_DoUploadProgress"
        OnBeforeNormalPreRender="uploadProgress_BeforeNormalPreRender" OnClientCompleted="onCompleted" />
    </form>
</body>
</html>
