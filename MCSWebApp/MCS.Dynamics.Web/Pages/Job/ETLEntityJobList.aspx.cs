using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Others;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.TaskServices;
using MCS.Web.Library;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Job;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Operations;

namespace MCS.Dynamics.Web.Pages.Job
{
    public partial class ETLEntityJobList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.searchBinding.Data = this.QueryCondition;
        }

        protected JobCondition QueryCondition
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "QueryCondition", new JobCondition());
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "QueryCondition", value);
            }
        }

        /// <summary>
        /// 数据检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSearch_Click(object sender, EventArgs e)
        {
            InnerRefreshList();
        }

        private void InnerRefreshList()
        {
            // 重新刷新列表
            this.dataSourceMain.LastQueryRowCount = -1;

            this.JobDeluxeGrid.SelectedKeys.Clear();

            this.Page.PreRender += new EventHandler(this.DelayRefreshList);
        }

        private void DelayRefreshList(object sender, EventArgs e)
        {
            this.JobDeluxeGrid.DataBind();
        }



        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            var keys = JobDeluxeGrid.SelectedKeys.ToArray();
            if (keys.Length > 0)
            {
                try
                {
                    var jobBases = ETLJobAdapter.Instance.LoadByInBuilder(inB => { inB.DataField = "JOB_ID"; inB.AppendItem(keys); });
                    foreach (ETLJob job in jobBases)
                    {
                        ETLJobOperations.Instance.DoOperation(EntityJobOperationMode.Delete, job);
                        //job.Enabled = false;
                        //ETLJobAdapter.Instance.Update(job);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteJob",
                    string.Format("alert('删除成功!');"),
                    true);
                }
                catch (Exception ex)
                {
                    WebUtility.ShowClientError(ex.Message, ex.StackTrace, "错误");
                }

                this.LastQueryRowCount = -1;
            }
        }

        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            this.QueryCondition = this.searchBinding.Data as JobCondition;
            WhereSqlClauseBuilder wherebuilder = ConditionMapping.GetWhereSqlClauseBuilder(this.QueryCondition);

            string auto = Request.QueryString["auto"];

            if (!string.IsNullOrEmpty(auto))
            {
                wherebuilder.AppendItem("ISManual", auto == "false");
            }

            this.dataSourceMain.Condition = wherebuilder;
            e.InputParameters["totalCount"] = LastQueryRowCount;
        }


        protected void objectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            LastQueryRowCount = (int)e.OutputParameters["totalCount"];
        }

        private int LastQueryRowCount
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "LastQueryRowCount", -1);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "LastQueryRowCount", value);
            }
        }

        protected void JobDeluxeGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btn_star")
            {
                try
                {
                    string jobID = e.CommandArgument.ToString();

                    ETLJob loadJob = ETLJobAdapter.Instance.Load(jobID);


                    #region 执行任务
                    SysTask sysTask = SysTaskAdapter.Instance.Load(jobID);
                    if (sysTask != null)
                    {
                        //自动任务
                        ExecuteTask(sysTask);
                    }
                    else
                    {
                        //手动任务
                        SysTask task = loadJob.ToSysTask();
                        task.FillData(BuildTaskExtraData(loadJob, new TimeSpan(0), null));
                        loadJob.SetCurrentJobBeginStatus();
                        SysTaskAdapter.Instance.Update(task);

                        ExecuteTask(task);
                    }
                    #endregion

                    // Task.Factory.StartNew(loadJob.Start).ContinueWith(task =>
                    //{
                    //    if (task.Status == System.Threading.Tasks.TaskStatus.Faulted)
                    //    {
                    //        string detail = EnvironmentHelper.GetEnvironmentInfo() + "\r\n" + task.Exception.GetAllStackTrace();

                    //        Exception realEx = task.Exception.InnerException.GetRealException();

                    //        realEx.TryWriteAppLog(detail, Request.RequestContext.HttpContext);
                    //    }
                    //});
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static void ExecuteTask(SysTask task)
        {
            try
            {
                //2014-4-19 by haoyk
                //当作业还没执行完成，将不再执行此任务
                if (task.Status != SysTaskStatus.Running)
                {
                    ISysTaskExecutor executor = SysTaskSettings.GetSettings().GetExecutor(task.TaskType);

                    executor.BeforeExecute(task);
                    Task.Factory.StartNew(() => executor.Execute(task));
                }
            }
            catch (System.Exception ex)
            {
                SysTaskAdapter.Instance.MoveToCompletedSysTask(task.TaskID, SysTaskStatus.Aborted, ex.GetRealException().ToString());
            }
        }


        private static Dictionary<string, string> BuildTaskExtraData(JobBase job, TimeSpan timeOffset, JobSchedule matchedSchedule)
        {
            Dictionary<string, string> extraData = new Dictionary<string, string>();

            if (job.LastStartExecuteTime != null)
                extraData.Add("LastStartExecuteTime", job.LastStartExecuteTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            if (matchedSchedule != null)
                extraData.Add("ScheduleID", matchedSchedule.ID);

            extraData.Add("TimeOffset", timeOffset.TotalSeconds.ToString("#,##0.00"));

            return extraData;
        }
    }


    /// <summary>
    /// 查询条件
    /// </summary>
    [Serializable]
    public sealed class JobCondition
    {
        [ConditionMapping("JOB_NAME", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Name { get; set; }

        [ConditionMapping("LAST_START_EXE_TIME", ">=")]
        public DateTime LastExecuteStartTime { get; set; }

        [ConditionMapping("LAST_START_EXE_TIME", "<", AdjustDays = 1)]
        public DateTime LastExecuteEndTime { get; set; }
    }
}