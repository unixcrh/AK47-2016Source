using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Validator
{


    /// <summary>
    /// 实体实例对象属性验证器的容器对象
    /// </summary>
    [ActionContextDescription(Key = "DEInstancePropertyValidatorContext")]
    public class DEInstancePropertyValidatorContext : ActionContextBase<DEInstancePropertyValidatorContext>
    {
        public DEInstancePropertyValidatorContext()
        {
        }

        /// <summary>
        /// Schema对象的属性验证器所涉及到的对象
        /// </summary>
        public DEEntityInstance Target
        {
            get;
            internal set;
        }

        /// <summary>
        /// Schema对象的属性验证器所涉及到的容器对象
        /// </summary>
        public DEEntityInstance Container
        {
            get;
            internal set;
        }
    }
}
