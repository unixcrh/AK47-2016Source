using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Logging;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Configuration
{
	public class DEObjectOperationActionSettings : ConfigurationSection
	{
        /// <summary>
        /// 获取关于操作类的配置文件信息
        /// </summary>
        /// <returns></returns>
		public static DEObjectOperationActionSettings GetConfig()
		{
			DEObjectOperationActionSettings settings =
				(DEObjectOperationActionSettings)ConfigurationBroker.GetSection("deObjectOperationActionSettings");

			if (settings == null)
				settings = new DEObjectOperationActionSettings();

			return settings;
		}

        private DEObjectOperationActionSettings()
		{
		}

        /// <summary>
        /// 获取所有操作
        /// </summary>
        /// <returns></returns>
		public DEObjectOperationActionCollection GetActions()
		{
			DEObjectOperationActionCollection actions = new DEObjectOperationActionCollection();

			foreach (DEObjectOperationActionConfigurationElement actionElem in Actions)
			{
				try
				{
					actions.Add((IDEObjectOperationAction)actionElem.CreateInstance());	
				}
				catch (Exception ex)
				{
					WriteToLog(ex);
				}
			}

			return actions;
		}

		[ConfigurationProperty("actions")]
		private DEOperationActionConfigurationElementCollection Actions
		{
			get
			{
				return (DEOperationActionConfigurationElementCollection)this["actions"];
			}
		}

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="ex"></param>
		private static void WriteToLog(Exception ex)
		{
			Logger logger = LoggerFactory.Create("WfRuntime");

			if (logger != null)
			{
				StringBuilder strB = new StringBuilder(1024);

				strB.AppendLine(ex.Message);

				strB.AppendLine(EnvironmentHelper.GetEnvironmentInfo());
				strB.AppendLine(ex.StackTrace);

				logger.Write(strB.ToString(), LogPriority.Normal, 8004, TraceEventType.Error, "WfRuntime 获取DEOperationAction出错");
			}
		}
	}
}
