using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Logs;
using MCS.Library.SOA.DataObjects.Security.Executors;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// Action中的操作上下文
    /// </summary>
    [Serializable]
    public class DESchemaObjectOperationContext : Dictionary<string, object>
    {
        private DEOperationType _OperationType = DEOperationType.None;
        private DEExecutorBase _Executor = null;
        private DEOperationLogCollection _Logs = null;

        /// <summary>
        /// 根据指定的操作类型和执行器初始化<see cref="DESchemaObjectOperationContext"/>的新实例。
        /// </summary>
        /// <param name="opType"><see cref="DEOperationType"/>值之一，表示操作的类型</param>
        /// <param name="executor"><see cref="DEExecutorBase"/>对象，表示操作</param>
        public DESchemaObjectOperationContext(DEOperationType opType, DEExecutorBase executor)
        {
            this._OperationType = opType;
            this._Executor = executor;
        }

        /// <summary>
        /// 获取操作日志的集合
        /// </summary>
        public DEOperationLogCollection Logs
        {
            get
            {
                if (this._Logs == null)
                    this._Logs = new DEOperationLogCollection();

                return this._Logs;
            }
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        public DEExecutorBase Executor
        {
            get
            {
                return this._Executor;
            }
        }

        /// <summary>
        /// 获取表示操作类型的<see cref="DEOperationType"/>值之一。
        /// </summary>
        public DEOperationType OperationType
        {
            get
            {
                return this._OperationType;
            }
        }
    }
}
