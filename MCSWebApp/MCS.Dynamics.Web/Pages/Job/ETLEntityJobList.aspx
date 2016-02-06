<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ETLEntityJobList.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.Job.ETLEntityJobList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/pc.js" type="text/javascript"></script>
    <script type="text/javascript">

        //查看方法
        function ViewJob(jobId) {
            var sFeature = "dialogWidth:800px; dialogHeight:600px;center:yes;help:no;resizable:yes;scroll:no;status:no";
            var result;

            var address = String.format("/MCSWebApp/WorkflowDesigner/PlanScheduleDialog/TaskAchivedDetail.aspx?Id={0}", jobId);
            result = window.showModalDialog(address, null, sFeature);

            if (result) {
                document.getElementById("hiddenServerBtn").click();
            }
            return false;
        }

        //添加方法
        function onClick() {
            var sFeature = "dialogWidth:800px; dialogHeight:600px;center:yes;help:no;resizable:yes;scroll:no;status:no";
            var result;
            result = window.showModalDialog("../ETL/Dialogs/ETLEntityJobInfo.aspx", null, sFeature);

            if (result) {
                document.getElementById("hiddenServerBtn").click();
            }
        }

        function onOKBtnClick() {
            var selectedKeys = $find("JobDeluxeGrid").get_clientSelectedKeys();

            if (selectedKeys.length > 0)
                window.returnValue = selectedKeys[0];

            top.close();
        }

        function openModalDialog(jobId) {
            var sFeature = "dialogWidth:800px; dialogHeight:600px;center:yes;help:no;resizable:yes;scroll:no;status:no";
            var result;
            var address = String.format("../ETL/Dialogs/ETLEntityJobInfo.aspx?ID={0}", jobId);
            result = window.showModalDialog(address, null, sFeature);

            if (result) {
                document.getElementById("hiddenServerBtn").click();
            }
            return false;
        }


        function onDeleteClick() {
            var selectedKeys = $find("JobDeluxeGrid").get_clientSelectedKeys();
            if (selectedKeys.length <= 0) {
                alert("请选择要删除的任务定义！");
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
<body>
    <form id="form1" runat="server">
    <div>
        <%--<div class="pc-frame-header">
            <pc:Banner ID="pcBanner" runat="server" ActiveMenuIndex="4" />
        </div>--%>
        <div class="pc-search-box-wrapper">
            <soa:DataBindingControl runat="server" ID="searchBinding" AllowClientCollectData="True">
                <ItemBindings>
                    <soa:DataBindingItem ControlID="text_Name" ControlPropertyName="Text" Direction="Both"
                        DataPropertyName="Name" />
                    <soa:DataBindingItem ControlID="dc_LastExecuteStartDate" ControlPropertyName="Value"
                        Direction="Both" DataPropertyName="LastExecuteStartTime" />
                    <soa:DataBindingItem ControlID="dc_LastExecuteEndDate" ControlPropertyName="Value"
                        Direction="Both" DataPropertyName="LastExecuteEndTime" />
                </ItemBindings>
            </soa:DataBindingControl>
            <div id="advSearchPanel" runat="server" style="display: block" class="pc-search-advpan">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            任务名称：
                        </th>
                        <td>
                            <soa:HBTextBox ID="text_Name" runat="server" Height="20px" Width="200px" />
                        </td>
                        <th>
                            上次执行时间：
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
            <a href="javascript:void(0);" onclick="onClick();">
                <img src="../../Images/appIcon/15.gif" alt="新建" border="0" />
            </a><a href="#" onclick="onDeleteClick();">
                <img src="../../Images/16/delete.gif" alt="删除" border="0" /></a>
            <soa:SubmitButton runat="server" ID="btnConfirm" Style="display: none" OnClick="btnConfirm_Click"
                RelativeControlID="btnDelete" PopupCaption="正在删除..." />
        </div>
        <div class="lefttitle" style="">
            <img src="../../Images/icon_01.gif" />
            任务列表</div>
        <mcs:DeluxeGrid ID="JobDeluxeGrid" runat="server" AutoGenerateColumns="False" DataSourceID="dataSourceMain"
            DataSourceMaxRow="0" AllowPaging="True" PageSize="10" Width="100%" ExportingDeluxeGrid="False"
            DataKeyNames="JobID" GridTitle="任务列表" CssClass="dataList" ShowExportControl="false"
            ShowCheckBoxes="true" OnRowCommand="JobDeluxeGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="JobID" HeaderText="任务ID" HeaderStyle-CssClass="process_id"
                    ItemStyle-CssClass="process_id" />
                <asp:TemplateField HeaderText="任务名称" SortExpression="JOB_NAME">
                    <ItemTemplate>
                        <a onclick="openModalDialog('<%#Server.UrlEncode((string)Eval("JobID"))%>')" href="#">
                            <%#(string)Eval("Name")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="任务描述" ItemStyle-HorizontalAlign="Center"
                    SortExpression="DESCRIPTION">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="任务类型" HeaderStyle-Width="140" SortExpression="JOB_TYPE">
                    <ItemTemplate>
                        <%#MCS.Library.Core.EnumItemDescriptionAttribute.GetDescription((MCS.Library.SOA.DataObjects.JobType)Eval("JobType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="启用" HeaderStyle-Width="100" SortExpression="ENABLED">
                    <ItemTemplate>
                        <%#((bool)Eval("Enabled")? "是" : "否")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否手动" HeaderStyle-Width="100" SortExpression="ISManual">
                    <ItemTemplate>
                        <%#((bool)Eval("ISManual")  ? "是" : "否")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LastStartExecuteTime" HeaderText="上次执行时间" HeaderStyle-Width="170"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" SortExpression="LAST_START_EXE_TIME" />
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_star" CommandName="btn_star" CommandArgument='<%#Eval("JobID") %>'
                            runat="server">立即执行</asp:LinkButton>
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
            SelectCountMethod="GetQueryCount" SelectMethod="Query" SortParameterName="orderBy"
            TypeName="MCS.Dynamics.Web.DataSource.EntityJobSearchDataSource" EnableViewState="False"
            OnSelecting="objectDataSource_Selecting" OnSelected="objectDataSource_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="whereCondition" Name="where" PropertyName="Value"
                    Type="String" />
                <asp:Parameter Direction="InputOutput" Name="totalCount" Type="Int32" />
            </SelectParameters>
        </soa:DeluxeObjectDataSource>
        <input runat="server" type="hidden" id="whereCondition" />
        <div style="display: none">
            <asp:Button ID="hiddenServerBtn" runat="server" Text="Button" /></div>
    </div>
    </form>
</body>
</html>
