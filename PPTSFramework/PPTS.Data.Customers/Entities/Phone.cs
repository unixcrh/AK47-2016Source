using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Phone.
    /// 电话号码
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.Phones")]
    [DataContract]
    public class Phone
    {
        public Phone()
        {
        }

        [ORFieldMapping("PhoneID", PrimaryKey = true)]
        [DataMember]
        public string PhoneID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("OwnerID")]
        [DataMember]
        public string OwnerID
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是主要联系电话
        /// </summary>
        [ORFieldMapping("IsPrimary")]
        [DataMember]
        public bool IsPrimary
        {
            get;
            set;
        }

        /// <summary>
        /// 电话类型(C_CODE_ABBR_CUSTOMER_CONTACT_PHONE_TYPE)
        /// </summary>
        [ORFieldMapping("PhoneType")]
        [DataMember]
        public int PhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// 国家代码
        /// </summary>
        [ORFieldMapping("CountryCode")]
        [DataMember]
        public string CountryCode
        {
            get;
            set;
        }

        /// <summary>
        /// 区号
        /// </summary>
        [ORFieldMapping("AreaNumber")]
        [DataMember]
        public string AreaNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        [ORFieldMapping("PhoneNumber")]
        [DataMember]
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 分机号
        /// </summary>
        [ORFieldMapping("Extension")]
        [DataMember]
        public string Extension
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 租户的ID
        /// </summary>
        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PhoneCollection : EditableDataObjectCollectionBase<Phone>
    {
        /// <summary>
        /// 得到主要联系方式。如果没有找到，则返回nulll
        /// </summary>
        /// <returns></returns>
        public Phone GetPrimaryPhone()
        {
            return this.Find(p => p.IsPrimary);
        }
    }
}