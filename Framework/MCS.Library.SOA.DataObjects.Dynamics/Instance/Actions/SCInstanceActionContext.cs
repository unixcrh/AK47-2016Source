using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Actions
{
	/// <summary>
	/// 动态实体操作的对象上下文
	/// </summary>
	public class SCInstanceActionContext
	{
		private static readonly SCInstanceActionContext _Instance = new SCInstanceActionContext();

		private SCInstanceActionContext()
		{
		}

		/// <summary>
		/// 获取一个表示当前上下文的<see cref="SCInstanceActionContext"/>实例。
		/// </summary>
		public static SCInstanceActionContext Current
		{
			get
			{
				return SCInstanceActionContext._Instance;
			}
		}

		/// <summary>
		/// 获取一个保存上下文对象的<see cref="T:Dictionary^2"/>。
		/// </summary>
		public Dictionary<string, object> Context
		{
			get
			{
				return DBTimePointActionContext.Current.Context;
			}
		}

		/// <summary>
		/// 获取更新数据时的原始数据
		/// </summary>
		public NoVersionedEntityInstanceObjectBase OriginalObject
		{
			get
			{
				object result = null;

				DBTimePointActionContext.Current.Context.TryGetValue("SCActionContext_OriginalObject", out result);

                return (NoVersionedEntityInstanceObjectBase)result;
			}
			set
			{
				DBTimePointActionContext.Current.Context["SCActionContext_OriginalObject"] = value;
			}
		}

		/// <summary>
		/// 获取正在更新的数据
		/// </summary>
        public NoVersionedEntityInstanceObjectBase CurrentObject
		{
			get
			{
				object result = null;

				DBTimePointActionContext.Current.Context.TryGetValue("SCActionContext_CurrentObject", out result);

                return (NoVersionedEntityInstanceObjectBase)result;
			}
			set
			{
				DBTimePointActionContext.Current.Context["SCActionContext_CurrentObject"] = value;
			}
		}

		/// <summary>
		/// 获取或设置表示操作的时间点的<see cref="DateTime"/> ，为<see cref="DateTime.MinValue"/>时表示当前时间。
		/// </summary>
		public DateTime TimePoint
		{
			get
			{
				return DBTimePointActionContext.Current.TimePoint;
			}

			set
			{
				DBTimePointActionContext.Current.TimePoint = value;
			}
		}

		/// <summary>
		/// 执行<paramref name="action"/>指定的操作。
		/// </summary>
		/// <param name="action">操作方法 或 <see langword="null"/>表示无操作。</param>
		public void DoActions(Action action)
		{
			DBTimePointActionContext.Current.DoActions(action);
		}

		/// <summary>
		/// 清除时间上下文
		/// </summary>
		public static void Clear()
		{
			DBTimePointActionContext.Clear();
		}
	}
}
