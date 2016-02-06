using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls
{
    public class DateTimePropertyEditor : PropertyEditorBase
    {
        protected internal override void OnPagePreRender(Page page)
        {
            if (page.Form != null)
            {
                HtmlGenericControl div = new HtmlGenericControl() { EnableViewState = false };

                div.Style["display"] = "none";

                DeluxeDateTime deluxeDateTimeControl = new DeluxeDateTime() { ID = "DateTimePropertyEditor_DeluxeDateTime", EnableViewState = false };

                div.Controls.Add(deluxeDateTimeControl);

                PropertyEditorHelper.EnsureContainer(page).Controls.Add(div);
            }
        }
    }
}
