using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.Web.WebControls.Test.DeluxeCalendar
{
    public partial class DeluxeCalendarTest2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendar.Value = DateTime.Now.AddYears(-1);
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            DateTime d = calendar.Value;
        }
    }
}