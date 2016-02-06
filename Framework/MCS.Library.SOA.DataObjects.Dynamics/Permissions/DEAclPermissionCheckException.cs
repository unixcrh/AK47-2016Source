using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Permissions
{
	/// <summary>
	/// 权限检查的异常类
	/// </summary>
	public class DEAclPermissionCheckException : System.Exception
	{
		public DEAclPermissionCheckException()
		{
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="message"></param>
		public DEAclPermissionCheckException(string message) :
			base(message)
		{
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public DEAclPermissionCheckException(string message, System.Exception exception) :
			base(message, exception)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="opType"></param>
		/// <param name="permissionName"></param>
		public static DEAclPermissionCheckException CreateException(DEOperationType opType, DESchemaDefine schemaInfo, string permissionName)
		{
			string opDesp = EnumItemDescriptionAttribute.GetDescription(opType);

			DEAclPermissionItem permissionInfo = schemaInfo.PermissionSet[permissionName];

			string permissionDesp = string.Empty;

			if (permissionInfo != null)
			{
				permissionDesp = permissionInfo.Description;

				if (permissionDesp.IsNullOrEmpty())
					permissionDesp = permissionInfo.Name;
			}

			return new DEAclPermissionCheckException(string.Format("不能执行\"{0}\"操作，您没有\"{0}\"权限", opDesp, permissionDesp));
		}
	}
}
