using MCS.Library.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    internal class TypeConstantCategoriesCache : CacheQueue<Type, SortedSet<string>>
    {
        public static readonly TypeConstantCategoriesCache Instance = CacheManager.GetInstance<TypeConstantCategoriesCache>();

        //实现SingleTon模式
        private TypeConstantCategoriesCache()
        {
        }
    }
}
