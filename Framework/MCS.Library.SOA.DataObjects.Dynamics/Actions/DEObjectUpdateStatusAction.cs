using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Schemas.Actions;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Actions
{
	internal class DEObjectUpdateStatusAction : ISchemaObjectUpdateAction
	{
		public void Prepare(VersionedSchemaObjectBase obj)
		{
		}

		public void Persist(VersionedSchemaObjectBase obj)
		{
			DESchemaObjectBase schemaObj = (DESchemaObjectBase)obj;
			obj.Schema.SnapshotTable.IsNotEmpty(tableName => DESnapshotBasicAdapter.Instance.UpdateCurrentSnapshotStatus(schemaObj));
		}
	}
}
