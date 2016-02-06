<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorLogList.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.ErrorLog.ErrorLogList" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>错误列表</title>
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/pc.js" type="text/javascript"></script>
    <script type="text/javascript">
        //列表刷新
        //  setTimeout("window.location.href='ErrorLogList.aspx';", 6000);
        function ViewInsertSQL(jobId) {
            var sFeature = "dialogWidth:800px; dialogHeight:600px;center:yes;help:no;resizable:yes;scroll:no;status:no";
            var result;
            var address = String.format("../ETL/Dialogs/ErrorLogDetails.aspx?ID={0}", jobId);
            result = window.showModalDialog(address, null, sFeature);
            if (result) {
                document.getElementById("hiddenServerBtn").click();
            }
            return false;
        }

        function onOKBtnClick() {
            var selectedKeys = $find("ErrorDeluxeGrid").get_clientSelectedKeys();
            if (selectedKeys.length > 0)
                window.returnValue = selectedKeys[0];

            top.close();
        }

        function onDeleteClick() {
            var selectedKeys = $find("ErrorDeluxeGrid").get_clientSelectedKeys();
            if (selectedKeys.length <= 0) {
                alert("请选择要删除的项！");
                return false;
            }
            var msg = "您确定要删除吗？";
            if (confirm(msg) == true) {
                document.getElementById("btnConfirm").click();
            }
        }

        function onSearchClick() {
            var result = verifyTime("dc_LastExecuteStartDate", "dc_LastExecuteEndDate");
            if (result == false)
                alert("开始日期比较不合法");
            else
                document.getElementById("SubmitbtnSearch").click();
        }

        //验证日期起始时间小于结束时间
        function verifyTime(beginTimeControlID, endTimeControlID) {
            var begin = document.getElementById(beginTimeControlID).value;
            var end = document.getElementById(endTimeControlID).value;
            var result = true;
            //有一个为空就不用验证
            if ((begin != "") && (end != "")) {
                var arrBegin = begin.split("-");
                var arrEnd = end.split("-");
                //构造Date对象
                var dateBegin = new Date(arrBegin[0], arrBegin[1] - 1, arrBegin[2]);
                var dateEnd = new Date(arrEnd[0], arrEnd[1] - 1, arrEnd[2]);

                if (dateBegin > dateEnd) {
                    result = false;
                }
            }
            return result;
        } 
    </script>
    <style type="text/css">
        .process_id
        {
            display: none;
        }
    </style>
</head>
<body style="width: 100%; height: 100%;">
    <form id="form1" runat="server">
    <div>
        <%-- <div class="pc-frame-header">
            <pc:Banner ID="pcBanner" runat="server" ActiveMenuIndex="6" />
        </div>--%>
        <div class="pc-search-box-wrapper">
            <soa:DataBindingControl runat="server" ID="searchBinding" AllowClientCollectData="True">
                <ItemBindings>
                    <soa:DataBindingItem ControlID="dr_ErrorType" ControlPropertyName="SelectedValue"
                        Direction="Both" DataPropertyName="ErrorType" />
                    <soa:DataBindingItem ControlID="dc_LastExecuteStartDate" ControlPropertyName="Value"
                        Direction="Both" DataPropertyName="ExecutionTimeStartTime" />
                    <soa:DataBindingItem ControlID="dc_LastExecuteEndDate" ControlPropertyName="Value"
                        Direction="Both" DataPropertyName="ExecutionTimeEndTime" />
                </ItemBindings>
            </soa:DataBindingControl>
            <div id="advSearchPanel" runat="server" style="display: block" class="pc-search-advpan">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            错误类型：
                        </th>
                        <td>
                            <soa:HBDropDownList ID="dr_ErrorType" runat="server" AppendDataBoundItems="True"
                                DataSourceID="dsJobType" DataTextField="Description" DataValueField="EnumValue"
                                SelectedText="全部">
                                <asp:ListItem Text="全部" Selected="True" Value="" />
                            </soa:HBDropDownList>
                            <asp:ObjectDataSource runat="server" ID="dsJobType" SelectMethod="GetErrorTypeSource"
                                TypeName="MCS.Dynamics.Web.Pages.ErrorLog.ErrorLogList" />
                        </td>
                        <th>
                            报错时间：
                        </th>
                        <td>
                            <mcs:DeluxeCalendar ID="dc_LastExecuteStartDate" runat="server" Width="70px">
                            </mcs:DeluxeCalendar>至
                            <mcs:DeluxeCalendar ID="dc_LastExecuteEndDate" runat="server" Width="70px">
                            </mcs:DeluxeCalendar>
                        </td>
                        <td>
                            <input type="button" class="formButton" onclick="onSearchClick();" runat="server"
                                value="查询(S)" id="btnSearch" accesskey="S" />
                            <soa:SubmitButton runat="server" ID="SubmitbtnSearch" Style="display: none" RelativeControlID="btnSearch"
                                OnClick="btnSearch_Click" PopupCaption="正在查询..." />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="height: 30px; background-color: #C0C0C0">
            <a href="javascript:void(0);" onclick="">
                <img src="../../Images/appIcon/15.gif" alt="执行任务" border="0" />
            </a><a href="#" onclick="onDeleteClick();">
                <img src="../../Images/16/delete.gif" alt="删除" border="0" /></a>
            <soa:SubmitButton runat="server" ID="btnConfirm" Style="display: none" OnClick="btnConfirm_Click"
                RelativeControlID="btnDelete" PopupCaption="正在删除..." />
        </div>
        <div class="lefttitle">
            <img src="../../Images/icon_01.gif" />
            容错列表</div>
        <mcs:DeluxeGrid ID="ErrorDeluxeGrid" runat="server" AutoGenerateColumns="False" DataSourceID="dataSourceMain"
            DataSourceMaxRow="0" AllowPaging="True" PageSize="10" Width="100%" ExportingDeluxeGrid="False"
            DataKeyNames="Code" GridTitle="容错列表" CssClass="dataList" ShowExportControl="false"
            ShowCheckBoxes="true" OnRowCommand="ErrorDeluxeGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="容错日志ID" HeaderStyle-CssClass="process_id"
                    ItemStyle-CssClass="process_id" />
                <asp:TemplateField HeaderText="报错类型" HeaderStyle-Width="100" SortExpression="ErrorLogType">
                    <ItemTemplate>
                        <%#MCS.Library.Core.EnumItemDescriptionAttribute.GetDescription((MCS.Library.SOA.DataObjects.Dynamics.ETL.Enums.ErrorType)Eval("ErrorLogType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="实体名称" HeaderStyle-Width="140" SortExpression="ETLEntityCode"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetETLEntityNames( Eval("ETLEntityCode").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="错误信息" SortExpression="ErrorMsg" Visible="False">
                    <ItemTemplate>
                        <%#Eval("ErrorMsg")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="执行时间" HeaderStyle-Width="140" SortExpression="ExecutionTime">
                    <ItemTemplate>
                        <%#Eval("ExecutionTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="140" SortExpression="CreateDate">
                    <ItemTemplate>
                        <%#Eval("CreateDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="140">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("Code") %>'
                            OnClientClick="return confirm('您确定要删除吗')">移除</asp:LinkButton>
                        <asp:LinkButton ID="lbtn_Execute" runat="server" CommandName="Executer" CommandArgument='<%#Eval("Code") %>'>单条执行</asp:LinkButton>
                        <a href="javascript:void(0)" onclick="ViewInsertSQL('<%#Eval("Code") %>')">查看详细</a>
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
        <asp:ObjectDataSource ID="dataSourceMain" runat="server" EnablePaging="True" SelectCountMethod="GetQueryCount"
            SelectMethod="Query" SortParameterName="orderBy" TypeName="MCS.Dynamics.Web.DataSource.ErrorLogSearchDataSourcecs"
            EnableViewState="False" OnSelecting="objectDataSource_Selecting" OnSelected="objectDataSource_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="whereCondition" Name="where" PropertyName="Value"
                    Type="String" />
                <asp:Parameter Direction="InputOutput" Name="totalCount" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <input runat="server" type="hidden" id="whereCondition" />
        <div style="display: none">
            <asp:Button ID="hiddenServerBtn" runat="server" Text="Button" /></div>
    </div>
    </form>
</body>
</html>
