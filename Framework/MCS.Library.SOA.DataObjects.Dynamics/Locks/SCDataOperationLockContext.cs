using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Caching;

namespace MCS.Library.SOA.DataObjects.Dynamics.Locks
{
	/// <summary>
	/// 与数据操作锁相关的上下文操作对象
	/// </summary>
	public class DEDataOperationLockContext
	{
		private DELock _Lock = null;

		private DEDataOperationLockContext()
		{
		}

		public static DEDataOperationLockContext Current
		{
			get
			{
				DEDataOperationLockContext result = (DEDataOperationLockContext)ObjectContextCache.Instance.GetOrAddNewValue("SCDataOperationLockContext",
						(cache, key) =>
						{
							DEDataOperationLockContext context = new DEDataOperationLockContext();
							cache.Add(key, context);

							return context;
						});

				return result;
			}
		}

		public DELock Lock
		{
			get
			{
				return this._Lock;
			}
		}

		/// <summary>
		/// 执行需要上锁的操作
		/// </summary>
		/// <param name="autoAddLock">是否自动加锁</param>
		/// <param name="description"></param>
		/// <param name="action"></param>
		public void DoAddLockAction(bool autoAddLock, string description, Action action)
		{
			DoAddLockAction(autoAddLock, DELockSettings.GetConfig().DefaultEffectiveTime, description, action);
		}

		/// <summary>
		/// 执行需要上锁的操作
		/// </summary>
		/// <param name="autoAddLock">是否自动加锁</param>
		/// <param name="effectiveTime"></param>
		/// <param name="description"></param>
		/// <param name="action"></param>
		public void DoAddLockAction(bool autoAddLock, TimeSpan effectiveTime, string description, Action action)
		{
			if (action != null)
			{
				bool lockCreator = false;

				if (this._Lock == null && autoAddLock)
				{
					lockCreator = true;
					AddLock(effectiveTime, description);
				}

				action();

				if (lockCreator)
					DeleteLock();
			}
		}

		/// <summary>
		/// 执行需要延迟锁的操作，如果之前没有锁，则不延迟
		/// </summary>
		/// <param name="description"></param>
		/// <param name="action"></param>
		public void DoExtendLockAction(string description, Action action)
		{
			DoExtendLockAction(DELockSettings.GetConfig().DefaultEffectiveTime, description, action);
		}

		/// <summary>
		/// 执行需要延迟锁的操作，如果之前没有锁，则不延迟
		/// </summary>
		/// <param name="effectiveTime"></param>
		/// <param name="description"></param>
		/// <param name="action"></param>
		public void DoExtendLockAction(TimeSpan effectiveTime, string description, Action action)
		{
			if (action != null)
			{
				if (this._Lock != null)
				{
					this._Lock.EffectiveTime = effectiveTime;
					this._Lock.Description = description;

					ExtendLock();
				}

				action();
			}
		}

		public void AddLock(string description)
		{
			AddLock(DELockSettings.GetConfig().DefaultEffectiveTime, description);
		}

		public void AddLock(TimeSpan effectiveTime, string description)
		{
			DELock lockData = DELock.CreateDefaultDataOperationLock();
			lockData.EffectiveTime = effectiveTime;
			lockData.Description = description;

			DECheckLockResult checkResult = DELockAdapter.Instance.AddLock(lockData);

			if (checkResult.Available == false)
				throw new DECheckLockException(DECheckLockException.CheckLockResultToMessage(checkResult));

			this._Lock = checkResult.Lock;
		}

		/// <summary>
		/// 延长锁的时间
		/// </summary>
		public void ExtendLock()
		{
			DELock lockData = this._Lock;

			if (lockData == null)
				lockData = DELock.CreateDefaultDataOperationLock();

			DECheckLockResult checkResult = DELockAdapter.Instance.ExtendLockTime(lockData);

			this._Lock = checkResult.Lock;
		}

		/// <summary>
		/// 删除锁
		/// </summary>
		public void DeleteLock()
		{
			DELock lockData = this._Lock;

			if (lockData == null)
				lockData = DELock.CreateDefaultDataOperationLock();

			DELockAdapter.Instance.DeleteLock(lockData);
			this._Lock = null;
		}

		/// <summary>
		/// 清除上下文
		/// </summary>
		public static void Clear()
		{
			if (ExistsInContext)
				ObjectContextCache.Instance.Remove("SCDataOperationLockContext");
		}

		/// <summary>
		/// 是否在上下文中存在
		/// </summary>
		public static bool ExistsInContext
		{
			get
			{
				return ObjectContextCache.Instance.ContainsKey("SCDataOperationLockContext");
			}
		}
	}
}
