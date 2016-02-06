using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 动态实体
    /// </summary>
    [Serializable]
    public class OuterEntityField : DEBase, IDEMemberObject
    {
        public OuterEntityField()
            : base(DEStandardObjectSchemaType.OuterEntityField.ToString())
        {
        }

        public OuterEntityField(string schemaType) : base(schemaType)
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

        private OuterEntity _OuterEntity;
        public OuterEntity OuterEntity
        {
            get
            {
                if (_OuterEntity == null)
                {
                    DESimpleRelationBase relation = this.CurrentMemberOfRelations.Where
                        (
                            p =>
                            p.SchemaType == DEStandardObjectSchemaType.OuterEntityFieldMapping.ToString() &&
                            p.ContainerSchemaType == DEStandardObjectSchemaType.OuterEntity.ToString()
                        ).FirstOrDefault();

                    if (relation != null)
                    {
                        _OuterEntity = relation.Container as OuterEntity;
                    }
                }

                return _OuterEntity;
            }
        }

    }

    /// <summary>
    ///  表示<see cref="DynamicEntityField"/>的集合
    /// </summary>
    [Serializable]
    public class OuterEntityFieldCollection : DESchemaObjectEditableKeyedCollectionBase<OuterEntityField, OuterEntityFieldCollection>
    {
        /// <summary>
        /// 获取某外部实体对应的字段集合
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public OuterEntityFieldCollection GetMappingField(string entityID)
        {
            OuterEntityFieldCollection result = new OuterEntityFieldCollection();
            this.Where(p => p.OuterEntity.ID.Equals(entityID)).ForEach(result.Add);

            return result;
        }

        public OuterEntityField GetOuterEntityFieldByContainerID(string containerID)
        {
            return this.FirstOrDefault(p => p.AllMemberOfRelations.FirstOrDefault(f => f.ContainerID == containerID) != null);
        }

        public static OuterEntityFieldCollection FromSchemaObjects(DESchemaObjectCollection schemaObjectCollection)
        {
            schemaObjectCollection.NullCheck<ArgumentNullException>("schemaObjectCollection");

            var result = new OuterEntityFieldCollection();
            schemaObjectCollection.ForEach(p => result.Add((OuterEntityField)p));

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
        protected override OuterEntityFieldCollection CreateFilterResultCollection()
        {
            return new OuterEntityFieldCollection();
        }

        protected override string GetKeyForItem(OuterEntityField item)
        {
            return item.ID;
        }
    }
}
