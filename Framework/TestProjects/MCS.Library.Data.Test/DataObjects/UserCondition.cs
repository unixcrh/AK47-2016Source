using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public class UserCondition
    {
        public UserCondition()
        {
            this.PageParams = new PageRequestParams();
            this.OrderByBuilder = new OrderBySqlClauseBuilder();
            this.OrderByBuilder.AppendItem("Gender", FieldSortDirection.Ascending);
        }

        [ConditionMapping(EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string UserName
        {
            get;
            set;
        }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderBySqlClauseBuilder OrderByBuilder
        {
            get;
            set;
        }
    }
}
