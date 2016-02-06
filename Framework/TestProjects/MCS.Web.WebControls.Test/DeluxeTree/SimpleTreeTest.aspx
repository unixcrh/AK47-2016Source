<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimpleTreeTest.aspx.cs"
    Inherits="MCS.Web.WebControls.Test.DeluxeTree.SimpleTreeTest" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>树测试</title>
    <style type="text/css">
        ._nodetext_selected {
            font-family: simsun;
            font-size: 12px;
            font-weight: bold;
            background-color: #b8ffff;
            color: black;
            cursor: hand;
        }

        .ajax__tree_nodetext {
            font-family: simsun;
            font-size: 12px;
            cursor: hand;
            color: black;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function onTreeNodeSelecting(sender, e) {
            //e.cancel = true;
            //alert("Selected");
        }

        function onTreeNodeContextMenu(sender, e) {
            addMessage("context menu: " + e.node.get_text());
            //e.defaultContextMenu = true;
        }

        function onTreeNodeDblClick(sender, e) {
            addMessage("dbl click: " + e.node.get_text());

            if (e.node.get_children().length > 0)
                e.node.set_expanded(!e.node.get_expanded());
        }

        function onTreeNodeAfterExpand(sender, e) {
            addMessage("after expand: " + e.node.get_text());
        }

        function onTreeNodeBeforeExpand(sender, e) {
            addMessage("before expand: " + e.node.get_text());

            if (e.node.get_value() == "loading") {
                addMessage("loading");

                e.node.clearChildren();

                for (var i = 0; i < Math.random() * 5; i++) {
                    var properties = { text: "动态子节点" + i };
                    e.node.appendChild($HGRootNS.DeluxeTreeNode.createNode(properties));
                }
                e.node.set_value("normal");
            }
        }

        function onTreeNodeCheckBoxBeforeClick(sender, e) {
            addMessage("check before click: " + e.node.get_text() + "; element checked: " + e.eventElement.checked);
            //e.cancel = true;
        }

        function onTreeNodeCheckBoxAfterClick(sender, e) {
            addMessage("check after click: " + e.node.get_text() + "; node checked:  " + e.node.get_checked());
        }

        function addMessage(msg) {
            result.innerHTML += "<p style='margin:0'>" + msg + "</p>";
        }

        function showMultiSelectedNodes() {
            var tree = $find("tree");
            var selectedNodes = tree.get_multiSelectedNodes();
            var strB = new Sys.StringBuilder();

            for (var i = 0; i < selectedNodes.length; i++) {
                strB.appendLine(selectedNodes[i].get_text());
            }

            result.innerText = strB.toString();

            if (tree.get_selectedNode())
                result.innerText += "\n单选:" + tree.get_selectedNode().get_text();
        }

        function onChangeNodeText() {
            var text = $get("nodeText").value;

            var selectedNode = $find("tree").get_selectedNode();

            if (selectedNode) {
                selectedNode.set_text(text);
            }
        }

        function onClearChildren() {
            var selectedNode = $find("tree").get_selectedNode();

            if (selectedNode)
                selectedNode.clearChildren(true, true);
        }

        function onReloadChildren() {
            var selectedNode = $find("tree").get_selectedNode();

            if (selectedNode)
                selectedNode.reloadChildren();
        }
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <%=DateTime.Now.ToString() %>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>--%>
        <div>
            <input id="nodeText" type="text" value="" /><input type="button" value="设置节点文本" onclick="onChangeNodeText();" />
        </div>
        <div id="container" runat="server">
            <DeluxeWorks:DeluxeTree ID="tree" runat="server" Width="200px" Height="300px" BorderStyle="Solid"
                BorderWidth="1px" OnNodeSelecting="onTreeNodeSelecting" OnNodeContextMenu="onTreeNodeContextMenu"
                OnNodeDblClick="onTreeNodeDblClick" OnNodeAfterExpand="onTreeNodeAfterExpand"
                OnGetChildrenData="tree_GetChildrenData" OnNodeCheckBoxBeforeClick="onTreeNodeCheckBoxBeforeClick"
                OnNodeCheckBoxAfterClick="onTreeNodeCheckBoxAfterClick" CollapseImage="" ExpandImage=""
                NodeCloseImg="closeImg.gif" NodeOpenImg="openImg.gif" CallBackContext="Test Context"
                NodeIndent="16">
                <Nodes>
                    <DeluxeWorks:DeluxeTreeNode Text="第一个节点" ShowCheckBox="True" CssClass="" NodeCloseImg=""
                        NodeOpenImg="" SelectedCssClass="" Checked="True" NavigateUrl="" Target="" />
                    <DeluxeWorks:DeluxeTreeNode Expanded="True" Text="第二个节点" CssClass="" NodeCloseImg=""
                        SelectedCssClass="_nodetext_selected" NodeOpenImg="" SubNodesLoaded="True" NavigateUrl=""
                        Target="">
                        <Nodes>
                            <DeluxeWorks:DeluxeTreeNode Text="我是text" Html="<B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert <B>A</B>lert"
                                CssClass="" NodeCloseImg="" NodeOpenImg="" SelectedCssClass="" NavigateUrl="javascript:alert(&quot;Hello, 李安(Turtle)&quot;)"
                                TextNoWrap="false" NodeVerticalAlign="Top" Target="" EnableToolTip="false" />
                            <DeluxeWorks:DeluxeTreeNode CssClass="" NodeCloseImg="" NodeOpenImg="" Text="www.sina.com.cn"
                                SelectedCssClass="" NavigateUrl="http://www.sina.com.cn" Target="innerFrame">
                            </DeluxeWorks:DeluxeTreeNode>
                            <DeluxeWorks:DeluxeTreeNode CssClass="" NodeCloseImg="" NodeOpenImg="" SelectedCssClass=""
                                Text="www.microsoft.com" NavigateUrl="http://www.microsoft.com" Target="innerFrame">
                            </DeluxeWorks:DeluxeTreeNode>
                        </Nodes>
                    </DeluxeWorks:DeluxeTreeNode>
                    <DeluxeWorks:DeluxeTreeNode Text="第三个节点" CssClass="" NodeCloseImg="" NodeOpenImg=""
                        ChildNodesLoadingType="LazyLoading" SelectedCssClass="" NavigateUrl="" Target=""
                        Selected="True" />
                    <DeluxeWorks:DeluxeTreeNode Text="节点的图标为切割图片">
                        <DeluxeWorks:DeluxeTreeNode Text="在线" ImgWidth="16px" ImgHeight="16px" NodeCloseImg="msn-icon4.gif"
                            NodeOpenImg="msn-icon4.gif" ChildNodesLoadingType="LazyLoading" />
                        <DeluxeWorks:DeluxeTreeNode Text="隐身" ImgWidth="16px" ImgHeight="16px" ImgMarginLeft="-16px"
                            NodeCloseImg="msn-icon4.gif" NodeOpenImg="msn-icon4.gif" ChildNodesLoadingType="LazyLoading" />
                        <DeluxeWorks:DeluxeTreeNode Text="忙碌" ImgWidth="16px" ImgHeight="16px" ImgMarginLeft="-32px"
                            NodeCloseImg="msn-icon4.gif" NodeOpenImg="msn-icon4.gif" ChildNodesLoadingType="LazyLoading" />
                        <DeluxeWorks:DeluxeTreeNode Text="离开" ImgWidth="16px" ImgHeight="16px" ImgMarginLeft="-48px"
                            NodeCloseImg="msn-icon4.gif" NodeOpenImg="msn-icon4.gif" ChildNodesLoadingType="LazyLoading" />
                    </DeluxeWorks:DeluxeTreeNode>
                    <DeluxeWorks:DeluxeTreeNode Text="在线" ImgWidth="16px" ImgHeight="16px" NodeCloseImg="msn-icon4.gif"
                        NodeOpenImg="msn-icon4.gif" ChildNodesLoadingType="LazyLoading" />
                    <DeluxeWorks:DeluxeTreeNode CssClass="" NodeCloseImg="" NodeOpenImg="" SelectedCssClass=""
                        Text="加载子节点会出现异常" NavigateUrl="" Target="" ChildNodesLoadingType="LazyLoading">
                    </DeluxeWorks:DeluxeTreeNode>
                    <DeluxeWorks:DeluxeTreeNode CssClass="" NodeCloseImg="" NodeOpenImg="" SelectedCssClass=""
                        Text="很多子节点，小心打开！" NavigateUrl="" Target="" ChildNodesLoadingType="LazyLoading">
                    </DeluxeWorks:DeluxeTreeNode>
                </Nodes>
            </DeluxeWorks:DeluxeTree>
            <input type="button" value="Show selected nodes" onclick="showMultiSelectedNodes();" />
            <input type="button" value="Clear Children" onclick="onClearChildren();" />
            <input type="button" value="Reload Children" onclick="onReloadChildren();" />
            <asp:Button ID="postBackBtn" runat="server" Text="PostBack" />
        </div>
        <div id="dynamicTreeContainer" runat="server">
        </div>
        <iframe style="border: 1px solid black; width: 100%; height: 150px" name="innerFrame"
            frameborder="0"></iframe>
        <div id="resultContainer">
            <div id="result" contenteditable="true" style="overflow: auto; border: 1px silver solid; width: 100%; height: 200px">
            </div>
        </div>
    </form>
</body>
</html>
