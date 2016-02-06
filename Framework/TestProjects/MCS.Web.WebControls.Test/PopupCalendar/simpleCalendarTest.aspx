<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="simpleCalendarTest.aspx.cs"
    Inherits="MCS.Web.WebControls.Test.PopupCalendar.simpleCalendarTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="MCS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple Calendar Test</title>
    <script type="text/javascript">
        function addCalendar() {
            var container = $get("div_insert");
            var input = $HGDomElement.createElementFromTemplate(
				        {
				            nodeName: "input",
				            properties: { type: "text" },
				            cssClasses: ["ajax_calendartextbox ajax__calendar_textbox"]
				        }, container);

            var calendar = new $HGRootNS.DeluxeCalendar(input);
            calendar.clientInitialize("ctrlDeluxeCalendar");
        }    
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
    <div id="container">
        <MCS:DeluxeCalendar ID="ctrlDeluxeCalendar" runat="server" AutoComplete="false" StartYear="2010" EndYear="2100">
        </MCS:DeluxeCalendar>
    </div>
    <br />
    <input type="button" value="addCalendar" onclick="addCalendar();" />
    <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />

        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
    <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />

        <br />
    <br />
        <br />
    <br />
        <br />
    <br />    <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
        <br />
    <br />

        <br />
    <br />
        <br />
    <br />
        <br />
    <br />
    <div id="div_insert" style="border: 1px solid #eee; padding: 5px;">
    </div>
    </form>
</body>
</html>
