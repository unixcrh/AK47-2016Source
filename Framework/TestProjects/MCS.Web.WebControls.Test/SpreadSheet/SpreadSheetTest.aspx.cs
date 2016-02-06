using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCS.Library.Office.SpreadSheet;
using System.Xml;
using MCS.Web.Library;

namespace MCS.Web.WebControls.Test.SpreadSheet
{
	public partial class SpreadSheetTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void exportDataSetBtn_Click(object sender, EventArgs e)
		{
			DataTable table = CreateDemoDataTable();

			DataViewExportOptions options = new DataViewExportOptions() { ExportColumnHeader = false };

			WorkbookNode workbook = table.DefaultView.ExportToSpreadSheet("Default", options);

			Response.AppendHeader("CONTENT-DISPOSITION",
						string.Format("{0};filename={1}", "inline", HttpUtility.UrlEncode("test.xml")));

			Response.ContentType = "text/xml";
			Response.Clear();
			workbook.Save(Response.OutputStream);
			Response.End();
		}

		protected void exportDataSetByTemplateBtn_Click(object sender, EventArgs e)
		{
			XmlDocument xmlDoc = WebXmlDocumentCache.GetXmlDocument("Interco upload template.xml");

			xmlDoc.PreserveWhitespace = true;

			WorkbookNode workbook = new WorkbookNode();

			workbook.LoadXml(xmlDoc.OuterXml);

			DataTable table = CreateDemoDataTable();

			DataViewExportOptions options = new DataViewExportOptions() { ExportColumnHeader = false };

			table.DefaultView.FillIntoSpreadSheet(workbook, "Sheet1", options);

			Response.AppendHeader("CONTENT-DISPOSITION",
						string.Format("{0};filename={1}", "inline", HttpUtility.UrlEncode("test.xml")));

			Response.ContentType = "text/xml";
			Response.Clear();
			workbook.Save(Response.OutputStream);
			Response.End();
		}

		private static DataTable CreateDemoDataTable()
		{
			DataTable table = new DataTable();

			table.Columns.Add("Seq", typeof(int));
			table.Columns.Add("User Name", typeof(string));
			table.Columns.Add("Birthday", typeof(DateTime));

			Random rand = new Random((int)DateTime.Now.Ticks);

			for (int i = 0; i < 10; i++)
			{
				DataRow row = table.NewRow();

				row["Seq"] = i;
				row["User Name"] = string.Format("User Name: {0:0000}", i);
				row["Birthday"] = new DateTime(1972, 4, 26).AddDays(rand.Next(6000) - 3000);

				table.Rows.Add(row);
			}

			return table;
		}
	}
}
