using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class ParentAdapterTest
    {
        [TestMethod]
        public void UpdateParent()
        {
            Parent parent = DataHelper.PrepareParentData();

            using (DbContext context = ParentAdapter.Instance.GetDbContext())
            {
                ParentAdapter.Instance.UpdateInContext(parent);

                context.ExecuteNonQuerySqlInContext();
            }

            Console.WriteLine(parent.CustomerCode);

            Parent loaded = ParentAdapter.Instance.Load(parent.ParentID);

            Assert.IsNotNull(loaded);
            parent.AreEqual(loaded);
        }
    }
}
