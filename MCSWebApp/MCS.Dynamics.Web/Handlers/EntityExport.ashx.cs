using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using MCS.Dynamics.Web.Validate;
using MCS.Library.Core;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.JScript;

namespace MCS.Dynamics.Web.Handlers
{
    /// <summary>
    /// EntityExport 的摘要说明
    /// </summary>
    public class EntityExport : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            if (req.IsAuthenticated)
            {
                var entityIDs = req.QueryString["id"].Split('|').Where(p => p.IsNotEmpty()).ToList();
                DynamicEntityCollection collection = new DynamicEntityCollection();
                try
                {
                    entityIDs.ForEach(id => collection.Add(DESchemaObjectAdapter.Instance.Load(id, DateTime.Now.SimulateTime()) as DynamicEntity));
                    //验证导出数据的完整性
                    string validResult = CheckEntityChildren.CheckSelectEntities(collection.Select(p => p.ID).ToArray());
                    validResult.IsNotEmpty().TrueThrow(validResult);

                    string fileName = "DynamicEntity" + "_" + DateTime.Now.SimulateTime().ToString("yyyyMMdd_HHmmss") + ".xml";
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + MCS.Web.Library.ResponseExtensions.EncodeFileNameInContentDisposition(context.Response, fileName) + "\"");

                    XmlWriter writer = XmlWriter.Create(context.Response.Output);
                    writer.WriteStartDocument();

                    collection.ToXElement().WriteTo(writer);
                    writer.WriteEndDocument();

                    writer.Close();
                }
                catch (Exception ex)
                {
                    var exception = ex.GetRealException();
                    throw new HttpException("导出实体出错!\r\n" + exception.Message, ex.InnerException);
                }
            }
            else
            {
                throw new HttpException("请求的方式错误");
            }
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