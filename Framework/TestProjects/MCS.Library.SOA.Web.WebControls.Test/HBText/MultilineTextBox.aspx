<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultilineTextBox.aspx.cs" Inherits="MCS.Library.SOA.Web.WebControls.Test.HBText.MultilineTextBox" %>

<%@ Register Assembly="MCS.Library.SOA.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="HB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiline TextBox Test</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <HB:HBTextBox runat="server" ID="tbx1" TextMode="MultiLine"></HB:HBTextBox>
        </div>
    </form>
</body>
</html>
