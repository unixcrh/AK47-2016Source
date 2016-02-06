using System;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Web.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using System.Text;
using MCS.Web.Library;

namespace MCS.Dynamics.Web.Pages.editNode
{
    public partial class NodeDetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = this.Request.QueryString["CODE"];
                DECategory root = CategoryAdapter.Instance.GetRoot();
                DeluxeTreeNode node = new DeluxeTreeNode(root.DisplayName, root.Code);
                node.NodeCloseImg = "../../Images/wenjianjia.gif";
                node.NodeOpenImg = "../../Images/wenjianjia.gif";
                node.CssClass = "treenodeParent";
                tree.Nodes.Add(node);
                node.Expanded = true;
                node.ShowCheckBox = true;

                if (code.Equals(root.Code))
                {
                    node.Checked = true;
                }
                tree_GetChildrenData(node, node.Nodes, null);

            }

        }
        protected void ReloadTree(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

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

                    node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.Normal;
                    node.CssClass = "treeNodeThree";
                    node.Expanded = true;

                }
                else
                {
                    node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;
                }

                node.NodeCloseImg = "../../Images/wenjianjia.gif";
                node.NodeOpenImg = "../../Images/wenjianjia.gif";
                result.Add(node);
                node.ShowCheckBox = true;
                string code = this.Request.QueryString["CODE"];
                if (code.Equals(item.Code))
                {
                    node.Checked = true;
                }

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

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string code = Request.Form["HiddenCode"];
            string codeName = Request.Form["HiddenName"];
            string fjId = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                DECategory root = CategoryAdapter.Instance.GetByID(code);
                fjId = root.ParentCode;
            }

            //创建传值JSON
            StringBuilder strJson = new StringBuilder();
            //选中行的编码值

            strJson.Append("{");
            strJson.Append("Code:'");
            strJson.Append(code);
            strJson.Append("',Name:'");
            strJson.Append(codeName);
            strJson.Append("',fjId:'");
            strJson.Append(fjId);
            strJson.Append("'");
            strJson.Append("}");

            WebUtility.RegisterOnLoadScriptBlock(this, string.Format("window.returnValue={0};top.close();", strJson.ToString()));

        }

    }
}