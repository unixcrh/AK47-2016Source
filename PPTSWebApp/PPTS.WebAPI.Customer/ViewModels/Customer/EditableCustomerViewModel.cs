using System.Collections.Generic;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    public class EditableCustomerViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public IDictionary<string, object> Dictionaries { get; set; }

        public EditableCustomerViewModel()
        {
            this.Dictionaries = new Dictionary<string, object>();
        }
    }
}