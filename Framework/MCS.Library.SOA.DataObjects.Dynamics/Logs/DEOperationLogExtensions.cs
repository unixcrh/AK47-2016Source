using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;

namespace MCS.Library.SOA.DataObjects.Dynamics.Logs
{
	public static class DEOperationLogExtensions
	{
		public static DEOperationLog ToOperationLog(this DEBase data, DEOperationType opType)
		{
			data.NullCheck("data");

			DEOperationLog log = DEOperationLog.CreateLogFromEnvironment();

			log.ResourceID = data.ID;
			log.SchemaType = data.SchemaType;
			log.OperationType = opType;
			log.Category = data.Schema.Category;
			log.Subject = string.Format("{0}: {1}",
				EnumItemDescriptionAttribute.GetDescription(opType), data.Name);

			log.SearchContent = data.ToFullTextString();

			return log;
		}
	}
}
