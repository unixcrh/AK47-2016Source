<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogView.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.Logs.LogView" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .process_id
        {
            display: none;
        }
        body
        {
            padding: 10px;
        }
    </style>
    <script type="text/javascript">
        function onconditionClick(sender, e) {
            var content = Sys.Serialization.JavaScriptSerializer.deserialize(e.ConditionContent);
            var bindingControl = $find("searchBinding");
            bindingControl.dataBind(content);
        }
        $(document).ready(function () {
            if (window.parent.showWait) {
                window.parent.showWait(false);
            }
            if (window.parent.showLoader)
                window.parent.showLoader(false);

        });
    
    </script>
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
                    <soa:DataBindingItem ControlID="sfOperatorName" DataPropertyName="OperatorName" />
                    <soa:DataBindingItem ControlID="sfRealOperatorName" DataPropertyName="RealOperatorName" />
                </ItemBindings>
            </soa:DataBindingControl>
            <div id="advSearchPanel" runat="server" style="display: none" class="pc-search-advpan">
                <asp:HiddenField runat="server" ID="sfAdvanced" Value="False" />
                <table border="0" cellpadding="0" cellspacing="0" class="pc-search-grid-duo">
                    <tr>
                        <td>
                            <label for="sfCodeName" class="pc-label">
                                操作人</label><asp:TextBox runat="server" ID="sfOperatorName" MaxLength="56" CssClass="pc-textbox" />
                        </td>
                        <td>
                            <label for="sfCodeDic" class="pc-label">
                                实际操作人</label><asp:TextBox runat="server" ID="sfRealOperatorName" MaxLength="56" CssClass="pc-textbox" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="lefttitle" style="margin-top: 20px;">
            <img src="../../Images/icon_01.gif" />
            日志列表</div>
        <MCS:DeluxeGrid ID="ProcessDescInfoDeluxeGrid" runat="server" AutoGenerateColumns="False"
            DataSourceID="dataSourceMain" DataSourceMaxRow="0" AllowPaging="True" PageSize="10"
            Width="100%" ExportingDeluxeGrid="False" GridTitle="实体列表" CssClass="dataList"
            ShowExportControl="False" ShowCheckBoxes="false">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="实体ID" HeaderStyle-CssClass="process_id"
                    ItemStyle-CssClass="process_id" />
                <asp:TemplateField HeaderText="操作人">
                    <ItemTemplate>
                        <%#Eval("OperatorName")%>&nbsp(实际操作人：<%#Eval("RealOperatorName")%>)
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Subject" HeaderText="操作" />
                <asp:TemplateField HeaderText="操作时间">
                    <ItemTemplate>
                        <%#Eval("CreateTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pager" />
            <RowStyle CssClass="item" />
            <HeaderStyle CssClass="head" />
            <AlternatingRowStyle CssClass="aitem" />
            <EmptyDataTemplate>
                暂时没有您需要的数据
            </EmptyDataTemplate>
            <PagerSettings FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" Mode="NextPreviousFirstLast"
                NextPageText="下一页" Position="Bottom" PreviousPageText="上一页"></PagerSettings>
        </MCS:DeluxeGrid>
        <soa:DeluxeObjectDataSource ID="dataSourceMain" runat="server" EnablePaging="True"
            TypeName="MCS.Dynamics.Web.DataSource.DynamicsLogDataSource" EnableViewState="false"
            OnSelecting="dataSourceMain_Selecting">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="ou" Type="String" Name="ou" DefaultValue="773BA455-6A2E-4A71-BDC7-AFE689789390" />
            </SelectParameters>
        </soa:DeluxeObjectDataSource>
    </div>
    </form>
</body>
</html>
