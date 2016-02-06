using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MCS.Library.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Others;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Configuration
{
    public class ConvertUepFiledValueMapping : ConfigurationSection
    {
        public static ConvertUepFiledValueMapping GetConfig()
        {
            ConvertUepFiledValueMapping result = (ConvertUepFiledValueMapping)ConfigurationBroker.GetSection("ConvertFiledValuesMapping");
            if (result == null)
            {
                result = new ConvertUepFiledValueMapping();
            }
            return result;
        }

        private ConvertUepFiledValueCollection _Fileds;
        /// <summary>
        /// 获取properties下的节点集合
        /// </summary>
        [ConfigurationProperty("properties", IsDefaultCollection = true)]
        public ConvertUepFiledValueCollection Fileds
        {
            get
            {
                if (_Fileds == null)
                {
                    _Fileds = (ConvertUepFiledValueCollection)base["properties"];
                }
                return _Fileds;
            }
        }


        /// <summary>
        /// 转换UEP属性的值
        /// </summary>
        /// <param name="UEPFiledType">字段类型</param>
        /// <param name="FileValue">字段值</param>
        /// <returns></returns>
        public static string ConvertUEPFiledValue(string UEPFiledType, string filedValue)
        {
            string sapFiledValue = string.Empty;

            var convertFiledValueElements = ConvertUepFiledValueMapping.GetConfig().Fileds.Cast<ConvertUepFiledValueElement>();

            //当前属性的类型匹配到配置文件中的类型，并且执行规则方法
            var element = convertFiledValueElements.FirstOrDefault(p => p.UEPValueType == UEPFiledType);
            if (element != null)
            {
                string[] typeStrings = element.UEPValueDelegate.Split(',');
                if (typeStrings.Length == 2)
                {
                    Type convertUEPFiledValue = Type.GetType(typeStrings[0]);
                    //创建该程序集下的类实例（用于委托调用a该类实例下的Method）
                    object instance = Activator.CreateInstance(convertUEPFiledValue);
                    //创建委托，指定委托调用的方法
                    GetConvertUEPFiledValue convertFiledValueMothend =
                        (GetConvertUEPFiledValue)Delegate.CreateDelegate(typeof(GetConvertUEPFiledValue), instance, typeStrings[1]);

                    sapFiledValue = convertFiledValueMothend(filedValue, element.Rule);
                }
            }
            return sapFiledValue;
        }

        /// <summary>
        /// 检查值是否合法
        /// </summary>
        /// <returns></returns>
        public static bool CheckValue(string UEPFiledType, string filedValue)
        {
            bool flag = true;
            if (UEPFiledType.ToLower() == "string" && filedValue == "")
            {
                flag = true;
            }
            else
            {
                if (string.IsNullOrEmpty(ConvertUEPFiledValue(UEPFiledType, filedValue)))
                    flag = false;
            }
            return flag;
        }
    }


    /// <summary>
    /// 获取Uep值转换的配置属性
    /// </summary>
    public class ConvertUepFiledValueElement : ConfigurationElement
    {
        /// <summary>
        /// 配置的数据类型
        /// </summary>
        [ConfigurationProperty("UEPValueType", IsRequired = true)]
        public string UEPValueType
        {
            get { return (string)base["UEPValueType"]; }

            set { base["UEPValueType"] = value; }
        }

        /// <summary>
        /// 配置的规则
        /// </summary>
        [ConfigurationProperty("Rule", IsRequired = true)]
        public string Rule
        {
            get { return (string)base["Rule"]; }

            set { base["Rule"] = value; }
        }

        /// <summary>
        /// 修改值得委托
        /// </summary>
        [ConfigurationProperty("UEPValueDelegate", IsRequired = true)]
        public string UEPValueDelegate
        {
            get { return (string)base["UEPValueDelegate"]; }

            set { base["UEPValueDelegate"] = value; }
        }
    }

    /// <summary>
    /// 获取配置文件的节点集合
    /// </summary>
    public class ConvertUepFiledValueCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建新原色
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConvertUepFiledValueElement();
        }
        /// <summary>
        /// 获取元素
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConvertUepFiledValueElement)element).UEPValueType;
        }

        public ConvertUepFiledValueElement this[int index]
        {
            get
            {
                return (ConvertUepFiledValueElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }

}
