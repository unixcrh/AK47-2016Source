using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.Validation;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Validator;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance
{
    /// <summary>
    /// 动态实体实例对象
    /// </summary>
    [Serializable]
    public class DEEntityInstance : DEEntityInstanceBase
    {
        public DEEntityInstance(string entityID) :
            base(entityID)
        {
        }

        public virtual string ToDescription()
        {
            string result = ToString();

            if (this.EntityCode.IsNotEmpty())
            {
                string name = this.Name;

                result = name.IsNotEmpty() ?
                    string.Format("{0}:{1}({2})", this.EntityCode, name, this.ID)
                    :
                    string.Format("{0}({1})", this.EntityCode, this.ID);
            }

            return result;
        }
        protected override DynamicEntity GetEntity(string entityID)
        {
            return DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
        }

         /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        public override ValidationResults Validate()
        {
            DEEntityInstanceValidator validator = new DEEntityInstanceValidator();

            return validator.Validate(this);
        }

        
    }
}
