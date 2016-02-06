using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Security.AUObjects.DataSources
{
	[DataObject]
	public abstract class AUDataViewDataSource : DataViewDataSourceQueryAdapterBase
	{
		protected override string GetConnectionName()
		{
			return AUCommon.DBConnectionName;
		}
	}
}
