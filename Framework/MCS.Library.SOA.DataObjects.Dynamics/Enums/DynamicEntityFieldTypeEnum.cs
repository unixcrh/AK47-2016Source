using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Enums
{
    /// <summary>
    /// 字段
    /// </summary>
    public enum DynamicEntityFieldTypeEnum
    {
        /// <summary>
        /// 数值类型
        /// </summary>
        [EnumItemDescription("数值类型")]
        Int,
        /// <summary>
        /// 字符类型
        /// </summary>
        [EnumItemDescription("字符类型")]
        String,
        /// <summary>
        /// 布尔类型
        /// </summary>
        [EnumItemDescription("布尔类型")]
        Bool,
        /// <summary>
        /// 日期类型
        /// </summary>
        [EnumItemDescription("日期类型")]
        DateTime,
        /// <summary>
        /// 货币类型
        /// </summary>
        [EnumItemDescription("货币类型")]
        Decimal,
       
    }
}
