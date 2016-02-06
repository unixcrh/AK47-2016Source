<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CompleteTreeTest.aspx.cs"
	Inherits="DeluxeWorks.Web.WebControls.Test.DeluxeTree.CompleteTreeTest" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DeluxeWorks.Web.WebControls" Namespace="ChinaCustoms.Framework.DeluxeWorks.Web.WebControls"
	TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>完整的树测试</title>
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
	        color: yellow;
        }
    </style>

	<script type="text/javascript">
    function onTreeNodeSelecting(sender, e)
    {
        addMessage("node selecting: " + e.node.get_text());
        //e.cancel = true;
    }

    function onTreeNodeContextMenu(sender, e)
    {
        addMessage("context menu: " + e.node.get_text());
        //e.defaultContextMenu = true;
    }

    function onTreeNodeDblClick(sender, e)
    {
        addMessage("dbl click: " + e.node.get_text());

        if (e.node.get_children().length > 0)
            e.node.set_expanded(!e.node.get_expanded());
    }
 
    function onTreeNodeAfterExpand(sender, e)
    {
        addMessage("after expand: " + e.node.get_text());
    }

    function onTreeNodeBeforeExpand(sender, e)
    {
        addMessage("before expand: " + e.node.get_text());

        if (e.node.get_value() == "loading")
        {
            addMessage("loading");

            e.node.clearChildren();

            for(var i = 0; i < Math.random() * 5; i++)
            {
                var properties =
                            {
                                text: "动态子节点" + i
                            };

                e.node.appendChild($HGRootNS.DeluxeTreeNode.createNode(properties));
            }
            
            e.node.set_value("normal");
        }
    }

    function onTreeNodeCheckBoxBeforeClick(sender, e)
    {
        addMessage("check before click: " + e.node.get_text() + "; element checked: " + e.eventElement.target.checked);
        //e.cancel = true;
    }

    function onTreeNodeCheckBoxAfterClick(sender, e)
    {
        addMessage("check after click: " + e.node.get_text() + "; node checked:  " + e.node.get_checked());
    }

    function addMessage(msg)
    {
        result.innerHTML += "<p style='margin:0'>" + msg + "</p>";
    }
	</script>

</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
		</asp:ScriptManager>
		<table width="50%">
			<tr>
				<td colspan="2">
					树控件的属性：</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label1" runat="server" Text="节点打开时的图片"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtParentNodeOpenImg" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label2" runat="server" Text="节点关闭时的图片"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtParentNodeCloseImg" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="height: 26px">
					<asp:Label ID="Label3" runat="server" Text="展开树节点的展开图片"></asp:Label></td>
				<td style="height: 26px">
					<asp:TextBox ID="txtExpandImage" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label5" runat="server" Text="折叠树节点的展开图片"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtCollapseImage" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="height: 26px">
					<asp:Label ID="Label7" runat="server" Text="子节点的缩进像素"></asp:Label></td>
				<td style="height: 26px">
					<asp:TextBox ID="txtNodeIndent" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td colspan="2">
				</td>
			</tr>
			<tr>
				<td colspan="2">
					树控件节点的属性：</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label8" runat="server" Text="节点的提示信息"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtToolTip" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label11" runat="server" Text="节点展开时的图片"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtNodeOpenImg" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label28" runat="server" Text="节点折叠时的图片"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtNodeCloseImg" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label13" runat="server" Text="是否可以展开"></asp:Label></td>
				<td>
					<asp:DropDownList ID="ddlExpanded" runat="server">
						<asp:ListItem Selected="True" Value="1">是</asp:ListItem>
						<asp:ListItem Value="0">否</asp:ListItem>
					</asp:DropDownList></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label15" runat="server" Text="是否显示多选框"></asp:Label></td>
				<td>
					<asp:DropDownList ID="ddlShowCheckBox" runat="server">
						<asp:ListItem Selected="True" Value="1">是</asp:ListItem>
						<asp:ListItem Value="0">否</asp:ListItem>
					</asp:DropDownList></td>
			</tr>
			<tr>
				<td style="height: 26px">
					<asp:Label ID="Label18" runat="server" Text="节点文字的样式"></asp:Label></td>
				<td style="height: 26px">
					<asp:TextBox ID="txtCssClass" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label12" runat="server" Text="节点选中时的样式"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtSelectedCssClass" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label20" runat="server" Text="子节点是否延迟加载"></asp:Label></td>
				<td>
					<asp:DropDownList ID="ddlChildNodesLoadingType" runat="server">
						<asp:ListItem Value="1">是</asp:ListItem>
						<asp:ListItem Value="0">否</asp:ListItem>
					</asp:DropDownList></td>
			</tr>
			<tr>
				<td>
					&nbsp;<asp:Label ID="Label19" runat="server" Text="延迟加载所显示的子节点的名称"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtLazyLoadingText" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td colspan="2">
					以下的节点数据为“父节点１”的第二个子节点的：
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label21" runat="server" Text="节点的值"></asp:Label></td>
				<td>
					<asp:Label ID="lbValue" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label22" runat="server" Text="是否被选中"></asp:Label></td>
				<td>
					<asp:Label ID="lbChecked" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label24" runat="server" Text="是否有子节点"></asp:Label></td>
				<td>
					<asp:Label ID="lbHasChildren" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label25" runat="server" Text="前一个兄弟节点"></asp:Label></td>
				<td>
					<asp:Label ID="lbPreviousSibling" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td style="height: 26px">
					<asp:Label ID="Label27" runat="server" Text="下一个兄弟节点"></asp:Label></td>
				<td style="height: 26px">
					<asp:Label ID="lbNextSibling" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td style="height: 26px">
					<asp:Label ID="Label30" runat="server" Text="父节点"></asp:Label></td>
				<td style="height: 26px">
					<asp:Label ID="lbParent" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:Button ID="btnSet" runat="server" Text="设置" OnClick="btnSet_Click" /></td>
			</tr>
		</table>
		<div>
			<DeluxeWorks:DeluxeTree runat="server" ID="tree" OnNodeSelecting="onTreeNodeSelecting"
				OnNodeContextMenu="onTreeNodeContextMenu" OnNodeAfterExpand="onTreeNodeAfterExpand"
				OnNodeBeforeExpand="onTreeNodeBeforeExpand" OnNodeCheckBoxAfterClick="onTreeNodeCheckBoxAfterClick"
				OnNodeCheckBoxBeforeClick="onTreeNodeCheckBoxBeforeClick" OnNodeDblClick="onTreeNodeDblClick"
				OnGetChildrenData="tree_GetChildrenData" CollapseImage="" ExpandImage="" NodeCloseImg=""
				NodeOpenImg="">
				<Nodes>
					<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
						SelectedCssClass="" SubNodesLoaded="True" Text="父节点１" Value="parent1">
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" SubNodesLoaded="True" Text="子节点１" Value="node1" ShowCheckBox="True">
							<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
								SelectedCssClass="" Text="孙节点１" Value="child1">
							</DeluxeWorks:DeluxeTreeNode>
							<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
								SelectedCssClass="" Text="孙节点２" Value="child2">
							</DeluxeWorks:DeluxeTreeNode>
							<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
								SelectedCssClass="" Text="孙节点３" Value="child3">
							</DeluxeWorks:DeluxeTreeNode>
						</DeluxeWorks:DeluxeTreeNode>
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" SubNodesLoaded="True" Text="子节点２" Value="node2" ShowCheckBox="True">
							<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
								SelectedCssClass="" Text="孙节点４" Value="child4">
							</DeluxeWorks:DeluxeTreeNode>
							<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
								SelectedCssClass="" Text="孙节点５" Value="child5">
							</DeluxeWorks:DeluxeTreeNode>
							<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
								SelectedCssClass="" Text="孙节点６" Value="child6">
							</DeluxeWorks:DeluxeTreeNode>
						</DeluxeWorks:DeluxeTreeNode>
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点３" Value="node3" ShowCheckBox="True">
						</DeluxeWorks:DeluxeTreeNode>
					</DeluxeWorks:DeluxeTreeNode>
					<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
						SelectedCssClass="" SubNodesLoaded="True" Text="父节点2" Value="parent2">
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点４" Value="node4">
						</DeluxeWorks:DeluxeTreeNode>
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点５" Value="node5">
						</DeluxeWorks:DeluxeTreeNode>
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点6" Value="node6">
						</DeluxeWorks:DeluxeTreeNode>
					</DeluxeWorks:DeluxeTreeNode>
					<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
						SelectedCssClass="" SubNodesLoaded="True" Text="父节点3" Value="parent3">
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点7" Value="node7">
						</DeluxeWorks:DeluxeTreeNode>
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点8" Value="node8">
						</DeluxeWorks:DeluxeTreeNode>
						<DeluxeWorks:DeluxeTreeNode CssClass="" Expanded="False" NodeCloseImg="" NodeOpenImg=""
							SelectedCssClass="" Text="子节点9" Value="node9">
						</DeluxeWorks:DeluxeTreeNode>
					</DeluxeWorks:DeluxeTreeNode>
				</Nodes>
			</DeluxeWorks:DeluxeTree>
			&nbsp;&nbsp;
		</div>
		<div id="resultContainer">
			<div id="result" enableviewstate="true" style="overflow: auto; border: 1px silver solid;
				width: 100%; height: 200px">
			</div>
		</div>
	</form>
</body>
</html>
