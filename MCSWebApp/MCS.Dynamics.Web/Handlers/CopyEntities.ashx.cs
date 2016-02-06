using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Dynamics.Web.Validate;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using System.Transactions;
using MCS.Library.Data;

namespace MCS.Dynamics.Web.Handlers
{
    /// <summary>
    /// CopyEntities 的摘要说明
    /// </summary>
    public class CopyEntities : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(context.Request["CopyEntities"]))
            {
                result = "请勾选实体！";
            }
            if (string.IsNullOrEmpty(context.Request["Categories"]))
            {
                result += "请选择目标类别！";
            }
            if (string.IsNullOrEmpty(result))
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
               
                string[] categories = context.Request["Categories"].Trim().Split(',');
                if (string.IsNullOrEmpty(result))
                {
                    try
                    {
                        
                        if (context.Request["Move"] == "true")
                        {
                            DEObjectOperations.InstanceWithPermissions.MoveEntities(ids.ToList(), categories.ToList());
                        }
                        else
                        {
                            DEObjectOperations.InstanceWithPermissions.CopyEntities(ids.ToList(), categories.ToList());
                        }
                    }
                    catch (Exception e)
                    {
                        result = e.Message;
                    }
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