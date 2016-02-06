<%@ Page Language="C#" AutoEventWireup="true" Inherits="SampleControl_ClientCallBack" Codebehind="ClientCallBack.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>Client Callback Example</title>
  <script type="text/ecmascript">
    function LookUpStock()
    {
        var lb = document.getElementById("ListBox1");
        var product = lb.options[lb.selectedIndex].text;
        CallServer(product, "");
    }
    
    function ReceiveServerData(rValue)
    {   
        document.getElementById("ResultsSpan").innerHTML = rValue;
        
    }
    
        function onOK()
        {
            alert("ok");
        }
        
        function onCancel()
        {
            alert("cancel");
        }
  </script>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:ListBox ID="ListBox1" Runat="server"></asp:ListBox>
      <br />
      <br />
      <button type="Button" onclick="LookUpStock()">Look Up Stock</button>
      <br />
      <br />
      Items in stock: <span id="ResultsSpan" runat="server"></span>
      <br />
      <a href="ClientCallBack.aspx">open ClientCallBack</a>
      <a href="javascript:showModalDialog('ClientCallBack.aspx')">showModalDialog ClientCallBack</a>
      <a href="javascript:window.open('Default.aspx', '_blank', <%=WindowFeatureStr %>)">open Default</a>
      <a href="javascript:showModalDialog('Default.aspx','',<%=DialogFeatureStr %>)">showModalDialog Default</a>
    </div>
  </form>
</body>
</html>