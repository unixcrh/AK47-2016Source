using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Locks
{
	[Serializable]
	public class DECheckLockException : Exception
	{
		public DECheckLockException() :
			base()
		{
		}

		public DECheckLockException(string message) :
			base(message)
		{
		}

		public DECheckLockException(string message, System.Exception innerException) :
			base(message, innerException)
		{
		}

		public static string CheckLockResultToMessage(DECheckLockResult checkResult)
		{
			checkResult.NullCheck("checkResult");

			StringBuilder strB = new StringBuilder();

			strB.AppendFormat("申请{0}失败。", EnumItemDescriptionAttribute.GetDescription(checkResult.Lock.LockType));

			if (OguBase.IsNotNullOrEmpty(checkResult.Lock.LockPerson))
				strB.AppendFormat("正在由\"{0}\"执行\"{1}\"。", checkResult.Lock.LockPerson.DisplayName, checkResult.Lock.Description);
			else
				strB.AppendFormat("正在执行\"{0}\"", checkResult.Lock.Description);

			strB.Append("请稍后再尝试。");

			return strB.ToString();
		}
	}
}
