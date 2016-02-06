using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Net.SNTP
{
    /// <summary>
    /// 时间同步服务器的配置信息
    /// </summary>
    public sealed class SNTPSettings : ConfigurationSection
    {
        /// <summary>
        /// 获取配置信息。如果不存在，则返回null
        /// </summary>
        /// <returns></returns>
        public static SNTPSettings GetConfig()
        {
            return (SNTPSettings)ConfigurationBroker.GetSection("sntpSettings");
        }

        /// <summary>
        /// 获取配置信息。如果不存在，则返回默认的设置
        /// </summary>
        /// <returns></returns>
        public static SNTPSettings GetConfigOrDefault()
        {
            SNTPSettings settings = SNTPSettings.GetConfig();

            if (settings == null)
                settings = new SNTPSettings();

            return settings;
        }

        /// <summary>
        /// 从配置信息中获取远程时间服务器的设置
        /// </summary>
        /// <returns></returns>
        public static RemoteSNTPServer GetSNTPServer()
        {
            SNTPSettings settings = SNTPSettings.GetConfig();

            RemoteSNTPServer server = RemoteSNTPServer.Default;

            if (settings != null)
                server = new RemoteSNTPServer(settings.ServerName, settings.Port);

            return server;
        }

        /// <summary>
        /// 服务器的地址或名称
        /// </summary>
        [ConfigurationProperty("serverName", IsRequired = false, DefaultValue = RemoteSNTPServer.DefaultHostName)]
        public string ServerName
        {
            get
            {
                return (string)this["serverName"];
            }
        }

        /// <summary>
        /// 使用服务器的端口号
        /// </summary>
        [ConfigurationProperty("port", IsRequired = false, DefaultValue = RemoteSNTPServer.DefaultPort)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
        }

        /// <summary>
        /// 网络连接的超时时间
        /// </summary>
        [ConfigurationProperty("timeout", IsRequired = false, DefaultValue = "00:00:01")]
        public TimeSpan Timeout
        {
            get
            {
                return (TimeSpan)this["timeout"];
            }
        }

        /// <summary>
        /// 同步线程的轮询间隔
        /// </summary>
        [ConfigurationProperty("poolInterval", IsRequired = false, DefaultValue = "00:01:00")]
        public TimeSpan PoolInterval
        {
            get
            {
                return (TimeSpan)this["poolInterval"];
            }
        }

        /// <summary>
        /// 默认的日期类型（Local或者Utc，默认为Local）
        /// </summary>
        [ConfigurationProperty("defaultDateTimeKind", IsRequired = false, DefaultValue = DateTimeKind.Local)]
        public DateTimeKind DefaultDateTimeKind
        {
            get
            {
                return (DateTimeKind)this["defaultDateTimeKind"];
            }
        }
    }
}
