using MCS.Web.MVC.Library.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MCS.Web.MVC.Library.Test.Controllers
{
    public class WebAPIController : ApiController
    {
        [HttpGet]
        [ApiExceptionFilter]
        public string ActionWithException()
        {
            throw new ApplicationException("This is a exception");
            return "Hello World";
        }
    }
}
