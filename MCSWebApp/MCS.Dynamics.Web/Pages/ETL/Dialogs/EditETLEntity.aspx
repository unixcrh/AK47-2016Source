<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditETLEntity.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.EditETLEntity" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑实体</title>
    <base target="_self" />
    <link href="../../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/form.css" type="text/css" rel="stylesheet" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../scripts/ETLEntity/EntityDetails.js" type="text/javascript"></script>
    <script src="../../../scripts/pc.js" type="text/javascript"></script>
    <script src="../../../scripts/json2.js" type="text/javascript"></script>
    <script src="../../../scripts/EntityCommon.js" type="text/javascript"></script>
    <script type="text/javascript">

        Request.IntalData();
        var $pwd;
        $(document).ready(function () {
            setTimeout(function () {
                var $pwdTd = $("#propertyForm_Cell_ETL_Pwd").next();
                $pwd = $pwdTd.find("input").hide();
                var $newPwd = $("<input type='password' name='newETL_Pwd'>").addClass("ajax__propertyGrid_input ajax__propertyGrid_input_alignLeft").focus(function () {
                    $(".ajax__propertyForm_nameCell_selected").removeClass("ajax__propertyForm_nameCell_selected").removeClass("ajax_propertyForm_defaultValue_diff_style");

                    $("#propertyForm_Cell_ETL_Pwd").addClass("ajax__propertyForm_nameCell_selected").addClass("ajax_propertyForm_defaultValue_diff_style");
                }).blur(function () {
                    $("#propertyForm_Cell_ETL_Pwd").removeClass("ajax__propertyForm_nameCell_selected").removeClass("ajax_propertyForm_defaultValue_diff_style");
                    $pwd.val($(this).val());
                }).val($pwd.val());

                $newPwd.appendTo($pwdTd);

                $("#propertyForm_Cell_UepUsers").next().find("input").attr("readOnly", true).focus(function () {
                    var windowDialog = window.showModalDialog("SapUserCompareChoose.aspx", "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=800px;scroll=yes;");
                    if (windowDialog) {
                        $(this).val(windowDialog);
                    }
                });
            }, 100);
        });

        function OnDataBaseInfoAdd(grid, e) {
            e.cancel = true;
            var ds = grid.get_dataSource();
            if (ds.length > 0) {
                alert("数据库登录信息已存在！");
                return;
            }
            var result = showModalDialog("DatabaseInfo.aspx", "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=800px;scroll=yes;");
            if (result) {
                var properties = eval(result);
                var isRef = true;
                $("#HF_TargetConnCode").val(properties["DBCode"]);
                if (isRef) {
                    ds.push(properties);
                }
                grid.set_dataSource(ds);
            }
        }
        function OnDataBaseInfoEdit(grid, e) {
            e.autoFormat = false;
            switch (e.column.dataField) {
                case "options":
                    e.autoFormat = true;
                    var text = "编辑";
                    var alink = { nodeName: "A", properties: { href: "javascript:void(0);", innerText: text} };
                    alink = $HGDomElement.createElementFromTemplate(alink, e.container);
                    e.editor.set_editorElement(alink);
                    $(alink).click(function () {
                        var ds = grid.get_dataSource();
                        var result = showModalDialog("DatabaseInfo.aspx", "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=800px;scroll=yes;");
                        if (result) {
                            var jsonArray = [result];
                            grid.set_dataSource(jsonArray);
                        }
                    });
                    break;
                default:
                    e.autoFormat = true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HF_EnumValueKey" runat="server" />
    <asp:HiddenField ID="HF_EnumKeyValue" runat="server" />
    <asp:HiddenField ID="HF_TargetConnCode" runat="server" />
    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
    </div>
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <soa:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true"
        AutoBinding="True" ValidateUnbindProperties="false" AllowClientCollectData="true">
        <ItemBindings>
            <soa:DataBindingItem ControlID="grid" ControlPropertyName="InitialData" DataPropertyName="EtlFields"
                Direction="Both">
            </soa:DataBindingItem>
            <%-- <soa:DataBindingItem ControlID="gridUepUsers" ControlPropertyName="InitialData" DataPropertyName="UepUsers"
                Direction="Both">
            </soa:DataBindingItem>--%>
        </ItemBindings>
    </soa:DataBindingControl>
    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <div class="dialogTitle">
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../../../Images/icon_01.gif" />
                                        编辑实体 <span class="pc-timepointmark">
                                            <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
                                        </span>
                                    </div>
                                </div>
                                <div>
                                    <soa:PropertyForm runat="server" ID="propertyForm" Width="100%" Height="100%" AutoSaveClientState="False" />
                                </div>
                                <div>
                                    <table style='width: 100%;' cellspacing='0' cellpadding='0'>
                                        <tr>
                                            <td class='ajax__propertyForm_header'>
                                                其他属性
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style='width: 100%' cellspacing='0' cellpadding='0'>
                                                    <tr>
                                                        <td class='ajax__propertyFrom_valueCell' style='width: 50%;'>
                                                            上次同步时间
                                                        </td>
                                                        <td class='ajax__propertyForm_valueCell' style='width: 50%; border-left: 1px solid #f0f0f0'>
                                                            <mcs:DeluxeDateTime ID="lastUpdateTime" runat="server" Width="200px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="dialogContent">
                                    <soa:ClientGrid runat="server" ID="gridDBInfo" ShowEditBar="False" AllowPaging="false"
                                        AutoPaging="false" ShowCheckBoxColumn="False" Width="100%" OnPreRowAdd="OnDataBaseInfoAdd"
                                        OnCellCreatingEditor="OnDataBaseInfoEdit">
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'center',fontWeight:'bold'}"
                                                ItemStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="DBCode" HeaderText="数据库信息主键" DataType="String" ItemStyle="{textAlign:'center'}"
                                                Visible="false">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="DBAddr" HeaderText="数据库连接地址" DataType="String" ItemStyle="{textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="DBName" HeaderText="数据库名" DataType="String" ItemStyle="{textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="DBLoginID" HeaderText="数据库登录账号" ItemStyle="{textAlign:'center'}"
                                                DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="DBPassword" HeaderText="数据库登录密码" ItemStyle="{textAlign:'center'}"
                                                DataType="String" Visible="false">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="options" HeaderText="操作" DataType="String" ItemStyle="{textAlign:'center'}">
                                                <EditTemplate EditMode="A" />
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
                                </div>
                                <!--表单开始-->
                                <!----------编辑ETL抽数账号开始---------->
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../../../Images/icon_01.gif" />
                                    编辑ETL抽数账号</div>
                                <div class="dialogContent">
                                    <soa:ClientGrid runat="server" ID="gridUepUsers" ShowEditBar="False" AllowPaging="false"
                                        AutoPaging="false" ShowCheckBoxColumn="False" Width="100%" OnPreRowAdd="clientGrid.OnPreRowAddUsers"
                                        OnCellCreatingEditor="clientGrid.OnCellSAPEditor">
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'center',fontWeight:'bold'}"
                                                ItemStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="SAPInstanceId" HeaderText="用户映射编码" DataType="String"
                                                ItemStyle="{textAlign:'left','padding-left':'5px'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="SapID" HeaderText="SAP用户名" DataType="String" ItemStyle="{textAlign:'left'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="SapServers_Client" HeaderText="SAP客户端" DataType="String"
                                                ItemStyle="{textAlign:'left'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="SapServers_ApplicationServer" HeaderText="SAP应用服务器"
                                                ItemStyle="{textAlign:'left'}" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="SapServers_SystemNo" HeaderText="SAP实例编号" DataType="String"
                                                ItemStyle="{textAlign:'left'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="options" HeaderText="操作" DataType="String" ItemStyle="{textAlign:'center'}">
                                                <EditTemplate EditMode="A" />
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
                                </div>
                                <!----------编辑ETL抽数账号结束---------->
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../../../Images/icon_01.gif" />
                                    编辑实体属性</div>
                                <div class="dialogContent">
                                    <soa:ClientGrid runat="server" ID="grid" ShowEditBar="true" AllowPaging="false" AutoPaging="false"
                                        ShowCheckBoxColumn="true" Width="100%" OnPreRowAdd="clientGrid.OnPreRowAdd" OnCellCreatingEditor="clientGrid.OnCellCreatingEditor"
                                        OnDataFormatting="clientGrid.OnDataFormatting">
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'center',fontWeight:'bold'}"
                                                ItemStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="rowIndex" HeaderText="序号" DataType="Integer" ItemStyle="{width:'30px',textAlign:'center'}"
                                                HeaderStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Name" HeaderText="字段名" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="FieldType" HeaderText="字段类型" DataType="String" EditorReadOnly="True"
                                                EditorEnabled="False">
                                                <EditTemplate EditMode="DropdownList" TemplateControlID="ddl_FieldType" />
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Description" HeaderText="字段描述" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Length" HeaderText="字段长度" DataType="Integer">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="IsPk" HeaderText="主键" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="RefTable" HeaderText="关联表" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="RefField" HeaderText="关联字段" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="options" HeaderText="操作" DataType="String">
                                                <EditTemplate EditMode="A" />
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
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
                        <asp:Label ID="errorMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                        <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
                            accesskey="S" onclick="return ($pc.getEnabled(this) && onSaveClick());" />
                        <input type="button" class="formButton" value="查看SQL(Q)" accesskey="Q" onclick="javascript:window.showModalDialog('ViewCreateTableSql.aspx?Code='+Request.QueryString('ID'),null,'dialogWidth:1000px; dialogHeight:600px;center:yes;help:no;resizable:yes;scroll:no;status:no');" />
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
    <input type="hidden" id="properties" runat="server" />
    <input type="hidden" id="currentSchemaType" runat="server" />
    <asp:HiddenField ID="connStrHidd" runat="server" />
    </form>
</body>
</html>
