using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Dynamics.Web.WebControls;

namespace MCS.Dynamics.Web.inc
{
    /// <summary>
    /// ajaxResult 的摘要说明
    /// </summary>
    public class ajaxResult : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string ajaxAction = context.Request.Form["ajaxAciont"];
            switch (ajaxAction)
            {
                case "getRoot":
                    GetRoot(context);
                    break;
            }
        }
        /// <summary>
        /// 获取当前目录子节点
        /// </summary>
        private void GetRoot(HttpContext context)
        {
            string json = "[{\"rootName\":\"销售板块\",\"category\":\"0\",\"categoryName\":\"\\\\销售板块\\\\销售板块\"},{\"rootName\":\"渠道板块\",\"category\":\"1\",\"categoryName\":\"\\\\销售板块\\\\销售板块\"}]"; // 子节点json字符串

            context.Response.Write(json);
            BannerNotice n = new BannerNotice();
            Type t = Type.GetType(n.GetType().ToString(), true);
            object c = Activator.CreateInstance(t);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}