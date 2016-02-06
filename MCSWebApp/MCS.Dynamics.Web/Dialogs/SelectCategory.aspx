<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCategory.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.SelectCategory" %>
    <%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择类别</title>
    <base target="_self" />
    <link href="../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../css/form.css" type="text/css" rel="stylesheet" />
    <script src="../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../scripts/Entity/EntityDetails.js" type="text/javascript"></script>
    <script src="../scripts/pc.js" type="text/javascript"></script>
    <script type="text/ecmascript">
        var selected = new Array();
        function onNodeSelecting(sender, e) {

            if (e.node._selected) {
                e.node._selected = false;
            }
            else {
                e.node._selected = true;
            }

        }

        function onNodeSelected(sender, e) {
            var selectedValue = e.node.get_value();
            if (e.node._selected) {
                selected.splice(selected.length, 0, selectedValue);
            }
            else {
                var index = GetIndex(selectedValue);
                selected.splice(index,1);
            }

        }

        function GetIndex(selectedValue) {
            var returnIndex = 0;
            for (var i = 0; i < selected.length; i++) {
                if (selected[i] == selectedValue) {
                    returnIndex = i;
                }
            }
            return returnIndex;
        }
        $(document).ready(function () {
            $(window).resize();
            var params = window.location.search;
            showWait(true);
            $("#frmView").attr("src", "pages/Entity/EntityInfo.aspx" + params);
        });
        $(window).resize(function () {
            var heigth = $(this).height() - $(".pc-frame-header").height() - 3;
            $("#vspanel").height(heigth);
        });
        function showWait(isShow) {
            if (isShow) {
                $("#wait").show();
            } else {
                $("#wait").hide();
            }
        }
        function onSaveClick() {
            window.returnValue = selected.join(",");

            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
    </div>
    <pc:SceneControl ID="SceneControl1" runat="server" />

    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <div class="dialogTitle">
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../Images/icon_01.gif" />
                                        选择类别</div>
                                </div>
                                <div>
                                    <DeluxeWorks:DeluxeTree ID="tree" runat="server" OnGetChildrenData="tree_GetChildrenData"  OnNodeCheckBoxBeforeClick="onNodeSelecting" OnNodeCheckBoxAfterClick="onNodeSelected">
                    </DeluxeWorks:DeluxeTree>
                                </div>
                              
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="gridfileBottom">
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px; vertical-align: middle; text-align: center">
                        <input type="button" runat="server" id="okButton" class="formButton" value="确定(S)"
                            accesskey="S" onclick="return onSaveClick();" />
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                        <%--<div style="display: none;">
                            <soa:SubmitButton runat="server" Text="保存..." PopupCaption="正在操作..." ID="btn_save"
                                OnClick="btn_Save_Click" />
                            <soa:HBDropDownList ID="ddl_FieldType" runat="server">
                            </soa:HBDropDownList>
                        </div>--%>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    </form>
</body>
</html>
