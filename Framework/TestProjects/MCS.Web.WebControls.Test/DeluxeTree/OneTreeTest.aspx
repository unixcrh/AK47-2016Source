<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OneTreeTest.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxeTree.OneTreeTest" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>树测试</title>
	<style type="text/css">
		._nodetext_selected
		{
			font-family: simsun;
			font-size: 12px;
			font-weight: bold;
			background-color: #b8ffff;
			color: black;
			cursor: hand;
		}
		
		.ajax__tree_nodetext
		{
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

			var children = e.node.get_children();

			if (children.length > 0 && children[0].get_text() == "正在加载...") {
				addMessage("loading");

				e.node.clearChildren();

				for (var i = 0; i < Math.random() * 5; i++) {
					var properties =
                            {
                            	text: "客户端动态子节点" + i
                            };

					e.node.appendChild($HGRootNS.DeluxeTreeNode.createNode(properties));
				}

				e.node.set_value("normal");
				e.cancel = true;
			}
		}

		function onTreeNodeCheckBoxBeforeClick(sender, e) {
			addMessage("check before click: " + e.node.get_text() + "; element checked: " + e.eventElement.target.checked);
			//e.cancel = true;
		}

		function onTreeNodeCheckBoxAfterClick(sender, e) {
			addMessage("check after click: " + e.node.get_text() + "; node checked:  " + e.node.get_checked());
		}

		function addMessage(msg) {
			result.innerHTML += "<p style='margin:0'>" + msg + "</p>";
		}

		function showMultiSelectedNodes() {
			var selectedNodes = $find("tree").get_multiSelectedNodes();
			var strB = new Sys.StringBuilder();

			for (var i = 0; i < selectedNodes.length; i++) {
				strB.appendLine(selectedNodes[i].get_text());
			}

			result.innerText = strB.toString();
		}
	</script>
</head>
<body>
	<form id="serverForm" runat="server">
	&nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<div id="container">
		<DeluxeWorks:DeluxeTree ID="tree" runat="server" Width="200px" Height="300px" BorderStyle="Solid"
			BorderWidth="1px" OnNodeSelecting="onTreeNodeSelecting" OnNodeContextMenu="onTreeNodeContextMenu"
			OnNodeDblClick="onTreeNodeDblClick" OnNodeAfterExpand="onTreeNodeAfterExpand"
			OnGetChildrenData="tree_GetChildrenData" OnNodeCheckBoxBeforeClick="onTreeNodeCheckBoxBeforeClick"
			OnNodeBeforeExpand="onTreeNodeBeforeExpand" OnNodeCheckBoxAfterClick="onTreeNodeCheckBoxAfterClick"
			CollapseImage="" ExpandImage="" NodeCloseImg="closeImg.gif" NodeOpenImg="openImg.gif">
			<Nodes>
				<DeluxeWorks:DeluxeTreeNode Text="第三个节点" CssClass="" NodeCloseImg="" NodeOpenImg=""
					ChildNodesLoadingType="LazyLoading" SelectedCssClass="" NavigateUrl="" Target="" />
			</Nodes>
		</DeluxeWorks:DeluxeTree>
		<input type="button" value="Show selected nodes" onclick="showMultiSelectedNodes();" />
		<asp:Button ID="postBackBtn" runat="server" Text="PostBack" />
	</div>
	<div id="Div1">
		<input type="button" value="Show selected nodes" onclick="showMultiSelectedNodes();" />
		<asp:Button ID="Button1" runat="server" Text="PostBack" />
	</div>
	<iframe style="border: 1px solid black; width: 100%; height: 150px" name="innerFrame"
		frameborder="0"></iframe>
	<div id="resultContainer">
		<div id="result" contenteditable="true" style="overflow: auto; border: 1px silver solid;
			width: 100%; height: 200px">
		</div>
	</div>
	</form>
</body>
</html>
