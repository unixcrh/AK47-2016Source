using MCS.Library.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace MCS.Web.MVC.Library.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public ApiExceptionFilterAttribute()
        {
            this.Enabled = true;
        }

        public ApiExceptionFilterAttribute(bool enabled)
        {
            this.Enabled = enabled;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (this.Enabled)
            {
                Exception exception = actionExecutedContext.Exception.GetRealException();

                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { number = -1, description = exception.Message, stackTrace = exception.StackTrace })),
                };
            };
        }
    }
}
