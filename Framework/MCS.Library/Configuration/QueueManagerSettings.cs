using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Configuration
{
    /// <summary>
    /// 队列相关的配置信息
    /// </summary>
    public class QueueManagerSettings : DeluxeConfigurationSection
    {
        /// <summary>
        /// 从配置信息中获取队列的配置信息。如果没有这个配置节，则抛出异常
        /// </summary>
        /// <returns></returns>
        public static QueueManagerSettings GetConfig()
        {
            QueueManagerSettings settings = (QueueManagerSettings)ConfigurationBroker.GetSection("queueManagerSettings");

            settings.CheckSectionNotNull("queueManagerSettings");

            return settings;
        }

        /// <summary>
        /// 队列的实现类的配置信息
        /// </summary>
        [ConfigurationProperty("typeFactories", IsRequired = true)]
        public TypeConfigurationCollection TypeFactories
        {
            get
            {
                return (TypeConfigurationCollection)this["typeFactories"];
            }
        }
    }
}
