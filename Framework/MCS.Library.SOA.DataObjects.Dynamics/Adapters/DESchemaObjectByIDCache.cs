using MCS.Library.Caching;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// DESchemaObject的缓存，用于数据实体的定义缓存
    /// </summary>
    internal class DESchemaObjectByIDCache : CacheQueue<string, DESchemaObjectBase>
    {
        public static readonly DESchemaObjectByIDCache Instance = CacheManager.GetInstance<DESchemaObjectByIDCache>();
    }
}
