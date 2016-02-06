using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine
{
    /// <summary>
    /// 表示<see cref="EntityFieldValue"/>的集合
    /// </summary>
    [Serializable]
    [XElementSerializable]
    public class EntityFieldValueCollection : SerializableEditableKeyedDataObjectCollectionBase<string, EntityFieldValue>
    {
        /// <summary>
        /// 初始化<see cref="EntityFieldValueCollection"/>的新实例
        /// </summary>
        public EntityFieldValueCollection()
        {
        }

        /// <summary>
        /// 使用指定的<see cref="SerializationInfo"/>和<see cref="StreamingContext"/>初始化<see cref="EntityFieldValueCollection"/>的新实例
        /// </summary>
        /// <param name="info">存储将对象序列化或反序列化所需的全部数据</param>
        /// <param name="context">序列化描述的上下文</param>
        protected EntityFieldValueCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        ///// <summary>
        ///// 使用指定的<see cref="SchemaDefine"/>初始化<see cref="EntityFieldValueCollection"/>的新实例
        ///// </summary>
        ///// <param name="schema">表示模式定义的<see cref="SchemaDefine"/></param>
        //public EntityFieldValueCollection(SchemaDefine schema)
        //{
        //    if (schema != null)
        //        schema.Properties.ForEach(pd => this.Add(new EntityFieldValue(pd)));
        //}

        /// <summary>
        /// 获取集合中指定的<see cref="SchemaTabDefine"/>的键。
        /// </summary>
        /// <param name="item">获取其键的<see cref="EntityFieldValue"/></param>
        /// <returns>表示键的字符串</returns>
        protected override string GetKeyForItem(EntityFieldValue item)
        {
            return item.Definition.Name;
        }

        /// <summary>
        /// 获取集合中指定键所对应的值
        /// </summary>
        /// <typeparam name="T">默认值的类型</typeparam>
        /// <param name="name">属性的名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns> 属性的值 或 缺省值</returns>
        public T GetValue<T>(string name, T defaultValue)
        {
            name.CheckStringIsNullOrEmpty("key");

            T result = defaultValue;

            EntityFieldValue v = this[name];

            if (v != null)
            {
                result = (T)((object)v.StringValue);

                //if (result != null)
                //    result = (T)DataConverter.ChangeType(v.StringValue, result.GetType());
                //else
                //{
                //    Type targetType = null;
                //    if (v.Definition.DataType == PropertyDataType.Enum)
                //        targetType = Type.GetType(v.Definition.EditorParams);
                //    else
                //        targetType = v.Definition.DataType.ToRealType();

                //    object realValue = DataConverter.ChangeType(v.StringValue, targetType);

                //    result = (T)DataConverter.ChangeType(realValue, typeof(T));
                //}
            }

            return result;

        }

        /// <summary>
        /// 设置属性的值。如果该属性不存在，则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SetValue<T>(string name, T data)
        {
            TrySetValue(name, data).FalseThrow("不能找到名称为{0}的属性", name);
        }

        /// <summary>
        /// 尝试去设置属性。如果该属性不存在，则返回false，否则返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool TrySetValue<T>(string name, T data)
        {
            name.CheckStringIsNullOrEmpty("key");

            EntityFieldValue v = this[name];

            bool result = (v != null);

            if (result)
            {
                if (data != null)
                    v.StringValue = data.ToString();
                else
                    v.StringValue = string.Empty;
            }

            return result;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="propDefinitions"></param>
        //public void InitFromPropertyDefineCollection(SchemaPropertyDefineCollection propDefinitions)
        //{
        //    this.Clear();

        //    AppendFromPropertyDefineCollection(propDefinitions);
        //}

        //public void AppendFromPropertyDefineCollection(SchemaPropertyDefineCollection propDefinitions)
        //{
        //    foreach (SchemaPropertyDefine propDef in propDefinitions)
        //    {
        //        EntityFieldValue pv = new EntityFieldValue(propDef);

        //        this.Add(pv);
        //    }
        //}

        ///// <summary>
        ///// 当前的属性值集合与新的属性定义进行合并
        ///// </summary>
        ///// <param name="definedProperties"></param>
        //public void MergeDefinedProperties(PropertyDefineCollection definedProperties)
        //{
        //    foreach (SchemaPropertyDefine pd in definedProperties)
        //    {
        //        EntityFieldValue pv = this[pd.Name];

        //        if (pv == null)
        //        {
        //            this.Add(new EntityFieldValue(pd));
        //        }
        //        else
        //        {
        //            if (pv.Definition.AllowOverride)
        //            {
        //                pv.Definition = pd;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 从PropertyValue的集合导入数据。仅仅导入StringValue，没有定义部分
        /// </summary>
        /// <param name="values"></param>
        public void FromPropertyValues(IEnumerable<PropertyValue> values)
        {
            if (values != null)
            {
                foreach (PropertyValue pv in values)
                {
                    EntityFieldValue spv = this[pv.Definition.Name];

                    if (spv != null)
                        spv.FromPropertyVaue(pv);
                }
            }
        }
        /// <summary>
        /// 转换为PropertyValue集合
        /// </summary>
        /// <returns></returns>
        public PropertyValueCollection ToPropertyValues()
        {
            PropertyValueCollection result = new PropertyValueCollection();
            this.OrderBy(p => p.SortOrder).ForEach(spv =>
            {
                PropertyValue pv = spv.Definition.ToDynamicPropertyValue();
                pv.StringValue = spv.StringValue;
                result.Add(pv);
            });

            return result;
        }

        public void CopyFiledValueFromPropertyValues(PropertyValueCollection propertys)
        {
            propertys.ForEach(p =>
            {
                string key = p.Definition.Name;
                if (this.ContainsKey(key))
                {
                    this.SetValue(key, p.GetRealValue());
                }
            });
        }

        //public void Write()
        //{
        //    Dictionary<string, IPropertyPersister<EntityFieldValue>> dicPers = PropertiesPersisterHelper<EntityFieldValue>.GetAllPropertiesPersisters();

        //    if (dicPers.Count > 0)
        //    {
        //        using (PersisterContext<EntityFieldValue> context = PersisterContext<EntityFieldValue>.CreatePersisterContext(this, null))
        //        {
        //            foreach (EntityFieldValue item in this)
        //            {
        //                if (item.Definition.PersisterKey.IsNotEmpty())
        //                {
        //                    if (dicPers.ContainsKey(item.Definition.PersisterKey))
        //                        dicPers[item.Definition.PersisterKey].Write(item, context);
        //                }
        //            }
        //        }
        //    }
        //}

        //public void Read()
        //{
        //    Dictionary<string, IPropertyPersister<EntityFieldValue>> dicPers = PropertiesPersisterHelper<EntityFieldValue>.GetAllPropertiesPersisters();

        //    if (dicPers.Count > 0)
        //    {
        //        using (PersisterContext<EntityFieldValue> context = PersisterContext<EntityFieldValue>.CreatePersisterContext(this, null))
        //        {
        //            foreach (EntityFieldValue item in this)
        //            {
        //                if (item.Definition.PersisterKey.IsNotEmpty())
        //                {
        //                    if (dicPers.ContainsKey(item.Definition.PersisterKey))
        //                        dicPers[item.Definition.PersisterKey].Read(item, context);
        //                }
        //            }
        //        }
        //    }
        //}


        //public static implicit operator PropertyValueCollection(EntityFieldValueCollection spvc)
        //{
        //    spvc.NullCheck("spvc");

        //    return spvc.ToPropertyValues();
        //}
    }
}
