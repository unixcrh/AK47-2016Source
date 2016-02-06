using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance
{
    /// <summary>
    /// 带版本的EntityInstance对象的基类
    /// </summary>
    [Serializable]
    public abstract class NoVersionedEntityInstanceObjectBase : IDefinitionFields<EntityFieldValue>, IMemberAccessor, ISCStatusObject
    {
        protected string _EntityID = string.Empty;
        private SchemaObjectStatus _Status = SchemaObjectStatus.Normal;
        private string _Tag = string.Empty;

        [NonSerialized]
        private DynamicEntity _Entity = null;

        protected NoVersionedEntityInstanceObjectBase()
        {
        }

        /// <summary>
        /// 使用指定的模式名初始化成员
        /// </summary>
        /// <param name="entityID">模式的名称</param>
        protected NoVersionedEntityInstanceObjectBase(string entityID)
        {
            entityID.CheckStringIsNullOrEmpty("entityID");

            this._EntityID = entityID;
        }

        /// <summary>
        /// 获取模式定义
        /// </summary>
        /// <value>表示模式定义的<see cref="DynamicEntity"/>对象</value>
        [NoMapping]
        [ScriptIgnore]
        public DynamicEntity EntityDefine
        {
            get
            {
                if (this._Entity == null && this.EntityCode.IsNotEmpty())
                    this._Entity = this.GetEntity(this.EntityCode);

                return this._Entity;
            }
        }

        public EntityFieldValueCollection Fields
        {
            get
            {
                return this.GetFieldValueCollection();
            }
        }

        /// <summary>
        /// 在派生类中重写时，获取或设置ID
        /// </summary>
        [ORFieldMapping("ID", PrimaryKey = true)]
        public virtual string ID
        {
            get;
            set;
            //get
            //{
            //    return this.Fields.GetValue("ID", string.Empty);
            //}
            //set
            //{
            //    string originalValue = this.Fields.GetValue("ID", string.Empty);

            //    this.Fields.TrySetValue("ID", value);

            //    if (originalValue != value)
            //    {
            //        OnIDChanged();
            //    }
            //}
        }

        /// <summary>
        /// 在派生类中重写时，获取模式的类型
        /// </summary>
        [ORFieldMapping("EntityCode")]
        public virtual string EntityCode
        {
            get
            {
                return this._EntityID;
            }
        }

        /// <summary>
        /// 在派生类中重写时，获取模式对象状态
        /// </summary>
        /// <value><see cref="SchemaObjectStatus"/>值之一</value>
        [ORFieldMapping("Status")]
        public virtual SchemaObjectStatus Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        /// <summary>
        /// 对象的一些标识型信息
        /// </summary>
        [NoMapping]
        public virtual string Tag
        {
            get
            {
                return this._Tag;
            }
            set
            {
                this._Tag = value;
            }
        }

        /// <summary>
        /// 在派生类中重写时，获取创建日期
        /// </summary>
        [ORFieldMapping("CreateDate")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Insert, DefaultExpression = "GETDATE()")]
        public virtual DateTime CreateDate
        {
            get;
            set;
        }

        private IUser _Creator = null;

        /// <summary>
        /// 获取或设置<see cref="SchemaObjectBase"/>对象的创建者
        /// </summary>
        /// <value>表示创建者的<see cref="IUser"/>的实例或<see langword="null"/></value>
        [SubClassORFieldMapping("ID", "Creator")]
        //[SubClassORFieldMapping("DisplayName", "CreatorName")]
        [SubClassType(typeof(OguUser))]
        [SubClassSqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Insert)]
        public IUser Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = (IUser)OguUser.CreateWrapperObject(value);
            }
        }

        /// <summary>
        /// 得到属性集合
        /// </summary>
        /// <returns></returns>
        protected abstract EntityFieldValueCollection GetFieldValueCollection();

        /// <summary>
        /// 得到实体的定义信息
        /// </summary>
        /// <returns></returns>
        protected abstract DynamicEntity GetEntity(string entityID);

        /// <summary>
        /// 当ID改变时
        /// </summary>
        protected virtual void OnIDChanged()
        {
        }

        SerializableEditableKeyedDataObjectCollectionBase<string, EntityFieldValue> IDefinitionFields<EntityFieldValue>.Fields
        {
            get
            {
                return this.Fields;
            }
        }

        public virtual object GetValue(object instance, string memberName)
        {
            object result = null;

            switch (memberName)
            {
                case "ID":
                    result = this.ID;
                    break;
                //case "VersionStartTime":
                //    result = this.VersionStartTime;
                //    break;
                //case "VersionEndTime":
                //    result = this.VersionEndTime;
                //    break;
                case "Status":
                    result = this.Status;
                    break;
                case "EntityID":
                    result = this.EntityCode;
                    break;
                case "CreateDate":
                    result = this.CreateDate;
                    break;
                case "Creator":
                    result = this.Creator;
                    break;
                default:
                    {
                        PropertyInfo pi = (PropertyInfo)TypePropertiesCacheQueue.Instance.GetPropertyInfo(instance.GetType(), memberName);

                        if (pi != null && pi.CanRead)
                            result = pi.GetValue(instance, null);

                        break;
                    }
            }

            return result;
        }

        public virtual void SetValue(object instance, string memberName, object newValue)
        {
            switch (memberName)
            {
                case "ID":
                    this.ID = (string)newValue;
                    break;
                //case "VersionStartTime":
                //    this.VersionStartTime = (DateTime)newValue;
                //    break;
                //case "VersionEndTime":
                //    this.VersionEndTime = (DateTime)newValue;
                //    break;
                case "Status":
                    this.Status = (SchemaObjectStatus)newValue;
                    break;
                case "EntityID":
                    this._EntityID = (string)newValue;
                    break;
                case "CreateDate":
                    this.CreateDate = (DateTime)newValue;
                    break;
                case "Creator":
                    this.Creator = (IUser)newValue;
                    break;
                default:
                    {
                        PropertyInfo pi = (PropertyInfo)TypePropertiesCacheQueue.Instance.GetPropertyInfo(instance.GetType(), memberName);

                        if (pi != null && pi.CanWrite)
                            pi.SetValue(instance, newValue, null);

                        break;
                    }
            }
        }

        /// <summary>
        /// 在派生类中重写时，将对象属性附加到<see cref="XElement"/>中。
        /// </summary>
        /// <param name="element">将在其中进行附加的<see cref="XElement"/></param>
        public virtual void ToXElement(XElement element)
        {
            element.NullCheck("element");

            element.SetAttributeValue("DEINS_ID", this.ID);
            element.SetAttributeValue("EntityID", this.EntityCode);

            this.Fields.ForEach(pv => pv.ToXElement(element));
        }

        /// <summary>
        /// 获取表示当前对象的<see cref="string"/>
        /// </summary>
        /// <returns>一个XML的字符串表示</returns>
        public override string ToString()
        {
            XElement element = new XElement("Object");

            ToXElement(element);

            return element.ToString();
        }

        /// <summary>
        /// 在派生类中重写时，从<see cref="XElement"/>恢复属性数据
        /// </summary>
        /// <param name="element">要从其中恢复数据的<see cref="XElement"/></param>
        public virtual void FromXElement(XElement element)
        {
            element.NullCheck("element");
            this.ID = string.IsNullOrEmpty(element.Attribute("DEINS_ID", string.Empty)) ? element.Attribute("ID", string.Empty) : element.Attribute("DEINS_ID", string.Empty);

            this.Fields.ForEach(pv => pv.FromXElement(element));
        }

        /// <summary>
        /// 在派生类中重写时，从XML字符串恢复数据
        /// </summary>
        /// <param name="data">一个XML格式的字符串</param>
        public virtual void FromString(string data)
        {
            if (data.IsNotEmpty())
            {
                XElement element = XElement.Parse(data);

                FromXElement(element);
            }
        }

        /// <summary>
        /// 获取用于全文索引的字符串
        /// </summary>
        /// <returns></returns>
        public string ToFullTextString()
        {
            //StringBuilder searchContent = new StringBuilder(256);

            //Entity.Properties.ForEach(pd =>
            //    pd.SnapshotMode.IfInFullTextIndex(() => searchContent.AppendWithSplitChars(this.Properties[pd.Name].StringValue)));

            //return searchContent.ToString();
            return string.Empty;
        }
    }
}
