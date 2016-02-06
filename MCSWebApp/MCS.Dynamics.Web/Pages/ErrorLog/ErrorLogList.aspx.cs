using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MCS.Library.SOA.DataObjects.Dynamics.ETL.Others;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Enums;
using MCS.Web.Library;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;
using System.Threading.Tasks;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;

namespace MCS.Dynamics.Web.Pages.ErrorLog
{
    public partial class ErrorLogList : System.Web.UI.Page
    {
        /// <summary>
        /// 获取枚举列表
        /// </summary>
        static readonly EnumItemDescriptionList errorTypeDesc = EnumItemDescriptionAttribute.GetDescriptionList(typeof(ErrorType));
        public static EnumItemDescriptionList GetErrorTypeSource()
        {
            return errorTypeDesc;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.searchBinding.Data = this.QueryCondition;
        }

        /// <summary>
        /// 给查询条件类获取值和返回值
        /// </summary>
        protected ErrorCondition QueryCondition
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "QueryCondition", new ErrorCondition());
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
            this.searchBinding.CollectData(true);
            this.QueryCondition = this.searchBinding.Data as ErrorCondition;

            //生成查询条件
            this.BuildWhereClause();

            this.LastQueryRowCount = -1;
            this.ErrorDeluxeGrid.PageIndex = 0;
            this.ErrorDeluxeGrid.SelectedKeys.Clear();

            this.ErrorDeluxeGrid.DataBind();
        }

        /// <summary>
        /// 拼成查询条件
        /// </summary>
        private void BuildWhereClause()
        {
            WhereSqlClauseBuilder wherebuilder = ConditionMapping.GetWhereSqlClauseBuilder(this.QueryCondition);

            this.whereCondition.Value = wherebuilder.ToSqlString(TSqlBuilder.Instance);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            var keys = ErrorDeluxeGrid.SelectedKeys.ToArray();
            if (keys.Length > 0)
            {
                try
                {
                    foreach (string strErrorCode in keys)
                    {
                        DeleteErrorLog(strErrorCode);
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

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
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

        /// <summary>
        /// 单条删除和单条执行入库（重置）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ErrorDeluxeGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string errorCode = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                //删除数据
                case "Del":
                    DeleteErrorLog(errorCode);
                    break;

                //执行单条数据
                case "Executer":
                    try
                    {
                        var log = ErrorLogAdapter.Instance.GetErrorLog(errorCode);
                        if (!log.IsDelete)
                        {
                            Task.Factory.StartNew(log.ReStart).ContinueWith(task =>
                            {
                                if (task.Status == System.Threading.Tasks.TaskStatus.Faulted)
                                {
                                    string detail = EnvironmentHelper.GetEnvironmentInfo() + "\r\n" + task.Exception.GetAllStackTrace();

                                    Exception realEx = task.Exception.InnerException.GetRealException();

                                    realEx.TryWriteAppLog(detail, Request.RequestContext.HttpContext);
                                }
                            });
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    break;

                default:
                    break;

            }
        }

        /// <summary>
        /// 删除容错日志信息
        /// </summary>
        private void DeleteErrorLog(string errorCode)
        {
            if (errorCode.IsNotEmpty())
            {
                try
                {
                    ErrorLogAdapter.Instance.RemoveErrorlog(errorCode);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteJob",
                  string.Format("alert('删除成功!');"),
                  true);
                }
                catch (Exception ex)
                {
                    WebUtility.ShowClientError(ex.Message, ex.StackTrace, "错误");
                }
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        [Serializable]
        public sealed class ErrorCondition
        {
            [ConditionMapping("ErrorType", EnumUsage = EnumUsageTypes.UseEnumValue)]
            public string ErrorType { get; set; }

            [ConditionMapping("ExecutionTime", ">=")]
            public DateTime ExecutionTimeStartTime { get; set; }

            [ConditionMapping("ExecutionTime", "<", AdjustDays = 1)]
            public DateTime ExecutionTimeEndTime { get; set; }
        }

        /// <summary>
        /// 根据ETL实体的Code获取ETL实体
        /// </summary>
        /// <param name="entityCodes"></param>
        /// <returns></returns>
        public string GetETLEntityNames(string entityCodes)
        {
            string entityNames = string.Empty;
            string[] codes = entityCodes.Split(',');
            foreach (string code in codes)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    ETLEntity entity = null;
                    try
                    {
                        entity = (ETLEntity)DESchemaObjectAdapter.Instance.Load(code);
                        entityNames += entity.Name + ",";
                    }
                    catch (Exception)
                    {
                        entityNames += "实体已被删除,";
                    }
                }
            }
            return entityNames.TrimEnd(',');
        }
    }
}