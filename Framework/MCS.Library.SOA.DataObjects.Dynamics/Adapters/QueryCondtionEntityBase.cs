using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Data.Mapping;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    public class QueryCondtionEntityBase
    {
        /// <summary>
        /// 版本开始时间
        /// </summary>
        [ConditionMapping("VersionStartTime", "<=")]
        public DateTime VersionStartTime { get; set; }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [ConditionMapping("VersionEndTime", ">", Template = "ISNULL(${DataField}$,'99990101 00:00:00.000') ${Operation}$ ${Data}$")]
        public DateTime VersionEndTime { get; set; }

        /// <summary>
        /// 有效性
        /// </summary>
        [ConditionMapping("ValidStatus")]
        public bool ValidStatus { get; set; }

        [ORFieldMapping("Code")]
        [ConditionMapping("Code")]
        public string Code { get; set; }
    }
}