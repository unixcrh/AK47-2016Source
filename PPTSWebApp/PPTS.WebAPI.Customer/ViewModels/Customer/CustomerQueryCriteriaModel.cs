using System;
using MCS.Library.Data.Mapping;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    [Serializable]
    public class CustomerQueryCriteriaModel
    {
        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Name { get; set; }

        [ConditionMapping("CustomerCode", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerCode { get; set; }

        [ConditionMapping("CreateTime", Operation = ">=")]
        public DateTime StartDate { get; set; }

        [ConditionMapping("CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime EndDate { get; set; }

        //[ConditionMapping("VipLevel", Operation = "IN")]
        public int[] VipLevels { get; set; }

        //[ConditionMapping("VipType", Operation = "IN")]
        public int[] VipTypes { get; set; }

        public PagedParam PagedParam { get; set; }
    }
}