using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Converters
{
    /// <summary>
    /// 实体定义字段序列化器
    /// </summary>
    public class DynamicEntityFieldConverter : JavaScriptConverter
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var dictionary = new Dictionary<string, object>();

            var data = (DynamicEntityField)obj;

            data.Properties.ForEach(p =>
            {
                dictionary.AddNonDefaultValue(p.Definition.Name, p.StringValue);
            });

            if (data.FieldType == Enums.FieldTypeEnum.Collection)
            {
                dictionary.AddNonDefaultValue("DynamicEntity", data.ReferenceEntity);
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
            DESchemaObjectBase data = SchemaExtensions.CreateObject(DEStandardObjectSchemaType.DynamicEntityField.ToString());

            dictionary.ForEach(p =>
            {
                data.Properties.TrySetValue(p.Key, p.Value);
            });

            return data;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new Type[] { typeof(DynamicEntityField) };
            }
        }
    }
}
