using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Logs;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Dynamics.Validators;
using MCS.Library.Validation;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics;

namespace MCS.Library.SOA.DataObjects.Security.Executors
{
    public abstract class DEMemberCollectionRelativeExecutorBase : DEObjectExecutor
    {
        private DESchemaObjectBase _Container = null;
        private DEMemberRelationCollection _Relation = null;
        private DESchemaObjectCollection _Members = null;
        private bool _RelationExisted = false;
        private bool _NeedContainerStatusCheck = false;
        DEStandardObjectSchemaType _RelationType = DEStandardObjectSchemaType.Entity_FieldsRelation;

        public DEMemberCollectionRelativeExecutorBase(DEOperationType opType, DEBase container, DESchemaObjectCollection members)
            : base(opType, container)
        {
            container.NullCheck("container");
            container.ClearRelativeData();

            members.NullCheck("members");
            members.ForEach(p => p.ClearRelativeData());

            this._Container = container;
            this._Members = members;

            this.SaveMemberData = true;
        }

        public DEMemberCollectionRelativeExecutorBase(DEOperationType opType, DEBase container, DESchemaObjectCollection members, DEStandardObjectSchemaType relationType)
            : base(opType, container)
        {
            container.NullCheck("container");
            container.ClearRelativeData();

            members.NullCheck("members");
            members.ForEach(p => p.ClearRelativeData());

            this._Container = container;
            this._Members = members;
            _RelationType = relationType;

            this.SaveMemberData = true;
        }

        /// <summary>
        /// 是否需要检查容器的状态
        /// </summary>
        public bool NeedContainerStatusCheck
        {
            get
            {
                return this._NeedContainerStatusCheck;
            }
            set
            {
                this._NeedContainerStatusCheck = value;
            }
        }

        public DESchemaObjectBase Parent
        {
            get
            {
                return this._Container;
            }
        }

        public DESchemaObjectCollection Members
        {
            get
            {
                return this._Members;
            }
        }

        public DEMemberRelationCollection Relation
        {
            get
            {
                return this._Relation;
            }
        }

        /// <summary>
        /// 是否覆盖保存已经存在的关系
        /// </summary>
        public bool OverrideExistedRelation
        {
            get;
            set;
        }

        public bool RelationExisted
        {
            get
            {
                return this._RelationExisted;
            }
        }

        /// <summary>
        /// 是否同时保存容器数据
        /// </summary>
        public bool SaveContainerData
        {
            get;
            set;
        }

        /// <summary>
        /// 是否同时保存成员数据
        /// </summary>
        public bool SaveMemberData
        {
            get;
            set;
        }

        protected override void PrepareData(DESchemaObjectOperationContext context)
        {
            if (this.Data.ID.IsNullOrEmpty())
            {
                this.Data.ID = Guid.NewGuid().ToString();
            }


            Members.ForEach(p =>
            {
                if (p.ID.IsNullOrEmpty())
                {
                    p.ID = Guid.NewGuid().ToString();
                }
            });

            this._Relation = this.PrepareRelationObject(this.Data, this.Members);

            base.PrepareData(context);
        }

        protected override object DoOperation(DESchemaObjectOperationContext context)
        {
            DEMemberRelationAdapter.Instance.RelationAction(this.Data, this.Members, this.Relation, this.SaveContainerData, this.SaveMemberData, this._RelationType);

            return this.Relation;
        }

        protected override void Validate()
        {
            if (this.NeedValidation == true)
            {
                DESchemaPropertyValidatorContext context = DESchemaPropertyValidatorContext.Current;

                try
                {
                    context.Target = this.Data;
                    context.Container = this._Container;

                    ValidationResults validationResult = new ValidationResults();

                    if (this.Data.Status == SchemaObjectStatus.Normal)
                        validationResult = this.Data.Validate();

                    //haoyk 2014-2-8
                    if (this.SaveMemberData && this.Members.Any())
                        this.Members.Where(p => p.Status == SchemaObjectStatus.Normal).ForEach(p =>
                        {
                            ValidationResults results = p.Validate();

                            if (results != null && results.Any())
                            {
                                results.ForEach(validationResult.AddResult);
                            }
                        });

                    ExceptionHelper.TrueThrow(validationResult.ResultCount > 0, validationResult.ToString());

                    CheckStatus();
                }
                finally
                {
                    DESchemaPropertyValidatorContext.Clear();
                }
            }
        }

        protected override void CheckStatus()
        {
            List<DESchemaObjectBase> dataToBeChecked = new List<DESchemaObjectBase>();

            if (this.NeedStatusCheck)
                dataToBeChecked.Add(this.Data);

            if (this.NeedContainerStatusCheck)
                dataToBeChecked.Add(this._Container);

            CheckObjectStatus(dataToBeChecked.ToArray());
        }

        protected override void PrepareOperationLog(DESchemaObjectOperationContext context)
        {
            DEOperationLog log = DEOperationLog.CreateLogFromEnvironment();

            log.ResourceID = this.Data.ID;
            log.SchemaType = this.Data.SchemaType;
            log.OperationType = this.OperationType;
            log.Category = this.Data.Schema.Category;
            log.Subject = string.Format("{0}: {1} 于 {2}",
                EnumItemDescriptionAttribute.GetDescription(this.OperationType), this.Data.Name, ((DEBase)this._Container).Name);

            log.SearchContent = this.Data.ToFullTextString() + " " + this._Container.ToFullTextString();

            context.Logs.Add(log);
        }

        //protected abstract DESimpleRelationBase CreateRelation(DESchemaObjectBase container, DESchemaObjectBase member);

        protected abstract DESimpleRelationBase CreateRelation(DESchemaObjectBase container, DESchemaObjectBase member, DEStandardObjectSchemaType relationType);

        private DEMemberRelationCollection PrepareRelationObject(DESchemaObjectBase container, DESchemaObjectCollection members)
        {
            var result = new DEMemberRelationCollection();

            members.ForEach(member =>
            {
                DESimpleRelationBase relation = DEMemberRelationAdapter.Instance.Load(container.ID, member.ID);

                if (relation == null)
                    relation = CreateRelation(container, member, _RelationType);
                else
                {
                    OverrideExistedRelation = true;
                }
                relation.Status = member.Status;
                result.Add(relation);
            });

            return result;
        }
    }
}
