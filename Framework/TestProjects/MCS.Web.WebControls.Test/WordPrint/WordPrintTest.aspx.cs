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

namespace ChinaCustoms.Framework.DeluxeWorks.Web.WebControls.Test.WordPrint
{
	public partial class WordPrintTest : System.Web.UI.Page
	{
		#region 待测试项列表
		//ctrlWordPrint.AutoCallBack;                   //设置控件是否自动CallBack              //Delete
		//ctrlWordPrint.Text;                           //控件显示的文本，默认为“打印”
		//ctrlWordPrint.Type;                           //控件类型，InputButton/ImageButton/LinkButton，默认为InputButton
		//ctrlWordPrint.CssClass;                       //控件的样式
		//ctrlWordPrint.ImageUrl;                       //如果为ImageButton，则为指定的图片路径
		//ctrlWordPrint.DataSourceList;                 //生成Word文档的数据源集合
		//ctrlWordPrint.TempleteUrl;                    //设置Word文档模板路径
		
		//ctrlWordPrint.OnBeforeDataSourceItemCreate;   //当一个项目创建之前触发的客户端事件
		//ctrlWordPrint.OnDataSourceItemCreated;        //当一个项目创建之后触发的客户端事件
		//ctrlWordPrint.OnCreateWordComplete;           //当Word文档创建完毕触发的客户端事件
		
		//ctrlWordPrint.Print;                          //打印事件

		//ctrlWordPrint.CallBackOnPrintMethod;
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				DataTable dtTrans1 = new DataTable();
				dtTrans1.Columns.Add("COLUMN1", typeof(string));
				dtTrans1.Columns.Add("COLUMN2", typeof(string));
				dtTrans1.Columns.Add("COLUMN3", typeof(string));
				ViewState.Add("dtTrans1", dtTrans1);

				DataTable dtTrans2 = new DataTable();
				dtTrans2.Columns.Add("COLUMN1", typeof(string));
				dtTrans2.Columns.Add("COLUMN2", typeof(string));
				dtTrans2.Columns.Add("COLUMN3", typeof(string));
				ViewState.Add("dtTrans2", dtTrans2);

				DataTable dtTrans3 = new DataTable();
				dtTrans3.Columns.Add("DocumentUrl1", typeof(string));
				dtTrans3.Columns.Add("DocumentUrl2", typeof(string));
				dtTrans3.Columns.Add("DocumentUrl3", typeof(string));
				ViewState.Add("dtTrans3", dtTrans3);
			}
		}

		protected override void OnInit(EventArgs e)
		{
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OnBeforeDataSourceItemCreate", @"function OnBeforeDataSourceItemCreate(){alert('Event OnBeforeDataSourceItemCreate 当一个项目创建之前触发的客户端事件');}", true);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OnDataSourceItemCreated", @"function OnDataSourceItemCreated(){alert('Event OnDataSourceItemCreated 当一个项目创建之后触发的客户端事件');}", true);
			//Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OnCreateWordComplete", @"function OnCreateWordComplete(){alert('Event OnCreateWordComplete 当Word文档创建完毕触发的客户端事件');}", true);
			//ctrlWordPrint.OnBeforeDataSourceItemCreate = "OnBeforeDataSourceItemCreate";
			//ctrlWordPrint.OnDataSourceItemCreated = "OnDataSourceItemCreated";
			//ctrlWordPrint.OnCreateWordComplete = "OnCreateWordComplete";
			base.OnInit(e);
		}

		private string BuildControlInfo()
		{
			StringBuilder strbInfo = new StringBuilder(512);
			strbInfo.Append("<cc1:WordPrint ID=\"ctrlWordPrint\" runat=\"server\"");
			if (txbText.Text != "打印")
			{
				strbInfo.Append("\n Text=\"" + ctrlWordPrint.Text + "\" ");
			}
            if (ddlType.Text != "Default")
            {
                strbInfo.Append("\n Type=\"" + ctrlWordPrint.Type.ToString() + "\" ");
            }
			if (ddlCssClass.Text != "Default")
			{
				strbInfo.Append("\n CssClass=\"" + ctrlWordPrint.CssClass + "\" ");
			}
            if (ddlImageUrl.Text != "Default")
            {
                strbInfo.Append("\n ImageUrl=\"" + ctrlWordPrint.ImageUrl + "\" ");
            }
            if (ddlTempleteUrl.Text != "Default")
            {
                strbInfo.Append("\n TempleteUrl=\"" + ctrlWordPrint.TempleteUrl + "\" ");
            }
			if (txbAccessKey.Text != "")
			{
				strbInfo.Append("\n AccessKey=\"" + ctrlWordPrint.AccessKey + "\" ");
			}
			strbInfo.Append("\n/>");
			return strbInfo.ToString();
		}

		protected void btnSetProperties_Click(object sender, EventArgs e)
		{
			ctrlWordPrint.Text = txbText.Text;
            if (ddlType.Text != "Default")
            {
                switch (ddlType.Text)
                {
                    case "InputButton":
                        ctrlWordPrint.Type = ButtonType.InputButton;
                        break;
                    case "ImageButton":
                        ctrlWordPrint.Type = ButtonType.ImageButton;
                        break;
                    case "LinkButton":
                        ctrlWordPrint.Type = ButtonType.LinkButton;
                        break;
                }
            }
			if (ddlCssClass.Text != "Default")
			{
				ctrlWordPrint.CssClass = ddlCssClass.Text;
			}
            if (ddlImageUrl.Text != "Default")
            {
                ctrlWordPrint.ImageUrl = ddlImageUrl.Text;
            }
            if (ddlTempleteUrl.Text != "Default")
            {
                ctrlWordPrint.TempleteUrl = ddlTempleteUrl.Text;
            }
			if (txbAccessKey.Text != "")
			{
				ctrlWordPrint.AccessKey = txbAccessKey.Text;
			}
			ctrlWordPrintHtmlShow.Value = BuildControlInfo();

            //复制到第二个WordPrint控件
            ctrlWordPrint2.Text = ctrlWordPrint.Text;
            ctrlWordPrint2.Type = ctrlWordPrint.Type;
            ctrlWordPrint2.CssClass = ctrlWordPrint.CssClass;
            ctrlWordPrint2.ImageUrl = ctrlWordPrint.ImageUrl;
            ctrlWordPrint2.TempleteUrl = ctrlWordPrint.TempleteUrl;
            ctrlWordPrint2.AccessKey = ctrlWordPrint.AccessKey;
		}

		protected void btnSetDefaultText_Click(object sender, EventArgs e)
		{
			DataTable dtTrans = ViewState["dtTrans1"] as DataTable;
			dtTrans.Clear();
			DataRow drTrans = dtTrans.NewRow();
			drTrans["COLUMN1"] = "文本数据源中的COLUMN1";
			drTrans["COLUMN2"] = "文本数据源中的COLUMN2";
			drTrans["COLUMN3"] = "文本数据源中的COLUMN3";
			dtTrans.Rows.Add(drTrans);
			ViewState["dtTrans1"] = dtTrans;
			txbDsTextCol1.Text = "文本数据源中的COLUMN1";
			txbDsTextCol2.Text = "文本数据源中的COLUMN2";
			txbDsTextCol3.Text = "文本数据源中的COLUMN3";
			DataTable dtTest = ViewState["dtTrans1"] as DataTable;
		}

		protected void btnSetDefaultTable_Click(object sender, EventArgs e)
		{
			DataTable dtTrans = ViewState["dtTrans2"] as DataTable;
			dtTrans.Clear();
			DataRow drTrans;
			for (int i = 1; i < 6; i++)
			{
				drTrans = dtTrans.NewRow();
				drTrans["COLUMN1"] = "表格中的COL1_" + i.ToString();
				drTrans["COLUMN2"] = "表格中的COL2_" + i.ToString();
				drTrans["COLUMN3"] = "表格中的COL3_" + i.ToString();
				dtTrans.Rows.Add(drTrans);
			}
			ViewState["dtTrans2"] = dtTrans;
			dlDsTable.DataSource = dtTrans;
			dlDsTable.DataBind();
			txbDsTableCol1.Text = string.Empty;
			txbDsTableCol2.Text = string.Empty;
			txbDsTableCol3.Text = string.Empty;
		}

		protected void btnSetDefaultFile_Click(object sender, EventArgs e)
		{
			DataTable dtTrans = ViewState["dtTrans3"] as DataTable;
			dtTrans.Clear();
			DataRow drTrans = dtTrans.NewRow();
			drTrans["DocumentUrl1"] = ".\\Doc1.doc";
			drTrans["DocumentUrl2"] = "~/wordprint/Doc2.doc";
			drTrans["DocumentUrl3"] = "http://10.1.1.96/wordprint/Doc3.doc";
			dtTrans.Rows.Add(drTrans);
			ViewState["dtTrans3"] = dtTrans;
			txbDsFileCol1.Text = ".\\Doc1.doc";
			txbDsFileCol2.Text = "~/wordprint/Doc2.doc";
			txbDsFileCol3.Text = "http://10.1.1.96/wordprint/Doc3.doc";
		}

		protected void btnSetDsText_Click(object sender, EventArgs e)
		{
			DataTable dtTrans = ViewState["dtTrans1"] as DataTable;
			dtTrans.Clear();
			DataRow drTrans = dtTrans.NewRow();
			drTrans["COLUMN1"] = txbDsTextCol1.Text;
			drTrans["COLUMN2"] = txbDsTextCol2.Text;
			drTrans["COLUMN3"] = txbDsTextCol3.Text;
			dtTrans.Rows.Add(drTrans);
			ViewState["dtTrans1"] = dtTrans;
		}

		protected void btnSetDsTable_Click(object sender, EventArgs e)
		{
			DataTable dtTrans = ViewState["dtTrans2"] as DataTable;
			DataRow drTrans = dtTrans.NewRow();
			drTrans["COLUMN1"] = txbDsTableCol1.Text;
			drTrans["COLUMN2"] = txbDsTableCol2.Text;
			drTrans["COLUMN3"] = txbDsTableCol3.Text;
			dtTrans.Rows.Add(drTrans);
			ViewState["dtTrans2"] = dtTrans;
			dlDsTable.DataSource = dtTrans;
			dlDsTable.DataBind();
		}

		protected void btnSetDsFile_Click(object sender, EventArgs e)
		{
			DataTable dtTrans = ViewState["dtTrans3"] as DataTable;
			dtTrans.Clear();
			DataRow drTrans = dtTrans.NewRow();
			drTrans["DocumentUrl1"] = txbDsFileCol1.Text;
			drTrans["DocumentUrl2"] = txbDsFileCol2.Text;
			drTrans["DocumentUrl3"] = txbDsFileCol3.Text;
			dtTrans.Rows.Add(drTrans);
			ViewState["dtTrans3"] = dtTrans;
		}

		protected void ctrlWordPrint_Print(WordPrintDataSourceCollection DataSourceList)
		{
			DataTable dtTrans1 = ViewState["dtTrans1"] as DataTable;
			WordPrintDataSource dsWordPrint1 = new WordPrintDataSource("dsWordPrint1", (IEnumerable)dtTrans1.DefaultView);
			DataTable dtTrans2 = ViewState["dtTrans2"] as DataTable;
			WordPrintDataSource dsWordPrint2 = new WordPrintDataSource("dsWordPrint2", (IEnumerable)dtTrans2.DefaultView);
			DataTable dtTrans3 = ViewState["dtTrans3"] as DataTable;
			WordPrintDataSource dsWordPrint3 = new WordPrintDataSource("dsWordPrint3", (IEnumerable)dtTrans3.DefaultView);
			DataSourceList.Add(dsWordPrint1);
			DataSourceList.Add(dsWordPrint2);
			DataSourceList.Add(dsWordPrint3);
		}

        protected void ctrlWordPrint2_Print(WordPrintDataSourceCollection DataSourceList)
        {
            DataTable dtTrans1 = ViewState["dtTrans1"] as DataTable;
            WordPrintDataSource dsWordPrint1 = new WordPrintDataSource("dsWordPrint1", (IEnumerable)dtTrans1.DefaultView);
            DataTable dtTrans2 = ViewState["dtTrans2"] as DataTable;
            WordPrintDataSource dsWordPrint2 = new WordPrintDataSource("dsWordPrint2", (IEnumerable)dtTrans2.DefaultView);
            DataTable dtTrans3 = ViewState["dtTrans3"] as DataTable;
            WordPrintDataSource dsWordPrint3 = new WordPrintDataSource("dsWordPrint3", (IEnumerable)dtTrans3.DefaultView);
            DataSourceList.Add(dsWordPrint1);
            DataSourceList.Add(dsWordPrint2);
            DataSourceList.Add(dsWordPrint3);
        }
	}
}