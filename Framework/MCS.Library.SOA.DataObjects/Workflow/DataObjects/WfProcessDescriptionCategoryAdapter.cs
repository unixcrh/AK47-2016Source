using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;

namespace MCS.Library.SOA.DataObjects.Workflow
{
	public class WfProcessDescriptionCategoryAdapter :
		   UpdatableAndLoadableAdapterBase<WfProcessDescriptionCategory, WfProcessDescriptionCategoryCollection>
	{
		public static readonly WfProcessDescriptionCategoryAdapter Instance = new WfProcessDescriptionCategoryAdapter();

		private WfProcessDescriptionCategoryAdapter() { }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
	}
}
