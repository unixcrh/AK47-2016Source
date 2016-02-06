using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MCS.Library.Caching;
using MCS.Library.Logging;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
	public sealed class DEExecutorLogContextInfo
	{
		public static TextWriter Writer
		{
			get
			{
				object writer = null;

				if (ObjectContextCache.Instance.TryGetValue("DEExecutorLogContextInfoWriter", out writer) == false)
				{
					StringBuilder strB = new StringBuilder(1024);

					writer = new StringWriter(strB);

					ObjectContextCache.Instance["DEExecutorLogContextInfoWriter"] = writer;
				}

				return (TextWriter)writer;
			}
		}

		/// <summary>
		/// 提交到真正的Logger
		/// </summary>
		public static void CommitInfoToLogger()
		{
			StringBuilder strB = ((StringWriter)Writer).GetStringBuilder();

			if (strB.Length > 0)
			{
				try
				{
					Logger logger = LoggerFactory.Create("DEExecutor");

					if (logger != null)
						logger.Write(strB.ToString(), LogPriority.Normal, 8005, TraceEventType.Information, "DEExecutor上下文信息");
				}
				catch (System.Exception)
				{
				}
				finally
				{
					strB.Clear();
				}
			}
		}
	}
}
