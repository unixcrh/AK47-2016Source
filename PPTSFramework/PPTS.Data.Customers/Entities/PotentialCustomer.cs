using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 潜在客户表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.PotentialCustomers")]
    [DataContract]
    public class PotentialCustomer
    {
        public PotentialCustomer()
        {
        }

        /// <summary>
        /// 客户的ID
        /// </summary>
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID命名规则：家长P+年份后两位+月+日+999999，学生S+年份后两位+月份+日期+999999
        /// </summary>
        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 是否单亲
        /// </summary>
        [ORFieldMapping("IsSingleParent")]
        [DataMember]
        public bool IsSingleParent
        {
            get;
            set;
        }

        /// <summary>
        /// 入学大年级。对应类别C_CODE_ABBR_CUSTOMER_GRADE（年级）
        /// </summary>
        [ORFieldMapping("EntranceGrade")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        public int EntranceGrade
        {
            get;
            set;
        }

        private StudentBranchType _Branch = StudentBranchType.NoBranch;

        /// <summary>
        /// 文理科(C_CODE_ABBR_STUDENTBRANCH)。1:文科，2:理科，3:不分科
        /// </summary>
        [ORFieldMapping("Branch")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_STUDENTBRANCH")]
        public StudentBranchType Branch
        {
            get
            {
                return this._Branch;
            }
            set
            {
                this._Branch = value;
            }
        }

        /// <summary>
        /// 学年制(C_CODE_ABBR_ACDEMICYEAR)
        /// </summary>
        [ORFieldMapping("SchoolYear")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_ACDEMICYEAR")]
        public int SchoolYear
        {
            get;
            set;
        }

        /// <summary>
        /// 学生描述
        /// </summary>
        [ORFieldMapping("Character")]
        [DataMember]
        public string Character
        {
            get;
            set;
        }

        /// <summary>
        /// 学生生日
        /// </summary>
        [ORFieldMapping("Birthday")]
        [DataMember]
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        [ORFieldMapping("Email")]
        [DataMember]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 当前年级。年级(C_CODE_ABBR_CUSTOMER_GRADE)
        /// </summary>
        [ORFieldMapping("Grade")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        public int Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 学生类型(C_CODE_ABBR_CUSTOMER_STUDENTTYPE)，默认51
        /// </summary>
        [ORFieldMapping("StudentType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_STUDENTTYPE")]
        public int StudentType
        {
            get;
            set;
        }

        /// <summary>
        /// 教学地点的ID
        /// </summary>
        [ORFieldMapping("TeachingLocation")]
        [DataMember]
        public string TeachingLocation
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询地点的ID
        /// </summary>
        [ORFieldMapping("ConsultingSite")]
        [DataMember]
        public string ConsultingSite
        {
            get;
            set;
        }

        /// <summary>
        /// 销售阶段(C_CODE_ABBR_Customer_CRM_SalePhase)
        /// </summary>
        [ORFieldMapping("SalesStage")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_SalePhase")]
        public SalesStageType SalesStage
        {
            get;
            set;
        }

        /// <summary>
        /// 购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)
        /// </summary>
        [ORFieldMapping("PurchaseIntention")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_PurchaseIntent")]
        public PurchaseIntentionDefine PurchaseIntention
        {
            get;
            set;
        }

        /// <summary>
        /// 证件类型(C_CODE_ABBR_BO_Customer_CertificateType)
        /// </summary>
        [ORFieldMapping("IDType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Customer_CertificateType")]
        public IDTypeDefine IDType
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        [ORFieldMapping("IDNumbar")]
        [DataMember]
        public string IDNumbar
        {
            get;
            set;
        }

        /// <summary>
        /// 接触方式(C_CODE_ABBR_Customer_CRM_NewContactType)。"1：呼入 2：呼出 3：直访 4：在线咨询-乐语 5：在线咨询-其他"
        /// </summary>
        [ORFieldMapping("ContactType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_NewContactType")]
        public int ContactType
        {
            get;
            set;
        }

        /// <summary>
        /// 信息来源一级分类(C_Code_Abbr_BO_Customer_Source)
        /// </summary>
        [ORFieldMapping("SourceMainType")]
        [DataMember]
        [ConstantCategory("C_Code_Abbr_BO_Customer_Source")]
        public int SourceMainType
        {
            get;
            set;
        }

        /// <summary>
        /// 信息来源二级分类(C_Code_Abbr_BO_Customer_Source)
        /// </summary>
        [ORFieldMapping("SourceSubType")]
        [DataMember]
        [ConstantCategory("C_Code_Abbr_BO_Customer_Source")]
        public int SourceSubType
        {
            get;
            set;
        }

        /// <summary>
        /// 客户状态(C_Code_Abbr_BO_CTI_CustomerStatus)0=未确认客户信息, 1 = 确认客户信息, 9=无效用户（逻辑删除）
        /// </summary>
        [ORFieldMapping("Status")]
        [DataMember]
        [ConstantCategory("C_Code_Abbr_BO_CTI_CustomerStatus")]
        public CustomerStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
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
        /// 校区ID
        /// </summary>
        [ORFieldMapping("SchoolID")]
        [DataMember]
        public int SchoolID
        {
            get;
            set;
        }

        /// <summary>
        /// 性别(C_CODE_ABBR_GENDER)。1--男，2--女
        /// </summary>
        [ORFieldMapping("Gender")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_GENDER")]
        public GenderType Gender
        {
            get;
            set;
        }

        /// <summary>
        /// vip客户(C_CODE_ABBR_CUSTOMER_VipType)。1:关系vip客户 2:大单vip客户
        /// </summary>
        [ORFieldMapping("VipType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_VipType")]
        public int VipType
        {
            get;
            set;
        }

        /// <summary>
        /// vip客户等级（C_CODE_ABBR_CUSTOMER_VipLevel）。1:A级 2:B级 3:C级
        /// </summary>
        [ORFieldMapping("VipLevel")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_VipLevel")]
        public int VipLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次跟进时间
        /// </summary>
        [ORFieldMapping("LastFollowupTime")]
        [DataMember]
        public DateTime LastFollowupTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次回访时间
        /// </summary>
        [ORFieldMapping("NextFollowupTime")]
        [DataMember]
        public DateTime NextFollowupTime
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
    public class PotentialCustomerCollection : EditableDataObjectCollectionBase<PotentialCustomer>
    {
    }
}