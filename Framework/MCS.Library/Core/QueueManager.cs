using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class QueueManager
    {
        /// <summary>
        /// 从配置文件中读取队列的实现类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static IQueue<T> GetQueue<T>(string queueName)
        {
            queueName.CheckStringIsNullOrEmpty("queueName");

            QueueManagerSettings settings = QueueManagerSettings.GetConfig();

            return settings.TypeFactories.CheckAndGet(queueName).CreateInstance<IQueue<T>>();
        }
    }
}
