function onDocumentLoad() {
    //receive window.dialogArguments
    //var data = window.dialogArguments.Data;
    //$find("propertyForm").set_properties(data);
    //debugger;
}

function onSaveClick() {
    var reValue = $HGRootNS.PropertyEditorControlBase.ValidateProperties();

    if (!reValue.isValid)	//返回是否通过
        return false; //不通过则阻止提交

    if ($("#hd_errorMsg").val() != "") {
        alert($("#hd_errorMsg").val());
        return false;
    }

    var allProperties = $find("propertyForm").get_properties();

    $get("properties").value = Sys.Serialization.JavaScriptSerializer.serialize(allProperties);

    var result = [];
    for (var i = 0; i < allProperties.length; i++) {
        result.push({ name: allProperties[i].name, value: allProperties[i].value });
    }

    //业务验证 字段类型
    var fieldType = JSLINQ(result).First(function (item) {
        return item.name == "FieldType";
    });

    //业务验证 是否是更新时间 IsUpadateTime
    var isUpdateTime = JSLINQ(result).First(function (item) {
        return item.name == "IsUpdateTime";
    });

    var reference = JSLINQ(result).First(function (item) {
        return item.name == "ReferenceEntityCodeName";
    });

    //fieldType==5 表示字段类型为实体集合
    if (fieldType && reference && fieldType.value == 5 && reference.value == "") {
        alert("请输入有效的[引用实体]后再保存！");
        return false;
    }

    //fieldType==3 表示字段类型为日期类型
    if (isUpdateTime && isUpdateTime.value == "true" && fieldType) {
        if (fieldType.value != 3) {
            alert("请确保该字段是日期类型！");
            return false;
        }
    }

    window.returnValue = result;

    window.close();
}