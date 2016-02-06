using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
	/// <summary>
	/// Executor执行时，状态检查出现异常时的异常类
	/// </summary>
	public class DEStatusCheckException : System.Exception
	{
		private DESchemaObjectBase _RelativeObject = null;
		private DEOperationType _OperationType = DEOperationType.None;

		/// <summary>
		/// 
		/// </summary>
		public DEStatusCheckException()
			: base()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public DEStatusCheckException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		public DEStatusCheckException(string message, System.Exception innerException) :
			base(message, innerException)
		{
		}

		public DEStatusCheckException(DESchemaObjectBase relativeObject, DEOperationType opType)
			: base(string.Format("对象\"{0}\"的状态不是正常状态，不能执行{1}操作", relativeObject.Properties["Name"], EnumItemDescriptionAttribute.GetDescription(opType)))
		{
			this._RelativeObject = relativeObject;
			this._OperationType = opType;
		}
	}
}
