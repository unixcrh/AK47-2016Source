using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Locks
{
	/// <summary>
	/// 锁检查状态
	/// </summary>
	[Serializable]
	public class DECheckLockResult
	{
		public bool Available
		{
			get
			{
				return this.LockStatus == DECheckLockStatus.NotLocked || this.LockStatus == DECheckLockStatus.LockExpired;
			}
		}

		public DECheckLockStatus LockStatus
		{
			get;
			set;
		}

		private DELock _Lock = null;

		public DELock Lock
		{
			get
			{
				return this._Lock;
			}
			internal set
			{
				this._Lock = value;
			}
		}
	}
}
