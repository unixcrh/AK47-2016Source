using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects
{
    /// <summary>
    /// Property相关的扩展方法
    /// </summary>
    public static class PropertyExtensions
    {
        private static Dictionary<PropertyDataType, Type> _DataTypeMappings = new Dictionary<PropertyDataType, Type>(){
			{PropertyDataType.Boolean, typeof(bool)},
			{PropertyDataType.DataObject, typeof(object)},
			{PropertyDataType.DateTime, typeof(DateTime)},
			{PropertyDataType.Decimal, typeof(Decimal)},
			{PropertyDataType.Integer, typeof(long)},
			{PropertyDataType.String, typeof(string)},
			{PropertyDataType.Enum, typeof(int)}
		};

        /// <summary>
        /// 试图转换成真实的类型
        /// </summary>
        /// <param name="pdt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TryToRealType(this PropertyDataType pdt, out Type type)
        {
            type = typeof(string);

            return _DataTypeMappings.TryGetValue(pdt, out type);
        }

        /// <summary>
        /// 将PropertyDataType转换成.Net的数据类型
        /// </summary>
        /// <param name="pdt"></param>
        /// <returns></returns>
        public static Type ToRealType(this PropertyDataType pdt)
        {
            Type result = typeof(string);

            TryToRealType(pdt, out result).FalseThrow("不支持PropertyDataType的{0}类型转换为CLR的数据类型", pdt);

            return result;
        }

        /// <summary>
        /// 基本类型转换到PropertyDataType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyDataType ToPropertyDataType(this System.Type type)
        {
            type.NullCheck("type");

            PropertyDataType result = PropertyDataType.DataObject;

            foreach (KeyValuePair<PropertyDataType, System.Type> kp in _DataTypeMappings)
            {
                if (kp.Value == type)
                {
                    result = kp.Key;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 校验器的类型信息转换成属性值集合
        /// </summary>
        /// <param name="validatorType"></param>
        /// <returns></returns>
        public static PropertyValueCollection ToPropertyValues(this ValidatorTypeConfigurationElement validatorType)
        {
            PropertyValueCollection properties = new PropertyValueCollection();

            foreach (ValidatorParameterConfigurationElement paramElement in validatorType.Parameters)
            {
                PropertyDefine pd = paramElement.ToPropertyDefine();

                properties.Add(new PropertyValue(pd));
            }

            return properties;
        }

        /// <summary>
        /// 将参数类型信息转换成属性定义
        /// </summary>
        /// <param name="paramElement"></param>
        /// <returns></returns>
        public static PropertyDefine ToPropertyDefine(this ValidatorParameterConfigurationElement paramElement)
        {
            paramElement.NullCheck("paramElement");

            PropertyDefine pd = new PropertyDefine();

            pd.Name = paramElement.Name;
            pd.DisplayName = paramElement.Description;
            pd.Description = paramElement.Description;
            pd.DataType = (PropertyDataType)Enum.Parse(typeof(PropertyDataType), paramElement.DataType.ToString(), true);
            pd.DefaultValue = paramElement.ParamValue;

            return pd;
        }
    }
}
