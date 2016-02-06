using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;
using MCS.Web.Library;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class ErrorLogDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetErrorLog();
            }

        }

        protected void GetErrorLog()
        {
            string errorCode = Request.QueryString["ID"];
            var errorLog = ErrorLogAdapter.Instance.GetErrorLog(errorCode);
            if (errorLog != null)
            {
                this.exTime.Text = errorLog.ExecutionTime.ToString();
                this.execSql.Text = errorLog.SqlStr;
                this.errorMsg.Text = errorLog.ErrorMsg;
                this.createor.Text = errorLog.CreateUser;
                this.taskJob.Text = errorLog.JobId;
                this.errorType.Text =
                    MCS.Library.Core.EnumItemDescriptionAttribute.GetDescription(errorLog.ErrorLogType);// errorLog.ErrorLogType.ToString();
                
            }

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            var log = ErrorLogAdapter.Instance.GetErrorLog(Request.QueryString["ID"]);
            Task.Factory.StartNew(log.ReStart);
        }

        protected void btn_remove_Error_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorLogAdapter.Instance.RemoveErrorlog(Request.QueryString["ID"]);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteJob",
              string.Format("alert('删除成功!');window.close();"),
              true);
            }
            catch (Exception ex)
            {
                WebUtility.ShowClientError(ex.Message, ex.StackTrace, "错误");
            }
        }
    }
}