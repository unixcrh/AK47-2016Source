using System;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// 条件表达式和对象属性的映射关系
    /// </summary>
    [DebuggerDisplay("PropertyName={propertyName}")]
    public class ConditionMappingItem : ConditionMappingItemBase
    {
        private string operation = SqlClauseBuilderBase.EqualTo;
        private string template = string.Empty;
        private bool escapeLikeString = false;

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

        /// <summary>
        /// 从对应属性进行填充
        /// </summary>
        /// <param name="attr"></param>
        protected internal override void FillFromAttr(ConditionMappingAttributeBase attr)
        {
            base.FillFromAttr(attr);

            ConditionMappingAttribute cmAttr = (ConditionMappingAttribute)attr;

            this.operation = cmAttr.Operation;
            this.template = cmAttr.Template;
            this.escapeLikeString = cmAttr.EscapeLikeString;
        }

        /// <summary>
        /// 根据数据调整流程
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected internal override object AdjustValue(object data)
        {
            object result = data;

            if (data is string)
            {
                if (this.EscapeLikeString)
                    result = TSqlBuilder.Instance.EscapeLikeString(data.ToString());
            }

            return base.AdjustValue(result);
        }
    }
}
