using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class PotentialCustomerAdapterTest
    {
        [TestMethod]
        public void UpdatePotentialCustomer()
        {
            PotentialCustomer customer = DataHelper.PreparePotentialCustomerData();

            using (DbContext context = PotentialCustomerAdapter.Instance.GetDbContext())
            {
                PotentialCustomerAdapter.Instance.UpdateInContext(customer);

                context.ExecuteNonQuerySqlInContext();
            }

            Console.WriteLine(customer.CustomerCode);

            PotentialCustomer loaded = PotentialCustomerAdapter.Instance.Load(customer.CustomerID);

            Assert.IsNotNull(loaded);
            customer.AreEqual(loaded);
        }
    }
}
