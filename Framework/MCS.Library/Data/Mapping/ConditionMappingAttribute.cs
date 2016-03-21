using System;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using MCS.Library.Data.Mapping;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// 条件对象的映射属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConditionMappingAttribute : ConditionMappingAttributeBase
    {
        private string operation = "=";
        private string template = string.Empty;
        private bool escapeLikeString = false;

        /// <summary>
        /// 
        /// </summary>
        public ConditionMappingAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public ConditionMappingAttribute(string fieldName)
            : base(fieldName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="op"></param>
        public ConditionMappingAttribute(string fieldName, string op)
            : base(fieldName)
        {
            this.operation = op;
        }

        /// <summary>
        /// 操作符，缺省为“=”
        /// </summary>
        public string Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }

        /// <summary>
        /// 生成的SQL子句的表达式模板。默认是${DataField}$ ${Operation}$ ${Data}$
        /// </summary>
        public string Template
        {
            get { return this.template; }
            set { this.template = value; }
        }

        /// <summary>
        /// 是否按照LIKE子句转义字符串中的LIKE保留字
        /// </summary>
        public bool EscapeLikeString
        {
            get { return this.escapeLikeString; }
            set { this.escapeLikeString = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class SubConditionMappingAttribute : ConditionMappingAttribute
    {
        private string subPropertyName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subPropertyName"></param>
        /// <param name="fieldName"></param>
        public SubConditionMappingAttribute(string subPropertyName, string fieldName)
            : base(fieldName)
        {
            this.subPropertyName = subPropertyName;
        }

        /// <summary>
        /// 源对象的属性名称
        /// </summary>
        public string SubPropertyName
        {
            get
            {
                return this.subPropertyName;
            }
            set
            {
                this.subPropertyName = value;
            }
        }
    }
}
