using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Web.Library.Script;

namespace MCS.Library.SOA.DataObjects.Dynamics.Converters
{
    /// <summary>
    /// 实体定义的序列化器
    /// </summary>
    public class DynamicEntityConvert : JavaScriptConverter
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

            DynamicEntity data = (DynamicEntity)obj;

            dictionary.AddNonDefaultValue("ID", data.ID);
            dictionary.AddNonDefaultValue("SchemaType", data.SchemaType);
            dictionary.AddNonDefaultValue("CategoryID", data.CategoryID);
            dictionary.AddNonDefaultValue("Name", data.Name);
            dictionary.AddNonDefaultValue("Description", data.Description);
            dictionary.AddNonDefaultValue("CodeName", data.CodeName);
            dictionary.AddNonDefaultValue("Tag", data.Tag);
            //dictionary.AddNonDefaultValue("CreateDate", data.CreateDate.Ticks.ToString());
            //dictionary.AddNonDefaultValue("CreateDate", data.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            dictionary.AddNonDefaultValue("CreateDate", data.CreateDate);
            dictionary.AddNonDefaultValue("Fields", data.Fields);

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
            //校验
            string schemaType = dictionary.GetValue("SchemaType", string.Empty);
            DEStandardObjectSchemaType.DynamicEntity.ToString().Equals(schemaType).FalseThrow("DynamicEntityConvert不能处理该对象");

            //反序列化
            DynamicEntity data = SchemaExtensions.CreateObject(DEStandardObjectSchemaType.DynamicEntity.ToString()) as DynamicEntity;

            data.ID = dictionary.GetValue("ID", string.Empty);
            data.Name = dictionary.GetValue("Name", string.Empty);
            data.CategoryID = dictionary.GetValue("CategoryID", string.Empty);
            data.Description = dictionary.GetValue("Description", string.Empty);
            data.CreateDate = dictionary.GetValue("CreateDate", DateTime.MinValue);
            data.Tag = dictionary.GetValue("Tag", string.Empty);
            data.Properties.SetValue("CodeName", dictionary.GetValue("CodeName", string.Empty));
            data.Fields = JSONSerializerExecute.Deserialize<DynamicEntityFieldCollection>(dictionary.GetValue("Fields", new ArrayList())) ?? new DynamicEntityFieldCollection();

            return data;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new Type[] { typeof(DynamicEntity) };
            }
        }
    }
}
