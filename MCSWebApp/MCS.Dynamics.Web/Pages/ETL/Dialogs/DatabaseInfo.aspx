<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseInfo.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.DatabaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="MCS" Namespace="MCS.Web.WebControls" Assembly="MCS.Web.WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据库明细</title>
    <base target="_self" />
    <script src="../../../JavaScript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../../../Css/form.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/basePage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        
        //编辑
        function openDialog(e) {
            var paramers = "scrollbars=yes;resizable=yes;help=no;status=no;center=yes;location=no;dialogHeight=300px;dialogWidth=800px;";
            var address = "EditDataBaseInfo.aspx?code=" + e;
            window.showModalDialog(address, paramers);
        }
        //新建
        function CreateopenDialog() {
            var paramers = "scrollbars=yes;resizable=yes;help=no;status=no;center=yes;location=no;dialogHeight=300px;dialogWidth=800px;";
            var address = "EditDataBaseInfo.aspx";
            window.showModalDialog(address, paramers);
            window.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div id="advSearchPanel" runat="server" style="display: block" class="pc-search-advpan">
        <table class="m_25" width="100%" align="center">
            <tr>
                
                <th>
                    数据库连接地址
                </th>
                <td>
                    <input type="text" id="txtDBAddr" name="txtDBAddr" runat="server" />
                </td>
                <th>
                    数据库名
                </th>
                <td>
                    <input type="text" id="txtDBName" name="txtDBName" runat="server" />
                </td>
                <th>
                    <label>
                        数据库登录账号</label>
                </th>
                <td>
                    <input type="text" id="txtLoginID" name="txtLoginID" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <input id="Button1" type="button" class="formButton" value="查询(S)" runat="server"
                        style="margin-bottom: 10px; cursor: pointer" onserverclick="btnsearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table width="100%">
        <tbody>
         <tr>
                <td style="height: 40px; vertical-align: middle;">
                    <div style="text-align: right">
                        <input type="button" class="formButton" value="新建" onclick="CreateopenDialog()" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="lefttitle" style="background-color: #E6EFF6">
                        <img src="../imgs/icon_01.gif" />
                        数据库明细</div>
                    <asp:Label ID="lblNoticeCount" runat="server" Text="" CssClass="lblCount"></asp:Label>
                    <div style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Text="" CssClass="lblCount"></asp:Label>
                        <MCS:DeluxeGrid ID="NoticeDeluxeGrid" runat="server" AutoGenerateColumns="False"
                            DataSourceID="objectDataSource" DataSourceMaxRow="0" AllowPaging="True" Width="100%"
                            DataKeyNames="DBCODE" GridTitle="Test" ShowExportControl="False" AllowSorting="True"
                            OnSorting="DeluxeGrid_Sorting" OnPageIndexChanging="DeluxeGrid_Paging" CascadeControlID=""
                            TitleColor="141, 143, 149" TitleFontSize="Large" OnRowCommand="NoticeDeluxeGrid_RowCommand"
                            ShowCheckBoxes="true" MultiSelect="false" CssClass="dataList">
                            <Columns>
                                <asp:BoundField DataField="DBCODE" HeaderText="数据库登录信息主键" Visible="false" />
                                <asp:BoundField DataField="DBADDR" HeaderText="数据库连接地址" Visible="true" />
                                <asp:BoundField DataField="DBNAME" HeaderText="数据库名" Visible="true" />
                                <asp:BoundField DataField="DBLOGINID" HeaderText="数据库登录账号" Visible="true" />
                                <asp:BoundField DataField="DBPASSWORD" HeaderText="数据库登录密码" Visible="false" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btneidt" runat="server" CommandArgument='<%# Eval("DBCode") %>'
                                            OnClientClick="return(openDialog(this.bdCode))" Text="修改" bdCode='<%# Eval("DBCode") %>' />
                                        <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%# Eval("DBCode") %>'
                                            CommandName="Delete" Text="删除" OnClientClick="return confirm('温馨提示：删除后将无法恢复!你确定要删除吗？');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pager" />
                            <HeaderStyle CssClass="head" />
                            <AlternatingRowStyle CssClass="aitem" />
                            <EmptyDataTemplate>
                                暂时没有您需要的数据
                            </EmptyDataTemplate>
                            <PagerSettings FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" Mode="NextPreviousFirstLast"
                                NextPageText="下一页" Position="Bottom" PreviousPageText="上一页"></PagerSettings>
                        </MCS:DeluxeGrid>
                        <asp:ObjectDataSource ID="objectDataSource" runat="server" EnablePaging="True" SelectMethod="Query"
                            SelectCountMethod="GetQueryCount" SortParameterName="orderBy" OnSelecting="objectDataSource_Selecting"
                            OnSelected="objectDataSource_Selected" TypeName="MCS.Dynamics.Web.DataSource.DBInfoObjectDataSource"
                            EnableViewState="False">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="whereCondition" Name="where" PropertyName="Value"
                                    Type="String" />
                                <asp:Parameter Direction="InputOutput" Name="totalCount" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 40px; vertical-align: middle;">
                    <div style="text-align: right">
                        <input type="button" runat="server" id="okButton" class="formButton" value="确定(O)"
                            accesskey="O" onserverclick="btn_Save_Click" />
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <!--查询的条件语句放在whereCondition控件里-->
    <input runat="server" type="hidden" id="whereCondition" />
    <%--<div style="display: none">
        <input type="button" runat="server" id="btn_save" class="formButton" value="确定(O)"
            onserverclick="btn_Save_Click" />
    </div>
    <asp:HiddenField ID="HF_CheckVal" runat="server" />--%>
    </form>
</body>
</html>
