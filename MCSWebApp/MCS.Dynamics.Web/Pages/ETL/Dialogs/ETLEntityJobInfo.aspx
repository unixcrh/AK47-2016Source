<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ETLEntityJobInfo.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.ETLEntityJobInfo" ValidateRequest="false" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>任务</title>
    <base target="_self" />
    <link href="../../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/form.css" type="text/css" rel="stylesheet" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../scripts/ETLEntity/EtlJobDetail.js" type="text/javascript"></script>
    <script src="../../../scripts/ETLEntity/common.js" type="text/javascript"></script>
    <script src="../../../scripts/pc.js" type="text/javascript"></script>
    <style>
        .jobPropertyTitleTD
        {
            text-align: right;
        }
        .jobPropertyContentTD
        {
        }
    </style>
    <script type="text/javascript">
        //invoking service related methods

        function openServiceDefDialog(key) {
            $("#invokingServiceGrid").cancel = true;
            var url = "/MCSWebApp/WorkflowDesigner/ModalDialog/WfServiceOperationDefEditor.aspx?hasRtn=false";
            var sFeature = "dialogWidth:680px; dialogHeight:460px;center:yes;help:no;resizable:no;scroll:no;status:no";
            var result;

            if (typeof (key) === "number") {
                var def = allSvcOperationDef[key];

                result = window.showModalDialog(url,
                {
                    jsonStr: Sys.Serialization.JavaScriptSerializer.serialize(def),
                    existDefJsonStr: Sys.Serialization.JavaScriptSerializer.serialize(allSvcOperationDef)
                },
				sFeature);

            } else if ("" != key) {
                var opDef = allSvcOperationDef.get(key, function (o, v) {
                    return o.Key == v;
                });

                result = window.showModalDialog(url,
                {
                    jsonStr: Sys.Serialization.JavaScriptSerializer.serialize(opDef),
                    existDefJsonStr: Sys.Serialization.JavaScriptSerializer.serialize(allSvcOperationDef)
                },
				sFeature);
            } else {
                result = window.showModalDialog(url,
                {
                    jsonStr: null,
                    existDefJsonStr: Sys.Serialization.JavaScriptSerializer.serialize(allSvcOperationDef)
                },
				sFeature);
            }

            if (result) {
                var resultObj = Sys.Serialization.JavaScriptSerializer.deserialize(result.jsonStr);
                if (resultObj) {
                    var fnCompare = function (o, v) {
                        return o.Key == v;
                    };

                    var isExist = allSvcOperationDef.has(resultObj.Key, fnCompare);

                    if (isExist == false) {
                        allSvcOperationDef.push(resultObj);
                    }
                    else {
                        allSvcOperationDef.remove(resultObj.Key, fnCompare);
                        allSvcOperationDef.push(resultObj);
                    }
                }
                bindGrid(allSvcOperationDef);
            }
        }

        function bindGrid(dataSource) {
            var gridDatasource = dataSource ? dataSource : [];
            var grid = $find("invokingServiceGrid");
            grid.set_dataSource(gridDatasource);
            //grid.dataBind();
            grid = null;
            gridDatasource = null;
        }

        function createSvcOperation(grid, e) {
            e.cancel = true;
            openServiceDefDialog("");
        }

        function removeInvokingService() {
            var grid = $find("invokingServiceGrid");
            var selectedData = grid.get_selectedData();
            if (selectedData.length <= 0)
                alert("请选择要删除的数据。");
            if (!(selectedData.length > 0 && confirm("确定删除？"))) {
                return;
            }
            for (var i = 0; i < selectedData.length; i++) {
                var element = selectedData[i];
                allSvcOperationDef.remove(element.Key, function (o, v) {
                    return o.Key == v;
                });
            }
            bindGrid(allSvcOperationDef);
        }

        function getServiceItemLink(e, text) {
            var lnkNode;
            lnkNode = document.createElement("a");
            lnkNode.href = "javascript:void(0);";
            e.cell.replaceChild(lnkNode, e.cell.firstChild);
            lnkNode.appendChild(document.createTextNode(text));

            if (e.data["Key"]) {
                lnkNode.onclick = function () { eval("openServiceDefDialog('" + e.data["Key"] + "')") };
            } else {
                var ind = Array.indexOf(allSvcOperationDef, e.data);
                if (ind >= 0) {
                    lnkNode.onclick = function () { eval("openServiceDefDialog(" + ind + ")"); };
                }
            }
            lnkNode = null;
        }

        function invokingServiceGridCellBound(g, e) {
            var dataFieldName = e.column.dataField;

            switch (dataFieldName) {
                case 'Key':
                    getServiceItemLink(e, e.data["Key"] || "(未定义Key)");
                    break;
                case 'AddressDef':
                    getServiceItemLink(e, e.data[dataFieldName] == null ? "<没有地址>" : e.data[dataFieldName].Address.toString());
                    break;
                case 'Params':
                    e.cell.innerText = e.data["Params"].length;
                    break;
            }

            lnkNode = null;
        }

        function scheduleCellBound(g, e) {
            var dataFieldName = e.column.dataField;

            switch (dataFieldName) {
                case 'Name':
                    var linkText = "<a href='#' style='color: Black;' onclick='openScheduleDialog(\"{1}\");'>{0}</a>";

                    if (typeof (e.data.ID) != "undefined")
                        e.cell.innerHTML = String.format(linkText, e.data["Name"].toString(), e.data["ID"].toString());
                    break;
                case "Description":
                    linkText = "<a href='#' style='color: Black;' onclick='openScheduleDialog(\"{1}\");'>{0}</a>";

                    if (typeof (e.data.ID) != "undefined")
                        e.cell.innerHTML = String.format(linkText, e.data["Description"].toString(), e.data["ID"].toString());

                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
    </div>
    <pc:SceneControl ID="SceneControl1" runat="server" />
    <%--<soa:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true"
        AutoBinding="True" ValidateUnbindProperties="false" AllowClientCollectData="true">
        <ItemBindings>
            <soa:DataBindingItem ControlID="txt_JobName" DataPropertyName="Name" />
            <soa:DataBindingItem ControlID="txt_jobCategory" DataPropertyName="Category" />
            <soa:DataBindingItem ControlID="txt_JobDescription" DataPropertyName="Description" />
            <soa:DataBindingItem ControlID="ddl_JobType" DataPropertyName="JobType" />
            <soa:DataBindingItem ControlID="ddl_Enabled" DataPropertyName="Enabled" />

        </ItemBindings>
    </soa:DataBindingControl>--%>
    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div style="overflow: auto;height: 100%; width: 100%">
                            <div class="dialogTitle">
                                <div class="lefttitle" style="text-align: left;">
                                    <img src="../../../Images/icon_01.gif" />
                                    任务
                                </div>
                            </div>
                            <div>
                                <fieldset title="">
                                    <legend class="label">基本信息</legend>
                                    <table style="width: 98%;">
                                        <tr>
                                            <td class="jobPropertyTitleTD">
                                                名称
                                            </td>
                                            <td class="jobPropertyContentTD">
                                                <soa:HBTextBox ID="txt_JobName" runat="server"></soa:HBTextBox>
                                            </td>
                                            <td class="jobPropertyTitleTD">
                                                类型
                                            </td>
                                            <td class="jobPropertyContentTD">
                                                <soa:HBDropDownList ID="ddl_JobType" runat="server">
                                                </soa:HBDropDownList>
                                            </td>
                                            <td style="text-align: right;">
                                                是否启用:
                                            </td>
                                            <td>
                                                <soa:HBDropDownList ID="ddl_Enabled" runat="server">
                                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                                </soa:HBDropDownList>
                                                <%--<select id="ddl_Enabled" runat="server">
                                                        <option value="true">是</option>
                                                        <option value="false">否</option>
                                                    </select>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="jobPropertyTitleTD">
                                                描述
                                            </td>
                                            <td class="jobPropertyContentTD">
                                                <soa:HBTextBox ID="txt_JobDescription" runat="server" Width="100%"></soa:HBTextBox>
                                            </td>
                                            <td class="jobPropertyTitleTD">
                                                是否自动
                                            </td>
                                            <td class="jobPropertyContentTD">
                                                <asp:CheckBox ID="ch_IsAuto" Width="100%" runat="server" />
                                            </td>
                                            <td class="jobPropertyTitleTD">
                                                分类
                                            </td>
                                            <td class="jobPropertyContentTD">
                                                <soa:HBTextBox ID="txt_jobCategory" Width="100%" runat="server"></soa:HBTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="jobPropertyTitleTD">
                                                增量
                                            </td>
                                            <td class="jobPropertyContentTD">
                                                <asp:CheckBox ID="ch_IsIncrement" Width="100%" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <!--表单开始-->
                            <fieldset id="scheduleList" title="">
                                <legend class="label">执行计划设置</legend>
                                <div class="dialogContent">
                                    <soa:ClientGrid runat="server" ID="grid" ShowEditBar="true" AllowPaging="false" AutoPaging="false"
                                        ShowCheckBoxColumn="true" Width="100%" OnPreRowAdd="clientGrid.OnPreRowAdd" OnClientCellDataBound="scheduleRowCreatingEtitor">
                                        <%-- OnCellCreatingEditor="clientGrid.OnCellCreatingEditor"
                                        OnDataFormatting="clientGrid.OnDataFormatting">--%>
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'center',fontWeight:'bold'}"
                                                ItemStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="rowIndex" HeaderText="序号" DataType="Integer" ItemStyle="{width:'30px',textAlign:'center'}"
                                                HeaderStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="ID" Visible="false" HeaderText="计划ID" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Name" HeaderText="计划名称" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Description" HeaderText="计划描述" DataType="String"
                                                EditorReadOnly="True" EditorEnabled="False">
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
                                </div>
                            </fieldset>
                            <fieldset title="etlEntityList" id="etlEntityList">
                                <legend class="label">ETL实体</legend>
                                <div class="dialogContent">
                                    <soa:ClientGrid runat="server" ID="gridEtl" ShowEditBar="true" AllowPaging="false"
                                        AutoPaging="false" ShowCheckBoxColumn="true" Width="100%" OnPreRowAdd="clientGrid.OnPreRowAdd"
                                        OnClientCellDataBound="rowCreatingEditor">
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" HeaderStyle="{width:'30px',textAlign:'center',fontWeight:'bold'}"
                                                ItemStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="rowIndex" HeaderText="序号" DataType="Integer" ItemStyle="{width:'30px',textAlign:'center'}"
                                                HeaderStyle="{width:'30px',textAlign:'center'}">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="ID" Visible="false" HeaderText="实体ID" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="CodeName" HeaderText="实体名称" DataType="String">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Description" HeaderText="实体描述" DataType="String"
                                                EditorReadOnly="True" EditorEnabled="False">
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Where" HeaderText="Where条件" DataType="String" EditorReadOnly="True"
                                                EditorEnabled="False">
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
                                </div>
                            </fieldset>
                            <div id="divInvokingService" style="display: none;">
                                <fieldset title="">
                                    <legend class="label">Web服务设置</legend>
                                    <%--<div style="background-color: #C0C0C0">
                                        <a href="#" onclick="createSvcOperation();">
                                            <img src="/MCSWebApp/Images/appIcon/15.gif" alt="添加" border="0" />
                                        </a><a href="#" onclick="removeInvokingService();">
                                            <img src="/MCSWebApp/Images/16/delete.gif" alt="删除" border="0" />
                                        </a>
                                    </div>--%>
                                    <%--<soa:ClientGrid ID="ClientGrid1" runat="server" PageSize="10" AllowPaging="true"
                                        AutoPaging="true" ShowEditBar="false" Width="100%" OnClientCellDataBound="invokingServiceGridCellBound">--%>
                                    <soa:ClientGrid ID="invokingServiceGrid" runat="server" ShowEditBar="true" PageSize="10"
                                        AllowPaging="false" AutoPaging="false" Width="100%" OnClientCellDataBound="invokingServiceGridCellBound"
                                        OnPreRowAdd="createSvcOperation">
                                        <Columns>
                                            <soa:ClientGridColumn SelectColumn="true" ShowSelectAll="true" ItemStyle="{width:'30px',TEXT-ALIGN: 'center' }"
                                                HeaderStyle="{width:'30px'}" />
                                            <soa:ClientGridColumn DataField="Key" HeaderText="Key" DataType="String" ItemStyle="{width:'90px',word-wrap:'break-word'}">
                                                <EditTemplate EditMode="None" />
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="AddressDef" HeaderText="服务地址" DataType="String"
                                                HeaderStyle="{TEXT-ALIGN:'center'}" ItemStyle="{width:'500px',word-wrap:'break-word'}">
                                                <EditTemplate EditMode="None" />
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="OperationName" HeaderText="方法名称" DataType="String"
                                                HeaderStyle="{width:'80px'}" ItemStyle="{width:'80px',word-wrap:'break-word'}">
                                                <EditTemplate EditMode="None" />
                                            </soa:ClientGridColumn>
                                            <soa:ClientGridColumn DataField="Params" HeaderText="参数个数" DataType="String" HeaderStyle="{width:'80px'}"
                                                ItemStyle="{width:'80px',TEXT-ALIGN:'center',word-wrap:'break-word'}">
                                                <EditTemplate EditMode="None" />
                                            </soa:ClientGridColumn>
                                        </Columns>
                                    </soa:ClientGrid>
                                </fieldset>
                                <br />
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
                            accesskey="S" onclick="return onSaveClick();" />
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
    <input type="hidden" id="schedules" runat="server" />
    <input type="hidden" id="etlEntities" runat="server" />
    <input type="hidden" id="currentSchemaType" runat="server" />
    <input type="hidden" id="conditions" runat="server" />
    <input type="hidden" id="services" runat="server" />
    </form>
</body>
</html>
