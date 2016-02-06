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
	internal class DEObjectSnapshotAction : ISchemaObjectUpdateAction
	{
		public void Prepare(VersionedSchemaObjectBase obj)
		{
		}

		public void Persist(VersionedSchemaObjectBase obj)
		{
			var schemaObj = (DESchemaObjectBase)obj;

			//入实体快照
            obj.Schema.SnapshotTable.IsNotEmpty(tableName => 
                DESnapshotBasicAdapter.Instance.UpdateCurrentSnapshot(schemaObj, obj.Schema.SnapshotTable, SnapshotModeDefinition.IsInSnapshot));

            //入SchemaObject快照
			if (obj.Schema.ToSchemaObjectSnapshot)
				DESnapshotBasicAdapter.Instance.UpdateCurrentSnapshot(schemaObj, "DE.SchemaObjectSnapshot", SnapshotModeDefinition.IsInCommonSnapshot);
		}
	}
}
