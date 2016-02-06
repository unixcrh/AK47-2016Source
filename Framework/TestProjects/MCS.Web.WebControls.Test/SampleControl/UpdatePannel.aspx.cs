using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls.Test.SampleControl
{
    public partial class UpdatePannel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			Response.Write((new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(System.DateTime.Now));
        }

        public int SetSampleObject(MCS.Web.WebControls.SampleObject[] o)
        {
            return o.Length;
        }

    }
}
