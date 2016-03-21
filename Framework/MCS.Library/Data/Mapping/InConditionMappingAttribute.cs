using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// In条件对象的映射属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InConditionMappingAttribute : ConditionMappingAttributeBase
    {
        /// <summary>
        /// 
        /// </summary>
        public InConditionMappingAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public InConditionMappingAttribute(string fieldName) : 
            base(fieldName)
        {
        }
    }
}
