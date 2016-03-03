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
    /// 校验器的类型配置
    /// </summary>
    public class ValidatorTypeConfigurationElement : TypeConfigurationElement
    {
        /// <summary>
        /// 校验器的参数集合
        /// </summary>
        [ConfigurationProperty("parameters", IsRequired = false)]
        public ValidatorParameterConfigurationElementCollection Parameters
        {
            get
            {
                return (ValidatorParameterConfigurationElementCollection)this["parameters"];
            }
        }
    }

    /// <summary>
    /// 校验器的类型配置集合
    /// </summary>
    public class ValidatorTypeConfigurationElementCollection : NamedConfigurationElementCollection<ValidatorTypeConfigurationElement>
    {
        /// <summary>
        /// 找到指定key的类型配置，并且创建指定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="autoThrow">是否自动抛出异常</param>
        /// <returns></returns>
        public T CheckAndGetInstance<T>(string key, bool autoThrow = true) where T : class
        {
            TypeConfigurationElement element = this.CheckAndGet(key, autoThrow);

            T instance = default(T);

            if (element != null)
                instance = (T)element.CreateInstance();

            return instance;
        } 
    }
}
