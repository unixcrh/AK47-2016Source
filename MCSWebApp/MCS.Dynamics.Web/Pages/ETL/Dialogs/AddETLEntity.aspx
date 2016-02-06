<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddETLEntity.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.AddETLEntity" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>创建抽数实体</title>
    <base target="_self" />
    <link href="../../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/form.css" rel="stylesheet" type="text/css" />
    <script src="../../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .label
        {
            font-size: 16px;
        }
        .commonData
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function Save() {
            if ($(".isEnable>:checked").length == 0) {
                alert("至少选择一个字段");
                return false;
            }

            if ($(".isEnable>:checked").parent().siblings(".primaryKeyCheck").children(":checked").length == 0) {
                alert("实体必须有主键");
                return false;
            }
            //多表必须选择外键
            if ($(".griditem").length > 1) {
                var $grid = $(".griditem");
                for (var o = 1; o < $grid.length; o++) {
                    if ($grid.eq(o).find(".isEnable>:checked").length > 0) {

                        var resultRefTableVaild = false;   //验证是否有外键
                        var resultRefFieldVaild = false;   //验证外键字段是否为勾选的字段
                        $grid.eq(o).find(".isEnable>:checked").each(function () {
                            var $tableRef = $(this).parent().siblings("td.RefTableNameChoose").children("select");
                            if ($tableRef.val() != "--请选择--") {
                                resultRefTableVaild = true;
                            }
                        });
                        if (!resultRefTableVaild) {
                            alert("表[" + $grid.eq(o).attr("title") + "]应至少有一个外键");
                            return false;
                        }
                    }
                }
            }

            if ($("#checkIsCommon").attr("checked")) {
                if ($('.commonKey :checked').length != 1 || $('.commonValue :checked').length != 1) {
                    alert("公用数据必须指定键和值");
                    return false;
                }
                if ($('.commonKey :checked').parent().siblings(".commonValue").find(":checked").length != 0) {
                    alert("键和值不能是相同的字段");
                    return false;
                }
            }
            var fileObjArray = [];  //所有字段的载体
            $(".griditem").each(function () {
                var fileObj = {};  //单表所有选择的字段
                $(this).find(".isEnable>:checked").parent().siblings(".fileNameText").children("span").each(function () {
                    fileObj[$(this).text().trim()] = true;
                });
                fileObjArray.push(fileObj);
            });
            //循环验证字段名是否重复
            for (var i = 0; i < fileObjArray.length; i++) {

                for (var j = i + 1; j < fileObjArray.length; j++) {
                    for (var field in fileObjArray[i]) {
                        if (fileObjArray[j][field]) {
                            //此时说明有重复字段名
                            var $griditem = $(".griditem:eq(" + j + ")");  //选择出当前外键表对象
                            var $field = $griditem.find(".isEnable>:checked").parent().siblings(".fileNameText").children("span"); //选择当前外键表勾选的字段对象
                            for (var k = 0; k < $field.length; k++) {  //循环外键表勾选字段名
                                var $this = $field.eq(k);
                                var $tdFiledSelect = $this.parent("td").siblings("td.refFiledNameChoose").children("select");
                                var $tdTalbeSelect = $this.parent("td").siblings("td.RefTableNameChoose").children("select");
                                if ($this.text().trim() == field && $tdFiledSelect.val().trim() != field) {  //判断选择的字段名和选择的主键名是否一致，如果选择不一致则提示不能提交
                                    var table1 = $griditem.attr("title"); //外键表的表名
                                    var talbe2 = $('.griditem:eq(' + i + ')').attr('title'); //引用的主表的表名
                                    alert("表[" + table1 + "]的字段[" + field + "]和表[" + talbe2 + "]的字段[" + field + "]相同,请设置表[" + table1 + "]的字段[" + field + "]为表[" + talbe2 + "]的外键");
                                    return false;

                                }
                            }

                        }

                    }
                }
            }
            $get("btn_save").click();
        }

        function validTableName() {
            var sapTableName = $("#txt_TCode").val();
            var $gridTable = $(".griditem[title='" + sapTableName + "']");
            if ($gridTable.length > 0) {
                alert("存在相同的表[" + sapTableName + "] 请重新选择");
                $("#txt_TCode").select();
            } else {
                $get("btn_GenerateEntity").click();
            }
            return false;
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
                    document.getElementById("hidUser").value = proId[0].User;
                    document.getElementById("hidClient").value = proId[0].Client;
                    document.getElementById("hidApplicationServer").value = proId[0].ApplicationServer;
                    document.getElementById("hidLanguage").value = proId[0].Language;
                    document.getElementById("hidSystemNumber").value = proId[0].SystemNumber;
                }

                $("#addDiv").show();
                $("#chooseDiv").hide();
            });
            $("#backButton").click(function () {
                $("#chooseDiv").show();
                $("#addDiv").hide();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <!-- 董彬添加取得SAP信息 -->
    <input runat="server" type="hidden" id="hidSAPInstanceId" />
    <input runat="server" type="hidden" id="hidUser" />
    <input runat="server" type="hidden" id="hidClient" />
    <input runat="server" type="hidden" id="hidApplicationServer" />
    <input runat="server" type="hidden" id="hidLanguage" />
    <input runat="server" type="hidden" id="hidSystemNumber" />
    <div id="chooseDiv">
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
    <!--强哥原有创建画面 -->
    <div id="addDiv" style="display: none">
        <div>
            <table style="height: 100%; width: 100%" width="100%">
                <tbody>
                    <tr align="center">
                        <td>
                            <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                                width: 100%">
                                <div style="height: 100%; width: 100%">
                                    <!--表单开始-->
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="lefttitle" style="text-align: left;">
                                                <img src="../../../Images/icon_01.gif" />
                                                配置SAP表和字段</div>
                                            <div class="dialogContent">
                                                <div>
                                                    <asp:Repeater runat="server" ID="rpt_Entities" OnItemDataBound="rpt_Entities_ItemDataBound"
                                                        OnItemCommand="rpt_Entities_ItemCommand">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div>
                                                                <fieldset>
                                                                    <legend class="label">
                                                                        <%#Eval("Key") %><asp:LinkButton runat="server" ID="del_item" CommandName="del_item"
                                                                            CommandArgument='<%#Eval("Key") %>'>移除</asp:LinkButton></legend>
                                                                    <div style="padding-top: 5px;">
                                                                    </div>
                                                                    <asp:GridView runat="server" ID="grid" AutoGenerateColumns="False" CellPadding="4"
                                                                        ForeColor="#333333" GridLines="None" CssClass="griditem" Width="100%">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <input type="checkbox" onclick="if (this.checked) {$(this).parents('.griditem').find('.isEnable input:checkbox').attr('checked',true); }else{$(this).parents('.griditem').find('.isEnable input:checkbox').attr('checked',false);} " />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="cb_IsEnable" Checked='<%# Eval("IsEnable") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="40px" HorizontalAlign="left" CssClass="isEnable" />
                                                                                <HeaderStyle Width="40px" HorizontalAlign="left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="主键">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="cb_IsPrimaryKey" Checked='<%# Eval("IsPrimaryKey") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="primaryKeyCheck" />
                                                                                <HeaderStyle Width="40px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="字段名">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lbl_FieldName" Text='<%#Eval("FieldName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="80px"></HeaderStyle>
                                                                                <ItemStyle Width="80px" CssClass="fileNameText"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="描述">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lbl_FieldDesc" Text='<%#Eval("FieldDesc")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="130px"></HeaderStyle>
                                                                                <ItemStyle Width="130px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="类型">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lbl_FieldType" Text='<%#Eval("FieldType")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="100px"></HeaderStyle>
                                                                                <ItemStyle Width="100px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="长度">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lbl_FieldLength" Text='<%#Eval("FieldLength")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="40px"></HeaderStyle>
                                                                                <ItemStyle Width="40px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="索引">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="cb_IsIndex" Checked='<%# Eval("IsIndex") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                                <HeaderStyle Width="40px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="键">
                                                                                <ItemTemplate>
                                                                                    <span class="commonKey" onclick="javascript:var $com= $('.commonKey').not(this); $com.find(':checked').attr('checked',false)">
                                                                                        <asp:CheckBox runat="server" ID="cb_IsKey" Checked='<%#Eval("IsKey") %>' Width="100%" />
                                                                                    </span>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <HeaderStyle Width="40px" />
                                                                                <ItemStyle Width="40px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="值">
                                                                                <ItemTemplate>
                                                                                    <span class="commonValue" onclick="javascript:var $com= $('.commonValue').not(this); $com.find(':checked').attr('checked',false)">
                                                                                        <asp:CheckBox runat="server" ID="cb_IsValue" Checked='<%# Eval("IsValue") %>' Width="100%" />
                                                                                    </span>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                                                <HeaderStyle Width="40px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="引用表">
                                                                                <ItemTemplate>
                                                                                    <soa:HBDropDownList runat="server" ID="ddl_RefTableName" Width="100%" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddl_RefTableName_SelectedIndexChanged" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="60px"></HeaderStyle>
                                                                                <ItemStyle Width="60px" CssClass="RefTableNameChoose" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="引用字段">
                                                                                <ItemTemplate>
                                                                                    <soa:HBDropDownList runat="server" ID="ddl_RefFieldName" Width="100%" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="100px"></HeaderStyle>
                                                                                <ItemStyle Width="100px" CssClass="refFiledNameChoose" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#2461BF" />
                                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#EFF3FB" />
                                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                    </asp:GridView>
                                                                    <div style="padding-top: 5px;">
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="lefttitle" style="text-align: left;">
                                                <img src="../../../Images/icon_01.gif" />
                                                添加SAP表结构</div>
                                            <div class="dialogContent">
                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td style="text-align: right; width: 70px;">
                                                            SAP表名<span style="color: red;">*</span>
                                                        </td>
                                                        <td>
                                                            <soa:HBTextBox runat="server" ID="txt_TCode" Width="200px" Text="" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txt_TCode" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <input type="button" class="formButton" value="添加SAP表结构(C)" onclick="validTableName()" />
                                                            <span style="display: none;">
                                                                <soa:SubmitButton runat="server" class="formButton" Text="添加SAP表结构(C)" AccessKey="C"
                                                                    PopupCaption="正在操作..." ID="btn_GenerateEntity" OnClick="btn_GenerateEntity_Click" />
                                                            </span>
                                                            <asp:CheckBox ID="checkIsCommon" Text="公用数据" AutoPostBack="True" runat="server" OnCheckedChanged="checkIsCommon_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div name='bottom'>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                            <input type="button" runat="server" id="backButton" class="formButton" value="上一步(S)"
                                accesskey="S" />
                            <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
                                accesskey="S" onclick="return Save();" />
                            <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                            <div style="display: none;">
                                <soa:SubmitButton runat="server" Text="保存..." PopupCaption="正在操作..." ID="btn_save"
                                    OnClick="btn_Save_Click" />
                                <soa:HBDropDownList ID="ddl_FieldType" runat="server">
                                </soa:HBDropDownList>
                                <asp:HiddenField ID="hidd_Users" runat="server" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
