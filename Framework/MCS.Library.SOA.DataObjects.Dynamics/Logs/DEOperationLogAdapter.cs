using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Actions;
using MCS.Library.SOA.DataObjects.Schemas.Actions;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Logs
{
	public class DEOperationLogAdapter
	{
		/// <summary>
		/// 表示<see cref="DEOperationLogAdapter"/>的实例，此字段为只读
		/// </summary>
		public static readonly DEOperationLogAdapter Instance = new DEOperationLogAdapter();

		private DEOperationLogAdapter()
		{
		}

		public DEOperationLogCollection LoadByResourceID(string resourceID)
		{
			resourceID.CheckStringIsNullOrEmpty("resourceID");

			WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

			builder.AppendItem("ResourceID", resourceID);

			return Load(builder);
		}

		public DEOperationLog Load(int id)
		{
			InSqlClauseBuilder builder = new InSqlClauseBuilder("ID");

			builder.AppendItem(id);

			return Load(builder).FirstOrDefault();
		}

		public DEOperationLogCollection Load(IConnectiveSqlClause sqlClause)
		{
			DEOperationLogCollection result = null;

			VersionedObjectAdapterHelper.Instance.FillData(GetMappingInfo().TableName, sqlClause, this.GetConnectionName(),
				(view) =>
				{
					result = new DEOperationLogCollection();

					ORMapping.DataViewToCollection(result, view);
				});

			return result;
		}

		public void Insert(DEOperationLog log)
		{
			if (log != null)
			{
                log.CreateTime = SCActionContext.Current.TimePoint;
				StringBuilder strB = new StringBuilder(256);

				strB.Append(ORMapping.GetInsertSql(log, this.GetMappingInfo(), TSqlBuilder.Instance));
				strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
				strB.Append("SELECT SCOPE_IDENTITY()");

				Decimal newID = (Decimal)DbHelper.RunSqlReturnScalar(strB.ToString(), this.GetConnectionName());

				log.ID = Decimal.ToInt32(newID);
			}
		}

		protected virtual ORMappingItemCollection GetMappingInfo()
		{
			return ORMapping.GetMappingInfo<DEOperationLog>();
		}

		protected virtual string GetConnectionName()
		{
			return DEConnectionDefine.DBConnectionName;
		}
	}
}
