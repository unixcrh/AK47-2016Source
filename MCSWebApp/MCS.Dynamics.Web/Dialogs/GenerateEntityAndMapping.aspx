<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateEntityAndMapping.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.GenerateEntityAndMapping" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>生成实体</title>
    <base target="_self" />
    <link href="../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../css/form.css" type="text/css" rel="stylesheet" />
    <script src="../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SAPInstanceId = document.getElementById("hidSAPInstanceId").value;
            if (SAPInstanceId == "" || SAPInstanceId == null) {
                $("#addDiv").hide();
                $("#chooseDiv").show();
            } else {
                $("#addDiv").show();
                $("#chooseDiv").hide();
            }
        }
        function Save() {
            $get("btn_save").click();
        }
        $(document).ready(function () {
            $("#nextButton").click(function () {
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
                    document.getElementById("hidSAPInstanceId").value = proId[0].ClientID;
                }

                $("#addDiv").show();
                $("#chooseDiv").hide();
            });
            $("#backButton").click(function () {
                $("#addDiv").hide();
                $("#chooseDiv").show();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <input runat="server" type="hidden" id="hidSAPInstanceId" />
    <div id="chooseDiv" runat="server">
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="Div1" class="dialogContent" style="overflow: auto; height: 100%; width: 100%">
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
                                    <mcs:DeluxeGrid ID="gridUser" runat="server" DataSourceMaxRow="0" AllowPaging="True"
                                        DataSourceID="objectDataSource" PageSize="10" Width="100%" DataKeyNames="ClientID"
                                        ExportingDeluxeGrid="False" GridTitle="实体列表" AutoGenerateColumns="False" OnPageIndexChanging="gridUser_PageIndexChanging"
                                        ShowCheckBoxes="True">
                                        <Columns>
                                            <asp:BoundField DataField="ClientID" HeaderText="SAPInstanceId" ControlStyle-Width="150"
                                                ItemStyle-CssClass="ClientID" />
                                            <asp:BoundField DataField="User" HeaderText="SAP用户名" ControlStyle-Width="150" ItemStyle-CssClass="UepID" />
                                            <asp:BoundField DataField="Client" HeaderText="SAPClient" ControlStyle-Width="150"
                                                ItemStyle-CssClass="SapServers_Client" />
                                            <asp:BoundField DataField="ApplicationServer" HeaderText="SAP服务器地址" ControlStyle-Width="150"
                                                ItemStyle-CssClass="SapServers_ApplicationServer" />
                                            <asp:BoundField DataField="Language" HeaderText="SAP语言" ControlStyle-Width="150"
                                                ItemStyle-CssClass="UepID" />
                                            <asp:BoundField DataField="SystemNumber" HeaderText="SAP系统编号" ControlStyle-Width="150"
                                                ItemStyle-CssClass="UepID" />
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
                        <input type="button" runat="server" id="nextButton" class="formButton" value="下一步"
                            accesskey="S" />
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="window.close();" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!--原有创建画面 -->
    <div id="addDiv" runat="server" style="display: none">
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <!--表单开始-->
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../Images/icon_01.gif" />
                                    生成实体</div>
                                <div class="dialogContent">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td style="text-align: right; width: 70px;">
                                                TCode<span style="color: red;">*</span>
                                            </td>
                                            <td>
                                                <soa:HBTextBox runat="server" ID="txt_TCode" Width="200px" Text="" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txt_TCode" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <soa:SubmitButton runat="server" class="formButton" Text="获取BDC结构(C)" AccessKey="C"
                                                    PopupCaption="正在操作..." ID="btn_GenerateEntity" OnClick="btn_GenerateEntity_Click" />
                                                <asp:Button ID="btn_GenerateEntityByRFC" class="formButton" runat="server" OnClick="btn_GenerateEntityByRFC_Click"
                                                    Text="获取RFC结构" />
                                                <%--<asp:Button ID="Button2" class="formButton" runat="server" OnClick="Button2_Click"
                                                    Text="调用Table" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../Images/icon_01.gif" />
                                        实体编辑</div>
                                    <div>
                                        <asp:Repeater runat="server" ID="rpt_Entities" OnItemDataBound="rpt_Entities_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <soa:ClientGrid runat="server" ID="grid" ShowEditBar="False" AllowPaging="false"
                                                        AutoPaging="false" Caption='<%#Eval("Key") %>' ShowCheckBoxColumn="False" Width="100%"
                                                        AutoBindOnLoad="True">
                                                        <Columns>
                                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'left',fontWeight:'bold'}"
                                                                ItemStyle="{width:'30px',textAlign:'left'}">
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="rowIndex" HeaderText="序号" DataType="Integer" ItemStyle="{width:'30px',textAlign:'center'}"
                                                                HeaderStyle="{width:'30px',textAlign:'center'}">
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="FieldName" HeaderText="字段名" DataType="String" HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="TextBox" />
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="FieldDesc" HeaderText="字段描述" DataType="String" HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="TextBox" />
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="FieldType" HeaderText="字段类型" DataType="String" HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="DropdownList" TemplateControlID="ddl_FieldType" />
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="FieldLength" HeaderText="字段长度" DataType="Integer"
                                                                HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="TextBox" />
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="FieldDefaultValue" HeaderText="字段默认值" DataType="String"
                                                                HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="TextBox" />
                                                            </soa:ClientGridColumn>
                                                            <%--<soa:ClientGridColumn DataField="IsStruct" HeaderText="结构" DataType="Boolean" HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="TextBox" />
                                                            </soa:ClientGridColumn>
                                                            <soa:ClientGridColumn DataField="ParamDirection" HeaderText="方向" DataType="String" HeaderStyle="{textAlign:'left'}">
                                                                <EditTemplate EditMode="TextBox" />
                                                            </soa:ClientGridColumn>--%>
                                                        </Columns>
                                                    </soa:ClientGrid>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
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
                        <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
                            accesskey="S" onclick="return Save();" />
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
    </form>
</body>
</html>
