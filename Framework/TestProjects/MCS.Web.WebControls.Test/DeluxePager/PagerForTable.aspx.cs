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
using System.Drawing;
using System.Web.Caching;

namespace MCS.Web.WebControls.Test.DeluxePager
{
    public partial class PagerForTable1 : System.Web.UI.Page
    {
        PagerPropertiesCls ppc = new PagerPropertiesCls();
        protected override void OnPreInit(EventArgs e)
        {
            if (PreviousPage != null)
            {
                if (PreviousPage.IsCrossPagePostBack == true)
                { 
                    string pagerObj = (PreviousPage.FindControl("hidPagerObject") as HtmlInputHidden).Value;
                    Session["pagerClss"] = pagerObj;
                    Tools.GetDeluxePager(DeluxePager1, pagerObj, ref ppc); 
                }
            }
            if (Session["pagerClss"] != null)
            {
                Tools.GetDeluxePager(DeluxePager1, Session["pagerClss"].ToString(), ref ppc);
            }
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 30;
            if (!IsPostBack)
            { 
                this.InitializePage(DeluxePager1.PageSize, DeluxePager1.PageIndex);
            }
        }

        private void InitializePage(int pageSize, int pageIndex)
        {
            DataSet ds = ObjData.GetPagerList(pageSize, pageIndex);

            int recordCount = ObjData.GetOrdersCount();

            DeluxePager1.RecordCount = recordCount;

            int numrows;
            int numcells;
            int i = 0;
            int j = 0;
            int row = 0;
            int col = 0;

            TableRow r;
            TableCell c;

            //产生表格
            numrows = pageSize;
            numcells = 5;
            for (i = 0; i < numrows; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                r = new TableRow();
                if (row / 2 != 0)
                {
                    r.BorderColor = Color.Red;
                }
                row += 1;
                col = 1;
                for (j = 0; j < numcells; j++)
                {

                    c = new TableCell();
                    c.Controls.Add(new LiteralControl(dr[col].ToString()));
                    r.Cells.Add(c);
                    col++;
                }
                Table1.Rows.Add(r);
            }

            r = new TableRow();
            c = new TableCell();
            c.Controls.Add(new LiteralControl("PRIORITY"));
            TableCell c1 = new TableCell();
            c1.Controls.Add(new LiteralControl("集团"));
            TableCell c2 = new TableCell();
            c2.Controls.Add(new LiteralControl("用户级别"));
            TableCell c3 = new TableCell();
            c3.Controls.Add(new LiteralControl("创建用户"));
            TableCell c4 = new TableCell();
            c4.Controls.Add(new LiteralControl("创建时间"));

            c.Width = Unit.Pixel(10);
            c1.Width = Unit.Pixel(60);
            c2.Width = Unit.Pixel(120);
            c3.Width = Unit.Pixel(120);
            c4.Width = Unit.Pixel(120);

            r.Cells.Add(c);
            r.Cells.Add(c1);
            r.Cells.Add(c2);
            r.Cells.Add(c3);
            r.Cells.Add(c4); 
             
            Table1.Rows.AddAt(0,r);
            Table1.CellPadding = 2;
            Table1.CellSpacing = 2;
            Table1.BorderColor = Color.Blue;
            Table1.BackColor = Color.BlueViolet;
            DeluxePager1.BackColor = Color.BlueViolet;

            DeluxePager1.Width = Table1.Width;
        }

        protected void DeluxePager1_CommonPageIndexChanged(object sender, EventArgs e)
        {
            this.InitializePage(DeluxePager1.PageSize, DeluxePager1.PageIndex); 
        }

        
    }
}
