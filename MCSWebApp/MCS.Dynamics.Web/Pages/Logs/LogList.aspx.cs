using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.WebControls;


namespace MCS.Dynamics.Web.Pages
{
    public partial class LogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //var settings = LogCategoryConfigSection.GetConfig();

            //if (settings == null)
            //    throw new System.Configuration.ConfigurationErrorsException("未找到日志类别配置");

            var nodeGenerl = new DeluxeTreeNode("用户操作", " ")
            {
                Expanded = true
            };

            var nodeAd = new DeluxeTreeNode("实体操作", "DynamicEntity")
            {
                Expanded = true,
                //NodeCloseImg = "../images/ad.png",
                //NodeOpenImg = "../images/ad.png"
            };

            var nodeAdReverseUp = new DeluxeTreeNode("实体字段操作", "DynamicEntityField");

            var nodeAdReverseOutUp = new DeluxeTreeNode("外部实体操作", "OuterEntity");

            var nodeAdReverseOutFiled = new DeluxeTreeNode("外部实体字段操作", "OuterEntityField");

            nodeGenerl.Nodes.Add(nodeAd);
            nodeGenerl.Nodes.Add(nodeAdReverseUp);
            nodeGenerl.Nodes.Add(nodeAdReverseOutUp);
            nodeGenerl.Nodes.Add(nodeAdReverseOutFiled);


            this.tree.Nodes.Add(nodeGenerl);
         
        }
    }
}