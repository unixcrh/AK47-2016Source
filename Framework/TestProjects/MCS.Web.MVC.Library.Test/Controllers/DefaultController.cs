using MCS.Web.MVC.Library.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCS.Web.MVC.Library.Test.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [PassportAuthorize]
        public ActionResult Index()
        {
            return View("DefaultView");
        }
    }
}