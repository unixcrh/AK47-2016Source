using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// 条件对象描述元素的基类
    /// </summary>
    public abstract class ConditionMappingItemBase
    {
        private string propertyName = string.Empty;
        private string dataFieldName = string.Empty;
        private string prefix = string.Empty;
        private string postfix = string.Empty;
        private double adjustDays = 0;
        private bool isExpression = false;
        private EnumUsageTypes enumUsage = EnumUsageTypes.UseEnumValue;
        private string subClassTypeDescription = string.Empty;
        private string subClassPropertyName = string.Empty;
        private MemberInfo memberInfo = null;

        /// <summary>
		/// 对象的属性名称
		/// </summary>
		public string PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        /// <summary>
        /// 数据字段的类型
        /// </summary>
        public string DataFieldName
        {
            get { return this.dataFieldName; }
            set { this.dataFieldName = value; }
        }

        /// <summary>
        /// 生成Value时的前缀
        /// </summary>
        public string Prefix
        {
            get { return this.prefix; }
            set { this.prefix = value; }
        }

        /// <summary>
        /// 生成Value时的后缀
        /// </summary>
        public string Postfix
        {
            get { return this.postfix; }
            set { this.postfix = value; }
        }

        /// <summary>
        /// 如果是日期型，需要调整天数。
        /// </summary>
        public double AdjustDays
        {
            get { return this.adjustDays; }
            set { this.adjustDays = value; }
        }

        /// <summary>
        /// 是否是表达式
        /// </summary>
        public bool IsExpression
        {
            get { return this.isExpression; }
            set { this.isExpression = value; }
        }

        /// <summary>
        /// 枚举类型的使用方法（值/还是描述）
        /// </summary>
        public EnumUsageTypes EnumUsage
        {
            get { return this.enumUsage; }
            set { this.enumUsage = value; }
        }

        /// <summary>
        /// 对应的子对象的类型描述
        /// </summary>
        public string SubClassTypeDescription
        {
            get { return this.subClassTypeDescription; }
            set { this.subClassTypeDescription = value; }
        }

        /// <summary>
        /// 子对象的属性名称
        /// </summary>
        public string SubClassPropertyName
        {
            get { return this.subClassPropertyName; }
            set { this.subClassPropertyName = value; }
        }

        /// <summary>
        /// 对应的成员对象信息
        /// </summary>
        public MemberInfo MemberInfo
        {
            get { return this.memberInfo; }
            internal set { this.memberInfo = value; }
        }

        /// <summary>
        /// 从对应属性进行填充
        /// </summary>
        internal protected virtual void FillFromAttr(ConditionMappingAttributeBase attr)
        {
            if (attr.DataFieldName.IsNotEmpty())
                this.dataFieldName = attr.DataFieldName;

            this.enumUsage = attr.EnumUsage;
            this.prefix = attr.Prefix;
            this.postfix = attr.Postfix;
            this.adjustDays = attr.AdjustDays;
            this.isExpression = attr.IsExpression;
        }

        /// <summary>
        /// 根据数据调整流程
        /// </summary>
        /// <param name="data"></param>
        internal protected virtual object AdjustValue(object data)
        {
            object result = data;

            if (data is string)
            {
                if (this.Prefix.IsNotEmpty())
                    result = this.Prefix + result;

                if (this.Postfix.IsNotEmpty())
                    result = result + this.Postfix;
            }
            else
            {
                if (data is DateTime && (DateTime)data != DateTime.MinValue && (DateTime)data != DateTime.MaxValue)
                {
                    if (this.AdjustDays != 0)
                        result = ((DateTime)data).AddDays(this.AdjustDays);
                }
            }

            return result;
        }
    }
}
