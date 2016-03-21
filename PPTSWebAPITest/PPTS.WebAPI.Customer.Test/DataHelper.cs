using System;
using MCS.Library.Core;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customer.ViewModels.Customer;

namespace PPTS.WebAPI.Customer.Test
{
    internal static class DataHelper
    {
        public static ParentViewModel PrepareParentData()
        {
            var result = new ParentViewModel();

            result.ParentId = UuidHelper.NewUuidString();
            result.ParentName = String.Format("测试添加家长{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static CustomerViewModel PreparePotentialCustomerData()
        {
            var result = new CustomerViewModel();

            result.CustomerId = UuidHelper.NewUuidString();
            result.CustomerName = String.Format("测试添加潜在用户{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static PhoneCollection PreparePhonesData(string ownerID)
        {
            var phones = new PhoneCollection();

            phones.Add(new Phone() { OwnerID = ownerID, PhoneID = UuidHelper.NewUuidString(), PhoneNumber = "021-68190909", IsPrimary = true });
            phones.Add(new Phone() { OwnerID = ownerID, PhoneID = UuidHelper.NewUuidString(), PhoneNumber = "18901067455", IsPrimary = false });

            return phones;
        }

        public static CustomerRelation PrepareCustomerRelation(string studentID, string parentID)
        {
            var relation = new CustomerRelation() { CustomerID = studentID, ParentID = parentID };

            relation.CustomerRole = 1;
            relation.ParentRole = 1;
            relation.IsPrimary = true;

            return relation;
        }
    }
}
