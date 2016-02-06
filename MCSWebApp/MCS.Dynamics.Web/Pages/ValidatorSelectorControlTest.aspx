<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidatorSelectorControlTest.aspx.cs" Inherits="MCS.Dynamics.Web.ValidatorSelectorControlTest" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.7.2.min.js"></script>
    <script src="../scripts/pc.js"></script>
    <script type="text/javascript">
        function test()
        {
            var allProperties = $find("propertyGrid").get_properties();
            var result = [];
            for (var i = 0; i < allProperties.length; i++)
            {
                result.push({ name: allProperties[i].name, value: allProperties[i].value });
            }
            var s = Sys.Serialization.JavaScriptSerializer.serialize(result);
            $("#divmsg").html(s);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
    <div>
        <div style="float:left;width:100%;">
            <soa:PropertyGrid ID="propertyGrid" runat="server" Width="500px" Height="300px" DisplayOrder="ByCategory"  ReadOnly="false"></soa:PropertyGrid>
        </div>
        <div style="float:left;width:100%;">
            <input type="button" value="显示属性值" onclick="test()"/>
        </div>
        <div id="divmsg" style="float:left;width:100%;"></div>
    </div>
        
    </form>
</body>
</html>
