<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MCS.Dynamics.Web.Index" %>

<%@ Register TagPrefix="menu" TagName="menu" Src="inc/WebControls/BannerIframe.ascx" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基础平台</title>
    <link href="Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="Css/home.css" rel="stylesheet" type="text/css" />
    <script src="scripts/MicrosoftAjax.debug.js" type="text/javascript"></script>
    <script src="scripts/pc.js" type="text/javascript"></script>
    <script src="scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .pc-frame-header a:hover {
            color: #5f5f5d;
        }
        .pc-frame-header a {
            font-family: "微软雅黑", "宋体", Arial, Helvetica, sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pc-frame-header">
            <pc:IframeBanner ID="frameBanner" runat="server" />
        </div>
        <%-- <div class="pc-frame-container">--%>
        <div class="pc-frame-container">
            <iframe id="iframe_content" style="width: 100%; height: 100%;" src="Default.aspx"
                frameborder="0" scrolling="no"></iframe>
        </div>
    </div>
    </form>
    <script>
        (function () {
            var heigth = $(window).height() - 40;
            $(".pc-frame-container").height(heigth);
        })();

    </script>
</body>
</html>
