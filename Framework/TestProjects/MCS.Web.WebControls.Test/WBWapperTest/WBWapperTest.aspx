<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBWapperTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.WBWapperTest.WBWapperTest" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var WebBrowserWrapperInstance;
        function OnLoad() {
        }
        function onBtnClick() {
            //var wrapper = $find("testWapper");
            WebBrowserWrapperInstance = WebBrowserWrapperFactory.Create();
            var Web2 = WebBrowserWrapperFactory.Create();
            alert(WebBrowserWrapperInstance == Web2)
            WebBrowserWrapperInstance.preview();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:WebBrowserWrapper ID="testWapper" runat="server" />
        <br />
        

        <input id="Button1" type="button" onclick="onBtnClick();" value="Preview" runat="server" /><br />
    </div>
    </form>
</body>
</html>
