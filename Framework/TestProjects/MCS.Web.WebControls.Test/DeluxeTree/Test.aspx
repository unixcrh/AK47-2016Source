<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxeTree.Test" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function OnNodeContextMenu(sender, e) {
            alert("context menu: " + e.node.get_text());
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <DeluxeWorks:DeluxeTree ID="tree" runat="server" NodeCloseImg="closeImg.gif" NodeOpenImg="openImg.gif"
            SupportPostBack="true" OnNodeContextMenu="OnNodeContextMenu">
        </DeluxeWorks:DeluxeTree>
    </div>
    <br />
    <div>
        <asp:Button ID="Button1" runat="server" Text="postBack" />
    </div>
    </form>
</body>
</html>
