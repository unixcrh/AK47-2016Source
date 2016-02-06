using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Logging;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Actions;
using MCS.Library.SOA.DataObjects.Schemas.Actions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Configuration
{
    /// <summary>
    /// 实体实例更新操作配置信息
    /// </summary>
	public class EntityInstanceUpdateActionSettings : ConfigurationSection
	{
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
		public static EntityInstanceUpdateActionSettings GetConfig()
		{
            EntityInstanceUpdateActionSettings settings =
                (EntityInstanceUpdateActionSettings)ConfigurationBroker.GetSection("entityInstanceUpdateActionSettings");

			if (settings == null)
                settings = new EntityInstanceUpdateActionSettings();

			return settings;
		}

        private EntityInstanceUpdateActionSettings()
		{
		}

        /// <summary>
        /// 获取配置的操作
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public EntityInstanceUpdateActionCollection GetActions(string operation)
		{
            EntityInstanceUpdateActionCollection actions = new EntityInstanceUpdateActionCollection();

			foreach (EntityInstanceUpdateActionConfigurationElement actionElem in this.Actions)
			{
				try
				{
					if (actionElem.Operation == operation)
                        actions.Add((IEntityInstanceUpdateAction)actionElem.CreateInstance());
				}
				catch (Exception ex)
				{
					WriteToLog(ex);
				}
			}

			return actions;
		}

		[ConfigurationProperty("actions")]
        private EntityInstanceUpdateActionConfigurationElementCollection Actions
		{
			get
			{
                return (EntityInstanceUpdateActionConfigurationElementCollection)this["actions"];
			}
		}

		private static void WriteToLog(Exception ex)
		{
			Logger logger = LoggerFactory.Create("WfRuntime");

			if (logger != null)
			{
				StringBuilder strB = new StringBuilder(1024);

				strB.AppendLine(ex.Message);

				strB.AppendLine(EnvironmentHelper.GetEnvironmentInfo());
				strB.AppendLine(ex.StackTrace);

				logger.Write(strB.ToString(), LogPriority.Normal, 8004, TraceEventType.Error, "WfRuntime 获取EntityInstanceUpdateAction出错");
			}
		}
	}
}
