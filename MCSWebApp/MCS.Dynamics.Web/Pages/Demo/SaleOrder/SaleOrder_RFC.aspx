<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaleOrder_RFC.aspx.cs"
    Inherits="MCS.Dynamics.Web.SaleOrder_RFC" %>

<%@ Register Assembly="MCS.Library.SOA.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="SOA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售订单</title>
    <link href="~/Css/Bigeasyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/demo.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/icon.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../scripts/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../../scripts/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../../scripts/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../scripts/json2.js" type="text/javascript"></script>
    <%--<script src="../../Scripts/createWorkOrder.js"></script>--%>
    <script type="text/javascript">



        function addRows() {
            $('#test').datagrid('appendRow', loadDataJson);
        }
        var products = [
        { productid: 'FI-SW-01', name: 'Koi' },
        { productid: 'K9-DL-01', name: 'Dalmation' },
        { productid: 'RP-SN-01', name: 'Rattlesnake' },
        { productid: 'RP-LI-02', name: 'Iguana' },
        { productid: 'FL-DSH-01', name: 'Manx' },
        { productid: 'FL-DLH-02', name: 'Persian' },
        { productid: 'AV-CB-01', name: 'Amazon Parrot' }
        ];

        //油品代码
        var ypdmJson = [
        { productid: '300032', name: '轻柴油 的8块钱' },
        { productid: '300566', name: '0# 普通柴油' },
        { productid: '300060', name: '93号 车用汽油(Ⅲ)' }
        ];

        //剂量单位
        var jldwJson = [
        { productid: 'TO', name: '吨' }
        ];
        //业务单元代码
        var ywdydmJson = [
        { productid: '1S1p', name: '宁夏银川销售分公司-油库' },
        { productid: '1S2P', name: '宁夏石嘴山销售分公司-油库' },
        { productid: '1S3P', name: '宁夏吴忠销售分公司-油库' },
        { productid: '1S4P', name: '宁夏中宁销售分公司-油库' },
        { productid: '1S5P', name: '宁夏固原销售分公司-油库' }
        ];
        //库存地点代码
        var kcdddmJson = [
        { productid: '025a', name: '银川油库' },
        { productid: '026A', name: '石嘴山油库' },
        { productid: '027A', name: '青铜峡油库 ' },
        { productid: '028A', name: '中宁油库' },
        { productid: '029A', name: '固原油库' }
        ];
        //货币代码
        var hbJson = [
        { productid: 'CNY', name: '人民币' }
        ];







        var json = {
        };


        function createTool() {
            return [{
                id: 'btnadd',
                text: '添加',
                iconCls: 'icon-add',
                handler: function () {
                    $('#btnsave').linkbutton('enable');
                    addRows();
                }
            }, {
                id: 'btndel',
                text: '删除',
                iconCls: 'icon-cut',
                handler: function () {
                    if (lastIndex == undefined) {
                        return;
                    }

                    var row = $datagrid.datagrid('getSelected');

                    $datagrid.datagrid('deleteRow', lastIndex);
                    lastIndex = undefined;
                    editIndex = undefined;
                }
            }, {
                id: 'btnSave',
                text: "接收",
                iconCls: 'icon-save',
                handler: function () {

                    if (endedit()) {
                        //$datagrid.datagrid('selectRow', rowIndex)
                        //        .datagrid('beginEdit', rowIndex);
                        //editIndex = rowIndex;
                        $datagrid.datagrid('acceptChanges');

                    }
                }
            }];
        }

        var loadDataJson = { "LFDAT": "20140915", "MATNR": "300032", "MAKTX": "轻柴油 的8块钱", "KWMENG": "12", "VRKME": "TO", "VRKME_NAME": "吨", "WERKS": "1S1P", "WERKS_NAME": "宁夏银川销售分公司-油库", "LGORT": "025A", "LGORT_NAME": "银川油库", "ZNETPR_C": "21.00", "ZongJinE": "252", "WAERK": "CNY", "WAERK_NAME": "人民币", "BWTAR": "自有", "POSNR": "10" };

        var vailtory = null;
        var $datagrid = null;
        var endedit = null;
        var lastIndex;
        var editIndex = undefined;

        setTimeout(function () {

            // getOrder_hetong();
            var safeId = $("#safeId").val();
            var tool = createTool();
            var pms = { opm: {}, url: "" };
            if (safeId.length > 0) {  //说明要查询出来

                if ($("#isEdit_text").val() != '0') {
                    tool = [];
                }

                pms.opm = { safeId: safeId, ajaxType: "TB_Biz_SalesOrderDetails" };
                pms.url = "../ajax/getElement1.ashx";
            }
            vailtory = $("#form1").validate({
                rules: {

                    CZDAT: { required: true }

                }
            });

            // alert(vailtory.form());

            $datagrid = $('#test');

            $datagrid.datagrid({
                //pageSize: 10,
                //pagination: true,

                rownumbers: true,
                title: '订单明细',
                url: pms.url,
                queryParams: pms.opm,
                //iconCls: 'icon-save',
                height: 300,
                //nowrap: true,
                autoRowHeight: false,
                striped: true,
                //collapsible: true,
                singleSelect: true,
                //url: 'datagrid_data.json',
                //sortName: 'code',
                //sortOrder: 'desc',
                remoteSort: false,
                //idField: 'code',
                frozenColumns: [[
                        {
                            title: '油品代码', field: 'MATNR', width: 80, sortable: false, //formatter: productFormatter,
                            editor: {
                                type: 'combobox',
                                options: {
                                    valueField: 'productid',
                                    textField: 'productid',
                                    data: ypdmJson,
                                    required: true,
                                    multiple: false,
                                    //validType: "defaulits['MATNR']",
                                    onSelect: function (obj) {
                                        var $ZNETPR_C = $datagrid.datagrid('getEditor', { index: lastIndex, field: 'ZNETPR_C' }).target;
                                        $.post("../ajax/getElement1.ashx", { ajaxType: "getPic_youpin", matnr: obj.productid }, function (data) {
                                            $ZNETPR_C.numberbox("setValue", data);
                                        });
                                        $datagrid.datagrid('getEditor', { index: lastIndex, field: 'MAKTX' }).target.val(obj.name);
                                    }
                                }
                            }
                        },
                        { title: '名称规格', field: 'MAKTX', width: 80, sortable: false, editor: { type: 'text'} }

                    ]],
                columns: [[
                        {
                            field: 'KWMENG', title: '数量', width: 120,
                            editor: {
                                type: 'numberbox',
                                required: true,
                                validType: 'rules',
                                options: {
                                    required: true
                                    // valiType: 'email'
                                }
                            }
                        },
                        {
                            field: 'VRKME', title: '计量单位代码', width: 120,
                            editor: {
                                type: 'combobox',
                                options: {
                                    valueField: 'productid',
                                    textField: 'productid',
                                    data: jldwJson,
                                    multiple: false,
                                    required: true,
                                    // validType: "defaulits['VRKME']",
                                    onSelect: function (obj) {

                                        $datagrid.datagrid('getEditor', { index: lastIndex, field: 'VRKME_NAME' }).target.val(obj.name);
                                    }
                                }
                            }
                        },
                        {
                            field: 'VRKME_NAME', title: '计量单位', width: 120,
                            editor: {
                                type: 'text'
                            }
                        },
                        {
                            field: 'WERKS', title: '业务单元代码', width: 120/*, formatter: productFormatter*/,
                            editor: {
                                type: 'combobox',
                                options: {
                                    valueField: 'productid',
                                    textField: 'productid',
                                    data: ywdydmJson,
                                    required: true,
                                    multiple: false,
                                    onSelect: function (obj) {
                                        // obj.productid;
                                        //                                        var proJson = [];
                                        //                                        $.each(kcdddmJson, function (index, value) {
                                        //                                            if (value.key == obj.productid) {
                                        //                                                proJson.push(value);
                                        //                                                //alert(JSON.stringify(proJson));
                                        //                                            }
                                        //                                        })
                                        $datagrid.datagrid('getEditor', { index: lastIndex, field: 'WERKS_NAME' }).target.val(obj.name);
                                        //$datagrid.datagrid('getEditor', { index: lastIndex, field: 'LGORT' }).target.combobox("loadData", proJson);
                                    }
                                }
                            }
                        },
                        {
                            field: 'WERKS_NAME', title: '业务单元名称', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'text'
                            }

                        },
                        {
                            field: 'LGORT', title: '库存地点代码', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'combobox',
                                options: {
                                    valueField: 'productid',
                                    textField: 'productid',
                                    data: kcdddmJson,
                                    required: true,
                                    multiple: false,
                                    onSelect: function (obj) {
                                        $datagrid.datagrid('getEditor', { index: lastIndex, field: 'LGORT_NAME' }).target.val(obj.name);
                                    }
                                }
                            }

                        },
                        {
                            field: 'LGORT_NAME', title: '库存地点名称', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'text'
                            }

                        },
                        {
                            field: 'ZNETPR_C', title: '价格', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'numberbox', options: { precision: 2 }
                            }

                        },
                        {
                            field: 'ZongJinE', title: '总金额', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'text'
                            }
                        },
                         {
                             field: 'LFDAT', title: '交货日期', width: 220, rowspan: 1, sortable: false,
                             editor:
                             {
                                 type: 'text'
                             }

                         },
                        {
                            field: 'WAERK', title: '货币代码', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'combobox',
                                options: {
                                    valueField: 'productid',
                                    textField: 'productid',
                                    data: hbJson,
                                    multiple: false,
                                    required: true,
                                    onSelect: function (obj) {
                                        $datagrid.datagrid('getEditor', { index: lastIndex, field: 'WAERK_NAME' }).target.val(obj.name);
                                    }
                                }
                            }

                        },
                        {
                            field: 'WAERK_NAME', title: '货币名称', width: 220, rowspan: 1, sortable: false,
                            editor: {
                                type: 'text'
                            }

                        }
                    ]],
                //pagination: true,
                toolbar: tool,
                onBeforeEdit: function (rowIndex, data) {
                    editIndexs = rowIndex;
                },
                onClickRow: function (rowIndex) {

                    if ($("#isEdit_text").val() == '0') {
                        if (editIndex != rowIndex) {
                            if (endedit()) {
                                $datagrid.datagrid('selectRow', rowIndex)
                                            .datagrid('beginEdit', rowIndex);

                                var WERKS = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'WERKS' }); //业务单元代码
                                var mart = WERKS.target.combobox("getValue");
                                //                                    var proJson = [];
                                //                                    $.each(kcdddmJson, function (index, value) {
                                //                                        if (value.key == mart) {
                                //                                            proJson.push(value);
                                //                                            //alert(JSON.stringify(proJson));
                                //                                        }
                                //                                    })
                                // $datagrid.datagrid('getEditor', { index: rowIndex, field: 'LGORT' }).target.combobox("loadData", proJson);
                                //$(MATNR.target).attr("readonly", true);
                                var ed = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'MAKTX' });
                                ed.target.attr("readonly", true);
                                var VRKME_NAME = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'VRKME_NAME' });
                                $(VRKME_NAME.target).attr("readonly", true);
                                var LGORT_NAME = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'LGORT_NAME' });
                                $(LGORT_NAME.target).attr("readonly", true);
                                //数量
                                var KWMENG = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'KWMENG' });
                                //单价
                                var ZNETPR_C = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'ZNETPR_C' });
                                ZNETPR_C.target.numberbox({
                                    required: true,
                                    onChange: function (value) {
                                        var num = KWMENG.target.numberbox("getValue");

                                        if (num == "") {
                                            KWMENG.target.focus();
                                        }
                                        ZongJinE.target.val(value * num);
                                    }
                                });
                                KWMENG.target.numberbox({
                                    onChange: function (value) {
                                        var num = ZNETPR_C.target.numberbox("getValue");
                                        ZongJinE.target.val(value * num);
                                    }
                                });

                                //总金额
                                var ZongJinE = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'ZongJinE' });
                                ZongJinE.target.attr("readonly", true);
                                var dat = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'LFDAT' });
                                dat.target.focus(function () {
                                    WdatePicker();
                                });
                                dat.target.validatebox({
                                    required: true
                                });

                                //人民币
                                var rmb = $datagrid.datagrid('getEditor', { index: rowIndex, field: 'WAERK_NAME' });
                                rmb.target.attr("readonly", true);
                                editIndex = rowIndex;
                            } else {
                                $datagrid.datagrid('selectRow', editIndex);
                            }
                        }
                        lastIndex = editIndex;

                    }

                }
            });


            $datagrid.datagrid("loadData", JSON.parse($("#hiddenDetail").val()));
            //$("#projson").text($("#hiddenDetail").val());
            endedit = function endEditing() {

                if (editIndex == undefined) { return true }
                if ($datagrid.datagrid('validateRow', editIndex)) {

                    $datagrid.datagrid('endEdit', editIndex);


                    editIndex = undefined;
                    return true;
                } else {
                    return false;
                }
            }


        }, 300);
        var editIndexs = null;
        function setData() {
            //添加验证逻辑,把空白数据去掉
            if (!endedit()) {
                $.messager.alert("  ", "请先结束编辑");
                return false;
            }
            var detailsJson = $("#test").datagrid("getData").rows.slice(0);
            //把空白的数据删除
            if (detailsJson.length > 0) {
                for (var i = detailsJson.length - 1; i >= 0; i--) {
                    if (!detailsJson[i].hasOwnProperty("KWMENG")) { //如果不含有该属性  则说明是无数据
                        detailsJson.splice(i, 1);
                    }
                }
            }
            if (detailsJson.length === 0) {
                $.messager.alert(" ", "数据为空");
                return false;
            }
            //alert(vailtory.form());

            if (vailtory.form() == true) {
                $("#hiddenDetail").val(JSON.stringify(detailsJson));
                return true;
            }


            return false;
        }



    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="projson">
    </div>
    <div>
        <table class="wftoolbar" cellpadding="0px" cellspacing="0px">
            <tr>
                <td>
                    <table class="bar">
                        <tr style="text-align: center;">
                            <td>
                                <asp:LinkButton ID="lbtnSave" runat="server" OnClientClick="if(!setData()){return false;}"
                                    OnClick="btnSave_Click" Text="保存">
                                </asp:LinkButton>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div style="float: left;">
        </div>
    </div>
    <asp:HiddenField ID="hiddenDetail" runat="server" />
    <div class='outerDiv'>
        <div class="panel" style="display: block;">
            <div class="panel-header">
                <div class="panel-title">
                    销售订单
                    <asp:Label ID="vbeln_order" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel-tool">
                </div>
            </div>
            <div class="easyui-panel">
                <table class="fromTable">
                    <tbody>
                        <tr>
                            <td class="titleTd">
                                公司：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="BUKRS">
                                    <asp:ListItem Value="4701" Text="公司代码"></asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                            <td class="titleTd">
                                运输方式：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="OIC_MOT">
                                    <asp:ListItem Value="01">陆运</asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleTd">
                                客户：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="KUNNR" required="true" Width="255" validType="exists['#KUNNR']">
                                    <asp:ListItem Value="0000420001" Text="客户名称">
                                    
                                    </asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                            <td class="titleTd">
                                销售代表：
                            </td>
                            <td class="contentTd">
                                <%--<SOA:HBDropDownList runat="server" ID="SALESNUM" />--%>
                                <SOA:HBTextBox ID="SALESNUM" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleTd">
                                销售部门：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="VKBUR">
                                    <asp:ListItem Text="销售部门" Value="4701"></asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                            <td class="titleTd">
                                单据日期：
                            </td>
                            <td class="contentTd">
                                <SOA:HBTextBox ID="CZDAT" runat="server" onfocus="WdatePicker()" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleTd">
                                销售主体：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="VKORG">
                                    <asp:ListItem Value="4701" Text="销售主体"></asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                            <td class="titleTd">
                                提货方式：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="YSHHS">
                                    <asp:ListItem Value="1" Text="自提"></asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleTd">
                                销售方式：
                            </td>
                            <td class="contentTd">
                                <SOA:HBDropDownList runat="server" ID="VTWEG">
                                    <asp:ListItem Value="21" Text="销售方式"></asp:ListItem>
                                </SOA:HBDropDownList>
                            </td>
                            <td class="titleTd">
                                联系人：
                            </td>
                            <td class="contentTd">
                                <SOA:HBTextBox ID="RMAN" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleTd">
                                有效截止日期：
                            </td>
                            <td class="contentTd">
                                <SOA:HBTextBox ID="EDAT" runat="server" onfocus="WdatePicker()" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div style="padding-top: 20px;">
            <table id="test">
            </table>
        </div>
        <%--<div>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="if(!setData()){return false;}" OnClick="btnSave_Click" />
                <asp:Button ID="btnMove" runat="server" Text="流转" OnClientClick="if(!setData()){return false;}" />
            </div>--%>
        <div style="display: none;">
            <div id="hetong-order" style="width: 500px; height: 200px;">
                <div>
                    <table id="order-hetong-details" class="order-hetong" cellpadding="0" cellspacing="0">
                        <thead>
                            <tr>
                                <th style="width: 50px;">
                                    &nbsp;
                                </th>
                                <th>
                                    合同编号
                                </th>
                                <th>
                                    合同名称
                                </th>
                                <!--               <th>油&nbsp;品</th>
                                    <th>数&nbsp;量</th>
                                    <th>金&nbsp;额</th>-->
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <input type="radio" name="order-hetong-sp" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" name="order-hetong-sp" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div style="display: none">
        </div>
    </div>
    <!-----------------各种库存的代码----------------------->
    <asp:HiddenField ID="safeId" runat="server" />
    <asp:HiddenField ID="isEdit_text" runat="server" Value="0" />
    <asp:HiddenField ID="TB_SAP_MAKT" runat="server" />
    <asp:HiddenField ID="TB_SAP_T006A" runat="server" />
    <asp:HiddenField ID="TB_SAP_T001W" runat="server" />
    <asp:HiddenField ID="TB_SAP_T001L" runat="server" />
    <asp:HiddenField ID="TB_SAP_TCURT" runat="server" />
    </form>
</body>
</html>
