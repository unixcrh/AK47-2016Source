<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MCS.Dynamics.Web._default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实体主页</title>
    <link href="Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="Css/home.css" rel="stylesheet" type="text/css" />
    <link href="Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/pc.js" type="text/javascript"></script>
    <script src="Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Javascript/home.js" type="text/javascript"></script>
    <script src="scripts/EntityCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        function onNodeSelecting(sender, e) {
            showWait(true);

            if (e.node._cssClass == "treeNodeThree") {
                $get("frmView").src = urldown + "?islast=true&categoryID=" + e.node.get_value();
            } else {
                $get("frmView").src = urldown + "?categoryID=" + e.node.get_value();
            }

            $get("lastVisitOrg")['value'] = e.node.get_value();
        }

        var recdiURL = {
            Entity: "pages/Entity/EntityInfo.aspx",
            ETLEntity: "pages/ETL/ETLEntitylist.aspx"
        };
        var urldown = "pages/Entity/EntityInfo.aspx";
        $(document).ready(function () {
            Request.IntalData();

            if (Request.QueryString("frameUrl")) {
                urldown = recdiURL[Request.QueryString("frameUrl")];
            }
            $(window).resize();
            var params = window.location.search;
            showWait(true);
            $("#frmView").attr("src", urldown);
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
    </script>
    <style type="text/css">
        .pc-frame-left{ width: 220px;}
        .pc-frame-splitter{ margin-left: 220px;}
        .pc-frame-right{ margin-left: 225px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pc-frame-container">
            <div class="pc-frame-vs" id="vspanel">
                <div class="pc-frame-left">
                    <ul class="pc-tree-list" id="tl">
                        <li style="position: static; zoom: 1; position: absolute; top: 20px; bottom: 0; left: 0;right: 0; overflow: auto;">
                            <!-- 树控件 -->
                            <DeluxeWorks:DeluxeTree ID="tree" runat="server" 
                                OnGetChildrenData="tree_GetChildrenData" 
                                InvokeWithoutViewState="true"
                                OnNodeSelecting="onNodeSelecting">
                            </DeluxeWorks:DeluxeTree>
                        </li>
                    </ul>
                    <asp:HiddenField ID="navOUID" runat="server" Value="" />
                </div>
                <!------------------------------右侧列表------------------------------------------>
                <div class="pc-frame-right">
                    <iframe frameborder="0" id="frmView" name="frmView" style="height: 100%; width: 100%;" src="about:blank">您的浏览器必须支持IFrame！ </iframe>
                </div>
                <div class="pc-frame-splitter-mask" style="z-index: 13">
                    <div class="pc-frame-splitter" style="z-index: 14" unselectable="on">
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none">
            <asp:HiddenField runat="server" ID="lastVisitOrg" />
            <asp:Button Text="刷新" ID="btnRefresh" runat="server" OnClick="ReloadTree" />
        </div>
        <div id="wait" style="z-index: 1; left: 50%; top: 50%; position: absolute; display: none;">
            <img src="Images/wait.gif" />
        </div>
    </div>
    </form>
</body>
</html>
