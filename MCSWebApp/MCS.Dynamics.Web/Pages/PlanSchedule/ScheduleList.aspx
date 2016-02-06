<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleList.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.PlanSchedule.ScheduleList" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>计划管理</title>
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/MicrosoftAjax.debug.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/pc.js" type="text/javascript"></script>
    <style type="text/css">
        .maxheightAndWidth
        {
            width: 100%;
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#innerFrameContent").height($(window).height() - $(".pc-frame-header").height() - 5);
        });
    </script>
</head>
<body class="maxheightAndWidth">
    <form id="form1" runat="server">
    <div>
        <%--<div class="pc-frame-header">
            <pc:Banner ID="pcBanner" runat="server" ActiveMenuIndex="3" />
        </div>--%>
        <div id="innerFrameContent" style="width: 100%;">
            <iframe style="width: 100%; height: 100%;" src="/MCSWebApp/WorkflowDesigner/PlanScheduleDialog/ScheduleList.aspx?showEditBtn=false"
                height="700" width="100%" frameborder="0" scrolling="no"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
