var fieldTypeValueKey = null;
var fieldTypeKeyValue = null;

Method = {
    Save: function ()
    {
        $get("btn_save").click();
    }
};

clientGrid = {
    GetProperty: function (properties, propertyName)
    {
        for (var i = 0; i < properties.length; i++)
        {
            if (properties[i].name == propertyName)
                return properties[i];
        }
        properties.push({ name: propertyName, value: "" });

        return properties[length];
    },
    PropertyToDataRow: function (grid, properties, ex)
    {

        if (fieldTypeValueKey == null)
        {
            fieldTypeValueKey = eval('(' + document.getElementById("HF_EnumValueKey").value + ')');
        }

        var ds = grid.get_dataSource();
        var error = "";
        var newName = "";

        for (var i = 0; i < properties.length; i++)
        {
            if (properties[i].name == "Name")
            {
                newName = properties[i].value;
            }
        }

        if (ex.rowData.Name != newName)
        {
            for (var i = 0; i < ds.length; i++)
            {
                if (ds[i].Name == newName)
                {
                    error += ("[" + newName + "]已存在!\r\n");
                }
            }
        }

        if (error)
        {
            alert(error);
            return false;
        }

        ex.rowData.SchemaType = "DynamicEntityField";
        ex.rowData.SortNo = ++ex.rowIndex;

        for (var i = 0; i < properties.length; i++)
        {
            //clientGrid rowData赋值
            if (properties[i].name == 'FieldType')
            {
                var typeValue = properties[i].value;
                if (typeValue == "" || typeValue == undefined)
                {
                    typeValue = "1";
                }
                ex.rowData[properties[i].name] = fieldTypeValueKey[typeValue]
            }
            else
            {
                ex.rowData[properties[i].name] = properties[i].value;
            }
        }

        return true;
    },
    OnPreRowAdd: function (grid, e)
    {
        var url = "showObjectInfo.aspx?schemaType=DynamicEntityField";
        var properties = clientGrid.ShowDialog(url, e);

        if (properties)
        {
            if (!clientGrid.PropertyToDataRow(grid, properties, e))
            {
                e.cancel = true;
            }
        } else
        {
            e.cancel = true;
        }
    },

    OnCellCreatingEditor: function (grid, e)
    {
        e.autoFormat = false;
        switch (e.column.dataField)
        {
            case "options":
                e.autoFormat = true;
                var text = grid.get_readOnly() ? "查看字段" : "编辑字段";
                //var text = "编辑字段";
                if (fieldTypeKeyValue == null)
                {
                    fieldTypeKeyValue = eval('(' + document.getElementById("HF_EnumKeyValue").value + ')');
                }
                var alink = { nodeName: "a", properties: { href: "javascript:void(0);", innerText: text } };
                alink = $HGDomElement.createElementFromTemplate(alink, e.container);


                $(alink).click(function ()
                {

                    var url = "showObjectInfo.aspx?schemaType=DynamicEntityField";
                    for (var f in e.rowData)
                    {
                        if (f != "SchemaType" && e.rowData[f])
                        {
                            var paramValue = "";
                            if (f == "FieldType")
                            {
                                paramValue = escape(fieldTypeKeyValue[e.rowData[f]]);
                            }
                            else
                            {
                                paramValue = escape(e.rowData[f]);
                            }
                            url += ("&" + f + "=" + paramValue);
                        }
                    }

                    var properties = clientGrid.ShowDialog(url, e);
                    if (properties)
                    {
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
    OnDataFormatting: function (grid, e)
    {
        switch (e.column.dataField)
        {
            case "options":
                e.showValueTobeChange = "查看字段";
                //alert(e.showValueTobeChange);
                break;
        }
    },
    ShowDialog: function (url, ex)
    {

        var parentWindow = window.showModalDialog(url, ex.rowData, "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=600px;dialogWidth=500px;");

        return parentWindow;
    }
};

function onSaveClick()
{
    var reValue = $HGRootNS.PropertyEditorControlBase.ValidateProperties();

    if (!reValue.isValid)	//返回是否通过
        return false; //不通过则阻止提交

    var allProperties = $find("propertyForm").get_properties();
    //alert(Sys.Serialization.JavaScriptSerializer.serialize(allProperties));
    $get("properties").value = Sys.Serialization.JavaScriptSerializer.serialize(allProperties);
    $get("btn_save").click();
}

$(document).ready(function ()
{
    if ($("#HFOperationType").val() == "2")
    {
        $("#btnClose").css("display","none");
    }
});