<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.LogList" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>操作日志</title>
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/home.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/json2.js" type="text/javascript"></script>
    <script src="../../scripts/pc.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showWait(isShow) {
            if (isShow) {
                $("#wait").show();
            } else {
                $("#wait").hide();
            }
        }
        function onNodeSelecting(sender, e) {
            showWait(true);
            if (e.node._cssClass == "treeNodeThree") {
                $get("frmView").src = "LogView.aspx&categoryID=" + e.node.get_value();
            } else {
                $get("frmView").src = "LogView.aspx?categoryID=" + e.node.get_value();
            }

            // showLoader(true);
           // $get("lastVisitOrg")['value'] = e.node.get_value();
        }
        $(document).ready(function () {
            showWait(true);
            $(window).resize();
            var params = window.location.search;
            $("#frmView").attr("src", "LogView.aspx" + params);
        });
        $(window).resize(function () {
            var heigth = $(this).height() - $(".pc-frame-header").height() - 3;
            $("#vspanel").height(heigth);
        });
    
    </script>
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
    <%--<div class="pc-frame-header">
        <pc:Banner ID="pcBanner" runat="server" ActiveMenuIndex="1" />
    </div>--%>
    <div class="pc-frame-container">
        <div class="pc-frame-vs" id="vspanel">
            <div class="pc-frame-left" style="overflow: auto; zoom: 1; margin-top:20px;">
                <!-- 树控件 -->
                <mcs:DeluxeTree runat="server" ID="tree" OnNodeSelecting="onNodeSelecting">

                </mcs:DeluxeTree>
                <asp:HiddenField ID="navOUID" runat="server" Value="" />
            </div>
            <div class="pc-frame-right">
                <iframe frameborder="0" id="frmView" style="height: 100%; width: 100%" src="about:blank">
                    您的浏览器必须支持IFrame！ </iframe>
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
        <input type="hidden" runat="server" id="shuttlePoint" />
        <asp:Button runat="server" ID="shuttleTrigger" />
    </div>
    </form>
    <div id="wait" style="z-index: 1; left: 50%; top: 50%; position: absolute; display: none;">
        <img src="../../Images/wait.gif" />
    </div>
    <script type="text/javascript">
        //	    $pc.ui.configFrame("vspanel");
        //	    $pc.ui.listMenuBehavior("listMenu");
        //	    var regG = /^G(\.\w+(\.\w+)?)?$/i;
        //	    var regAd = /^AD(\.\w+(\.\w+)?)?$/i;
        //	    var regReverseAd = /^RAD(\.\w+(\.\w+)?)?$/i;

        //	    function shuttle(timePoint) {
        //	        document.getElementById("shuttlePoint").value = timePoint;
        //	        document.getElementById("shuttleTrigger").click();
        //	    }

        //	    function onNodeSelecting(sender, e) {
        //	        var cate = e.node.get_value();
        //	        if (regG.test(cate)) {
        //	            $get("frmView").src = $pc.appRoot + "lists/LogView.aspx?catelog=" + cate.substring(2);
        //	        } else if (regAd.test(cate)) {
        //	            $get("frmView").src = $pc.appRoot + "lists/ADLog.aspx?catelog=" + cate.substring(3);
        //	        } else if (regReverseAd.test(cate)) {
        //	            $get("frmView").src = $pc.appRoot + "lists/ADReverseLog.aspx?catelog=" + cate.substring(4);
        //	        }

        //	        showLoader(true);
        //	        //$get("result").innerText = e.node.get_value() + ": " + e.node.get_text() + "\n" + $get("result").innerText;
        //	    }

        //	    function showLoader(show) {
        //	        var ele = $get("ldpg");
        //	        ele.style.display = show ? "block" : "none";
        //	        ele = null;
        //	    }

        //	    Sys.Application.add_init(function () {
        //	        var tree = $find("tree");
        //	        if (tree) {
        //	            tree.selectNode(tree.get_nodes()[0]);
        //	            tree.get_element().style.zoom = 1; // 树在页面刷新之后消失
        //	        }
        //	        tree = null;
        //	        $get("frmView").src = $pc.appRoot + "lists/LogView.aspx";
        //	        showLoader(true);

        //	    });
    </script>
</body>
</html>
