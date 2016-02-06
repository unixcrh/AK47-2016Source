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
	public partial class TestAutoComp : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected override void OnInit(EventArgs e)
		{
			StaticCallBackProxy.Instance.TargetControlLoaded += new StaticCallBackProxyControlLoadedEventHandler(Instance_TargetControlLoaded);
			base.OnInit(e);
		}
		
		protected override void OnInitComplete(EventArgs e)
		{
			base.OnInitComplete(e);
		}
		
		private void Instance_TargetControlLoaded(Control targetControl)
		{
			AutoCompleteExtender auto = targetControl as AutoCompleteExtender;
			if (auto != null)
			{
				auto.GetDataSource += new AutoCompleteExtender.GetDataSourceDelegate(ctrlAutoCompleteExtender_GetDataSource);
			}
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