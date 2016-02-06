var fieldTypeValueKey = null;
var fieldTypeKeyValue = null;
var whereArray = new Array();
var allSvcOperationDef = [];
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
};
Array.prototype.remove = function (val) {

    for (var i = 0; i < this.length; i++) {
        if (val == this[i].ID) {
            this.splice(i, 1);
        }
    }

};

Array.prototype.removeFromID = function (val) {

    for (var i = 0; i < this.length; i++) {
        if (val == this[i].ID) {
            this.splice(i, 1);
        }
    }

};
Array.prototype.removeWhere = function (val) {
    //JOB_ID Condition ETLEntity_ID ETLOuterEntity_ID
    for (var i = 0; i < this.length; i++) {
        if (val.ETLEntity_ID == this[i].ETLEntity_ID && val.ETLOuterEntity_ID == this[i].ETLOuterEntity_ID) {
            this.splice(i, 1);
        }
    }

};
Method = {
    Save: function () {
        $get("btn_save").click();
    }
};

clientGrid = {
    GetProperty: function (properties, propertyName) {
        for (var i = 0; i < properties.length; i++) {
            if (properties[i].name == propertyName)
                return properties[i];
        }
        properties.push({ name: propertyName, value: "" });

        return properties[length];
    },
    PropertyToDataRow: function (grid, properties, ex) {

        if (fieldTypeValueKey == null) {
            fieldTypeValueKey = eval('(' + document.getElementById("HF_EnumValueKey").value + ')');
        }

        var ds = grid.get_dataSource();
        var error = "";
        var newName = "";

        for (var i = 0; i < properties.length; i++) {
            if (properties[i].name == "Name") {
                newName = properties[i].value;
            }
        }

        if (ex.rowData.Name != newName) {
            for (var i = 0; i < ds.length; i++) {
                if (ds[i].Name == newName) {
                    error += ("[" + newName + "]已存在!\r\n");
                }
            }
        }

        if (error) {
            alert(error);
            return false;
        }

        ex.rowData.SchemaType = "DynamicEntityField";
        ex.rowData.SortNo = ++ex.rowIndex;

        for (var i = 0; i < properties.length; i++) {
            //clientGrid rowData赋值
            if (properties[i].name == 'FieldType') {
                var typeValue = properties[i].value;
                if (typeValue == "" || typeValue == undefined) {
                    typeValue = "1";
                }
                ex.rowData[properties[i].name] = fieldTypeValueKey[typeValue]
            }
            else {
                ex.rowData[properties[i].name] = properties[i].value;
            }
        }

        return true;
    },
    OnPreRowAdd: function (grid, e) {
        var gridID = grid._element.id;
        e.cancel = true;
        var sFeature = "dialogWidth:800px; dialogHeight:460px;center:yes;help:no;resizable:yes;scroll:no;status:no";
        var result;
        //计划
        if (gridID == "grid") {
            result = window.showModalDialog("/MCSWebApp/WorkflowDesigner/PlanScheduleDialog/ScheduleList.aspx", null, sFeature);
        }
        //etl实体
        else if (gridID == "invokingServiceGrid") {
            result = window.showModalDialog("/MCSWebApp/WorkflowDesigner/ModalDialog/WfServiceOperationDefEditor.aspx", null, sFeature);
        }
        else {
            result = window.showModalDialog("SelectETLEntityFrameSet.aspx", null, sFeature);
        }


        if (result) {
            var datas;
            if (gridID == "grid") {
                datas = Sys.Serialization.JavaScriptSerializer.deserialize(result.jsonStr);
            }
            else if (gridID == "invokingServiceGrid") {

            }
            else {
                datas = Sys.Serialization.JavaScriptSerializer.deserialize(result).ETLs;
            }
            var currSchedules = $find(gridID).get_dataSource();
            if (datas) {
                for (var i = 0; i < datas.length; i++) {
                    if (currSchedules.length != 0) {
                        //删除重复的元素
                        currSchedules.removeFromID(datas[i].ID);
                    }
                    currSchedules.push(datas[i]);
                }
            }

            $find(gridID).set_dataSource(currSchedules);
            //            $find("grid").set_dataSource(currSchedules);

        }
    },
    OnCellCreatingEditor: function (grid, e) {
        e.autoFormat = false;
        switch (e.column.dataField) {
            case "options":
                e.autoFormat = true;
                var text = grid.get_readOnly() ? "查看字段" : "编辑字段";
                //var text = "编辑字段";
                if (fieldTypeKeyValue == null) {
                    fieldTypeKeyValue = eval('(' + document.getElementById("HF_EnumKeyValue").value + ')');
                }
                var alink = { nodeName: "a", properties: { href: "javascript:void(0);", innerText: text} };
                alink = $HGDomElement.createElementFromTemplate(alink, e.container);


                $(alink).click(function () {

                    var url = "showObjectInfo.aspx?schemaType=DynamicEntityField";

                    for (var f in e.rowData) {
                        if (f != "SchemaType" && e.rowData[f]) {
                            var paramValue = "";
                            if (f == "FieldType") {
                                paramValue = escape(fieldTypeKeyValue[e.rowData[f]]);
                            }
                            else {
                                paramValue = escape(e.rowData[f]);
                            }
                            url += ("&" + f + "=" + paramValue);
                        }
                    }

                    var properties = clientGrid.ShowDialog(url, e);
                    if (properties) {
                        clientGrid.PropertyToDataRow(grid, properties, e);
                    }

                    grid.rebind();
                });
                e.editor.set_editorElement(alink);


                break;
            default:
                e.autoFormat = true;
        }
    },
    OnDataFormatting: function (grid, e) {
        switch (e.column.dataField) {
            case "options":
                e.showValueTobeChange = "查看字段";
                //alert(e.showValueTobeChange);
                break;
        }
    },
    ShowDialog: function (url, ex) {

        var parentWindow = window.showModalDialog(url, ex.rowData, "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=400px;");

        return parentWindow;
    }
};
String.prototype.trim = function() {
    return this.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
  }
function onSaveClick() {
    //$get("schedules").value = $find("grid").get_dataSource();
    //$get("schedules").value = Sys.Serialization.JavaScriptSerializer.serialize($find("grid").get_dataSource());

    //任务名称校验
    if ($get("txt_JobName").value == null || $get("txt_JobName").value.trim() == "") {
        alert("任务名称不能为空！");
        return;
    } 

    //计划列表
    var scheduleList = $find("grid").get_dataSource();
    var scheduleIDs = "";
    for (var i = 0; i < scheduleList.length; i++) {
        scheduleIDs += scheduleList[i].ID + ","
    }
    $get("schedules").value = scheduleIDs;

    //任务列表
    var etls = $find("gridEtl").get_dataSource();
    var etlIDs = "";
    for (var i = 0; i < etls.length; i++) {
        etlIDs += etls[i].ID + ","
    }
    $get("etlEntities").value = etlIDs;

    //条件
    $get("conditions").value = Sys.Serialization.JavaScriptSerializer.serialize(whereArray);
    //WebService
    $get('services').value = Sys.Serialization.JavaScriptSerializer.serialize($find('invokingServiceGrid').get_dataSource())

    $get("btn_save").click();
}

function rowCreatingEditor(sender, e) {
    //http://win2008sp1-develop.dev.corp/MCSWebApp/WorkflowDesigner/PlanScheduleDialog/ScheduleEditor.aspx?scheduleId=2961ae18-b7e0-474e-93b2-871e8abccc06
    switch (e.column.dataField) {
        case "Where":
            var linkText = "<a href='#' onclick='getWhere(\"{0}\");'>编辑条件</a>";
            e.cell.innerHTML = "";
            e.cell.innerHTML = String.format(linkText, e.data["ID"].toString());
            break;
        case "CodeName":
            var linkText = "<a href='#' onclick='entityDetail(\"{0}\");'>" + e.data["CodeName"].toString() + "</a>";
            e.cell.innerHTML = "";
            e.cell.innerHTML = String.format(linkText, e.data["ID"].toString());
            break;

    }
}

function scheduleRowCreatingEtitor(sender, e) {
    switch (e.column.dataField) {
        case "Name":
            var linkText = "<a href='#' onclick='scheduleDetail(\"{0}\");'>" + e.data["Name"].toString() + "</a>";
            e.cell.innerHTML = "";
            e.cell.innerHTML = String.format(linkText, e.data["ID"].toString());
            break;
    }
}

function scheduleDetail(id) {
    var sFeature = "dialogWidth:850px; dialogHeight:460px;center:yes;help:no;resizable:yes;scroll:no;status:no";
    window.showModalDialog("/MCSWebApp/WorkflowDesigner/PlanScheduleDialog/ScheduleEditor.aspx?scheduleId=" + id, null, sFeature);
}

function entityDetail(id) {

    window.showModalDialog("EditETLEntity.aspx?ID=" + id, null, "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=900px;");
}
function getWhere(id) {
    var jobId = getQueryString("id");
    var sFeature = "dialogWidth:800px; dialogHeight:460px;center:yes;help:no;resizable:yes;scroll:no;status:no";
    var result;
    var paramArray = new Array();
    for (var i = 0; i < whereArray.length; i++) {
        if (whereArray[i].ETLEntity_ID == id) {
            paramArray.push(whereArray[i]);
        }
    }
    result = window.showModalDialog("WhereCondition.aspx?id=" + id + "&jobid=" + jobId, paramArray, sFeature);
    if (result != undefined) {
        for (var i = 0; i < result.length; i++) {
            if (whereArray.length != 0) {
                whereArray.removeWhere(result[i]);
            }
            whereArray.push(result[i]);
        }
    }
}
$(document).ready(function () {

    //给Where条件初始化
    //$get("conditions").value = Sys.Serialization.JavaScriptSerializer.serialize(whereArray);
    //$("ch_IsAuto").
    $("#ch_IsAuto").click(function () {
        if (this.checked) {
            $("#scheduleList").show();
        }
        else {
            $("#scheduleList").hide();
        }

    });

    $("#ddl_JobType").change(function () {
        if (this.value == "InvokeService") {
            $("#etlEntityList").hide();
            $("#divInvokingService").show();
        }
        else {
            $("#etlEntityList").show();
            $("#divInvokingService").hide();
        }

    });

    if ($("#ddl_JobType").val() == "InvokeService") {
        $("#etlEntityList").hide();
        $("#divInvokingService").show();
    }

    if (!$("#ch_IsAuto")[0].checked) {
        $("#scheduleList").hide();
    }

    //allSvcOperationDef = $find('invokingServiceGrid').get_dataSource();
    setTimeout(function () { allSvcOperationDef = $find('invokingServiceGrid').get_dataSource(); }, 500);


    if ($get("conditions").value == "") {
        return;
    }

    whereArray = eval($get("conditions").value);
});

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}