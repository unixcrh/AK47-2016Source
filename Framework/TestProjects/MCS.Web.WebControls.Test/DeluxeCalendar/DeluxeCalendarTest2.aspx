<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeCalendarTest2.aspx.cs"
    Inherits="MCS.Web.WebControls.Test.DeluxeCalendar.DeluxeCalendarTest2" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function set_readOnly(p) {
            if (p == 1) {
                $find("calendar").set_readOnly(true);
            }
            else {
                $find("calendar").set_readOnly(false);
            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 200px">
        <cc1:DeluxeCalendar runat="server" ID="calendar" ReadOnly="true">
        </cc1:DeluxeCalendar>
        <input value="test" type="button" onclick="set_readOnly(1)" />
        <input value="test2" type="button" onclick="set_readOnly(2)" />
    </div>
    <br />
    <asp:Button runat="server" Text="PostBack" OnClick="Unnamed1_Click" />
    </form>
</body>
</html>
