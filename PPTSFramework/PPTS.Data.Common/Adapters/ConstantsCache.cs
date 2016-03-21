using MCS.Library.Caching;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    /// <summary>
    /// 常量的Cache
    /// </summary>
    internal sealed class ConstantsCache : CacheQueue<string, ConstantEntityInCategoryCollection>
    {
        public static readonly ConstantsCache Instance = CacheManager.GetInstance<ConstantsCache>();

        //实现SingleTon模式
        private ConstantsCache()
        {
        }
    }
}
