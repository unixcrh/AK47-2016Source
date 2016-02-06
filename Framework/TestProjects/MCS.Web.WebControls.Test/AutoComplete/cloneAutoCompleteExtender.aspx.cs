using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using MCS.Web.Library.Script;

namespace MCS.Web.WebControls.Test.AutoComplete
{
	public partial class cloneAutoCompleteExtender : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void ctrlAutoCompleteExtender_GetDataSource(string sPrefix, int iCount, object context, ref IEnumerable result)
		{
			DataTable dtTrans = new DataTable();

			dtTrans.Columns.Add("ID", typeof(string));
			dtTrans.Columns.Add("Text", typeof(string));
			dtTrans.Columns.Add("Value", typeof(string));

			DataRow datarowTrans;

			if (iCount == -1)
			{
				iCount = 26;
			}
			for (int i = 0; i < iCount; i++)
			{
				datarowTrans = dtTrans.NewRow();
				datarowTrans["ID"] = i.ToString();
				datarowTrans["Text"] = sPrefix + Convert.ToChar(i + 97);
				datarowTrans["Value"] = "Value_" + i.ToString();
				dtTrans.Rows.Add(datarowTrans);
			}
			//ctrlAutoCompleteExtender.DataSource = dtTrans;
			result = (IEnumerable)dtTrans.DefaultView;
		}
	}
}