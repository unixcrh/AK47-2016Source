using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects
{
	public class WfGroupQuery : ObjectDataSourceQueryAdapterBase<WfGroup, WfGroupCollection>
	{
		protected override string GetConnectionName()
		{
			return ConnectionDefine.UserRelativeInfoConnectionName;
		}

		protected override void OnBuildQueryCondition(QueryCondition qc)
		{
			qc.OrderByClause = qc.OrderByClause.IsNotEmpty() ? qc.OrderByClause : "GROUP_NAME";
			qc.SelectFields = "*";
		}
	}
}
