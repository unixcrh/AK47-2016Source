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

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListItem itm;
        for (int i = 0; i < 40; i++)
        {
            itm = new ListItem("测试数据_" + i.ToString(), i.ToString());
            GenericInputExtender1.Items.Add(itm);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ListItem itm = new ListItem();
        itm.Value = txtItemValue.Text;
        itm.Text = txtItemText.Text;
        itm.Selected = chkSelected.Checked;
        GenericInputExtender1.Items.Add(itm);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        GenericInputExtender1.Items.Clear();
    }
    protected void btnSetBorderColor_Click(object sender, EventArgs e)
    {
        GenericInputExtender1.HighlightBorderColor = System.Drawing.ColorTranslator.FromHtml(txtBorderColor.Text);
    }
    protected void btnSetFontColor_Click(object sender, EventArgs e)
    {
        //GenericInputExtender1.ItemFontColor = System.Drawing.ColorTranslator.FromHtml(txtFontColor.Text);
    }
    protected void btnSelectItemFontColor_Click(object sender, EventArgs e)
    {
       // GenericInputExtender1.ItemHoverFontColor = System.Drawing.ColorTranslator.FromHtml(txtSelectItemFontColor.Text);
    }
    protected void btnHoveBGColor_Click(object sender, EventArgs e)
    {
       // GenericInputExtender1.ItemHoverBackgroundColor = System.Drawing.ColorTranslator.FromHtml(txtHoveBGColor.Text);
    }
    protected void btnSetBgColor_Click(object sender, EventArgs e)
    {
        //GenericInputExtender1.ItemHoverBackgroundColor = System.Drawing.ColorTranslator.FromHtml(txtHoveBGColor.Text);
    }
}
