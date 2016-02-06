using System.Text;
using System.Web.Script.Serialization;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System.Collections.Generic;
using System;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using System.Linq;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine;
using MCS.Web.Library.Script;
using System.Collections;
using MCS.Library.Principal;

namespace MCS.Library.SOA.DataObjects.Dynamics.Converters
{
    /// <summary>
    /// 实体实例序列化器
    /// </summary>
    public class DEEntityInstanceConverter : JavaScriptConverter
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
            DEEntityInstanceBase data = (DEEntityInstanceBase)obj;

            dictionary.AddNonDefaultValue("EntityCode", data.EntityCode);
            dictionary.AddNonDefaultValue("EntityCodeName", data.EntityDefine.CodeName);
            dictionary.AddNonDefaultValue("EntityInstanceCode", data.ID);
            if (!data._notSerialize)
            {
                dictionary.AddNonDefaultValue("EntityDefine", data.EntityDefine);
            }

            dictionary.AddNonDefaultValue("EntityFieldValue", data.Fields);

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
            object EntityCodeName = null;
            object instanceID = null;
            DateTime createDate = DateTime.MinValue;

            dictionary.TryGetValue("EntityInstanceCode", out instanceID);

            dictionary.TryGetValue("EntityCodeName", out EntityCodeName);
            EntityCodeName.NullCheck("实体定义反序化失败,缺少EntityCodeName.");

            createDate = DateTime.MinValue;

            //海军写的方法，为减轻前台生成JSON压力，此方法为各子实体添加EntityCodeName属性。
            SetCodeName(dictionary.GetValue("EntityFieldValue", new ArrayList()), EntityCodeName.ToString());

            //根据创建日期获取实体            
            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadByCodeName(EntityCodeName.ToString(), createDate) as DynamicEntity;

            DEEntityInstanceBase data = entity.CreateInstance();
            if (instanceID != null && instanceID.ToString().IsNotEmpty())
            {
                data.ID = instanceID.ToString();
            }

            EntityFieldValueCollection values = JSONSerializerExecute.Deserialize<EntityFieldValueCollection>(dictionary.GetValue("EntityFieldValue", new ArrayList())) ?? new EntityFieldValueCollection();

            values.ForEach(p =>
            {
                var field = data.Fields.Where(f => f.Definition.Name == p.Definition.Name).FirstOrDefault();

                if (field != null)
                {
                    if (field.Definition.FieldType == Enums.FieldTypeEnum.Collection)
                    {
                        data.Fields.TrySetValue(field.Definition.Name, p.StringValue);
                    }
                    else
                    {
                        field.StringValue = p.StringValue;
                    }
                }
            });

            return data;
        }

        private void SetCodeName(ArrayList list, string codeName)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i] as Dictionary<string, object>;

                //Collection类型
                if (item.ContainsKey("StringValue") && !(item["StringValue"] is string || item["StringValue"] is int || item["StringValue"] is float || item["StringValue"] is double || item["StringValue"] is decimal))
                {
                    var cn = item["EntityCodeName"].ToString();
                    var array = item["StringValue"] as ArrayList;

                    for (int j = 0; j < array.Count; j++)
                    {
                        var dic = array[j] as Dictionary<string, object>;

                        if (dic != null)
                        {
                            if (!dic.ContainsKey("EntityCodeName"))
                            {
                                dic.Add("EntityCodeName", cn);
                            }
                            var clist = dic["EntityFieldValue"] as ArrayList;

                            SetCodeName(clist, cn);
                        }
                    }
                }

                if (!item.ContainsKey("CodeName"))
                {
                    item.Add("CodeName", codeName);
                }
            }
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
        //    object entityID = null;
        //    object instanceID = null;
        //    object entityDefine = null;

        //    dictionary.TryGetValue("EntityCode", out entityID);
        //    dictionary.TryGetValue("EntityInstanceCode", out instanceID);
        //    dictionary.TryGetValue("EntityDefine", out entityDefine);

        //    entityID.NullCheck("实体定义反序化失败,缺少EntityCode.");

        //    object createDate = null;
        //    object define = null;
        //    define = entityDefine as IDictionary<string, object>;
        //    if (define != null)
        //    {
        //        ((IDictionary<string, object>)entityDefine).TryGetValue("CreateDate", out createDate); 
        //    }
        //    else
        //    {
        //        createDate = DateTime.Now.SimulateTime().Ticks.ToString();
        //    }


        //    //获取定义的创建日期
        //    DateTime date = new DateTime(long.Parse(createDate.ToString()));
        //    //根据创建日期获取实体
        //    DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(entityID.ToString(), date) as DynamicEntity;

        //    DEEntityInstanceBase data = entity.CreateInstance();
        //    if (instanceID != null && instanceID.ToString().IsNotEmpty())
        //    {
        //        data.ID = instanceID.ToString();
        //    }

        //    EntityFieldValueCollection values = JSONSerializerExecute.Deserialize<EntityFieldValueCollection>(dictionary.GetValue("EntityFieldValue", new ArrayList())) ?? new EntityFieldValueCollection();

        //    values.ForEach(p =>
        //    {
        //        var field = data.Fields.Where(f => f.Definition.ID == p.Definition.ID).FirstOrDefault();

        //        if (field != null)
        //        {
        //            if (field.Definition.FieldType == Enums.FieldTypeEnum.Collection)
        //            {
        //                data.Fields.TrySetValue(field.Definition.Name, p.StringValue);
        //            }
        //            else
        //            {
        //                field.StringValue = p.StringValue;
        //            }
        //        }
        //    });

        //    return data;
        //}

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new Type[] { typeof(DEEntityInstance), typeof(DEEntityInstanceBase) };
            }
        }
    }
}
