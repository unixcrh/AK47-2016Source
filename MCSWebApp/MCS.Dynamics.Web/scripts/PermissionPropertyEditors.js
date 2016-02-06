$HGRootNS.ReferenceEntityCodeNameEditor = function (prop, container, delegations) {
    $HGRootNS.ReferenceEntityCodeNameEditor.initializeBase(this, [prop, container, delegations]);
}

$HGRootNS.ReferenceEntityCodeNameEditor.prototype = {

    _editElement_onchange: function (eventElement) {
        var validateEventArgs = new Sys.EventArgs();
        validateEventArgs.result = true;
        if (this._delegations["editorValidating"]) {
            this._delegations["editorValidating"](this.get_property(), eventElement.handlingElement);

            if (this._delegations["editorValidate"]) {
                this._delegations["editorValidate"](this.get_property(), eventElement.handlingElement, validateEventArgs);
            }
            if (validateEventArgs.result == false) {
                eventElement.preventDefault();
                return false;
            }
        }
        if (this._delegations["editorValidated"]) {
            this._delegations["editorValidated"](this.get_property(), eventElement.handlingElement, validateEventArgs);
        }

        var currentEditor = this;

        var referenceEntityCodeName = this.get_property().value;
        if (referenceEntityCodeName) {
            MCS.Dynamics.Web.Services.CommonService.ValidateCodeNameExist($get("currentSchemaType").value, this.getObjID(), $get("currentParentID").value, referenceEntityCodeName, false, function (result) {
                if (!result) {
                    //currentEditor._editElement.value = "";
                    currentEditor.commitValue();

                    currentEditor._delegations._owner._activePropertyEditor(currentEditor.get_property());
                    currentEditor._delegations._owner._raiseEvent("clickEditor", currentEditor.get_property());
                    currentEditor._editElement.focus();

                    var errorMsg = "引用实体[" + referenceEntityCodeName + "]不存在!";
                    alert(errorMsg);

                    $("#hd_errorMsg").val(errorMsg);
                } else {
                    $("#hd_errorMsg").val("");
                }
            }, function (err) {
                alert("无法访问代码名称检查服务：" + err.message);
                $("#hd_errorMsg").val("无法访问代码名称检查服务：" + err.message);
            });
        } else {
            $("#hd_errorMsg").val("");
        }
    },

    getObjID: function () {
        var properties = this._delegations._owner.get_properties();
        var currentEditID = "";
        for (var i = 0; i < properties.length; i++) {
            var itemproperty = properties[i];
            if (itemproperty.name == "ID") {
                currentEditID = itemproperty.value;
            }
        }

        return currentEditID;
    }
}

$HGRootNS.ReferenceEntityCodeNameEditor.registerClass($HGRootNSName + ".ReferenceEntityCodeNameEditor", $HGRootNS.StandardPropertyEditor);
