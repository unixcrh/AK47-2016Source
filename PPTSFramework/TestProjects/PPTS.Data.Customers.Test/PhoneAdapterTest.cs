using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using MCS.Library.Core;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class PhoneAdapterTest
    {
        [TestMethod]
        public void UpdatePhonesTest()
        {
            string ownerID = UuidHelper.NewUuidString();

            PhoneCollection phones = DataHelper.PreparePhonesData(ownerID);

            using (DbContext context = PhoneAdapter.Instance.GetDbContext())
            {
                PhoneAdapter.Instance.UpdateByOwnerIDInContext(ownerID, phones);

                context.ExecuteNonQuerySqlInContext();
            }

            PhoneCollection loaded = PhoneAdapter.Instance.LoadByOwnerID(ownerID);

            phones.AreEqual(loaded);
        }
    }
}
