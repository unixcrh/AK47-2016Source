using MCS.Library.Core;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    public class CreatableCustomerViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public ParentViewModel PrimaryParent { get; set; }
        public IDictionary<string, object> Dictionaries { get; set; }

        public CreatableCustomerViewModel()
        {
            this.Customer = new CustomerViewModel { CustomerId = UuidHelper.NewUuidString() };
            this.PrimaryParent =  new ParentViewModel { ParentId = UuidHelper.NewUuidString() };
            this.Dictionaries = new Dictionary<string, object>();
        }
    }
}