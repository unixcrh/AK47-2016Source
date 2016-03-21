using System;
using System.Collections.Generic;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customer.Helper
{
    public class DataHelper
    {
        public static IDictionary<string, object> LoadDictionaryData(IDictionary<string, object> dictionaries, params Type[] types)
        {
            var categories = ConstantCategoryAttribute.GetConstantCategories(types);

            foreach (var category in categories)
            {
                dictionaries[category] = ConstantAdapter.Instance.GetByCategory(category, false).ToSimpleEntity();
            }

            return dictionaries;
        }
    }
}