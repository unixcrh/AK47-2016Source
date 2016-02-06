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
	public class DEMemberCollectionRelativeExecutor : DEMemberCollectionRelativeExecutorBase
	{
        public DEMemberCollectionRelativeExecutor(DEOperationType opType, DEBase container, DESchemaObjectCollection members)
            : base(opType, container, members)
        {
        }

        public DEMemberCollectionRelativeExecutor(DEOperationType opType, DEBase container, DESchemaObjectCollection members,DEStandardObjectSchemaType relationType)
            : base(opType, container, members, relationType)
        {
        }

        protected override DESimpleRelationBase CreateRelation(DESchemaObjectBase container, DESchemaObjectBase member, DEStandardObjectSchemaType relationType)
        {
            return new DEMemberRelation(container, member, relationType);
        }
	}
}
