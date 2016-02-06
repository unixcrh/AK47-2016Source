using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Contract;
using MCS.Library.Net.SNTP;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 动态实体
    /// </summary>
    [Serializable]
    public class OuterEntity : DEBase, IDEMemberObject, IDEContainerObject
    {
        public OuterEntity()
            : base(DEStandardObjectSchemaType.OuterEntity.ToString())
        {
        }

        public OuterEntity(string schomeType)
            : base(schomeType)
        {

        }

        public DEObjectContainerRelationCollection GetCurrentMemberOfRelations()
        {
            return this.CurrentMemberOfRelations;
        }

        /// <summary>
        /// 获取一个<see cref="DEObjectContainerRelationCollection"/>，表示当前用户的成员关系
        /// </summary>
        [ScriptIgnore]
        [NoMapping]
        public DEObjectContainerRelationCollection CurrentMemberOfRelations
        {
            get
            {
                return (DEObjectContainerRelationCollection)AllMemberOfRelations.FilterByStatus(DESchemaObjectStatusFilterTypes.Normal);
            }
        }

        private OuterEntityFieldCollection _outerFields = null;
        /// <summary>
        /// 实体字段
        /// </summary>
        public OuterEntityFieldCollection Fields
        {
            get
            {
                if (this._outerFields == null && this.CurrentMembers != null)
                {
                    var fileds = new DESchemaObjectCollection();

                    fileds.CopyFrom(this.CurrentMembers.Where(p => p.SchemaType.Trim() == DEStandardObjectSchemaType.OuterEntityField.ToString()));

                    this._outerFields = OuterEntityFieldCollection.FromSchemaObjects(fileds);
                }
                return this._outerFields;
            }

            set
            {
                _outerFields = value;
            }
        }

        [NonSerialized]
        private DEObjectContainerRelationCollection _AllMemberOfRelations = null;

        /// <summary>
        /// 获取用户的所有成员关系的集合
        /// </summary>
        /// <value> 一个<see cref="DEObjectContainerRelationCollection"/></value>
        [ScriptIgnore]
        [NoMapping]
        public DEObjectContainerRelationCollection AllMemberOfRelations
        {
            get
            {
                if (this._AllMemberOfRelations == null && this.ID.IsNotEmpty())
                    this._AllMemberOfRelations = DEMemberRelationAdapter.Instance.LoadByMemberID(this.ID);

                return this._AllMemberOfRelations;
            }
        }

        [NonSerialized]
        private DEObjectMemberRelationCollection _AllMembersRelations = null;

        [ScriptIgnore]
        [NoMapping]
        public DEObjectMemberRelationCollection AllMembersRelations
        {
            get
            {
                if (this._AllMembersRelations == null && this.ID.IsNotEmpty())
                    this._AllMembersRelations = DEMemberRelationAdapter.Instance.LoadByContainerID(this.ID);

                return this._AllMembersRelations;
            }
        }
        /// <summary>
        /// 获取一个<see cref="DEObjectMemberRelationCollection"/>，表示当前实体的成员
        /// </summary>
        [ScriptIgnore]
        [NoMapping]
        public DEObjectMemberRelationCollection CurrentMembersRelations
        {
            get
            {
                return (DEObjectMemberRelationCollection)AllMembersRelations.FilterByStatus(DESchemaObjectStatusFilterTypes.Normal);
            }
        }
        public DEObjectMemberRelationCollection GetCurrentMembersRelations()
        {
            return this.CurrentMembersRelations;
        }

        [NonSerialized]
        private DESchemaObjectCollection _CurrentMembers = null;

        /// <summary>
        /// 获取动态实体的字段
        /// </summary>
        [ScriptIgnore]
        [NoMapping]
        public DESchemaObjectCollection CurrentMembers
        {
            get
            {
                if (this._CurrentMembers == null && this.ID.IsNotEmpty())
                {
                    this._CurrentMembers = DESchemaObjectAdapter.Instance.Load(CurrentMembersRelations.ToMemberIDsBuilder());
                }

                return _CurrentMembers;
            }
        }
        public DESchemaObjectCollection GetCurrentMembers()
        {
            return this.CurrentMembers;
        }

        public InType CustomType
        {
            get
            {
                return (InType)this.Properties.GetValue("CustomType", InType.StandardInterface);
            }
            set
            {
                this.Properties.SetValue("CustomType", value);
            }
        }

        #region

        /// <summary>
        /// 外部实体的XML序列化，包括所有子对象（字段）
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            XElement entity = new XElement("OuterEntity");
            this.ToXElement(entity);

            #region 字段集合

            if (this.Fields.Any())
            {
                XElement fields = new XElement("OuterFields");
                this.Fields.ForEach(f =>
                {
                    XElement field = new XElement("OuterField");
                    f.ToXElement(field);

                    var relation = f.CurrentMemberOfRelations.FirstOrDefault(p =>
                         p.SchemaType == DEStandardObjectSchemaType.DynamicEntityFieldMapping.ToString() &&
                         p.Member.ID == f.ID);

                    if (relation != null)
                    {
                        field.SetAttributeValue("MappingFieldID", relation.ContainerID);
                    }

                    fields.Add(field);
                });
                entity.Add(fields);
            }

            #endregion

            return entity;
        }

        /// <summary>
        /// 动态实体的XML反序列化，包括所有子对象（字段、外部实体）
        /// </summary>
        /// <param name="xEntity">XElement对象</param>
        /// <returns></returns>
        public new void FromXElement(XElement xEntity)
        {
            //反序列化外部实体
            this.FromString(xEntity.ToString());

            //反序列化外部实体字段
            this.Fields = new OuterEntityFieldCollection();
            xEntity.XPathSelectElements("OuterFields/OuterField").ForEach(p =>
            {
                OuterEntityField field = new OuterEntityField();
                field.FromXElement(p);
                this.Fields.Add(field);
            });
        }

        #endregion

        public void BuildNewEntity()
        {
            foreach (var field in this.Fields)
            {
                field.ID = Guid.NewGuid().ToString();
                field.CreateDate = SNTPClient.AdjustedTime.SimulateTime();
                field.VersionStartTime = DateTime.MinValue;
                field.VersionEndTime = DateTime.MinValue;
            }

            this.ID = Guid.NewGuid().ToString();
            this.CreateDate = SNTPClient.AdjustedTime.SimulateTime();
            this.VersionStartTime = DateTime.MinValue;
            this.VersionEndTime = DateTime.MinValue;

        }
    }

    /// <summary>
    ///  表示<see cref="DynamicEntityField"/>的集合
    /// </summary>
    [Serializable]
    public class OuterEntityCollection : DESchemaObjectEditableKeyedCollectionBase<OuterEntity, OuterEntityCollection>
    {

        public static OuterEntityCollection FromSchemaObjects(DESchemaObjectCollection schemaObjectCollection)
        {
            schemaObjectCollection.NullCheck<ArgumentNullException>("schemaObjectCollection");

            var result = new OuterEntityCollection();
            schemaObjectCollection.ForEach(p => result.Add((OuterEntity)p));

            return result;
        }

        public DESchemaObjectCollection ToSchemaObjects()
        {
            DESchemaObjectCollection result = new DESchemaObjectCollection();

            this.ForEach(u => result.Add(u));

            return result;
        }

        /// <summary>
        /// 创建过滤器结果集合
        /// </summary>
        /// <returns></returns>
        protected override OuterEntityCollection CreateFilterResultCollection()
        {
            return new OuterEntityCollection();
        }

        protected override string GetKeyForItem(OuterEntity item)
        {
            return item.ID;
        }
    }
}
