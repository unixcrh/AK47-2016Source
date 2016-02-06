<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNode.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.editNode.AddNode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加节点</title>
    <base target="_self" />
    <script src="../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var param = "scrollbars=yes;resizable=yes;help=no;status=no;center=yes;location=no;dialogHeight=350px;dialogWidth=400px;";
        function okButtonClick() {
            var name = document.getElementById("codeName").value;
            var fjid = document.getElementById("fjid").value;
            if (trim(name) == "") {
                alert("显示名称不能为空");
                return;
            }
            if (trim(fjid) == "") {
                alert("父级ID不能为空");
                return;
            }
            $("#btn_save").click();
        }

        function fujidblClick() {
            var fjidVar = document.getElementById("fjid").value;
            var result = window.showModalDialog("NodeDetails.aspx?CODE=" + fjidVar, null, param);
            if (result) {
                $("#fjid").val(result.Code);
                $("#name1").val(result.Name);
            }

        }

        function trim(str) { //删除左右两端的空格  
            return str.replace(/(^\s*)|(\s*$)/g, "");
        }  

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="margin-left: 10px; margin-top: 15px;">
        <tr>
            <td>
                <label for="CodeName" class="pc-label">显示名称</label>
            </td>
            <td>
                <input type="text" runat="server" id="codeName" class="pc-textbox" />
            </td>
        </tr>
        <tr>
            <td>
                <label for="fjid" class="pc-label">父级ID(双击)</label>
            </td>
            <td>
                <input type="text" runat="server" id="fjid" style="width: 255px;" ondblclick="fujidblClick()"  class="pc-textbox" readonly />
            </td>
        </tr>
        <tr>
            <td>
                <label for="description" class="pc-label">描述</label>
            </td>
            <td><input type="text" runat="server" id="description" style="width: 255px;" class="pc-textbox" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <input type="button" runat="server" id="okButton" class="formButton" value="确定(O)"  accesskey="O" onclick="okButtonClick();" />
                <div style="display: none;">
                    <input type="submit" runat="server" text="确定..." popupcaption="正在操作..." id="btn_save" onserverclick="okButton_Click" />
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none;">
        <input type="text" runat="server" id="code" />
        <input type="text" runat="server" id="name1" />
        <input type="text" runat="server" id="dataTime" />
    </div>
    </form>
</body>
</html>
