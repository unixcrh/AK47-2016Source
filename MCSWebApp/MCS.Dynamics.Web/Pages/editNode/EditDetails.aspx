<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDetails.aspx.cs" Inherits="MCS.Dynamics.Web.Pages.editNode.EditDetails" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <base target="_self" />
    <link href="../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/home.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <script src="../../Javascript/pc.js" type="text/javascript"></script>
    <script src="../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Javascript/home.js" type="text/javascript"></script>
    <script src="../../scripts/EntityCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        function nodeDblClick(sender, e) {
            $get("frmView").src = "AddNode.aspx?code=" + e.node.get_value();
        }
        function addButton() {
            $get("frmView").src = "AddNode.aspx";
        }
        function select(sender, e) {
            $("#HiddenCode").val(e.node.get_value());
        }
        function delButton() {
            if ($("#HiddenCode").val() != "") {
                var a = window.confirm("是否删除？");
                if (a) {
                    $("#btn_del").click();
                }

            } else {
                alert("请选择删除对象！");
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pc-frame-container">
            <div class="pc-frame-splitter">
                <div class="pc-frame-left">
                    <a onclick="delButton()" style="float: right; margin-top: 5px; cursor: pointer;">
                        <img src="../../Images/16/delete.gif" alt="删除" border="0" height="50%" /></a>
                    <a onclick="addButton()" style="float: right; margin-left: -30px; cursor: pointer;">
                        <img src="../../Images/appIcon/15.gif" alt="添加" border="0" />
                    </a>
                    <ul class="pc-tree-list" id="tl">
                        <li style="position: static; zoom: 1; position: absolute; top: 20px; bottom: 0; left: 0;
                            right: 0; overflow: auto;">
                            <!-- 树控件 -->
                            <DeluxeWorks:DeluxeTree ID="tree" runat="server" OnGetChildrenData="tree_GetChildrenData"
                                OnNodeDblClick="nodeDblClick" OnNodeSelecting="select">
                            </DeluxeWorks:DeluxeTree>
                        </li>
                    </ul>
                    <asp:HiddenField ID="HiddenCode" runat="server" />
                    <asp:HiddenField ID="HiddenName" runat="server" />
                    <asp:HiddenField ID="HiddenId" runat="server" />
                    <div style="display: none;">
                        <input type="submit" runat="server" text="删除..." popupcaption="正在操作..." id="btn_del"
                            onserverclick="del_Click" />
                    </div>
                </div>
            </div>
        </div>
        <!------------------------------右侧列表------------------------------------------>
        <div class="pc-frame-right">
            <iframe frameborder="0" id="frmView" name="frmView" style="height: 100%; width: 100%;"
                src="about:blank">您的浏览器必须支持IFrame！ </iframe>
        </div>
    </div>
    </form>
</body>
</html>
