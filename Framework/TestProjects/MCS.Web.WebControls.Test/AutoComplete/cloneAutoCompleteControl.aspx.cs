using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library.Script;
using System.Collections;

namespace MCS.Web.WebControls.Test.AutoComplete
{
	public partial class cloneAutoCompleteControl : System.Web.UI.Page
	{
		protected override void OnInit(EventArgs e)
		{
			StaticCallBackProxy.Instance.TargetControlLoaded += new StaticCallBackProxyControlLoadedEventHandler(Instance_TargetControlLoaded);
			base.OnInit(e);
		}

		private void Instance_TargetControlLoaded(Control targetControl)
		{
			AutoCompleteExtender auto = targetControl as AutoCompleteExtender;

			if (auto != null)
			{
				auto.GetDataSource += new AutoCompleteExtender.GetDataSourceDelegate(autoComplete_GetDataSource);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void autoComplete_GetDataSource(string sPrefix, int iCount, object context, ref IEnumerable result)
		{
			List<SimpleVendorInfo> list = new List<SimpleVendorInfo>();

			foreach (SimpleVendorInfo vendor in Vendors)
			{
				if (vendor.Value.IndexOf(sPrefix) == 0)
					list.Add(vendor);
			}

			result = list;
		}

		private readonly SimpleVendorInfo[] Vendors = new SimpleVendorInfo[]{
			new SimpleVendorInfo() { Value = "100101", Text = "联想集团"},
			new SimpleVendorInfo() { Value = "100102", Text = "联想北研"},
			new SimpleVendorInfo() { Value = "100103", Text = "联想USA"},
			new SimpleVendorInfo() { Value = "200101", Text = "方正集团"},
			new SimpleVendorInfo() { Value = "200102", Text = "方正电子"},
			new SimpleVendorInfo() { Value = "200103", Text = "方正研究院"}
		};

		private class SimpleVendorInfo
		{
			public string Value { get; set; }
			public string Text { get; set; }
		}
	}
}