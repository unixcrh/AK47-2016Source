<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllSapUserCompareChoose.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.AllSapUserCompareChoose" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择Sap与UEP对照用户</title>
    <base target="_self" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../scripts/json2.js" type="text/javascript"></script>
    <style type="text/css">
        .col-chekbox
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#okButton").click(function () {
                var $checked = $(".col-chekbox>:checked");
                var proId = [];
                if ($checked.length == 0) {
                    alert("请选择用户");
                    return;
                } else {
                    $checked.parent().parent("tr").each(function () {
                        var resultJson = {};
                        $(this).find("td[class]").slice(1).each(function () {
                            var $this = $(this);
                            resultJson[$this.attr("class")] = $this.text();
                        });
                        proId.push(resultJson);
                    });
                    window.returnValue = JSON.stringify(proId);
                    window.close();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <div class="dialogTitle">
                                </div>
                                <div>
                                </div>
                                <!--表单开始-->
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../../../Images/icon_01.gif" />
                                    选择Sap对照用户</div>
                                <div class="dialogContent">
                                    <mcs:DeluxeGrid ID="gridUser" runat="server" DataSourceMaxRow="0" AllowPaging="True" DataSourceID="objectDataSource"
                                        PageSize="10" Width="100%" DataKeyNames="ClientID" ExportingDeluxeGrid="False" GridTitle="实体列表"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gridUser_PageIndexChanging"
                                        ShowCheckBoxes="True">
                                        <Columns>
                                            <asp:BoundField DataField="ClientID" HeaderText="SAPInstanceId"  ControlStyle-Width="150" ItemStyle-CssClass="SAPInstanceId" />
                                            <asp:BoundField DataField="User" HeaderText="SAP用户名" ControlStyle-Width="150" ItemStyle-CssClass="SapID" />
                                            <asp:BoundField DataField="Client" HeaderText="SAPClient" ControlStyle-Width="150"
                                                ItemStyle-CssClass="SapServers_Client" />
                                            <asp:BoundField DataField="ApplicationServer" HeaderText="SAP服务器地址" ControlStyle-Width="150"
                                                ItemStyle-CssClass="SapServers_ApplicationServer" />
                                            <asp:BoundField DataField="SystemNumber" HeaderText="系统编号" ControlStyle-Width="150" ItemStyle-CssClass="SapServers_SystemNo" />
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
                                    <asp:ObjectDataSource ID="objectDataSource" runat="server" EnablePaging="True" SelectMethod="Query"
                                        SelectCountMethod="GetQueryCount" SortParameterName="orderBy" OnSelecting="objectDataSource_Selecting"
                                        OnSelected="objectDataSource_Selected" TypeName="MCS.Dynamics.Web.DataSource.SAPClientDeluxeGridObjectDataSource"
                                        EnableViewState="False">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="whereCondition" Name="where" PropertyName="Value"
                                                Type="String" />
                                            <asp:Parameter Direction="InputOutput" Name="totalCount" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <!--查询的条件语句放在whereCondition控件里-->
                                    <input runat="server" type="hidden" id="whereCondition" />
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
                        <input type="button" runat="server" id="okButton" class="formButton" value="保存"
                            accesskey="S" />
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="window.close();" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
