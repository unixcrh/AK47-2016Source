using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Dynamics.Web.Validate;

namespace MCS.Dynamics.Web.Ajax
{
    /// <summary>
    /// CheckCopyEntity 的摘要说明
    /// </summary>
    public class CheckCopyEntity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["CopyEntities"]))
            {
                string[] ids = context.Request["CopyEntities"].Trim().Split(',');
                if (context.Request["Move"] == "true")
                {
                    result = CheckEntityChildren.CheckSelectMoveEntities(ids);
                }
                else
                {
                    result = CheckEntityChildren.CheckSelectEntities(ids);
                }
               
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
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