using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Logs;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
    public class DEInstenceExecutor : DEInstenceExecutorBase
    {
        public DEInstenceExecutor(DEOperationType opType, DEEntityInstanceBase data, DEInstenceSnapshotAdapter snapshotAdapter)
            : this(opType, data)
        {
            this.SnapshotAdapter = snapshotAdapter;
        }
        public DEInstenceExecutor(DEOperationType opType, DEEntityInstanceBase data)
            : base(opType)
        {
            this.Data = data;
        }

        private bool _NeedValidation = true;
        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool NeedValidation
        {
            get { return this._NeedValidation; }
            set { this._NeedValidation = value; }
        }

        private bool _NeedSnapshot = true;
        /// <summary>
        /// 是否需要添加快照表
        /// </summary>
        public bool NeedSnapshot
        {
            get { return this._NeedSnapshot; }
            set { this._NeedSnapshot = value; }
        }

        private DEInstenceSnapshotAdapter _snapshotAdapter = null;
        /// <summary>
        /// 快照表业务操作类
        /// </summary>
        public DEInstenceSnapshotAdapter SnapshotAdapter
        {
            get { return this._snapshotAdapter; }
            set { this._snapshotAdapter = value; }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public DEEntityInstanceBase Data
        {
            get;
            private set;
        }

        protected override void PrepareData(DEInstenceOperationContext context)
        {
            this.Validate();
            base.PrepareData(context);
        }

        protected override void PrepareOperationLog(DEInstenceOperationContext context)
        {
            DEOperationLog log = DEOperationLog.CreateLogFromEnvironment();

            log.ResourceID = this.Data.ID;
            log.SchemaType = "DynamiceEntityInstence";
            log.OperationType = this.OperationType;
            log.Category = this.Data.Name;
            log.Subject = string.Format("{0}: {1}",
                EnumItemDescriptionAttribute.GetDescription(this.OperationType), this.Data.Name);

            log.SearchContent =this.Data.Name;
            context.Logs.Add(log);
        }
        /// <summary>
        /// 通常重载此方法来进行校验工作
        /// </summary>
        /// <param name="validationResults"></param>
        protected virtual void DoValidate(ValidationResults validationResults)
        {
            if (this.Data.Status == SchemaObjectStatus.Normal)
            {
                ValidationResults dataValidationResults = this.Data.Validate();

                foreach (ValidationResult result in dataValidationResults)
                    validationResults.AddResult(result);
            }
        }

        /// <summary>
        /// 验证当前数类型
        /// </summary>
        protected virtual void Validate()
        {
            #region “验证”
            if (this._NeedValidation)
            {
                ValidationResults validationResults = new ValidationResults();

                DoValidate(validationResults);

                ExceptionHelper.TrueThrow(validationResults.ResultCount > 0, validationResults.ToString());
            }
            #endregion
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override object DoOperation(DEInstenceOperationContext context)
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

            if (this.NeedSnapshot && this.SnapshotAdapter!=null)
            {
                this.SnapshotAdapter.Update(this.Data);
            }

            return result;
        }
    }
}
