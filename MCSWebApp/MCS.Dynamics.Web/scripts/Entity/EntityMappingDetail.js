Method = {
    Save: function () {
        $get("btn_save").click();
    }
};

clientGrid = {
    OnCellCreatingEditor: function (grid, e) {
        e.autoFormat = false;
        switch (e.column.dataField) {
            case "DestinationName":
                break;
            case "options":
                var text = grid.get_readOnly() ? "查看字段" : "编辑字段";

                var alink = { nodeName: "a", properties: { href: "javascript:void(0);", innerText: text} };
                alink = $HGDomElement.createElementFromTemplate(alink, e.container);

                $(alink).click(function () {
                    var url = "showObjectInfo.aspx?schemaType=DynamicEntityField";

                    for (var f in e.rowData) {
                        if (f != "SchemaType") {
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
    }
};

function onSaveClick() {
    $get("btn_save").click();
}