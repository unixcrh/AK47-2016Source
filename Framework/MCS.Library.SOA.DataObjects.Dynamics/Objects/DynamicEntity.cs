using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 动态实体
    /// </summary>
    [Serializable]
    public class DynamicEntity : DEBase, IDEContainerObject
    {
        public DynamicEntity()
            : base(DEStandardObjectSchemaType.DynamicEntity.ToString())
        {
        }

        public DynamicEntity(string strType)
            : base(strType)
        {

        }

        /// <summary>
        /// 快照表名称
        /// </summary>
        [NoMapping]
        public string SnapshotTable
        {
            get
            {
                return this.Properties.GetValue("SnapshotTable", string.Empty);
            }
            set
            {
                this.Properties.SetValue("SnapshotTable", value);
            }
        }
        /// <summary>
        /// 分类编码
        /// </summary>
        [NoMapping]
        public string CategoryID
        {
            get
            {
                return this.Properties.GetValue("CategoryID", string.Empty);
            }
            set
            {
                this.Properties.SetValue("CategoryID", value);
            }
        }

        private DynamicEntityFieldCollection _Fields = null;

        /// <summary>
        /// 实体字段
        /// </summary>
        public DynamicEntityFieldCollection Fields
        {
            get
            {
                if (this._Fields == null && this.CurrentMembers != null)
                {
                    DESchemaObjectCollection fileds = new DESchemaObjectCollection();

                    fileds.CopyFrom(this.CurrentMembers.Where(p =>
                        p.SchemaType.Trim() == DEStandardObjectSchemaType.DynamicEntityField.ToString() ||
                        p.SchemaType.Trim() == DEStandardObjectSchemaType.ETLEntityField.ToString()
                    ));

                    this._Fields = DynamicEntityFieldCollection.FromSchemaObjects(fileds);
                }

                return this._Fields;
            }
            set
            {
                this._Fields = value;
            }
        }

        #region MembersRelations

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
                    this._CurrentMembers = DESchemaObjectAdapter.Instance.Load(CurrentMembersRelations.ToMemberIDsBuilder(), true);
                }

                return _CurrentMembers;
            }
        }

        public DESchemaObjectCollection GetCurrentMembers()
        {
            return this.CurrentMembers;
        }

        #endregion

        /// <summary>
        /// 转换为属性值集合
        /// </summary>
        /// <returns></returns>
        public PropertyValueCollection ToDynamicProperties()
        {
            PropertyValueCollection properties = new PropertyValueCollection();

            foreach (DynamicEntityField field in this.Fields)
                properties.Add(field.ToDynamicPropertyValue());

            return properties;
        }

        /// <summary>
        /// 按规则生成CodeName
        /// </summary>
        public void BuidCodeName()
        {
            if (this.CategoryID.IsNotEmpty() && this.Name.IsNotEmpty())
            {
                var codeName = DESchemaObjectAdapter.Instance.BuildCodeName(this.CategoryID, this.Name);
                this.Properties.SetValue("CodeName", codeName);
            }
        }

        private List<EntityFieldMappingDisplayItem> _ListDisplayItem;

        public List<EntityFieldMappingDisplayItem> DynamicEntityMappingCollection
        {
            get
            {
                if (_ListDisplayItem == null)
                {
                    _ListDisplayItem = new List<EntityFieldMappingDisplayItem>();
                    this.Fields.ForEach(p =>
                    {
                        _ListDisplayItem.Add(new EntityFieldMappingDisplayItem()
                        {
                            FieldID = p.ID,
                            FieldDesc = p.Description,
                            FieldTypeName = p.FieldType.ToString(),
                            FieldName = p.Name,
                            FieldLength = p.Length,
                            FieldDefaultValue = p.DefaultValue,
                            DestinationName=string.Empty
                            //DestinationName = p.OuterEntityFields.Any() ? p.OuterEntityFields.FirstOrDefault().Name : string.Empty
                        });
                    });
                }

                return this._ListDisplayItem;
            }
            set
            {
                _ListDisplayItem = value;
            }
        }

        #region 注释掉OuterEntity。By 王雷平 2013-8-13

        //private OuterEntityCollection _outerEntities = null;

        //public OuterEntityCollection OuterEntities
        //{
        //    get
        //    {
        //        if (this._outerEntities == null && this.CurrentMembers != null)
        //        {
        //            var oEntities = new DESchemaObjectCollection();

        //            oEntities.CopyFrom(this.CurrentMembers.Where(p => p.SchemaType.Trim() == DEStandardObjectSchemaType.OuterEntity.ToString()));

        //            this._outerEntities = OuterEntityCollection.FromSchemaObjects(oEntities);
        //        }

        //        return this._outerEntities;
        //    }
        //    set
        //    {
        //        _outerEntities = value;
        //    }
        //}

        ///// <summary>
        ///// 转换为目标结构
        ///// </summary>
        ///// <param name="outerEntityName">目标结构名称</param>
        ///// <returns></returns>
        //public OuterEntity ToOuterEntity(string outerEntityName)
        //{
        //    outerEntityName.CheckStringIsNullOrEmpty<ArgumentNullException>("outerEntityName");

        //    OuterEntity outerEntity = this.OuterEntities.FirstOrDefault(p => p.Name.Equals(outerEntityName));
        //    outerEntity.NullCheck(string.Format("找不到目标结构[{0}]", outerEntityName));

        //    this.Fields.ForEach(f =>
        //    {
        //        f.GetOuterEntityFieldByOuterEntityID(outerEntity.ID);
        //    });

        //    return null;
        //}
        #endregion

        /// <summary>
        /// 清除自身已缓存的数据
        /// </summary>
        public void ClearCacheData()
        {
            string keyEn = this.ID + (TimePointContext.Current.UseCurrentTime).ToString();
            string keyEt1 = this.CodeName + (TimePointContext.Current.UseCurrentTime).ToString();
            ObjectCacheQueue.Instance.Remove(keyEn);
            ObjectCacheQueue.Instance.Remove(keyEt1);
        }

        /// <summary>
        /// 创建该实体定义的实例(提醒一下各位Coder：实体实例不能New出来只能调用此方法获得)
        /// </summary>
        /// <returns></returns>
        public DEEntityInstanceBase CreateInstance()
        {
            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadWithCache(this.ID) as DynamicEntity;
            DEEntityInstanceBase result = new DEEntityInstance(entity.ID);

            result.ID = UuidHelper.NewUuidString();
            result.Name = this.Name + "实体实例";
            result.Description = this.Description;
            return result;
        }
        /// <summary>
        /// 创建该实体定义的实例(提醒一下各位Coder：实体实例不能New出来只能调用此方法获得)
        /// </summary>
        /// <param name="instenceID">流程示例ID</param>
        /// <returns>流程示例</returns>
        public DEEntityInstanceBase CreateInstance(string instenceID)
        {
            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadWithCache(this.ID) as DynamicEntity;
            DEEntityInstanceBase result = new DEEntityInstance(entity.ID);
            result.ID = instenceID.IsNotEmpty() ? instenceID : UuidHelper.NewUuidString();
            result.Name = this.Name + "实体实例";
            result.Description = this.Description;
            return result;
        }
        #region 动态实体的XML序列化

        /// <summary>
        /// 动态实体的XML序列化，包括所有子对象（字段、外部实体）
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            XElement entity = new XElement("Entity");
            this.ToXElement(entity);

            #region 字段集合

            if (this.Fields.Any())
            {
                XElement fields = new XElement("Fields");
                this.Fields.ForEach(f =>
                {
                    XElement field = new XElement("Field");
                    f.ToXElement(field);

                    fields.Add(field);
                });
                entity.Add(fields);
            }

            #endregion

            #region 外部实体
            //if (this.OuterEntities.Any())
            //{
            //    XElement outerEntities = new XElement("OuterEntities");
            //    this.OuterEntities.ForEach(oe => outerEntities.Add(oe.ToXElement()));
            //    entity.Add(outerEntities);
            //}
            #endregion

            return entity;
        }

        /// <summary>
        /// 动态实体的XML反序列化，包含实体基础属性和DynamicEntityField
        /// </summary>
        /// <param name="xEntity">XElement对象</param>
        /// <returns></returns>
        public new void FromXElement(XElement xEntity)
        {
            //反序列实体
            FromString(xEntity.ToString());

            //反序列化实体字段
            this.Fields = new DynamicEntityFieldCollection();
            xEntity.XPathSelectElements("Fields/Field").ForEach(p =>
            {
                DynamicEntityField field = new DynamicEntityField();
                field.FromXElement(p);
                this.Fields.Add(field);
            });

        }

        #endregion

        #region 根据当前对象生成一个新实体
        /// <summary>
        /// 从新给当前实体及其子实体的主键（ID\VersionTime）赋值，从而创建一个新的实体。
        /// </summary>
        /// <param name="categoryID">类别ID</param>
        public void BuildNewEntity(string categoryID)
        {
            foreach (var field in this.Fields)
            {
                field.ID = UuidHelper.NewUuidString();
                field.CreateDate = SNTPClient.AdjustedTime.SimulateTime();
                field.VersionStartTime = DateTime.MinValue;
                field.VersionEndTime = DateTime.MinValue;
            }

            this.ID = UuidHelper.NewUuidString();
            this.CategoryID = categoryID;
            //this.BuidCodeName();
            this.CreateDate = SNTPClient.AdjustedTime.SimulateTime();
            this.VersionStartTime = DateTime.MinValue;
            this.VersionEndTime = DateTime.MinValue;
        }

        #endregion

        /// <summary>
        /// 实体的验证方法
        /// </summary>
        public new void Validate()
        {
            base.Validate();

            StringBuilder error = new StringBuilder();

            //引用实体必须是本类别下的
            if (this.Fields.Any(f => f.ReferenceEntityCodeName.IsNotEmpty() && f.FieldType == FieldTypeEnum.Collection))
            {
                string categoryName = CategoryAdapter.Instance.GetByID(this.CategoryID).FullPath;

                this.Fields.Where(f => f.ReferenceEntityCodeName.IsNotEmpty() && f.FieldType == FieldTypeEnum.Collection).ForEach(
                    f =>
                    {
                        if (!f.ReferenceEntityCodeName.Contains(categoryName))
                        {
                            error.AppendLine(string.Format("字段[{0}\\{1}]不能引用非本类别实体", this.Name, f.Name));
                        }
                    }
                );
            }

            //抛出异常
            (error.Length == 0).FalseThrow(error.ToString());
        }
    }

    /// <summary>
    /// 动态实体<see cref="DynamicEntity"/>集合
    /// </summary>
    [Serializable]
    public class DynamicEntityCollection : DESchemaObjectCollectionBase<DynamicEntity, DynamicEntityCollection>
    {
        protected override DynamicEntityCollection CreateFilterResultCollection()
        {
            return new DynamicEntityCollection();
        }

        public XElement ToXElement()
        {
            XElement xEntities = new XElement("DynamicEntities");

            if (this.Any())
            {
                this.ForEach(entity => xEntities.Add(entity.ToXElement()));
            }

            return xEntities;
        }

        public override string ToString()
        {
            XElement xEntities = this.ToXElement();
            return xEntities.ToString();
        }
    }

    [Serializable]
    public class EntityFieldMappingDisplayItem
    {
        public string FieldID { get; set; }

        public string FieldName { get; set; }

        public string FieldDesc { get; set; }

        public string FieldTypeName { get; set; }

        public int FieldLength { get; set; }

        public string FieldDefaultValue { get; set; }

        /// <summary>
        /// 外部名称
        /// </summary>
        public string DestinationName { get; set; }
    }
}
