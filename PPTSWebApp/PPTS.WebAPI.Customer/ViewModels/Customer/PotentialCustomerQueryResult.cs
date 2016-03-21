using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    public class PotentialCustomerQueryResult
    {
        public PagedQueryResult<PotentialCustomer, PotentialCustomerCollection> QueryResult
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }
    }
}