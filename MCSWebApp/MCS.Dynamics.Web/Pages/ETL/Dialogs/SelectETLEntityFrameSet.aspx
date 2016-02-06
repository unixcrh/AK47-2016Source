<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectETLEntityFrameSet.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.SelectETLEntityFrameSet" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>实体主页</title>
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/home.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/pc.js" type="text/javascript"></script>
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../Javascript/home.js" type="text/javascript"></script>
    <script type="text/javascript">
        function onNodeSelecting(sender, e) {
            showWait(true);
            if (e.node._cssClass == "treeNodeThree") {
                $get("frmView").src = "SelectETLEntity.aspx?islast=true&categoryID=" + e.node.get_value();
            } else {
                $get("frmView").src = "SelectETLEntity.aspx?categoryID=" + e.node.get_value();
            }

            // showLoader(true);
            $get("lastVisitOrg")['value'] = e.node.get_value();
        }
        $(document).ready(function () {
            $(window).resize();
            var params = window.location.search;
            showWait(true);
            $("#frmView").attr("src", "SelectETLEntity.aspx" + params);
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pc-frame-container">
            <div class="pc-frame-vs" id="vspanel">
                <div class="pc-frame-left">
                    <ul class="pc-tree-list" id="tl">
                        <li style="position: static; zoom: 1; position: absolute; top: 0px; bottom: 0; left: 120px;
                            right: 50px; overflow: auto;">
                            <!-- 树控件 -->
                            <DeluxeWorks:DeluxeTree ID="tree" runat="server" OnGetChildrenData="tree_GetChildrenData"
                                OnNodeSelecting="onNodeSelecting">
                            </DeluxeWorks:DeluxeTree>
                        </li>
                    </ul>
                    <asp:HiddenField ID="navOUID" runat="server" Value="" />
                </div>
                <!------------------------------右侧列表------------------------------------------>
                <div class="pc-frame-right">
                    <iframe frameborder="0" id="frmView" name="frmView" style="height: 100%; width: 90%;"
                        src="about:blank">您的浏览器必须支持IFrame！ </iframe>
                </div>
                <div class="pc-frame-splitter-mask" style="z-index: 13">
                    <div class="pc-frame-splitter" style="z-index: 14" unselectable="on">
                    </div>
                </div>
            </div>
        </div>
        <div id="ldpg" class="pc-loader" style="display: block">
        </div>
        <div style="display: none">
            <asp:HiddenField runat="server" ID="lastVisitOrg" />
            <asp:Button Text="刷新" ID="btnRefresh" runat="server" OnClick="ReloadTree" />
        </div>
    </div>
    <div id="wait" style="z-index: 1; left: 50%; top: 50%; position: absolute; display: none;">
        <img src="../../../Images/wait.gif" />
    </div>
    </form>
</body>
</html>
