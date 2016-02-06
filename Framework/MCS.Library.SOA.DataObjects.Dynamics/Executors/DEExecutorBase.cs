using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Net.SNTP;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Logs;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
    /// <summary>
    /// 执行器的基类
    /// </summary>
    public abstract class DEExecutorBase
    {
        private bool _AutoStartTransaction = true;

        protected DEExecutorBase(DEOperationType opType)
        {
            this.OperationType = opType;
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public DEOperationType OperationType
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public object Execute()
        {
            object result = null;

            ExecutionWrapper(EnumItemDescriptionAttribute.GetDescription(OperationType),
                    () => result = InternalExecute());

            return result;
        }

        /// <summary>
        /// 是否开启自动事物
        /// </summary>
        public bool AutoStartTransaction
        {
            get
            {
                return this._AutoStartTransaction;
            }
            protected set
            {
                this._AutoStartTransaction = value;
            }
        }

        /// <summary>
        /// 准备数据
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PrepareData(DESchemaObjectOperationContext context)
        {
        }

        /// <summary>
        /// 准备操作日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PrepareOperationLog(DESchemaObjectOperationContext context)
        {
        }

        /// <summary>
        /// 检查数据的状态
        /// </summary>
        /// <param name="objsToCheck"></param>
        protected void CheckObjectStatus(params DESchemaObjectBase[] objsToCheck)
        {
            List<DESchemaObjectBase> normalizedObjsToCheck = new List<DESchemaObjectBase>();

            //foreach (DESchemaObjectBase obj in objsToCheck)
            //{
            //    if (obj.ID != DEOrganization.RootOrganizationID)
            //        normalizedObjsToCheck.Add(obj);
            //}

            InSqlClauseBuilder idBuilder = new InSqlClauseBuilder("ID");

            normalizedObjsToCheck.ForEach(o => idBuilder.AppendItem(o.ID));

            if (idBuilder.IsEmpty == false)
            {
                DESchemaObjectCollection originalDataList = DESchemaObjectAdapter.Instance.Load(idBuilder);

                string opName = EnumItemDescriptionAttribute.GetDescription(this.OperationType);

                foreach (DESchemaObjectBase objToCheck in normalizedObjsToCheck)
                {
                    if (originalDataList.ContainsKey(objToCheck.ID) == false)
                        throw new DEStatusCheckException(string.Format("ID为\"{0}\"的对象不存在，不能执行{1}操作", objToCheck.ID, opName));

                    DESchemaObjectBase originalData = originalDataList[objToCheck.ID];

                    if (originalData.Status != SchemaObjectStatus.Normal)
                        throw new DEStatusCheckException(originalData, this.OperationType);
                }
            }
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected abstract object DoOperation(DESchemaObjectOperationContext context);

        private object InternalExecute()
        {
            DESchemaObjectOperationContext context = new DESchemaObjectOperationContext(this.OperationType, this);

            ExecutionWrapper("PrepareData", () => PrepareData(context));
            ExecutionWrapper("PrepareOperationLog", () => PrepareOperationLog(context));

            object result = null;

            if (this.AutoStartTransaction)
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    ExecutionWrapper("DoOperation", () => result = DoOperation(context));
                    ExecutionWrapper("PersistOperationLog", () => PersistOperationLog(context));

                    scope.Complete();
                }
            }
            else
            {
                ExecutionWrapper("DoOperation", () => result = DoOperation(context));
                ExecutionWrapper("PersistOperationLog", () => PersistOperationLog(context));
            }

            return result;
        }

        private void PersistOperationLog(DESchemaObjectOperationContext context)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                context.Logs.ForEach(log => DEOperationLogAdapter.Instance.Insert(log));

                scope.Complete();
            }
        }

        private static void ExecutionWrapper(string operationName, Action action)
        {
            operationName.CheckStringIsNullOrEmpty("operationName");
            action.NullCheck("action");

            DEExecutorLogContextInfo.Writer.WriteLine("\t\t{0}开始：{1:yyyy-MM-dd HH:mm:ss.fff}",
                    operationName, SNTPClient.AdjustedTime);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            try
            {
                action();
            }
            finally
            {
                sw.Stop();
                DEExecutorLogContextInfo.Writer.WriteLine("\t\t{0}结束：{1:yyyy-MM-dd HH:mm:ss.fff}；经过时间：{2:#,##0}毫秒",
                    operationName, SNTPClient.AdjustedTime, sw.ElapsedMilliseconds);
            }
        }
    }
}
