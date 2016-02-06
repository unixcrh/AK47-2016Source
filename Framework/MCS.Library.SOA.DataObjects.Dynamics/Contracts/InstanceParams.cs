using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Contract
{
    [Serializable]
    public class SAPPara
    {
        public string system { get; set; }
        public string ApplicationServer { get; set; }
        public string SystemNumber { get; set; }
        public string Client { get; set; }
        public string user { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
    }
    [Serializable]
    public enum InType
    {
        [EnumItemDescription("标准类型")]
        StandardInterface,
        [EnumItemDescription("自定义类型")]
        CustomInterface
    }

    [Serializable]
    public class InstanceParam
    {
        public SAPPara SapParam { get; set; }
        public string TCode { get; set; }
        public InType SapType { get; set; }
        public List<SapValue> Values { get; set; }
    }

    [Serializable]
    public class SapValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
