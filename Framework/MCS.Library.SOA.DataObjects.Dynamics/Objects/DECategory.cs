using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.OGUPermission;
using MCS.Library.Validation;
using System.Text;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    #region 数据实体
    /// <summary>
    /// 自然人
    /// </summary>
    [Serializable]
    [XElementSerializable]
    [ORTableMapping("DE.Categories")]
    public class DECategory : VersionedEntityBase
    {
        /// <summary>
        /// 编码
        /// </summary>
        [ORFieldMapping("Code", PrimaryKey = true)]
        [StringLengthValidator(0, 36, MessageTemplate = "编码长度不能超过36")]
        public override string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [ORFieldMapping("DisplayName")]
        [StringLengthValidator(0, 100, MessageTemplate = "显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 父级Code
        /// </summary>
        [ORFieldMapping("ParentCode")]
        [StringLengthValidator(0, 50, MessageTemplate = "父级Code")]
        public string ParentCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [ORFieldMapping("Description")]
        [StringLengthValidator(0, 255, MessageTemplate = "描述")]
        public string Desc { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [ORFieldMapping("Status")]
        [IntegerRangeValidator(0, 2, MessageTemplate = "状态")]
        public string Status { get; set; }

        ///// <summary>
        ///// 排序编号
        ///// </summary>
        //[ORFieldMapping("SortNo")]
        //[IntegerRangeValidator(0, 100, MessageTemplate = "排序编号")]
        //public string SortNo { get; set; }

        ///// <summary>
        ///// 创建者
        ///// </summary>
        //[ORFieldMapping("Creator")]
        //[StringLengthValidator(0, 36, MessageTemplate = "创建者")]
        //public string Creator { get; set; }

        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //[ORFieldMapping("CreateTime")]
        //[StringLengthValidator(0, 50, MessageTemplate = "创建时间")]
        //public DateTime CreateTime { get; set; }

        ///// <summary>
        ///// 修改者
        ///// </summary>
        //[ORFieldMapping("Modifier")]
        //[StringLengthValidator(0, 36, MessageTemplate = "创建者")]
        //public string Modifier { get; set; }

        ///// <summary>
        ///// 修改时间
        ///// </summary>
        //[ORFieldMapping("ModifyTime")]
        //[StringLengthValidator(0, 50, MessageTemplate = "修改时间")]
        //public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 层级级别
        /// </summary>
        [ORFieldMapping("Level")]
        [StringLengthValidator(0, 5, MessageTemplate = "层级级别")]
        public string Level { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        [ORFieldMapping("FullPath")]
        [StringLengthValidator(0, 255, MessageTemplate = "全路径")]
        public string FullPath { get; set; }
    }

    [Serializable]
    [XElementSerializable]
    public class CategoryCollection : EditableDataObjectCollectionBase<DECategory>
    {

    }
    #endregion
}
