using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;

namespace MCS.Library.SOA.DataObjects.Dynamics.Schemas
{
    /// <summary>
    /// FieldType相关的扩展方法
    /// </summary>
    public static class DynamicExtensions
    {
        private static Dictionary<FieldTypeEnum, Type> _DataTypeMappings = new Dictionary<FieldTypeEnum, Type>(){
			{FieldTypeEnum.Bool, typeof(bool)},
			{FieldTypeEnum.Collection, typeof(DynamicEntityCollection)},
			{FieldTypeEnum.DateTime, typeof(DateTime)},
			{FieldTypeEnum.Decimal, typeof(Decimal)},
			{FieldTypeEnum.Int, typeof(int)},
			{FieldTypeEnum.String, typeof(string)}
		};

        /// <summary>
        /// 试图转换成真实的类型
        /// </summary>
        /// <param name="pdt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TryToRealType(this FieldTypeEnum pdt, out Type type)
        {
            type = typeof(string);

            return _DataTypeMappings.TryGetValue(pdt, out type);
        }

        /// <summary>
        /// 将DynamicEntityField转换成.Net的数据类型
        /// </summary>
        /// <param name="pdt"></param>
        /// <returns></returns>
        public static Type ToRealType(this FieldTypeEnum pdt)
        {
            Type result = typeof(string);

            TryToRealType(pdt, out result).FalseThrow("不支持FieldTypeEnum的{0}类型转换为CLR的数据类型", pdt);

            return result;
        }

        /// <summary>
        /// 基本类型转换到FieldTypeEnum
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FieldTypeEnum ToPropertyDataType(this System.Type type)
        {
            type.NullCheck("type");

            FieldTypeEnum result = FieldTypeEnum.String;

            foreach (KeyValuePair<FieldTypeEnum, System.Type> kp in _DataTypeMappings)
            {
                if (kp.Value == type)
                {
                    result = kp.Key;
                    break;
                }
            }

            return result;
        }
    }
}
