using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 动态实体对象基类
    /// </summary>
    [Serializable]
    public class DEBase : DESchemaObjectBase
    {
        public DEBase(string schemaType)
            : base(schemaType)
        {
        }

        /// <summary>
        /// 编码
        /// </summary>
        [NoMapping]
        public string Name
        {
            get
            {
                return this.Properties.GetValue("Name", string.Empty);
            }
            set
            {
                this.Properties.SetValue("Name", value);
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [NoMapping]
        public string CodeName
        {
            get
            {
                return this.Properties.GetValue("CodeName", string.Empty);
            }
            set
            {
                this.Properties.SetValue("CodeName", value);
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [NoMapping]
        public string Description
        {
            get
            {
                return this.Properties.GetValue("Description", string.Empty);
            }
            set
            {
                this.Properties.SetValue("Description", value);
            }
        }

        /// <summary>
        /// 排序号
        /// </summary>
        [NoMapping]
        public int SortNo
        {
            get
            {
                return this.Properties.GetValue("SortNo", 0);
            }
            set
            {
                this.Properties.SetValue("SortNo", value);
            }
        }

        public virtual string ToDescription()
        {
            string result = ToString();

            if (this.SchemaType.IsNotEmpty())
            {
                string name = this.Properties.GetValue("Name", string.Empty);

                result = name.IsNotEmpty() ? 
                    string.Format("{0}:{1}({2})", this.SchemaType, name, this.ID) 
                    : 
                    string.Format("{0}({1})", this.SchemaType, this.ID);
            }

            return result;
        }
    }
}
