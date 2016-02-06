using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics.Actions
{
	public class DEOperationSnapshotAction : IDEObjectOperationAction
	{
		#region ISCObjectOperationAction Members

		public void BeforeExecute(DEOperationType operationType)
		{
		}

		public void AfterExecute(DEOperationType operationType)
		{
            //DEOperationSnapshot snapshot = new DEOperationSnapshot() { DEOperationType = operationType };

            //DEOperationSnapshotAdapter.Instance.Update(snapshot);
		}

		#endregion
	}
}
