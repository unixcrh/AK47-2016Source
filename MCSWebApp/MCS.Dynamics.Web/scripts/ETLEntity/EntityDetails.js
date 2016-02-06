var fieldTypeValueKey = null;
var fieldTypeKeyValue = null;

Method = {
    Save: function () {

        alert("qq");
        $get("btn_save").click();
    }
};

clientGrid = {
    GetProperty: function (properties, propertyName) {
        for (var i = 0; i < properties.length; i++) {
            if (properties[i].name == propertyName) {
                return properties[i];
            }
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

        ex.rowData.SchemaType = "ETLEntityField";
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
        var url = "showObjectInfo.aspx?schemaType=ETLEntityField";
        var properties = clientGrid.ShowDialog(url, e);

        if (properties) {
            if (!clientGrid.PropertyToDataRow(grid, properties, e)) {
                e.cancel = true;
            }
        } else {
            e.cancel = true;
        }
    },
    OnPreRowAddUsers: function (grid, e) {
        e.cancel = true;
        var result = window.showModalDialog("SapUserCompareChoose.aspx", "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=800px;scroll=yes;");
        if (result) {
            var ds = grid.get_dataSource();
            var properties = JSON.parse(result);
            var length = ds.length;
            for (var i = 0; i < properties.length; i++) {
                var isRef = true;
                for (var j = 0; i < length; j++) {
                    //验证数据是否完全相同，如果完全相同则不附加
                    if (ds[j]["UepID"] == properties[i]["UepID"] && ds[j]["SapServers_Client"] == properties[i]["SapServers_Client"] && ds[j]["SapServers_ApplicationServer"] == properties[i]["SapServers_ApplicationServer"]) {
                        isRef = false;
                        break;
                    }
                }
                if (isRef) {
                    ds.push(properties[i]);
                }

            }
            grid.set_dataSource(ds);
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

                    var url = "showObjectInfo.aspx?schemaType=ETLEntityField";
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
    OnCellSAPEditor: function (grid, e) {
        e.autoFormat = false;
        switch (e.column.dataField) {
            case "options":
                e.autoFormat = true;
                var text = "编辑";
                if (fieldTypeKeyValue == null) {
                    fieldTypeKeyValue = eval('(' + document.getElementById("HF_EnumKeyValue").value + ')');
                }
                var alink = { nodeName: "a", properties: { href: "javascript:void(0);", innerText: text} };
                alink = $HGDomElement.createElementFromTemplate(alink, e.container);
                e.editor.set_editorElement(alink);

                $(alink).click(function () {
                    var url = "AllSapUserCompareChoose.aspx";
                    e.newWidth = "800px";
                    e.newHeight = "500px";
                    var properties = clientGrid.ShowDialog(url, e);
                    if (properties) {
                        clientGrid.SAPToDataRow(grid, properties, e);
                    }
                });
                break;
            default:
                e.autoFormat = true;
        }
    },
    SAPToDataRow: function (grid, properties, ex) {
        
        grid.set_dataSource(eval(properties));
        return true;
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
        var params = "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=400px;";
        if (ex.newHeight && ex.newWidth) {
            params = params.replace("dialogHeight=500px;dialogWidth=400px", "dialogHeight=" + ex.newHeight + ";dialogWidth=" + ex.newWidth);
        }

        var parentWindow = window.showModalDialog(url, ex.rowData, params);

        return parentWindow;
    }
};

function onSaveClick() {
    var reValue = $HGRootNS.PropertyEditorControlBase.ValidateProperties();

    if (!reValue.isValid)	//返回是否通过
        return false; //不通过则阻止提交

    var allProperties = $find("propertyForm").get_properties();

    $get("properties").value = Sys.Serialization.JavaScriptSerializer.serialize(allProperties);
    $get("btn_save").click();
}

