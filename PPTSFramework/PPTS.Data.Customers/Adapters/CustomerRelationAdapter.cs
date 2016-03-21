using MCS.Library.Core;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerRelationAdapter : CustomerAdapterBase<CustomerRelation, CustomerRelationCollection>
    {
        public static readonly CustomerRelationAdapter Instance = new CustomerRelationAdapter();

        private CustomerRelationAdapter()
        {
        }

        public CustomerRelation Load(string customerID, string parentID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");
            parentID.CheckStringIsNullOrEmpty("parentID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("ParentID", parentID)).SingleOrDefault();
        }
    }
}
