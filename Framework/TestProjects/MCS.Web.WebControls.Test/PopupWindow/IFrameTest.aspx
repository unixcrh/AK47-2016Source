<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IFrameTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.PopupWindow.IFrameTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function getInfo() {
            popup = document.getElementById("popupFrame");

            var input = popup.contentDocument.createElement("input");
            popup.contentDocument.body.appendChild(input);
        }
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
    <div>
        <iframe src="innerPage.html" id="popupFrame"></iframe>
        <input type="button" onclick="getInfo()" value="Get Info"></input>
    </div>
    </form>
</body>
</html>
