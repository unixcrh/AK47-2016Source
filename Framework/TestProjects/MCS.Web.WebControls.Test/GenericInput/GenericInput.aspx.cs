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

public partial class GenericInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListItem itm;
        for (int i = 0; i < 10; i++)
        {
            itm = new ListItem("测试数据_" + i.ToString(), i.ToString());
            GenericInput1.Items.Add(itm);
        }
    }
    protected void chkReadOnly_CheckedChanged(object sender, EventArgs e)
    {
        //GenericInput1.IsReadOnly = chkReadOnly.Checked;
    }
    protected void chkAutoPostBack_CheckedChanged(object sender, EventArgs e)
    {
        GenericInput1.AutoPostBack = chkAutoPostBack.Checked;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        GenericInput1.Items.Clear();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ListItem itm = new ListItem();
        itm.Value = txtItemValue.Text;
        itm.Text = txtItemText.Text;
        itm.Selected = chkSelected.Checked;
        GenericInput1.Items.Add(itm);
    }
    protected void btnSetBorderColor_Click(object sender, EventArgs e)
    {
        GenericInput1.HighlightBorderColor = System.Drawing.ColorTranslator.FromHtml(txtBorderColor.Text);
    }
    protected void btnSetFontColor_Click(object sender, EventArgs e)
    {
       // GenericInput1.ItemFontColor = System.Drawing.ColorTranslator.FromHtml(txtFontColor.Text);
    }
    protected void btnSetBgColor_Click(object sender, EventArgs e)
    {
        GenericInput1.DropArrowBackgroundColor = System.Drawing.ColorTranslator.FromHtml(txtBgColor.Text);
    }
    protected void btnItemSelectScript_Click(object sender, EventArgs e)
    {
        GenericInput1.Attributes.Add("onSelectItem",txtItemSelectScript.Text);
    }
    protected void btnItemSelectedScript_Click(object sender, EventArgs e)
    {
        GenericInput1.Attributes.Add("onSelectedItem",txtItemSelectedScript.Text);
    }
    protected void btnOnTextChange_Click(object sender, EventArgs e)
    {
        GenericInput1.Attributes.Add("onChange", txtOnTextChange.Text);
    }
    protected void btnSelectItemFontColor_Click(object sender, EventArgs e)
    {
       // GenericInput1.ItemHoverFontColor = System.Drawing.ColorTranslator.FromHtml(txtSelectItemFontColor.Text);
    }
    protected void btnHoveBGColor_Click(object sender, EventArgs e)
    {
        //GenericInput1.ItemHoverBackgroundColor = System.Drawing.ColorTranslator.FromHtml(txtHoveBGColor.Text);
    }
    protected void GenericInput1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "当前选择的Index为：" + GenericInput1.SelectedIndex.ToString();

        lblMsg.Text += "  当前选择的Text为：" + GenericInput1.SelectedItem.Text;

        lblMsg.Text += "  当前选择的Value为：" + GenericInput1.SelectedItem.Value;
    }
    protected void GenericInput1_TextChanged(object sender, EventArgs e)
    {
        if(GenericInput1.Text!="")
            lblMsg.Text += "     当前控件中的文本为：" + GenericInput1.Text;
    }
    protected void btnSetInputBgColor_Click(object sender, EventArgs e)
    {
        GenericInput1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtInputBgColor.Text);
    }
    protected void btnSetForeColor_Click(object sender, EventArgs e)
    {
        GenericInput1.ForeColor = System.Drawing.ColorTranslator.FromHtml(txtForeColor.Text);
    }
    protected void btnDataBind_Click(object sender, EventArgs e)
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("ID", typeof(string));
        Dt.Columns.Add("Text", typeof(string));

        DataRow newRow;

        for (int i = 0; i < 40; i++)
        {
            newRow = Dt.NewRow();
            newRow["ID"] = i.ToString();
            newRow["Text"] = "我就是：" + i.ToString();
            Dt.Rows.Add(newRow);
        }



        GenericInput1.DataSource = Dt;
        GenericInput1.DataTextField = "Text";
        GenericInput1.DataValueField = "ID";
        GenericInput1.DataBind();
    }
}
