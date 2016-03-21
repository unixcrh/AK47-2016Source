using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class CustomerRelationAdapterTest
    {
        [TestMethod]
        public void UpdateCustomerRelation()
        {
            CustomerRelation relation = DataHelper.PrepareCustomerRelation(UuidHelper.NewUuidString(), UuidHelper.NewUuidString());

            using (DbContext context = CustomerRelationAdapter.Instance.GetDbContext())
            {
                CustomerRelationAdapter.Instance.UpdateInContext(relation);

                context.ExecuteNonQuerySqlInContext();
            }

            CustomerRelation loaded = CustomerRelationAdapter.Instance.Load(relation.CustomerID, relation.ParentID);

            relation.AreEqual(loaded);
        }
    }
}
