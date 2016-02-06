using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library.Script;

namespace MCS.Web.WebControls.Test.JSONTest
{
	public partial class DateTimeSerializationTest : System.Web.UI.Page
	{
		public class TestData
		{
			public DateTime CurrentDate
			{
				get;
				set;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			TestData data = new TestData() { CurrentDate = DateTime.Now };

			string serializedData = JSONSerializerExecute.Serialize(data);

			firstSerializationLabel.Text = HttpUtility.HtmlEncode(string.Format("{0}-{1}", serializedData, data.CurrentDate));

			TestData deserializedData = JSONSerializerExecute.Deserialize<TestData>(serializedData);

			string reserializedData = JSONSerializerExecute.Serialize(deserializedData);

			secondSerializationLabel.Text = HttpUtility.HtmlEncode(string.Format("{0}-{1}", reserializedData, deserializedData.CurrentDate));
		}
	}
}