using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using System.Xml.Linq;
using MCS.Web.Library.Script;
using System.Collections;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics.Converters
{
    /// <summary>
    /// 实体字段值的序列化器
    /// </summary>
    public class EntityFieldValueConverter : JavaScriptConverter
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            EntityFieldValue data = (EntityFieldValue)obj;

            //dictionary.AddNonDefaultValue("FieldID", data.Definition.ID);
            dictionary.AddNonDefaultValue("CodeName", data.Definition.CodeName);
            if (!string.IsNullOrEmpty(data.Definition.ReferenceEntityCodeName))
            {
                dictionary.AddNonDefaultValue("EntityCodeName", data.Definition.ReferenceEntityCodeName);
            }
            dictionary.AddNonDefaultValue("FieldName", data.Definition.Name);

            if (data.Definition.FieldType == Enums.FieldTypeEnum.Collection)
            {
                DEEntityInstanceBaseCollection coll = data.GetRealValue() as DEEntityInstanceBaseCollection;
                if (coll != null)
                {
                    coll.ForEach(i =>
                    {
                        i._notSerialize = true;
                    });
                    dictionary.AddNonDefaultValue("StringValue", coll);
                }
                //else
                //{
                //    dictionary.AddNonDefaultValue("StringValue", " ");
                //}

            }
            else
            {
                if (string.IsNullOrEmpty(data.StringValue))
                {
                    //dictionary.AddNonDefaultValue("StringValue", " ");
                }
                else
                {
                    dictionary.AddNonDefaultValue("StringValue", data.StringValue);
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="type"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            //反序列化
            EntityFieldValue data = new EntityFieldValue();
            string codeName = dictionary.GetValue("CodeName", string.Empty);
            string fieldName = dictionary.GetValue("FieldName", string.Empty);
            //todo:这需要做时间穿梭测试

            DynamicEntity entity = null;

            entity = DEDynamicEntityAdapter.Instance.LoadByCodeName(codeName) as DynamicEntity;
            entity.NullCheck("反序列化字段是找不到字段所对应的实体");

            var field = entity.Fields.FirstOrDefault(p => p.Name.Equals(fieldName));

            field.NullCheck(string.Format("EntityFieldValue反序列化出错,不能找到名称为{0}的对象！", fieldName));

            data.Definition = field;
            object stringValue = string.Empty;
            dictionary.TryGetValue("StringValue", out stringValue);
            if (data.Definition.FieldType == Enums.FieldTypeEnum.Collection)
            {
                if (stringValue != null && !string.IsNullOrEmpty(stringValue.ToString().Trim()))
                {
                    var values = JSONSerializerExecute.Deserialize<List<DEEntityInstanceBase>>(dictionary.GetValue("StringValue", new ArrayList())) ?? new List<DEEntityInstanceBase>();

                    DEEntityInstanceBaseCollection intances = new DEEntityInstanceBaseCollection();
                    foreach (var instanceBase in values)
                    {
                        if (string.IsNullOrEmpty(instanceBase.ID))
                        {
                            instanceBase.ID = Guid.NewGuid().ToString();
                        }
                        intances.Add(instanceBase);
                    }
                    data.StringValue = intances.ToString();
                }
            }
            else
            {
                data.StringValue = dictionary.GetValue("StringValue", Convert.ToString(stringValue));
            }


            return data;
        }

        ///// <summary>
        ///// 反序列化
        ///// </summary>
        ///// <param name="dictionary"></param>
        ///// <param name="type"></param>
        ///// <param name="serializer"></param>
        ///// <returns></returns>
        //public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        //{
        //    //反序列化
        //    EntityFieldValue data = new EntityFieldValue();
        //    string fieldID = dictionary.GetValue("FieldID", string.Empty);

        //    //todo:这需要做时间穿梭测试
        //    var field = DESchemaObjectAdapter.Instance.Load(fieldID, DateTime.Now) as DynamicEntityField;
        //    field.NullCheck(string.Format("EntityFieldValue反序列化出错,不能找到ID为{0}的对象！", fieldID));

        //    data.Definition = field;

        //    if (data.Definition.FieldType == Enums.FieldTypeEnum.Collection)
        //    {
        //        var values = JSONSerializerExecute.Deserialize<List<DEEntityInstanceBase>>(dictionary.GetValue("StringValue", new ArrayList())) ?? new List<DEEntityInstanceBase>();

        //        DEEntityInstanceBaseCollection intances = new DEEntityInstanceBaseCollection();
        //        foreach (var instanceBase in values)
        //        {
        //            if (string.IsNullOrEmpty(instanceBase.ID))
        //            {
        //                instanceBase.ID = Guid.NewGuid().ToString();
        //            }
        //            intances.Add(instanceBase);
        //        }
        //        data.StringValue = intances.ToString();

        //    }
        //    else
        //    {
        //        data.StringValue = dictionary.GetValue("StringValue", string.Empty);
        //    }


        //    return data;
        //}

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new Type[] { typeof(EntityFieldValue) };
            }
        }
    }
}
