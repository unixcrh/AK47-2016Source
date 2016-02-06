using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Caching;

namespace MCS.Library.SOA.DataObjects.Dynamics.Permissions
{
    internal sealed class DEPrincipalCache : CacheQueue<string, bool>
    {
        public static readonly DEPrincipalCache Instance = CacheManager.GetInstance<DEPrincipalCache>();
    }
}
