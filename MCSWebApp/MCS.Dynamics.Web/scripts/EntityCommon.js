/// <reference path="pc.js" />

var Request = {
    Data: {},
    QueryString: function (key) {
        return this.Data[key.toLowerCase()];
    },
    IntalData: function () {
        var params = window.location.search;
        if (params) {
            params = params.substring(1);
            var searchView = params.split("&");
            for (var i = 0; i < searchView.length; i++) {
                var searchsplit = searchView[i].split("=");
                this.Data[searchsplit[0].toLowerCase()] = searchsplit[1];
            }
        }
    }
};

function deleteEntity() {
    //hd_entityID
    if (!disAbledControl(this)) {
        return false;
    }
    var $check = $("#ProcessDescInfoDeluxeGrid td").find("input:checked");
    if ($check.length == 0) {
        alert("请选择要删除的数据");
        return false;
    } else {
        if (!confirm("确定要删除吗？")) {
            return false;
        }
        var proId = [];
        $check.each(function () {
            proId.push($(this).parent().siblings(".process_id").text());
        });

        $("#hd_entityID").val(proId.join(","));
        return true;
    }
}
function disAbledControl(ele) {
    return $pc.getEnabled(ele);
}
function view(entityId) {
    var params = "";
    if (entityId !== undefined) {
        params = "&OuterEntityID=" + entityId;
    } else {
        if (!disAbledControl(this)) {
            return false;
        }
    }
    var parentWin = window.showModalDialog(urllocation + params, "", "scroll=yes;scrollbars=yes;resizable=no;help=yes;status=no;center=yes;location=no;dialogHeight=700px;dialogWidth=900px;");
    if (parentWin) {
        window.location.href = window.location.href;
    }

    return false;
}
