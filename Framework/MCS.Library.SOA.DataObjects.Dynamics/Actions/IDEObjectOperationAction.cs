using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Actions
{
	public interface IDEObjectOperationAction
	{
		/// <summary>
		/// 执行之前
		/// </summary>
		/// <param name="operationType"></param>
		void BeforeExecute(DEOperationType operationType);
		
		/// <summary>
		/// 执行之后
		/// </summary>
		/// <param name="operationType"></param>
		void AfterExecute(DEOperationType operationType);
	}

	[Serializable]
	public class DEObjectOperationActionCollection : EditableDataObjectCollectionBase<IDEObjectOperationAction>
	{
		/// <summary>
		/// 执行之前
		/// </summary>
		/// <param name="obj">一个<see cref="DESchemaObjectBase"/>实例</param>
		public void BeforeExecute(DEOperationType operationType)
		{
			this.ForEach(action => action.BeforeExecute(operationType));
		}

		/// <summary>
		/// 执行之后
		/// </summary>
		/// <param name="operationType"></param>
		public void AfterExecute(DEOperationType operationType)
		{
			this.ForEach(action => action.AfterExecute(operationType));
		}
	}
}
