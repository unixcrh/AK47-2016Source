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
using MCS.Library.Core;
using MCS.Web.Library;

[assembly: WebResource("MCS.Web.WebControls.Test.Configuration.mail.gif", "image/gif")]
namespace MCS.Web.WebControls.Test.Configuration
{
	public partial class ContentTypesSectionTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			tableContainer.Controls.Add(CreateContentTypesTable());

			ContentTypeConfigElement elem = 
				ContentTypesSection.GetConfig().ContentTypes.FindElementByFileName("abc.ra");

			HtmlTable table = (HtmlTable)CreateContentTypesTableHead();

			CreateOneElementRow(elem, table);
			tableContainer.Controls.Add(table);

			table = (HtmlTable)CreateContentTypesTableHead();

			CreateOneElementRow(ContentTypesSection.GetConfig().DefaultElement, table);
			tableContainer.Controls.Add(table);
		}

		private Control CreateContentTypesTableHead()
		{
			HtmlTable table = new HtmlTable();
			table.Border = 1;
			HtmlTableRow row = new HtmlTableRow();
			table.Controls.Add(row);

			HtmlTableCell cellName = new HtmlTableCell();
			cellName.InnerText = "Key";
			row.Controls.Add(cellName);

			HtmlTableCell cellDesp = new HtmlTableCell();
			cellDesp.InnerText = "ContentType";
			row.Controls.Add(cellDesp);

			HtmlTableCell cellImg = new HtmlTableCell();
			cellImg.InnerText = "Logo";
			row.Controls.Add(cellImg);

			HtmlTableCell cellOpenMode = new HtmlTableCell();
			cellOpenMode.InnerText = "Open Mode";
			row.Controls.Add(cellOpenMode);

			return table;
		}

		private Control CreateContentTypesTable()
		{
			HtmlTable table = (HtmlTable)CreateContentTypesTableHead();

			foreach (ContentTypeConfigElement elem in ContentTypesSection.GetConfig().ContentTypes)
				CreateOneElementRow(elem, table);

			return table;
		}

		private void CreateOneElementRow(ContentTypeConfigElement elem, Control parent)
		{
			HtmlTableRow row = new HtmlTableRow();
			parent.Controls.Add(row);

			HtmlTableCell cellName = new HtmlTableCell();
			cellName.InnerText = elem.Key;
			row.Controls.Add(cellName);

			HtmlTableCell cellDesp = new HtmlTableCell();
			cellDesp.InnerText = elem.ContentType;
			row.Controls.Add(cellDesp);

			HtmlTableCell cellImg = new HtmlTableCell();
			HtmlImage img = new HtmlImage();
			img.Src = elem.LogoImage;

			cellImg.Controls.Add(img);
			row.Controls.Add(cellImg);

			HtmlTableCell cellOpenMode = new HtmlTableCell();
			cellOpenMode.InnerText = elem.OpenMode.ToString();
			row.Controls.Add(cellOpenMode);
		}
	}
}
