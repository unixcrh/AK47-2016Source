using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace PPTS.Data.Common
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        [EnumItemDescription("男")]
        Male = 1,

        [EnumItemDescription("女")]
        Female = 2
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IDTypeDefine
    {
        [EnumItemDescription("身份证")]
        IDCard = 1,

        [EnumItemDescription("军官证")]
        MilitaryOfficerCard = 2,

        [EnumItemDescription("驾驶证")]
        DrivingLicense = 3,

        [EnumItemDescription("护照")]
        Passport = 4
    }
}
