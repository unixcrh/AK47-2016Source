using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class PotentialCustomerAdapter : CustomerAdapterBase<PotentialCustomer, PotentialCustomerCollection>
    {
        public static PotentialCustomerAdapter Instance = new PotentialCustomerAdapter();

        private PotentialCustomerAdapter()
        {
        }

        public PotentialCustomer Load(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID)).SingleOrDefault();
        }

        protected override void BeforeInnerUpdateInContext(PotentialCustomer data, DbContext dbContext, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("S");
        }

        protected override void BeforeInnerUpdate(PotentialCustomer data, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("S");
        }
    }
}
