using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Enums
{
    /// <summary>
    /// 字段输入输出类型
    /// </summary>
    public enum ParamDirectionEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumItemDescription("未知")]
        NotKnown,

        /// <summary>
        /// 输入参数
        /// </summary>
        [EnumItemDescription("输入参数")]
        Import,
        
        /// <summary>
        /// 输出参数
        /// </summary>
        [EnumItemDescription("输出参数")]
        Export
    }
}
