using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Executors
{
    class DEEntityInstanceExecutor : DEExecutorBase
    {
        public DEEntityInstanceExecutor(DEOperationType opType, DEEntityInstanceBase data)
            : base(opType)
        {
            this.Data = data;
        }

        /// <summary>
        /// 是否需要验证
        /// </summary>
        private bool _NeedValidation = true;

        public bool NeedValidation
        {
            get { return this._NeedValidation; }
            set { this._NeedValidation = value; }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public DEEntityInstanceBase Data
        {
            get;
            private set;
        }

        protected override object DoOperation(Dynamics.Adapters.DESchemaObjectOperationContext context)
        {
            object result = null;

            if (this.NeedValidation)
            {
                //验证数据
                this.Data.Validate();
                DEInstanceAdapter.Instance.Update(this.Data);
            }
            else
            {
                //不验证 直接入库
                DEInstanceAdapter.Instance.Update(this.Data);
            }

            return result;
        }
    }
}
