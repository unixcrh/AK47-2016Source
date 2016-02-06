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
            ex.rowData[properties[i].name] = properties[i].value;
        }

        return true;
    },
    OnPreRowAdd: function (grid, e) {
        var url = "showObjectInfo.aspx?schemaType=DynamicEntityField";
        var properties = clientGrid.ShowDialog(url, e);

        if (properties) {
            if (!clientGrid.PropertyToDataRow(grid, properties, e)) {
                e.cancel = true;
            }
        } else {
            e.cancel = true;
        }
    },
    OnCellCreatingEditor: function (grid, e) {
        e.autoFormat = false;
        switch (e.column.dataField) {
            case "options":
                var text = grid.get_readOnly() ? "查看字段" : "编辑字段";

                var alink = { nodeName: "a", properties: { href: "javascript:void(0);", innerText: text} };
                alink = $HGDomElement.createElementFromTemplate(alink, e.container);

                $(alink).click(function () {
                    var url = "showObjectInfo.aspx?schemaType=DynamicEntityField";

                    //                    if (e.rowData.Properties) {
                    //                        for (var i = 0; i < e.rowData.Properties.length; i++) {
                    //                            if (e.rowData.Properties[i].name == "SchemaType" && e.rowData.Properties[i].value == "") {
                    //                                continue;
                    //                            }

                    //                            url += ("&" + e.rowData.Properties[i].name + "=" + e.rowData.Properties[i].value);
                    //                        }
                    //                    }

                    for (var f in e.rowData) {
                        if (f != "SchemaType" && e.rowData[f]) {
                            url += ("&" + f + "=" + escape(e.rowData[f]));
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
                e.showValueTobeChange = grid.get_readOnly() ? "查看字段" : "编辑字段";
                break;
        }
    },
    ShowDialog: function (url, ex) {

        var parentWindow = window.showModalDialog(url, ex.rowData, "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=400px;");

        return parentWindow;
    }
};

function onSaveClick() {
    $get("btn_save").click();
}