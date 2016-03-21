using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public class ConditionObject
    {
        [ConditionMapping("SUBJECT", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Subject
        {
            get;
            set;
        }

        [ConditionMapping("SEARCH_CONTENT", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string FullTextTerm
        {
            get;
            set;
        }

        [ConditionMapping("GENDER")]
        public GenderType Gender
        {
            get;
            set;
        }

        [ConditionMapping("CREATE_TIME", Operation = ">=")]
        public DateTime StartTime
        {
            get;
            set;
        }

        [ConditionMapping("CREATE_TIME", Operation = "<", AdjustDays = 1)]
        public DateTime EndTime
        {
            get;
            set;
        }
    }
}
