using System;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customer.ViewModels.Customer
{
    [Serializable]
    public class CustomerQueryResultModel
    {
        public PagedList<CustomerViewModel> ViewModelList { get; private set; }
        public Dictionary<string, object> Dictionaries { get; private set; }

        public CustomerQueryResultModel(PagedParam pageParam)
        {
            this.ViewModelList = new PagedList<CustomerViewModel>(pageParam);
            this.Dictionaries = new Dictionary<string, object>();
        }
    }
}