<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeDetails.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.editNode.NodeDetails" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>父级</title>
    <base target="_self" />
    <script src="../../Javascript/pc.js" type="text/javascript"></script>
    <script src="../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Javascript/home.js" type="text/javascript"></script>
    <script src="../../scripts/EntityCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        function onNodeSelected(sender, e) {
            $("#HiddenCode").val(e.node.get_value());
            $("#HiddenName").val(e.node.get_text());
            var element = window.event.srcElement;
            if (element.checked) {
                tv = document.getElementById("tree");
                es = tv.getElementsByTagName("input");
                for (var i = 0; i < es.length; i++) {
                    if (es[i].checked) {
                        es[i].checked = false;
                        element.checked = true;
                    }
                }
            }
        }
    </script>
    <style type="text/css">
        .cel
        {
            padding-left: 35%;
        }
        .height
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-top: 5px;">
    <div>
        <table border="1px" style="height: 100%; width: 96%;">
            <tr>
                <td class="cel height">
                    <label style="font-size: 20px; font-weight: bold;">
                        请选择父级
                    </label>
                </td>
            </tr>
            <tr>
                <td class="cel">
                    <!-- 树控件 -->
                    <DeluxeWorks:DeluxeTree ID="tree" runat="server" OnGetChildrenData="tree_GetChildrenData"
                        OnNodeCheckBoxAfterClick="onNodeSelected">
                    </DeluxeWorks:DeluxeTree>
                </td>
            </tr>
            <tr>
                <td class="cel">
                    <asp:Button ID="okButton" runat="server" class="formButton" Text="确定(O)" AccessKey="O"
                        OnClick="btn_Save_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenCode" runat="server" />
        <asp:HiddenField ID="HiddenName" runat="server" />
    </div>
    </form>
</body>
</html>
