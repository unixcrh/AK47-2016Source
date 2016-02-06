using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library.Script;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine
{
    [Serializable]
    [XElementSerializable]
    [DebuggerDisplay("Value={StringValue}")]
    public class EntityFieldValue : IFieldValueAccessor
    {
        private DynamicEntityField _Definition = null;

        internal EntityFieldValue()
        {
        }

        public EntityFieldValue(DynamicEntityField def)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(def != null, "def");

            this._Definition = def;
        }

        [XElementFieldSerialize(AlternateFieldName = "_SV")]
        private string _StringValue = null;

        /// <summary>
        /// 属性的字符串值
        /// </summary>
        public string StringValue
        {
            get
            {
                string result = this._StringValue;

                if (result == null)
                {
                    result = this.Definition.DefaultValue;

                    if (this.Definition.DefaultValue.IsNullOrEmpty() && this.Definition.FieldType != FieldTypeEnum.String)
                    {
                        object data = TypeCreator.GetTypeDefaultValue(this.Definition.FieldType.ToRealType());

                        if (data != null)
                            result = data.ToString();
                    }
                }

                return result;
            }
            set
            {
                this._StringValue = value;
            }
        }

        private int _SortOrder = -1;
        /// <summary>
        /// 排序码
        /// </summary>
        public int SortOrder
        {
            get
            {
                if (this._SortOrder < 0 && this.Definition != null)
                {
                    this._SortOrder = this.Definition.SortNo;
                }
                else
                {
                    this._SortOrder = 0;
                }
                return this._SortOrder;
            }
            set
            {
                this._SortOrder = value;
            }
        }

        /// <summary>
        /// 得到强类型的值
        /// </summary>
        /// <returns></returns>
        public object GetRealValue()
        {
            object result = this.StringValue;

            if (this.Definition.FieldType == FieldTypeEnum.Collection)
            {
                if (this.StringValue.IsNotEmpty())
                {
                    DEEntityInstanceBaseCollection collection = new DEEntityInstanceBaseCollection();
                    IEnumerable<XElement> objs = XElement.Parse(this.StringValue).XPathSelectElements("Object");

                    objs.ForEach(p =>
                    {
                        //这里需要优化，得到一个实体的定义，应该通过缓存逻辑实现。或者在Instance中，直接保存定义信息
                        DynamicEntity entity =
                            DESchemaObjectAdapter.Instance.Load(p.AttributeValue("EntityID")) as DynamicEntity;
                        DEEntityInstanceBase item = entity.CreateInstance();

                        item.FromXElement(p);

                        collection.Add(item);
                    });

                    result = collection;
                }
            }
            else
            {
                Type realType = typeof(string);

                //author haoyk 2014-3-3
                if (this.Definition.FieldType.TryToRealType(out realType))
                    result = DataConverter.ChangeType(result, realType);
            }

            return result;
        }

        public DynamicEntityField Definition
        {
            get { return this._Definition; }
            set { this._Definition = value; }
        }

        //沈峥注释，原来的Validators属性已经被注释掉了
        //[NonSerialized]
        //private List<MCS.Library.Validation.Validator> _Validators = null;

        ///// <summary>
        ///// 返回所有校验器
        ///// </summary>
        //public IEnumerable<MCS.Library.Validation.Validator> Validators
        //{
        //    get
        //    {
        //        if (this._Validators == null)
        //        {
        //            this._Validators = new List<MCS.Library.Validation.Validator>();

        //            this.Definition.Validators.ForEach(pvd => this._Validators.Add(pvd.GetValidator()));
        //        }

        //        return this._Validators;
        //    }
        //}

        public EntityFieldValue Clone()
        {
            EntityFieldValue newValue = new EntityFieldValue(this._Definition);

            newValue._StringValue = this._StringValue;

            return newValue;
        }

        /// <summary>
        /// 将Property的Value，赋值到当前的Value中。仅仅赋值StringValue，不包括定义部分
        /// </summary>
        /// <param name="pv"></param>
        public void FromPropertyVaue(PropertyValue pv)
        {
            if (pv != null)
                this._StringValue = pv.StringValue;
        }

        //public void SaveImageProperty()
        //{
        //    if (this.StringValue.IsNotEmpty())
        //    {
        //        var img = JSONSerializerExecute.Deserialize<ImageProperty>(this.StringValue);
        //        if (img != null)
        //        {
        //            ImagePropertyAdapter.Instance.UpdateContent(img);
        //            this.StringValue = JSONSerializerExecute.Serialize(img);
        //        }
        //    }
        //}

        //public FieldValue ToFieldValue()
        //{
        //    FieldValue pv = new FieldValue(this.Definition);

        //    pv.StringValue = this.StringValue;

        //    return pv;
        //}

        //public static implicit operator FieldValue(EntityFieldValue spv)
        //{
        //    spv.NullCheck("spv");

        //    return spv.ToFieldValue();
        //}

        public virtual void ToXElement(XElement element)
        {
            element.NullCheck("element");

            if (this.Definition.FieldType == FieldTypeEnum.Collection)
            {
                if (this.StringValue.IsNotEmpty())
                {
                    //集合属性
                    XElement celement = XElement.Parse(this.StringValue);

                    element.Add(celement);
                }
            }
            else
            {
                element.SetAttributeValue(this.Definition.Name, this.StringValue);
            }
        }

        public virtual void FromXElement(XElement element)
        {
            element.NullCheck("element");
            //这没考虑多个集合的情况，以后再改
            if (this.Definition.FieldType == FieldTypeEnum.Collection)
            {
                this.StringValue = string.Empty;

                var group = element.Descendants("Children").FirstOrDefault(s => s.Attribute("GroupEntityID").Value == this.Definition.ReferenceEntity.ID);

                if (group != null)
                    this.StringValue = group.ToString();
            }
            else
            {
                this.StringValue = element.AttributeValue(this.Definition.Name);
            }
        }

        DynamicEntityField IFieldValueAccessor.Definition
        {
            get { return this._Definition; }
            set { this._Definition = (DynamicEntityField)value; }
        }
    }
}
