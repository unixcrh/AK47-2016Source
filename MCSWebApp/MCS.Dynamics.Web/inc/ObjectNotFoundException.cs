using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Dynamics.Web
{
	/// <summary>
	/// 表示对象未找到
	/// </summary>
	[Serializable]
	public class ObjectNotFoundException : Exception
	{
		public ObjectNotFoundException()
			: base("指定的对象未找到，请检查对象在当前时间中是否存在。")
		{
		}

		public ObjectNotFoundException(string message)
			: base(message)
		{
		}

		public ObjectNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected ObjectNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

		public static ObjectNotFoundException CreateForID(string id)
		{
			return new ObjectNotFoundException(string.Format("未找到ID为 {0} 的对象，请检查对象在当前时间中是否存在。", id));
		}
	}
}