<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxeCalendar.test" %>

<%@ Register assembly="MCS.Web.WebControls" namespace="MCS.Web.WebControls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <script type="text/javascript">
      function onValueChange(e) {
          alert(e.get_value());
          e.set_value("");
      }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:DeluxeCalendar ID="DeluxeCalendar1" runat="server" OnClientValueChanged="onValueChange">
        </cc1:DeluxeCalendar>
    
    </div>
    </form>
</body>
</html>
