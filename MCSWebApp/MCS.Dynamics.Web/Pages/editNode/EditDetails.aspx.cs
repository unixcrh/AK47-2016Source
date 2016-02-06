using System;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Web.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using System.Text;
using MCS.Web.Library;
using MCS.Library.Data.Builder;

namespace MCS.Dynamics.Web.Pages.editNode
{
    public partial class EditDetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DECategory root = CategoryAdapter.Instance.GetRoot();
                DeluxeTreeNode node = new DeluxeTreeNode(root.DisplayName, root.Code);
                node.NodeCloseImg = "../../Images/wenjianjia.gif";
                node.NodeOpenImg = "../../Images/wenjianjia.gif";
                node.CssClass = "treenodeParent";
                tree.Nodes.Add(node);
                node.Expanded = true;
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void del_Click(object sender, EventArgs e)
        {
            string code = string.Empty;
            if (!string.IsNullOrEmpty(HiddenCode.Value))
            {
                code = HiddenCode.Value;
                try
                {
                    DECategory root = CategoryAdapter.Instance.GetByID(code);
                    string dt = root.VersionStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    root.VersionEndTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    //更新条件
                    WhereSqlClauseBuilder sqlWhere = new WhereSqlClauseBuilder();
                    sqlWhere.AppendItem("Code", root.Code);
                    sqlWhere.AppendItem("VersionStartTime", dt);
                    //更新
                    CategoryAdapter.Instance.UpdateData(root, root.VersionEndTime, sqlWhere);
                    Response.Write("<script language=javascript>window.alert('删除成功！'); window.location.href = window.location.href;</script>");

                }
                catch (Exception ex)
                {
                    WebUtility.ShowClientError(ex.Message, ex.StackTrace, "错误");

                }

            }
        }

    }
}