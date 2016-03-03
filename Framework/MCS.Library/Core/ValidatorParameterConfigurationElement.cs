using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Core
{
    /// <summary>
    /// 校验器参数的数据类型
    /// </summary>
    public enum ValidatorParameterDataType
    {
        /// <summary>
        /// 对象
        /// </summary>
        [EnumItemDescription("对象", 1)]
        DataObject = 1,

        /// <summary>
        /// 布尔
        /// </summary>
        [EnumItemDescription("布尔", 3)]
        Boolean = 3,
        
        /// <summary>
        /// 整型
        /// </summary>
        [EnumItemDescription("整型", 9)]
        Integer = 9,
        
        /// <summary>
        /// 浮点
        /// </summary>
        [EnumItemDescription("浮点", 15)]
        Decimal = 15,
        
        /// <summary>
        /// 时间
        /// </summary>
        [EnumItemDescription("时间", 16)]
        DateTime = 16,
        
        /// <summary>
        /// 文本
        /// </summary>
        [EnumItemDescription("文本", 18)]
        String = 18,
        
        /// <summary>
        /// 枚举
        /// </summary>
        [EnumItemDescription("枚举", 20)]
        Enum = 20
    }

    /// <summary>
    /// 校验器参数的配置元素
    /// </summary>
    public class ValidatorParameterConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
        /// 参数的数据类型
        /// </summary>
        [ConfigurationProperty("type", DefaultValue = ValidatorParameterDataType.String)]
        public ValidatorParameterDataType DataType
        {
            get
            {
                return (ValidatorParameterDataType)this["type"];
            }
        }

        /// <summary>
        /// 参数的默认值
        /// </summary>
        [ConfigurationProperty("paramValue", DefaultValue = "")]
        public string ParamValue
        {
            get
            {
                return (string)this["paramValue"];
            }
        }
    }

    /// <summary>
    /// 校验器参数的配置元素集合
    /// </summary>
    public sealed class ValidatorParameterConfigurationElementCollection : NamedConfigurationElementCollection<ValidatorParameterConfigurationElement>
    {
    }
}
