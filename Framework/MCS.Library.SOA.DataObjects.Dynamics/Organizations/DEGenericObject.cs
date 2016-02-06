using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Organizations
{
	[Serializable]
	public class DEGenericObject : DESchemaObjectBase
	{
		public DEGenericObject(string schemaTypeString) :
			base(schemaTypeString)
		{
		}
	}
}
