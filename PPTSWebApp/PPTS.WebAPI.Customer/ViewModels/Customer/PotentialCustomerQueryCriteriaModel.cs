using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    public class PotentialCustomerQueryCriteriaModel
    {
        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Name { get; set; }

        [ConditionMapping("CustomerCode", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerCode { get; set; }

        [ConditionMapping("CreateTime", Operation = ">=")]
        public DateTime StartDate { get; set; }

        [ConditionMapping("CreateTime", Operation = "<", AdjustDays = 1)]
        public DateTime EndDate { get; set; }

        [InConditionMapping("VipLevel")]
        public int[] VipLevels { get; set; }

        [InConditionMapping("VipType")]
        public int[] VipTypes { get; set; }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderBySqlClauseBuilder OrderBy
        {
            get;
            set;
        }
    }
}