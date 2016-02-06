<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidatorSelectorDialog.aspx.cs" Inherits="MCS.Dynamics.Web.ValidatorSelector.ValidatorSelectorDialog" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<%@ Register Assembly="MCS.Library.SOA.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="SOA" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择校验器</title>
    <link href="../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../Themes/default/css/common.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../scripts/JSLINQ.js" type="text/javascript"></script>
    <script src="../scripts/pc.js" type="text/javascript"></script>
    <script src="../scripts/jquery.tmpl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
        <table class="formTable" style="border:1px solid #f5f5f5;">
                <tr>
                    <td style="background-color:#f5f5f5;color:#000000;border-right:1px solid #f5f5f5;">已选择的校验器</td>
                    <td style="background-color:#f5f5f5;color:#000000;">校验器属性设置</td>
                </tr>
                <tr>
                    <td style="vertical-align: top;border-right:1px solid #f5f5f5;">
                        <table class="listTable">
                            <thead>
                                <tr>
                                    <th style="width: 30px; text-align: center;">序号</th>
                                    <th style="width: 300px; text-align: center;">校验器</th>
                                    <th style="width: 100px; text-align: center;">操作</th>
                                </tr>
                            </thead>
                            <tbody id="gridValidator">
                            </tbody>
                        </table>
                    </td>
                    <td style="vertical-align: top; width: 300px;">
                        <div style="text-align: center;margin:2px 0;">
                            <soa:HBDropDownList ID="ddlValidator" runat="server" ClientIDMode="Static" Width="100%"></soa:HBDropDownList>
                        </div>
                        <div style="width:100%;margin:2px 0;">
                            <%--<soa:PropertyForm runat="server" ID="propertyGrid" Width="100%" Height="100%"   AutoSaveClientState="False" />--%>
                             <soa:PropertyGrid runat="server" ID="propertyGrid" Width="100%" Height="300px"
                            OnClientClickEditor="onClickEditor" DisplayOrder="ByCategory" ReadOnly="false" BackColor="White" />
                        </div>
                        <div style="text-align:center;margin:2px 0;background-color:#f5f5f5;">
                             <input type="button" id="btnSave" value="添加"  class="pcdlg-button btn-def" onclick="saveClick()" />
                        </div>
                    </td>
                </tr>
            </table>
        <div class="pcdlg-floor">
            <div class="pcdlg-button-bar">
                <input type="button" id="okButton" runat="server" onclick="okClick();" accesskey="S" class="pcdlg-button btn-def" value="保存(S)" />
                <input type="button" accesskey="C" class="pcdlg-button btn-cancel" onclick="window.close();" value="关闭(C)" />
            </div>
        </div>
        <script id="_tempValidator" type="text/x-jquery-tmpl">
            <tr>
                <td data-field="vrindex" style="text-align: center;"></td>
                <td style="text-align: left;">${Description}</td>
                <td style="text-align: center;">
                    <a href="javascript:void(0);" onclick="editValidatorProperty(this)">编辑</a> | 
                    <a href="javascript:void(0);" onclick="return deleteValidator(this);">删除</a>
                    <input type="hidden" name="hidValidatorJsonData" value='${$item.getJsonData()}' />
                </td>
            </tr>
        </script>
    </form>
</body>
</html>
<script type="text/javascript">
    function collectPropertiesValue()
    {
        var strB = new Sys.StringBuilder();
        var properties = $find("propertyGrid").get_properties();

        for (var i = 0; i < properties.length; i++)
        {
            if (strB.isEmpty() == false)
                strB.append("\n");

            var prop = properties[i];
            strB.append(prop.name + ": " + prop.value ? prop.value : 'no value');
        }

        return strB.toString();
    }

    function onClickEditor(sender, e)
    {
        var activeEditor = sender.get_activeEditor();
    }

    //function onShowPropertiesValueClick()
    //{
    //    $get("propertyResult").innerText = collectPropertiesValue();
    //}

    //function onEnterEditor(sender, e)
    //{
    //    $get("propertyResult").innerText += "\nEnter Property: " + e.propertyValue.value;
    //}

    //function OnBindEditorDropdownList(sender, e)
    //{
    //    var enumTypes = jQuery.evalJSON(jQuery('#enumTypeStore').val());
    //    var result = enumTypes[e.property.editorParams];

    //    if (result)
    //        e.enumDesc = result;
    //}

    //function onClickValidated(sender, e)
    //{

    //}

    //function onPopertyClientShow(sender, e)
    //{
    //    var activeEditor = sender;
    //}

    $(document).ready(function ()
    {
        $("#ddlValidator").bind("change", initPropertyGrid);
        var validators = window.dialogArguments;
        loadValidators(validators);
        initPropertyGrid();
    });

    //加载Validator列表
    function loadValidators(validators)
    {
        if (validators != null && validators != undefined)
        {
            $("#_tempValidator").tmpl(validators,
            {
                getJsonData: function ()
                {
                    return JSON.stringify(this.data);
                }
            }).appendTo('#gridValidator');
            setSequenceNumber();
        }
    }
    //删除Validator
    function deleteValidator(obj)
    {
        if (confirm("确定要删除该条数据吗？"))
        {
            $(obj).parent().parent().remove();
            setSequenceNumber();
            return true;
        }
        return false;
    }
    //设置序号
    function setSequenceNumber()
    {
        $("td[data-field='vrindex']").each(function (i, td)
        {
            td.innerText = i + 1;
        });
    }
    //编辑Validator属性设置
    function editValidatorProperty(obj)
    {
        var json = $(obj).nextAll("input[name='hidValidatorJsonData']").val();
        if (json != "")
        {
            var propertyGrid = $find("propertyGrid");
            var v = Sys.Serialization.JavaScriptSerializer.deserialize(json);
            $("#ddlValidator option:selected").removeAttr("selected");
            $("#ddlValidator option[text='" + v.Description + "']").attr("selected", true);
            //给PropertyGrid的属性赋值
            var properties = getValidatorPropertis(v.ValidatorName);
            $.each(v.Parameters, function (i, p)
            {
                for (var i = 0; i < properties.length; i++)
                {
                    if (properties[i].name.toLowerCase() == p.Name.toLowerCase())
                    {
                        properties[i].value = p.ParamValue;
                        break;
                    }
                }
            });

            propertyGrid.set_properties(properties);
            propertyGrid.dataBind();
        }
    }

    //初始化PropertyGrid
    function initPropertyGrid()
    {
        var v = $("#ddlValidator").val();
        var propertyGrid = $find("propertyGrid");
        if (v == "")
        {
            if (propertyGrid != null)
            {
                propertyGrid.set_properties([]);
                propertyGrid.dataBind();
            }
        }
        else
        {
            var arr = v.split("|");
            var name = arr.length == 2 ? arr[0] : "";
            var type = arr.length == 2 ? arr[1] : "";
            if (propertyGrid != null)
            {
                var properties = getValidatorPropertis(name);
                if (properties == null || properties == undefined)
                {
                    properties = [];
                }
                propertyGrid.set_properties(properties);
                propertyGrid.dataBind();
            }
        }
    }
    //根据校验器名称获取校验器属性集合
    function getValidatorPropertis(validatorName)
    {
        var key = validatorName + "Properties";
        for (var i = 0; i < arrValidatorDefine.length; i++)
        {
            if (arrValidatorDefine[i].ValidatorName == key)
            {
                return arrValidatorDefine[i].PropertyValues;
                break;
            }
        }
    }

    //保存PropertyGrid内容
    function saveClick()
    {
        var name_type = $("#ddlValidator").val();
        if (name_type != "")
        {
            var arr = name_type.split("|");
            var name = arr.length == 2 ? arr[0] : "";
            var type = arr.length == 2 ? arr[1] : "";
            var validator =
                   {
                       ValidatorName: name,
                       ValidatorType: type,
                       Description: $("#ddlValidator option:selected").text(),
                       Parameters:[]
                   };
            var properties = $find("propertyGrid").get_properties();
            $.each(properties, function (i, p)
            {
                validator.Parameters.push({ Name: p.name, ParamValue: p.value, DataType: p.dataType });
            });
            var isExsist = false;
            $("input[name='hidValidatorJsonData']").each(function (i, o)
            {
                $o = $(o);
                if ($o.val() != "")
                {
                    var v = eval("(" + $o.val() + ")");
                    if (v.ValidatorName != undefined && v.ValidatorName == validator.ValidatorName)
                    {
                        $o.val(Sys.Serialization.JavaScriptSerializer.serialize(validator));
                        isExsist = true;
                    }
                }
            });

            if (!isExsist)
            {
                var validators = [];
                validators.push(validator);
                loadValidators(validators);
                initPropertyGrid();
            }
        }
    }
    //保存选中项，并关闭页面
    function okClick()
    {
        var validators = [];
        $("input[name='hidValidatorJsonData']").each(function (i, o)
        {
            $o = $(o);
            if ($o.val() != "")
            {
                var v = eval("(" + $o.val() + ")");
                if (v)
                {
                    validators.push(v);
                }
                
            }
        });
        //JSON.stringify(validators)
        var jsonData = validators.length == 0 ? "" : Sys.Serialization.JavaScriptSerializer.serialize(validators);
        window.returnValue = jsonData;
        window.close();
    }

</script>
