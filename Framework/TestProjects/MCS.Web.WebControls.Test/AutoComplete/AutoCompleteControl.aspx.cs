using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MCS.Library.Core;

namespace MCS.Web.WebControls.Test.AutoComplete
{
	public partial class AutoCompleteControl : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//this.autoComplete.CompareFieldName = new string[] { "Value" };
			//this.autoComplete.DataTextFieldList = new string[] { "Value", "Text" };
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

		protected override void OnPreRender(EventArgs e)
		{
			if (this.autoComplete1.Value.IsNotEmpty())
				valuesText.InnerText = string.Format("Value: {0}, Text: {1}", this.autoComplete1.Value, this.autoComplete1.Text);

			base.OnPreRender(e);
		}
	}
}