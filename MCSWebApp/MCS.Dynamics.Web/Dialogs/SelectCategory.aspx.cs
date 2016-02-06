using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Converters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library;
using MCS.Web.Library.Script;
using MCS.Web.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Web.Library.MVC;

namespace MCS.Dynamics.Web.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class SelectCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DECategory root = CategoryAdapter.Instance.GetRoot();
                DeluxeTreeNode node = new DeluxeTreeNode(root.DisplayName, root.Code);
                node.NodeCloseImg = "../Images/wenjianjia.gif";
                node.NodeOpenImg = "../Images/wenjianjia.gif";
                //node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;
                node.CssClass = "treenodeParent";
                tree.Nodes.Add(node);
                node.Expanded = true;
                tree_GetChildrenData(node, node.Nodes, null);
            }
        }
        protected void ReloadTree(object sender, EventArgs e)
        {
        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {

        }

        protected void tree_SelectingNode(DeluxeTreeNode parentNode, DeluxeTreeNodeCollection result, string callBackContext)
        {

        }
        protected void tree_GetChildrenData(DeluxeTreeNode parentNode, DeluxeTreeNodeCollection result, string callBackContext)
        {

            string cssclass = parentNode.CssClass;
            CategoryCollection root = CategoryAdapter.Instance.GetByParentCode(parentNode.Value);
            foreach (var item in root)
            {
                DeluxeTreeNode node = new DeluxeTreeNode(item.DisplayName, item.Code);
                if (IsLastNode(item.Code))
                {
                    node.ShowCheckBox = true;
                    node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.Normal;
                    node.Expanded = true;
                }
                else
                {
                    node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;
                }
                node.NodeCloseImg = "../Images/wenjianjia.gif";
                node.NodeOpenImg = "../Images/wenjianjia.gif";



                result.Add(node);
            }

        }

        /// <summary>
        /// 判断是不是叶子节点
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        protected bool IsLastNode(string categoryCode)
        {
            bool result = true;
            CategoryCollection root = CategoryAdapter.Instance.GetByParentCode(categoryCode);
            if (root.Count > 0)
            {
                result = false;
            }
            return result;
        }
    }
}