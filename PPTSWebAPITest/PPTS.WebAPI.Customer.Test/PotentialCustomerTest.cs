using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Customer.Controllers;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customer.Helper;
using PPTS.WebAPI.Customer.ViewModels.Customer;

namespace PPTS.WebAPI.Customer.Test
{
    [TestClass]
    public class PotentialCustomerTest
    {
        [ClassInitialize()]
        public static void Init(TestContext context)
        {
            MapperConfiguration.Configure();
        }

        [TestMethod]
        public void GetAllPotentialCustomersForGetAll()
        {
            var controller = new PotentialCustomersController();
            var model = controller.GetAllCustomers(new CustomerQueryCriteriaModel
            {
                Name = "11",
                CustomerCode = "11",
                VipLevels = new int[] { 1, 2 },
                VipTypes = new int[] { 1, 2 },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            });

            Assert.IsNotNull(model.ViewModelList);
            Assert.IsNotNull(model.Dictionaries.Count > 0);
        }

        [TestMethod]
        public void CreatePotentialCustomerForGet()
        {
            var controller = new PotentialCustomersController();

            var model = controller.CreateCustomer();

            Assert.IsNotNull(model.Customer);
            Assert.IsNotNull(model.PrimaryParent);
            Assert.IsTrue(model.Dictionaries.Count > 0);
        }

        [TestMethod]
        public void CreatePotentialCustomerForSave()
        {
            var controller = new PotentialCustomersController();

            var model = new CreatableCustomerViewModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.Customer = DataHelper.PreparePotentialCustomerData();

            controller.CreateCustomer(model);

            var customerLoaded = PotentialCustomerAdapter.Instance.Load(model.Customer.CustomerId);
            var parentLoaded = ParentAdapter.Instance.Load(model.PrimaryParent.ParentId);
            CustomerRelation relationLoaded = CustomerRelationAdapter.Instance.Load(model.Customer.CustomerId, model.PrimaryParent.ParentId);

            model.Customer.Equals(customerLoaded.ConvertToViewModel<PotentialCustomer, CustomerViewModel>());
            model.PrimaryParent.Equals(parentLoaded.ConvertToViewModel<Parent, ParentViewModel>());

            Assert.IsNotNull(relationLoaded);

            Assert.AreEqual(model.Customer.CustomerId, relationLoaded.CustomerID);
            Assert.AreEqual(model.PrimaryParent.ParentId, relationLoaded.ParentID);
        }

        private static PotentialCustomersController PrepareController()
        {
            return new PotentialCustomersController();
        }
    }
}
