using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Schemas;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Dynamics.Validators;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine;
using MCS.Library.SOA.DataObjects.Dynamics.Contract;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance
{
    /// <summary>
    /// 表示模式对象的基类
    /// </summary>
    [Serializable]
    [ORTableMapping("DE.EntityInstance")]
    public abstract class DEEntityInstanceBase : NoVersionedEntityInstanceObjectBase, ISCStatusObject
    {
        /// <summary>
        /// Warnning！ 不序列化定义信息，仅供序列化
        /// </summary>
        [NoMapping]
        [ScriptIgnore]
        public bool _notSerialize { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private EntityFieldValueCollection _Fields = null;

        /// <summary>
        /// 初始化<see cref="DEEntityInstanceBase"/>成员
        /// </summary>
        private DEEntityInstanceBase()
        {
        }

        /// <summary>
        /// 使用指定的模式名初始化<see cref="DEEntityInstanceBase"/>成员
        /// </summary>
        /// <param name="entityID">实体编码</param>
        protected DEEntityInstanceBase(string entityID)
            : base(entityID)
        {
        }

        /// <summary>
        /// 根据CodeName构造实体实例
        /// </summary>
        /// <param name="codeName"></param>
        public void BuildByCodeName(string codeName)
        {
            this.NullCheck<ArgumentNullException>("实体实例不能为NULL");

            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadByCodeName(codeName) as DynamicEntity;

            base._EntityID = entity.ID;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        public virtual ValidationResults Validate()
        {
            DESchemaObjectValidator validator = new DESchemaObjectValidator();

            return validator.Validate(this);
        }

        /// <summary>
        /// 清除相关的数据，例如AllParents、AllChildren等
        /// </summary>
        public void ClearRelativeData()
        {
            this.OnIDChanged();
        }

        ///// <summary>
        ///// 处理ID的变更
        ///// </summary>
        //protected override void OnIDChanged()
        //{
        //    this._AllParentRelations = null;
        //    this._AllParents = null;
        //    this._CurrentlParents = null;
        //}

        protected override EntityFieldValueCollection GetFieldValueCollection()
        {
            if (this._Fields == null)
            {
                this._Fields = this.EntityDefine.Fields.ToFieldValueCollection();
            }

            return this._Fields;
        }

        /// <summary>
        /// 将实例转化为契约格式
        /// </summary>
        /// <param name="outerEntityName">目标转换结构名称</param>
        public List<SapValue> ToParams(string outerEntityName)
        {
            outerEntityName.CheckStringIsNullOrEmpty<ArgumentNullException>("outerEntityName");
            //抬头实体的映射实体
            //OuterEntity headerOuterEntity = this.EntityDefine.OuterEntities.FirstOrDefault(p => p.Name.Equals(outerEntityName));
            //headerOuterEntity.NullCheck(string.Format("找不到名称为[{0}]的目标结构", outerEntityName));

            List<SapValue> result = new List<SapValue>();

            #region
            //this.Fields.ForEach(field =>
            //{
            //    //普通类型
            //    if (field.Definition.FieldType != FieldTypeEnum.Collection)
            //    {
            //        //映射的外部字段
            //        OuterEntityField outerEntityField = field.Definition.GetOuterEntityFieldByOuterEntityID(headerOuterEntity.ID);
            //        if (outerEntityField != null)
            //        {
            //            result.Add(new SapValue()
            //            {
            //                Key = outerEntityField.Name,
            //                Value = ConvertUepFiledValueMapping.ConvertUEPFiledValue(field.Definition.FieldType.ToString(), field.StringValue)
            //            });
            //        }
            //    }
            //    else//集合类型
            //    {
            //        #region 集合类型字段的映射关系
            //        OuterEntityField collEntityField = field.Definition.GetOuterEntityFieldByOuterEntityID(headerOuterEntity.ID);

            //        if (collEntityField != null)
            //        {
            //            //集合对象内容
            //            DEEntityInstanceBaseCollection colls = field.GetRealValue() as DEEntityInstanceBaseCollection;
            //            if (colls != null)
            //            {
            //                #region
            //                List<SapValue> items = new List<SapValue>();

            //                #region

            //                //集合实体所映射的外部实体
            //                OuterEntity collEntity = field.Definition.ReferenceEntity.OuterEntities.FirstOrDefault(p => p.Name.Equals(collEntityField.Name));
            //                collEntity.NullCheck(string.Format("没有找到集合类型[{0}]的映射实体", field.Definition.Name));

            //                //遍历集合对象内容
            //                colls.ForEach(p =>
            //                {
            //                    //自定义集合明细
            //                    SapValue item = new SapValue();
            //                    item.Key = p.ID; //以前是p.Name
            //                    List<SapValue> children = new List<SapValue>();

            //                    #region 明细字段
            //                    p.Fields.ForEach(f =>
            //                    {
            //                        //引用实体字段对应字段
            //                        OuterEntityField fieldMappingField = f.Definition.GetOuterEntityFieldByOuterEntityID(collEntity.ID);

            //                        if (fieldMappingField != null)
            //                        {
            //                            #region add by chen qiang 2014-11-05  如果子表中在有嵌套的话


            //                            if (f.Definition.FieldType == FieldTypeEnum.Collection)
            //                            {
            //                                //先获取实体实例
            //                                DEEntityInstanceBaseCollection fieldInstanceBaseCollection = f.GetRealValue() as DEEntityInstanceBaseCollection;
            //                                List<SapValue> childrenTableValue = new List<SapValue>();
            //                                if (fieldInstanceBaseCollection != null)
            //                                {
            //                                    fieldInstanceBaseCollection.ForEach(fieldInstance =>
            //                                    {
            //                                        OuterEntity outerFieldEntity =
            //                                            fieldInstance.EntityDefine.OuterEntities.FirstOrDefault();

            //                                        if (outerFieldEntity != null)
            //                                        {
            //                                            childrenTableValue.Add(new SapValue()
            //                                            {
            //                                                Key = fieldMappingField.Name,
            //                                                Value = fieldInstance.ToParams(outerFieldEntity.Name)
            //                                            });
            //                                        }
            //                                    });
            //                                    if (childrenTableValue.Any())
            //                                    {
            //                                        children.Add(new SapValue()
            //                                        {
            //                                            Key = fieldMappingField.Name,
            //                                            Value = childrenTableValue
            //                                        });
            //                                    }
            //                                }

            //                            }
            //                            #endregion
            //                            else
            //                            {
            //                                children.Add(new SapValue()
            //                                 {
            //                                     Key = fieldMappingField.Name,
            //                                     Value = ConvertUepFiledValueMapping.ConvertUEPFiledValue(f.Definition.FieldType.ToString(), f.StringValue),
            //                                 });
            //                            }

            //                        }
            //                    });
            //                    #endregion

            //                    item.Value = children;
            //                    items.Add(item);
            //                });
            //                #endregion

            //                result.Add(new SapValue()
            //                {
            //                    Key = field.Definition.Name,
            //                    Value = items
            //                });

            //                #endregion
            //            }
            //        }
            //        #endregion
            //    }
            //});
            #endregion

            return result;
        }

        /// <summary>
        /// 将契约格式转化为实例
        /// </summary>
        /// <param name="keyValues">契约内容</param>
        public void FromParams(List<SapValue> keyValues)
        {
            this.NullCheck<ArgumentNullException>("实体实例不能为NULL");

            SetInstanceValue(this, keyValues);
        }

        private void SetInstanceValue(DEEntityInstanceBase instance, List<SapValue> keyvalues)
        {
            keyvalues.ForEach(p =>
            {
                //找到实例中对应的键值
                EntityFieldValue fieldValue = instance.Fields.FirstOrDefault(f => f.Definition.Name.Equals(p.Key));

                if (fieldValue != null)
                {
                    if (p.Value is string)
                    {
                        instance.Fields.TrySetValue<string>(p.Key, p.Value.ToString().Trim());
                    }
                    else
                    {
                        //集合类型
                        var childValue = p.Value as List<SapValue>;
                        var refInstanceColl = fieldValue.GetRealValue() as DEEntityInstanceBaseCollection;

                        if (childValue != null)
                        {
                            if (refInstanceColl != null && childValue.Count == refInstanceColl.Count)
                            {
                                //调用RFC之前该实例集合已有值，现在只需将SAP返回值更新至该实例即可
                                int index = 0;
                                childValue.ForEach(child =>
                                {
                                    DEEntityInstanceBase ins = refInstanceColl[index++];
                                    if (ins != null)
                                    {
                                        SetInstanceValue(ins, child.Value as List<SapValue>);
                                    }
                                });
                            }
                            else
                            {
                                //调用RFC之前该实体没有值（有可能是null）,现在需要将SAP返回值转换为实例集合类型将赋值给fieldValue
                                DEEntityInstanceBaseCollection coll = new DEEntityInstanceBaseCollection();

                                //新添加  判断  如果是 Struct类型 时 应该返回 一条的子表数据类型  但是返回的不是集合类型  所以加一个这样的判断来 接收返回数据
                                if (fieldValue.Definition.IsStruct)
                                {
                                    DEEntityInstanceBase ins = fieldValue.Definition.ReferenceEntity.CreateInstance();
                                    childValue.ForEach(child =>
                                    {
                                        if (child.Value is string)
                                        {
                                            ins.Fields.TrySetValue<string>(child.Key, child.Value.ToString().Trim());
                                        }
                                    });
                                    coll.Add(ins);
                                }
                                else
                                {
                                    childValue.ForEach(child =>
                                    {
                                        DEEntityInstanceBase ins = fieldValue.Definition.ReferenceEntity.CreateInstance();
                                        //递归方法
                                        SetInstanceValue(ins, child.Value as List<SapValue>);
                                        coll.Add(ins);

                                    });
                                }


                                instance.Fields.TrySetValue<DEEntityInstanceBaseCollection>(p.Key, coll);
                            }
                        }
                    }
                }
            });
        }
    }

    /// <summary>
    /// 表示模式对象的集合
    /// </summary>
    [Serializable]
    public class DEEntityInstanceBaseCollection : DEInstanceEditableKeyedCollectionBase<DEEntityInstanceBase, DEEntityInstanceBaseCollection>
    {
        public DEEntityInstanceBaseCollection()
        {
        }

        public DEEntityInstanceBaseCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// 合并两个集合。结果是两个集合的并集
        /// </summary>
        /// <param name="source"></param>
        public virtual void Merge(DEEntityInstanceBaseCollection source)
        {
            if (source != null)
                source.ForEach(this.AddNotExistsItem);
        }

        protected override string GetKeyForItem(DEEntityInstanceBase item)
        {
            return item.ID;
        }

        protected override DEEntityInstanceBaseCollection CreateFilterResultCollection()
        {
            return new DEEntityInstanceBaseCollection();
        }

        public DEEntityInstanceBaseCollection FromXElement(XElement element)
        {
            element.NullCheck<ArgumentNullException>("element");

            DEEntityInstanceBaseCollection result = new DEEntityInstanceBaseCollection();

            return result;
        }

        public override string ToString()
        {
            XElement element = new XElement("Children");

            if (!this.Any())
            {
                element.SetAttributeValue("GroupEntityID", string.Empty);
                return element.ToString();
            }

            element.SetAttributeValue("GroupEntityID", this.FirstOrDefault().EntityDefine.ID);

            this.ForEach(p =>
            {
                XElement child = XElement.Parse(p.ToString());

                element.Add(child);
            });

            return element.ToString();
        }
    }
}
