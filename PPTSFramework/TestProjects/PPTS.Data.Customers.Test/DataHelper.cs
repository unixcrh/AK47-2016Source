using MCS.Library.Core;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    internal static class DataHelper
    {
        public static Parent PrepareParentData()
        {
            Parent result = new Parent();

            result.ParentID = UuidHelper.NewUuidString();
            result.ParentName = string.Format("测试添加家长{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static PotentialCustomer PreparePotentialCustomerData()
        {
            PotentialCustomer result = new PotentialCustomer();

            result.CustomerID = UuidHelper.NewUuidString();
            result.CustomerName = string.Format("测试添加潜在用户{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static PhoneCollection PreparePhonesData(string ownerID)
        {
            PhoneCollection phones = new PhoneCollection();

            phones.Add(new Phone() { OwnerID = ownerID, PhoneID = UuidHelper.NewUuidString(), PhoneNumber = "021-68190909", IsPrimary = true });
            phones.Add(new Phone() { OwnerID = ownerID, PhoneID = UuidHelper.NewUuidString(), PhoneNumber = "18901067455", IsPrimary = false });

            return phones;
        }

        public static CustomerRelation PrepareCustomerRelation(string studentID, string parentID)
        {
            CustomerRelation relation = new CustomerRelation() { CustomerID = studentID, ParentID = parentID };

            relation.CustomerRole = 1;
            relation.ParentRole = 1;
            relation.IsPrimary = true;

            return relation;
        }
    }
}
