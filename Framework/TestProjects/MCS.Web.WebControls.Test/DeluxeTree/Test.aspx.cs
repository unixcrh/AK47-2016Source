using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Web.WebControls.Test.DeluxeTree
{
    public partial class Test : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeluxeTreeNode rootNode = new DeluxeTreeNode();
                rootNode.Text = "rootNode";
                rootNode.Value = "rootNode";
                rootNode.Expanded = true;
                rootNode.NavigateUrl = "http://www.baidu.com";
                rootNode.Target = "_self";
                //rootNode.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;
                //rootNode.LazyLoadingText = "正在加载..";

                DeluxeTreeNode node1 = new DeluxeTreeNode();
                node1.Text = "node1";
                node1.Value = "node1";
                //node1.Html = "<b>node1</b>";

                DeluxeTreeNode node2 = new DeluxeTreeNode();
                node2.Text = "node2";
                node2.Value = "node2";

                rootNode.Nodes.Add(node1);
                rootNode.Nodes.Add(node2);

                DeluxeTreeNode node3 = new DeluxeTreeNode();
                node3.Text = "node3";
                node3.Value = "node3";

                DeluxeTreeNode node4 = new DeluxeTreeNode();
                node4.Text = "node4";
                node4.Value = "node4";

                node2.Nodes.Add(node3);
                node2.Nodes.Add(node4);

                this.tree.Nodes.Add(rootNode);
                //this.tree.DataBind(); 
            }
        }
    }
}