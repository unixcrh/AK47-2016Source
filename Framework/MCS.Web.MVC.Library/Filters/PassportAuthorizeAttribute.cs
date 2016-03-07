using MCS.Library.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MCS.Web.MVC.Library.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PassportAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public PassportAuthorizeAttribute()
        {
            this.Enabled = true;
        }

        public PassportAuthorizeAttribute(bool enabled)
        {
            this.Enabled = enabled;
        }

        public bool Enabled
        {
            get;
            set;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = !this.Enabled;

            if (result == false)
            {
                IPrincipal principal = DeluxePrincipal.CreateByRequest();

                result = principal != null;
            }

            return result;
        }
    }
}
