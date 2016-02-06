using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Security.Executors;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
	public class DEMemberRelativeExecutor : DEMemberRelativeExecutorBase
	{
		public DEMemberRelativeExecutor(DEOperationType opType, DESchemaObjectBase container, DEBase member)
			: base(opType, container, member)
		{
		}

		protected override DESimpleRelationBase CreateRelation(DESchemaObjectBase container, DESchemaObjectBase member)
		{
			return new DEMemberRelation(container, member);
		}
	}
}
