using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 常量的基本信息
    /// </summary>
    public class BaseConstantEntity
    {
        public BaseConstantEntity()
        {

        }

        public BaseConstantEntity(BaseConstantEntity source)
        {
            source.NullCheck("source");

            this.Key = source.Key;
            this.ParentKey = source.ParentKey;
            this.Value = source.Value;
        }

        public virtual string Key
        {
            get;
            set;
        }

        public virtual string Value
        {
            get;
            set;
        }

        public virtual string ParentKey
        {
            get;
            set;
        }
    }
}
