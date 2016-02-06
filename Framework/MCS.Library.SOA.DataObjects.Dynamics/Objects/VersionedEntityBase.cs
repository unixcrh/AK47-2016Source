using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 开放时间段版本管理实体基类
    /// </summary>
    [Serializable]
    [XElementSerializable]
    public abstract class VersionedEntityBase : ILoadableDataEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Description("编码")]
        [ORFieldMapping("Code", PrimaryKey = true)]
        public virtual string Code { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        [Description("中文名")]
        [ORFieldMapping("CnName")]
        [StringEmptyValidator(MessageTemplate = "中文名不能为空")]
        public virtual string CnName { get; set; }
     
        /// <summary>
        /// 英文名
        /// </summary>
        [Description("英文名")]
        [ORFieldMapping("EnName")]
        public virtual string EnName { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Description("创建人")]
        [ORFieldMapping("Creator")]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        [ORFieldMapping("CreateTime")]
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Description("修改人")]
        [ORFieldMapping("Modifier")]
        public virtual string Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        [ORFieldMapping("ModifyTime")]
        public virtual DateTime ModifyTime { get; set; }

        /// <summary>
        /// 有效性
        /// </summary>
        [Description("有效性")]
        [ORFieldMapping("ValidStatus")]
        public virtual bool ValidStatus { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Description("排序号")]
        [ORFieldMapping("SortNo")]
        public virtual int SortNo { get; set; }

        /// <summary>
        /// 版本开始时间
        /// </summary>
        [Description("版本开始时间")]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]//基类方法，修改请知会全员
        public virtual DateTime VersionStartTime { get; set; }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [Description("版本结束时间")]
        [ORFieldMapping("VersionEndTime")]
        public virtual DateTime VersionEndTime { get; set; }

      

        /// <summary>
        /// 是否从数据库加载
        /// </summary>
        [Description("是否从数据加载")]
        [NoMapping]
        public bool Loaded
        {
            get;
            set;
        }
    }
}
