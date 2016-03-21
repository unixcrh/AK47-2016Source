using MCS.Library.Caching;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConstantCategoryAttribute : Attribute
    {
        public ConstantCategoryAttribute()
        {
        }

        public ConstantCategoryAttribute(string category)
        {
            category.CheckStringIsNullOrEmpty("categoryName");

            this.Category = category;
        }

        public string Category
        {
            get;
            set;
        }

        public static string[] GetConstantCategories(params Type[] types)
        {
            SortedSet<string> result = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            if (types != null)
            {
                foreach (Type type in types)
                {
                    SortedSet<string> categories = GetSingleTypeConstantCategories(type);

                    foreach (string category in categories)
                    {
                        if (result.Contains(category) == false)
                            result.Add(category);
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 从一个类型上得到所有标注了ConstantCategoryAttribute的类别信息集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static SortedSet<string> GetSingleTypeConstantCategories(Type type)
        {
            type.NullCheck("type");

            return TypeConstantCategoriesCache.Instance.GetOrAddNewValue(type, (cache, innerKey) =>
            {
                SortedSet<string> categories = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

                List<string> result = new List<string>();

                Dictionary<string, PropertyInfo> properties = TypeFlattenHierarchyPropertiesCacheQueue.Instance.GetPropertyDictionary(type);

                foreach (KeyValuePair<string, PropertyInfo> kp in properties)
                {
                    foreach (Attribute attribute in ConstantCategoryAttribute.GetCustomAttributes(kp.Value))
                    {
                        ConstantCategoryAttribute ca = attribute as ConstantCategoryAttribute;

                        if (ca != null && ca.Category.IsNotEmpty())
                        {
                            if (categories.Contains(ca.Category) == false)
                                categories.Add(ca.Category);
                        }
                    }
                }

                return categories;
            });
        }
    }
}
