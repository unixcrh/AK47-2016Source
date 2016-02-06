using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library;
using MCS.Web.Library.Resources;

namespace MCS.Web.WebControls.Test.ModuleTest
{
	public partial class ExceptionTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			WebUtility.RequiredScript(typeof(ClientMsgResources));
		}

		protected void throwException_Click(object sender, EventArgs e)
		{
			throw new ApplicationException("异常测试");
		}

		protected void throwMsgBoxException_Click(object sender, EventArgs e)
		{
			try
			{
				throw new ApplicationException("异常测试");
			}
			catch (System.Exception ex)
			{
				WebUtility.ShowClientError(ex);
			}
		}

		protected void throwMsgBoxExceptionInUpdataPanel_Click(object sender, EventArgs e)
		{
			try
			{
				throw new ApplicationException("异常测试 in update panel");
			}
			catch (System.Exception ex)
			{
				WebUtility.RegisterClientErrorMessage(ex);
			}
		}
	}
}