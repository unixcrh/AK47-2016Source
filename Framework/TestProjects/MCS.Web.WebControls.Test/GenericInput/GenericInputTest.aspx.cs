using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ChinaCustoms.Framework.DeluxeWorks.Web.WebControls.Test.GenericInput
{
    public partial class GenericInputTest : System.Web.UI.Page
    {
        #region 待测试属性
        //ctrlGenericInput.AccessKey;                   New
        //ctrlGenericInput.IsReadOnly;                  Delete      //控件是否只读
        //ctrlGenericInput.AutoPostBack;                            //设置控件是否自动PostBack,如果为True则在Text改变的时候和选择项目的时候PostBack，默认为false

        //ctrlGenericInput.BorderWidth;                 New         //边框宽度
        //ctrlGenericInput.BorderColor;                 New         //边框颜色，默认值为　#2353B2
        //ctrlGenericInput.BorderStyle;                 New         //边框样式
        //ctrlGenericInput.HighlightBorderColor;                    //控件边框的颜色，默认值为　#2353B2
        //ctrlGenericInput.ItemFontColor;               Delete
        //ctrlGenericInput.ItemHoverFontColor;          Delete
        //ctrlGenericInput.ItemHoverBackgroundColor;    Delete
        //ctrlGenericInput.DropArrowBackgroundColor;                //下拉箭头区域的背景色，默认值为　#C6E1FF

        //ctrlGenericInput.Text;                                    //控件的当前文本
        //ctrlGenericInput.ClientID;                    Delete

        //ctrlGenericInput.Items;                                   //控件中的选择项目集合

        //ctrlGenericInput.CssClass;                    New         //控件样式
        //ctrlGenericInput.Font;                        New         //控件字体
        //ctrlGenericInput.Height;                      New         //控件高度
        //ctrlGenericInput.ItemCssClass;                New         //选择项目默认样式
        //ctrlGenericInput.ItemHoverCssClass;           New         //鼠标移动到选项项目上时的样式

        //ctrlGenericInput.OnSelectItem;                            //在选择一个项目并改变当前控件值之前触发
        //ctrlGenericInput.OnSelectedItem;                          //在选择一个项目并改变当前控件值之后触发
        //ctrlGenericInput.OnChange;                                //在手工输入文本后触发
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private string BuildControlInfo()
        {
            StringBuilder strbInfo = new StringBuilder(512);
            strbInfo.Append("<cc1:genericinput id=\"ctrlGenericInput\"\n runat=\"server\" Width=\"200px\" ");
            if (ckbAutoPostBack.Checked)
            {
                strbInfo.Append("\n AutoPostBack=\"" + ctrlGenericInput.AutoPostBack.ToString() + "\" ");
            }
            if (ddlHighlightBorderColor.Text != "Default")
            {
                strbInfo.Append("\n HighlightBorderColor=\"" + GetColorString(ctrlGenericInput.HighlightBorderColor.ToString()) + "\" ");
            }
            if (ddlDropArrowBackgroundColor.Text != "Default")
            {
                strbInfo.Append("\n DropArrowBackgroundColor=\"" + GetColorString(ctrlGenericInput.DropArrowBackgroundColor.ToString()) + "\" ");
            }
            if (txbHeight.Text != string.Empty)
            {
                strbInfo.Append("\n Height=\"" + ctrlGenericInput.Height.ToString() + "\" ");
            }
            if (ddlCssClass.Text != "Default")
            {
                strbInfo.Append("\n CssClass=\"" + ctrlGenericInput.CssClass.ToString() + "\" ");
            }
            if (ddlItemCssClass.Text != "Default")
            {
                strbInfo.Append("\n ItemCssClass=\"" + ctrlGenericInput.ItemCssClass.ToString() + "\" ");
            }
            if (ddlItemHoverCssClass.Text != "Default")
            {
                strbInfo.Append("\n ItemHoverCssClass=\"" + ctrlGenericInput.ItemHoverCssClass.ToString() + "\" ");
            }
            if (txbAccessKey.Text != string.Empty)
            {
                strbInfo.Append("\n AccessKey=\"" + ctrlGenericInput.AccessKey.ToString() + "\" ");
            }
            strbInfo.Append("/>\n");
            if (ctrlGenericInput.Attributes["onSelectItem"] != null)
            {
                strbInfo.Append("\n\nctrlGenericInput.Attributes[\"onSelectItem\"] = " + ctrlGenericInput.Attributes["onSelectItem"].ToString());
            }
            if (ctrlGenericInput.Attributes["onSelectedItem"] != null)
            {
                strbInfo.Append("\n\nctrlGenericInput.Attributes[\"onSelectedItem\"] = " + ctrlGenericInput.Attributes["onSelectedItem"].ToString());
            }
            if (ctrlGenericInput.Attributes["onChange"] != null)
            {
                strbInfo.Append("\n\nctrlGenericInput.Attributes[\"onChange\"] = " + ctrlGenericInput.Attributes["onChange"].ToString());
            }
            return strbInfo.ToString();
        }

        protected void btnSetProperties_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.AutoPostBack = ckbAutoPostBack.Checked;
            if (ddlHighlightBorderColor.Text == "Default")
            {
                ctrlGenericInput.HighlightBorderColor = System.Drawing.ColorTranslator.FromHtml("#2353B2");
            }
            else
            {
                ctrlGenericInput.HighlightBorderColor = System.Drawing.ColorTranslator.FromHtml(ddlHighlightBorderColor.Text);
            }
            if (ddlDropArrowBackgroundColor.Text == "Default")
            {
                ctrlGenericInput.DropArrowBackgroundColor = System.Drawing.ColorTranslator.FromHtml("#C6E1FF");
            }
            else
            {
                ctrlGenericInput.DropArrowBackgroundColor = System.Drawing.ColorTranslator.FromHtml(ddlDropArrowBackgroundColor.Text);
            }
            if (txbHeight.Text != string.Empty)
            {
                ctrlGenericInput.Height = Unit.Pixel(Convert.ToInt32(txbHeight.Text));
            }
            if (ddlCssClass.Text != "Default")
            {
                ctrlGenericInput.CssClass = ddlCssClass.Text;
            }
            if (ddlItemCssClass.Text != "Default")
            {
                ctrlGenericInput.ItemCssClass = ddlItemCssClass.Text;
            }
            if (ddlItemHoverCssClass.Text != "Default")
            {
                ctrlGenericInput.ItemHoverCssClass = ddlItemHoverCssClass.Text;
            }
            if (txbAccessKey.Text != string.Empty)
            {
                ctrlGenericInput.AccessKey = txbAccessKey.Text;
            }
            ctrlGenericInput.Font.Bold = ckbBold.Checked;
            ctrlGenericInput.Font.Italic = ckbItalic.Checked;
            ctrlGenericInput.Font.Underline = ckbUnderline.Checked;
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();

            //复制到第二个GenericInput控件
            ctrlGenericInput2.AutoPostBack = ctrlGenericInput.AutoPostBack;
            ctrlGenericInput2.HighlightBorderColor = ctrlGenericInput.HighlightBorderColor;
            ctrlGenericInput2.DropArrowBackgroundColor = ctrlGenericInput.DropArrowBackgroundColor;
            ctrlGenericInput2.BorderWidth = ctrlGenericInput.BorderWidth;
            ctrlGenericInput2.BorderColor = ctrlGenericInput.BorderColor;
            ctrlGenericInput2.BorderStyle = ctrlGenericInput.BorderStyle;
            ctrlGenericInput2.Height = ctrlGenericInput.Height;
            ctrlGenericInput2.CssClass = ctrlGenericInput.CssClass;
            ctrlGenericInput2.ItemCssClass = ctrlGenericInput.ItemCssClass;
            ctrlGenericInput2.ItemHoverCssClass = ctrlGenericInput.ItemHoverCssClass;
            ctrlGenericInput2.AccessKey = ctrlGenericInput.AccessKey;
        }

        private string GetColorString(string colorString)
        {
            int i = colorString.IndexOf("[");
            int j = colorString.IndexOf("]");
            return colorString.Substring(7, j - i - 1);
        }

        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            if (ctrlGenericInput.SelectedIndex == -1)
            {
                txtaGetValue.Value = "ctrlGenericInput.Text=\"" + ctrlGenericInput.Text
                    + "\"\nctrlGenericInput.SelectedIndex= " + ctrlGenericInput.SelectedIndex.ToString() + "\"";
            }
            else
            {
                txtaGetValue.Value = "ctrlGenericInput.Text=\"" + ctrlGenericInput.Text
                    + "\"\nctrlGenericInput.SelectedItem.Text=\"" + ctrlGenericInput.SelectedItem.Text + "\"";
            }
        }

        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Text = txbSetValue.Text;
        }

        protected void ctrlGenericInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbSelectedIndex.Text = ctrlGenericInput.SelectedIndex.ToString();
            txbSelectedText.Text = ctrlGenericInput.SelectedItem.Text; ;
            txbSelectedValue.Text = ctrlGenericInput.SelectedItem.Value;
        }

        protected void btnSetOnSelectItem_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Attributes.Add("onSelectItem", txbOnSelectItem.Text);
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();
        }

        protected void btnClearOnSelectItem_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Attributes.Remove("onSelectItem");
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();
        }

        protected void btnSetOnSelectedItem_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Attributes.Add("onSelectedItem", txbOnSelectedItem.Text);
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();
        }

        protected void btnClearOnSelectedItem_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Attributes.Remove("onSelectedItem");
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();
        }

        protected void btnSetOnChange_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Attributes.Add("onChange", txbOnChange.Text);
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();
        }

        protected void btnClearOnChange_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Attributes.Remove("onChange");
            ctrlGenericInputHtmlShow.Value = BuildControlInfo();
        }

        protected void ctrlGenericInput_TextChanged(object sender, EventArgs e)
        {
            txbTextInput.Text = ctrlGenericInput.Text;
        }

        protected void btnSetListitems_Click(object sender, EventArgs e)
        {
            ListItem litmTrans = new ListItem();
            litmTrans.Text = txbLitmText.Text;
            litmTrans.Value = txbLitmValue.Text;
            ctrlGenericInput.Items.Add(litmTrans);

            //复制到第二个GenericInput控件
            ctrlGenericInput2.Items.Add(litmTrans);
        }

        protected void btnUseDefault_Click(object sender, EventArgs e)
        {
            ctrlGenericInput.Items.Clear();            
            for (int i = 0; i < 8; i++)
            {
                ListItem litmTrans = new ListItem();
                litmTrans.Text = "Text_" + i.ToString();
                litmTrans.Value = "Value_" + i.ToString();
                ctrlGenericInput.Items.Add(litmTrans);

                //复制到第二个GenericInput控件
                ctrlGenericInput2.Items.Add(litmTrans);
            }
        }
    }
}
