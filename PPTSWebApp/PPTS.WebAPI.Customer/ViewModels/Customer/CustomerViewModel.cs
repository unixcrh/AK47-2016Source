using System;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    [Serializable]
    public class CustomerViewModel
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public GenderType Gender { get; set; }
    }
}