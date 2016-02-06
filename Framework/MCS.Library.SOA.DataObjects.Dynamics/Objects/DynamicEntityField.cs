using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;
using MCS.Web.Library.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 动态实体字段
    /// </summary>
    [Serializable]
    public class DynamicEntityField : DEBase, IDEMemberObject, IDEContainerObject, IXElementSerializable
    {
        public DynamicEntityField()
            : base(DEStandardObjectSchemaType.DynamicEntityField.ToString())
        {
        }

        public DynamicEntityField(string schemaType)
            : base(schemaType)
        {

        }

        /// <summary>
        /// 是否在快照表中
        /// </summary>
        [NoMapping]
        public bool IsInSnapshot
        {
            get
            {
                return this.Properties.GetValue("IsInSnapshot", false);
            }
            set
            {
                this.Properties.SetValue("IsInSnapshot", value);
            }
        }

        /// <summary>
        /// 长度
        /// </summary>
        [NoMapping]
        public int Length
        {
            get
            {
                return this.Properties.GetValue("Length", 0);
            }
            set
            {
                this.Properties.SetValue("Length", value);
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        [NoMapping]
        public FieldTypeEnum FieldType
        {
            get
            {
                return this.Properties.GetValue("FieldType", FieldTypeEnum.String);
            }
            set
            {
                this.Properties.SetValue("FieldType", value);
            }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        [NoMapping]
        public string DefaultValue
        {
            get
            {
                return this.Properties.GetValue("DefaultValue", string.Empty);
            }
            set
            {
                this.Properties.SetValue("DefaultValue", value);
            }
        }
        /// <summary>
        /// 验证规则定义JSON数据
        /// </summary>
        [NoMapping]
        public string ValidatorDefine
        {
            get
            {
                return this.Properties.GetValue("ValidatorDefine", string.Empty);
            }
            set
            {
                this.Properties.SetValue("ValidatorDefine", value);
            }
        }

        /// <summary>
        /// 验证器
        /// </summary>
        [ScriptIgnore]
        [NoMapping]
        public List<Validator> Validators
        {
            get
            {
                List<Validator> validators = new List<Validator>();

                string json = this.ValidatorDefine;

                if (json.IsNotEmpty())
                {
                    List<ValidatorDefine> list = JSONSerializerExecute.DeserializeObject(json) as List<ValidatorDefine>;

                    if (list != null && list.Count > 0)
                    {
                        list.ForEach(vd => validators.Add(vd.ToValidator()));
                    }
                }

                return validators;
            }
        }

        private DynamicEntity _ReferenceEntity = null;
        public DynamicEntity ReferenceEntity
        {
            get
            {
                if (this._ReferenceEntity == null && ReferenceEntityCodeName.IsNotEmpty())
                {
                    _ReferenceEntity = DEDynamicEntityAdapter.Instance.LoadByCodeName(this.ReferenceEntityCodeName) as DynamicEntity;
                }

                return _ReferenceEntity;
            }
        }

        /// <summary>
        /// 引用实体CodeName
        /// </summary>
        [NoMapping]
        public string ReferenceEntityCodeName
        {
            get
            {
                return this.Properties.GetValue("ReferenceEntityCodeName", string.Empty);
            }
            set
            {
                this.Properties.SetValue("ReferenceEntityCodeName", value);
            }
        }

        /// <summary>
        /// 参数方向
        /// </summary>
        [NoMapping]
        public ParamDirectionEnum ParamDirection
        {
            get
            {
                return this.Properties.GetValue("ParamDirection", ParamDirectionEnum.NotKnown);
            }
            set
            {
                this.Properties.SetValue("ParamDirection", value);
            }
        }

        #region ToDynamicProperties
        public string Category
        {
            get
            {
                return this.Properties.GetValue("Category", string.Empty);
            }
            set
            {
                this.Properties.SetValue("Category", value);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this.Properties.GetValue("ReadOnly", false);
            }
            set
            {
                this.Properties.SetValue("ReadOnly", value);
            }
        }

        public string DisplayName
        {
            get
            {
                return this.Properties.GetValue("DisplayName", string.Empty);
            }
            set
            {
                this.Properties.SetValue("DisplayName", value);
            }
        }

        public int MaxLength
        {
            get
            {
                return this.Properties.GetValue("MaxLength", 0);
            }
            set
            {
                this.Properties.SetValue("MaxLength", value);
            }
        }

        public bool IsRequired
        {
            get
            {
                return this.Properties.GetValue("IsRequired", false);
            }
            set
            {
                this.Properties.SetValue("IsRequired", value);
            }
        }

        public bool ShowTitle
        {
            get
            {
                return this.Properties.GetValue("ShowTitle", false);
            }
            set
            {
                this.Properties.SetValue("ShowTitle", value);
            }
        }

        public bool Visible
        {
            get
            {
                return this.Properties.GetValue("Visible", true);
            }
            set
            {
                this.Properties.SetValue("Visible", value);
            }
        }

        public string EditorKey
        {
            get
            {
                return this.Properties.GetValue("EditorKey", "StandardPropertyEditor");
            }
            set
            {
                this.Properties.SetValue("EditorKey", value);
            }
        }


        public string EditorParams
        {
            get
            {
                return this.Properties.GetValue("EditorParams", string.Empty);
            }
            set
            {
                this.Properties.SetValue("EditorParams", value);
            }
        }

        /// <summary>
        /// 是否结构
        /// </summary>
        [NoMapping]
        public bool IsStruct
        {
            get
            {
                return this.Properties.GetValue("IsStruct", false);
            }
            set
            {
                this.Properties.SetValue("IsStruct", value);
            }
        }
        #endregion ToDynamicProperties

        #region IDEMemberObject
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

        public DEObjectContainerRelationCollection GetCurrentMemberOfRelations()
        {
            return this.CurrentMemberOfRelations;
        }

        #endregion

        public PropertyDefine ToDynamicPropertyDefine()
        {
            PropertyDefine pd = new PropertyDefine();

            pd.Name = this.Name;
            pd.Description = this.Description;
            pd.DefaultValue = this.DefaultValue;
            pd.Category = this.Category;
            pd.DisplayName = this.DisplayName;
            pd.SortOrder = this.SortNo;
            pd.Visible = this.Visible;
            pd.ShowTitle = this.ShowTitle;
            pd.IsRequired = this.IsRequired;
            pd.MaxLength = this.MaxLength;
            pd.ReadOnly = this.ReadOnly;
            pd.DataType = this.FieldType.ToPropertyDataType();
            pd.EditorKey = this.EditorKey;
            pd.EditorParams = this.EditorParams;
            //add validators
            this.Validators.ForEach(v => pd.Validators.Add(v));
            return pd;
        }

        public void CopyFromPropertyDefine(PropertyDefine propertyDefine)
        {
            this.ID = UuidHelper.NewUuidString();
            this.CreateDate = SNTPClient.AdjustedTime;
            this.Creator = DeluxeIdentity.CurrentUser;
            this.Name = propertyDefine.Name;
            this.Length = propertyDefine.MaxLength;
            this.Description = propertyDefine.Description;
            this.DefaultValue = propertyDefine.DefaultValue;
            this.Category = propertyDefine.Category;
            this.DisplayName = propertyDefine.DisplayName;
            this.SortNo = propertyDefine.SortOrder;
            this.Visible = propertyDefine.Visible;
            this.ShowTitle = propertyDefine.ShowTitle;
            this.IsRequired = propertyDefine.IsRequired;
            this.MaxLength = propertyDefine.MaxLength;
            this.ReadOnly = propertyDefine.ReadOnly;
            this.EditorKey = propertyDefine.EditorKey;
            this.EditorParams = propertyDefine.EditorParams;
              
            switch (propertyDefine.DataType)
            {
                case PropertyDataType.String:
                    this.FieldType = FieldTypeEnum.String;
                    break;
                case PropertyDataType.Integer:
                    this.FieldType = FieldTypeEnum.Int;
                    break;
                case PropertyDataType.Boolean:
                    this.FieldType = FieldTypeEnum.Bool;
                    break;
                case PropertyDataType.DateTime:
                    this.FieldType = FieldTypeEnum.DateTime;
                    break;
                case PropertyDataType.Decimal:
                    this.FieldType = FieldTypeEnum.Decimal;
                    break;
                default:
                    this.FieldType = FieldTypeEnum.String;
                    break;
            }

            //add validators
            //propertyDefine.Validators.ForEach(v => {this.Validators.Add(v)});
        }

        public PropertyValue ToDynamicPropertyValue()
        {
            PropertyDefine pd = this.ToDynamicPropertyDefine();

            return new PropertyValue(pd);
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
        public DESchemaObjectCollection GetCurrentMembers()
        {
            return this.CurrentMembers;
        }

        public void Serialize(System.Xml.Linq.XElement node, XmlSerializeContext context)
        {
            throw new NotImplementedException();
            //参考MCS.Library.SOA.DataObjects.PropertyDefine
        }

        public void Deserialize(System.Xml.Linq.XElement node, XmlDeserializeContext context)
        {
            throw new NotImplementedException();
            //参考MCS.Library.SOA.DataObjects.PropertyDefine
        }

        public DynamicEntity Entity
        {
            get
            {
                var relation = this.AllMemberOfRelations.Where(p => p.ContainerSchemaType == "DynamicEntity").FirstOrDefault();
                DynamicEntity entity = null;
                if (relation != null)
                {
                    entity = relation.Container as DynamicEntity;
                }
                return entity;

            }
        }
    }

    /// <summary>
    ///  表示<see cref="DynamicEntityField"/>的集合
    /// </summary>
    [Serializable]
    public class DynamicEntityFieldCollection : DESchemaObjectEditableKeyedCollectionBase<DynamicEntityField, DynamicEntityFieldCollection>
    {
        public SchemaPropertyValueCollection ToProperties()
        {
            SchemaPropertyValueCollection result = new SchemaPropertyValueCollection();

            this.ForEach(p =>
            {
                SchemaPropertyDefine pd = new SchemaPropertyDefine
                {
                    Name = p.Name,
                    Description = p.Description,
                    DefaultValue = p.DefaultValue
                };
                result.Add(new SchemaPropertyValue(pd));
            });

            return result;
        }


        public EntityFieldValueCollection ToFieldValueCollection()
        {
            EntityFieldValueCollection result = new EntityFieldValueCollection();

            this.OrderBy(p => p.SortNo).ForEach(p =>
            {
                result.Add(new EntityFieldValue(p));
            });

            return result;
        }
        /// <summary>
        /// 转换为PropertyValue集合
        /// </summary>
        /// <returns></returns>
        public PropertyValueCollection ToPropertyValues()
        {
            PropertyValueCollection result = new PropertyValueCollection();
            this.OrderBy(p => p.SortNo).ForEach(spv =>
            {
                PropertyValue pv = spv.ToDynamicPropertyValue();
                //为PropertyForm布局是添加默认的section
                if (string.IsNullOrEmpty(pv.Definition.Category))
                {
                    pv.Definition.Category = "section1";
                }
                result.Add(pv);
            });

            return result;
        }
        /// <summary>
        /// 从属性定义集合中复制字段定义
        /// </summary>
        /// <param name="propertyDefineCollection"></param>
        public void CopyFromPropertyDefineCollection(PropertyDefineCollection propertyDefineCollection)
        {
            propertyDefineCollection.ForEach(p => 
            {
                DynamicEntityField field = new DynamicEntityField();
                field.CopyFromPropertyDefine(p);
                this.Add(field);
            });
        }

        public static DynamicEntityFieldCollection FromSchemaObjects(DESchemaObjectCollection schemaObjectCollection)
        {
            schemaObjectCollection.NullCheck<ArgumentNullException>("schemaObjectCollection");

            var temp = new DynamicEntityFieldCollection();
            schemaObjectCollection.ForEach(p => temp.Add((DynamicEntityField)p));

            var result = new DynamicEntityFieldCollection();

            result.CopyFrom(temp.OrderBy(p => p.SortNo));

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
        protected override DynamicEntityFieldCollection CreateFilterResultCollection()
        {
            return new DynamicEntityFieldCollection();
        }

        /// <summary>
        /// 获取集合中指定的<see cref="DynamicEntityField"/>的键。
        /// </summary>
        /// <param name="item">获取其键的<see cref="DynamicEntityField"/></param>
        /// <returns>表示键的字符串</returns>
        protected override string GetKeyForItem(DynamicEntityField item)
        {
            return item.ID;
        }
    }
}
