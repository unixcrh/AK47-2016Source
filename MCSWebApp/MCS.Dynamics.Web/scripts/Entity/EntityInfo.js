$(function() {
    $("#btn_save_seachLoading").click(function () {
        return false;
    });
    $("[id$='searchButton']").click(function () {
        $get("btn_save_seachLoading").click();
    });
});
function onconditionClick(sender, e) {
    var content = Sys.Serialization.JavaScriptSerializer.deserialize(e.ConditionContent);
    var bindingControl = $find("searchBinding");
    bindingControl.dataBind(content);
}

function disAbledControl(ele) {
    return $pc.getEnabled(ele);
}

function view(entityId) {

    var params = "";
    if (entityId !== undefined) {
        params = "&ID=" + entityId;
    } else {
        if (!disAbledControl(this)) {
            return false;
        }
    }

    var parentWin = window.showModalDialog(urllocation + params, "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=900px;");
    if (parentWin) {
        window.location.href = window.location.href;
    }

    return false;
}

function addEntity() {
    if (!disAbledControl(this)) {
        return false;
    }
    var params = "";

    var parentWin = window.showModalDialog("../../dialogs/GenerateEntityAndMapping.aspx" + window.location.search, "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=1000px;scroll=yes;");
    if (parentWin) {
        window.location.href = window.location.href;
    }

    return false;
}

function addETLEntity() {
    if (!disAbledControl(this)) {
        return false;
    }
    var params = "";

    var parentWin = window.showModalDialog("../ETL/Dialogs/AddETLEntity.aspx" + window.location.search, "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=1000px;scroll=yes;");
    if (parentWin) {
        window.location.href = window.location.href;
    }

    return false;
}

function editView(entityId, categoryID) {

    var params = "";
    if (entityId !== undefined) {
        params = "ID=" + entityId;
        params += "&categoryID=" + categoryID
    }

    var parentWin = window.showModalDialog("../../dialogs/EntityDetails.aspx?" + params, "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=900px;");

    if (parentWin) {
        window.location.href = window.location.href;
    }
}

function editETLView(entityId, categoryID) {

    var params = "";
    if (entityId !== undefined) {
        params = "ID=" + entityId;
        params += "&categoryID=" + categoryID;
    }

    var parentWin = window.showModalDialog("../ETL/Dialogs/EditETLEntity.aspx?" + params, "", "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=900px;");

    if (parentWin) {
        window.location.href = window.location.href;
    }
}
//查看Entity映射
function editEntityMapping(entityId, categoryID) {
    var params = "";
    if (entityId !== undefined) {
        params = "ID=" + entityId;
        params += "&categoryID=" + categoryID;
    }

    //获得窗口的垂直位置
    var iTop = (window.screen.availHeight - 30 - 400) / 2;
    //获得窗口的水平位置
    var iLeft = (window.screen.availWidth - 10 - 600) / 2;
    var parentWin = window.open("../EntityMapping/EntityMapping.aspx?" + params, "", "scrollbars=yes,resizable=no,help=no,status=no,center=yes,location=no,height=400,width=600,top=" + iTop + ",left=" + iLeft);
}
//查看ETL实体映射
function editETLEntitymapping(entityId, categoryID) {
    var params = "";
    if (entityId !== undefined) {
        params = "ID=" + entityId;
        params += "&categoryID=" + categoryID;
    }

    //获得窗口的垂直位置
    var iTop = (window.screen.availHeight - 30 - 400) / 2;
    //获得窗口的水平位置
    var iLeft = (window.screen.availWidth - 10 - 600) / 2;
    var parentWin = window.open("../ETL/Dialogs/ETLEntityMappingDialog.aspx?" + params, "", "scrollbars=yes,resizable=no,help=no,status=no,center=yes,location=no,height=400,width=600,top=" + iTop + ",left=" + iLeft);

}

function deleteEntity() {
    //hd_entityID
    if (!disAbledControl(this)) {
        return false;
    }

    //获取选中行
    var proId = privateMethod.getSeletedItemId();
    if (!proId) {
        alert("请选择要删除的数据");
        return false;
    }

    if (!confirm("确定要删除吗？")) {
        return false;
    }
    $("#hd_entityID").val(proId.join(","));
    return true;

}

var urllocation = "../../dialogs/EntityDetails.aspx";
$(document).ready(function () {
    urllocation += window.location.search;
    window.parent.showWait(false);
    $("a.aspNetDisabled").attr("disabled", true);


});

function copyEntities(isDelete) {
    if (!disAbledControl(this)) {
        return false;
    }
    //获取选中行的编码集合
    var proId = privateMethod.getSeletedItemId();

    if (!proId) {
        if (isDelete == "true") {

            alert("请选择要移动的数据");
        } else {
            alert("请选择要复制的数据");
        }

        return false;
    }

    // var error = validMethod.checkEntities(proId, isDelete);
    var error = "";

    if (error != null && error != "") {
        alert(error);
        return false;
    }

    $("#hd_entityID").val(proId.join(","));

    var url = "../../Dialogs/SelectCategory.aspx";

    var catetories = window.showModalDialog(url, ex.rowData, "scrollbars=yes;resizable=no;help=no;status=no;center=yes;location=no;dialogHeight=500px;dialogWidth=400px;");

    if (catetories == null || catetories == undefined) {
        return false;
    }

    var selectedCatetories = catetories;

    var msg = doCopyEntities(proId, selectedCatetories, isDelete);

    if (msg != null && msg != "") {
        alert(msg);
        return false;
    }
    else {
        alert("操作成功!");
        if (isDelete == 'true') {
            return true;
        }
    }
    return false;

}

function doCopyEntities(idArray, categoryArray, isDelete) {
    var errorMessage = null;
    var dataParam = null;

    dataParam = { "CopyEntities": idArray.join(","), "Categories": categoryArray, "Move": isDelete }

    $.ajax({
        type: "post",
        async: false,
        url: "../../Handlers/CopyEntities.ashx",
        data: dataParam,
        success: function (result) {
            errorMessage = result;
        }
    });

    return errorMessage;
}

handler = {
    //导出
    Export: function () {
        //选中行
        var ids = privateMethod.getSeletedItemId();
        if (!ids) {
            alert("请选择要导出的数据");
            return false;
        }
        $("#hd_entityID").val(ids.join(","));

        //验证选中实体的完整性
        // var error = validMethod.checkEntities(ids);
        var error = "";
        if (error != null && error != "") {
            alert(error);
            return false;
        }

        return true;
    }
}

//验证方法
validMethod = {
    //验证实体选择的完整性
    //    checkEntities: function (idArray, isDel) {
    //        var errorMessage = null;
    //        $.ajax({
    //            type: "post",
    //            async: false,
    //            url: "../../Handlers/CheckCopyEntity.ashx",
    //            data: { "CopyEntities": idArray.join(","), "Move": "true" },
    //            success: function (result) {
    //                errorMessage = result;
    //            }
    //        });
    //        return errorMessage;
    //    }
};

//私有方法
privateMethod = {
    //获取已选中行项目的编码集合
    getSeletedItemId: function () {
        var $check = $("#ProcessDescInfoDeluxeGrid td").find("input:checked");
        if ($check.length == 0) {
            // alert("请选择数据");
            return false;
        }

        var proId = [];
        $check.each(function () {
            proId.push($(this).parent().siblings(".process_id").text());
        });

        return proId;
    }
};

function onPrepareData(e) {
    //向Server传递数据
    //e.postedData = $get("postedData").value;
}

function onCompleted(e) {
    if (e.dataChanged) {
        window.location.href = window.location.href;
    }
}