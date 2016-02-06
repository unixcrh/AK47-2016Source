using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Schemas.Actions;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Logs;
using MCS.Library.SOA.DataObjects.Dynamics.Permissions;
using MCS.Library.Validation;

namespace MCS.Library.SOA.DataObjects.Security.Executors
{
    public class DEObjectExecutor : DEExecutorBase
    {
        private DEBase _Data = null;
        private bool _NeedValidation = true;
        private bool _NeedDeleteRelations = false;
        private bool _NeedDeleteMemberRelations = false;
        private bool _ObjectNameChanged = false;
        private bool _NeedGenerateFullPaths = false;
        private bool _NeedDeleteConditions = false;
        private bool _NeedStatusCheck = false;

        public DEObjectExecutor(DEOperationType opType, DEBase data)
            : base(opType)
        {
            data.NullCheck("data");

            data.ClearRelativeData();
            this._Data = data;
        }

        /// <summary>
        /// 对象的Name属性是否改变
        /// </summary>
        public bool ObjectNameChanged
        {
            get
            {
                return this._ObjectNameChanged;
            }
        }

        /// <summary>
        /// 是否需要状态检查
        /// </summary>
        public bool NeedStatusCheck
        {
            get
            {
                return this._NeedStatusCheck;
            }
            set
            {
                this._NeedStatusCheck = value;
            }
        }

        /// <summary>
        /// 是否需要全局生成FullPath
        /// </summary>
        public bool NeedGenerateFullPaths
        {
            get
            {
                return this._NeedGenerateFullPaths;
            }
        }

        /// <summary>
        /// 是否删除相关的条件对象
        /// </summary>
        public bool NeedDeleteConditions
        {
            get
            {
                return this._NeedDeleteConditions;
            }
            set
            {
                this._NeedDeleteConditions = value;
            }
        }

        public bool NeedValidation
        {
            get
            {
                return this._NeedValidation;
            }
            set
            {
                this._NeedValidation = value;
            }
        }

        public bool NeedDeleteRelations
        {
            get
            {
                return this._NeedDeleteRelations;
            }
            set
            {
                this._NeedDeleteRelations = value;
            }
        }

        /// <summary>
        /// 是否连带删除成员关系
        /// </summary>
        public bool NeedDeleteMemberRelations
        {
            get
            {
                return this._NeedDeleteMemberRelations;
            }
            set
            {
                this._NeedDeleteMemberRelations = value;
            }
        }

        public DEBase Data
        {
            get
            {
                return this._Data;
            }
        }

        protected override void PrepareData(DESchemaObjectOperationContext context)
        {
            this.Validate();
            base.PrepareData(context);

            //SCActionContext.Current.OriginalObject = DESchemaObjectAdapter.Instance.Load(this.Data.ID);
            //SCActionContext.Current.CurrentObject = this.Data;

            //string originalName = SCActionContext.Current.OriginalObject != null ? SCActionContext.Current.OriginalObject.Properties.GetValue("Name", string.Empty) : string.Empty;
            //string currentName = SCActionContext.Current.OriginalObject != null ? SCActionContext.Current.CurrentObject.Properties.GetValue("Name", string.Empty) : string.Empty;

            //this._ObjectNameChanged = originalName != currentName;

            ////是否是关系容器
            //IDERelationContainer rContainer = this.Data as IDERelationContainer;

            //if (this._ObjectNameChanged)
            //{
            //    this._NeedGenerateFullPaths = (rContainer != null && rContainer.CurrentChildren.Count > 0);

            //    this.Data.CurrentParentRelations.ForEach(relation =>
            //    {
            //        if (relation.FullPath.IsNotEmpty())
            //        {
            //            int index = relation.FullPath.LastIndexOf(originalName);

            //            if (index >= 0)
            //                relation.FullPath = relation.FullPath.Substring(0, index) + currentName;
            //        }
            //    });

            //    context["parentRelations"] = this.Data.CurrentParentRelations;
            //}

            //this.Validate();

            //if (this._NeedDeleteRelations)
            //{
            //    //加载关系对象，然后为了统一删除和打标志。
            //    if (rContainer != null)
            //        context["childrenRelations"] = rContainer.CurrentChildrenRelations;

            //    context["parentRelations"] = this.Data.CurrentParentRelations;
            //}

            //if (this._NeedDeleteMemberRelations)
            //{
            //    if (this.Data is IDEMemberObject)
            //        context["containersRelations"] = ((IDEMemberObject)this.Data).GetCurrentMemberOfRelations();

            //    if (this.Data is IDEContainerObject)
            //        context["membersRelations"] = ((IDEContainerObject)this.Data).GetCurrentMembersRelations();
            //}

            //if (this.Data.Status != SchemaObjectStatus.Normal)
            //{
            //    if (this.Data is IDEAclContainer)
            //        context["aclMembers"] = ((IDEAclContainer)this.Data).GetAclMembers();

            //    if (this.Data is IDEAclMember)
            //        context["aclContainers"] = ((IDEAclMember)this.Data).GetAclContainers();
            //}
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

                //foreach (DERelationObject relation in this.Data.CurrentParentRelations)
                //{
                //    ValidationResults relationValidationResults = relation.Validate();

                //    foreach (ValidationResult result in relationValidationResults)
                //        validationResults.AddResult(result);
                //}
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

            CheckStatus();
            #endregion
        }

        /// <summary>
        /// 验证对象的状态
        /// </summary>
        protected virtual void CheckStatus()
        {
            if (this._NeedStatusCheck)
                CheckObjectStatus(this.Data);
        }

        protected override object DoOperation(DESchemaObjectOperationContext context)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DESchemaObjectAdapter.Instance.Update(Data);

                DoRelativeDataOperation(context);

                scope.Complete();
            }

            return this.Data;
        }

        /// <summary>
        /// 处理相关数据
        /// </summary>
        /// <param name="context"></param>
        protected void DoRelativeDataOperation(DESchemaObjectOperationContext context)
        {
            //if (this._ObjectNameChanged)
            //{
            //    if (context.ContainsKey("parentRelations"))
            //        UpdateRelations((IEnumerable<DERelationObject>)context["parentRelations"]);
            //}

            //if (this._NeedDeleteRelations)
            //{
            //    if (context.ContainsKey("childrenRelations"))
            //        UpdateRelationsStatus((IEnumerable<DERelationObject>)context["childrenRelations"]);

            //    if (context.ContainsKey("parentRelations"))
            //        UpdateRelationsStatus((IEnumerable<DERelationObject>)context["parentRelations"]);
            //}

            if (context.ContainsKey("containersRelations"))
                UpdateMembersRelationsStatus((IEnumerable<DESimpleRelationBase>)context["containersRelations"]);

            if (context.ContainsKey("membersRelations"))
                UpdateMembersRelationsStatus((IEnumerable<DESimpleRelationBase>)context["membersRelations"]);

            if (context.ContainsKey("aclMembers"))
                DEAclAdapter.Instance.UpdateStatus(((DEAclContainerOrMemberCollectionBase)context["aclMembers"]), SchemaObjectStatus.Deleted);

            if (context.ContainsKey("aclContainers"))
                DEAclAdapter.Instance.UpdateStatus(((DEAclContainerOrMemberCollectionBase)context["aclContainers"]), SchemaObjectStatus.Deleted);
        }

        //private static void UpdateRelations(IEnumerable<DERelationObject> relations)
        //{
        //    relations.ForEach(r => DESchemaRelationObjectAdapter.Instance.Update(r));
        //}

        //private static void UpdateRelationsStatus(IEnumerable<DERelationObject> relations)
        //{
        //    relations.ForEach(r => DESchemaRelationObjectAdapter.Instance.UpdateStatus(r, SchemaObjectStatus.Deleted));
        //}

        private static void UpdateMembersRelationsStatus(IEnumerable<DESimpleRelationBase> relations)
        {
            relations.ForEach(r => DEMemberRelationAdapter.Instance.UpdateStatus(r, SchemaObjectStatus.Deleted));
        }

        protected override void PrepareOperationLog(DESchemaObjectOperationContext context)
        {
            context.Logs.Add(this.Data.ToOperationLog(this.OperationType));
        }
    }
}
