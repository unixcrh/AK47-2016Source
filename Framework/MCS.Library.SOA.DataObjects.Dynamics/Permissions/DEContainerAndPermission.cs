using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System.Diagnostics;
using MCS.Library.SOA.DataObjects.Dynamics.Debugger;

namespace MCS.Library.SOA.DataObjects.Dynamics.Permissions
{
	/// <summary>
	/// 表示容器和权限的对象。用于根据MemberID查询返回的结果
	/// </summary>
	[Serializable]
	public struct DEContainerAndPermission
	{
		/// <summary>
		/// 根据ContainerID和Permission构造SCContainerAndPermission
		/// </summary>
		/// <param name="containerID"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		public static DEContainerAndPermission Construct(string containerID, string containerPermission)
		{
			containerID.CheckStringIsNullOrEmpty("containerID");
			containerPermission.CheckStringIsNullOrEmpty("containerPermission");

			DEContainerAndPermission result = new DEContainerAndPermission();

			result.ContainerID = containerID;
			result.ContainerPermission = containerPermission;

			return result;
		}

		[ORFieldMapping("ContainerID")]
		public string ContainerID
		{
			get;
			set;
		}

		[ORFieldMapping("ContainerPermission")]
		public string ContainerPermission
		{
			get;
			set;
		}
	}

	/// <summary>
	/// 表示容器和权限的对象集合。用于根据MemberID查询返回的结果
	/// </summary>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DEContainerAndPermissionCollection.DictionaryDebugView))]
	public class DEContainerAndPermissionCollection : SerializableEditableKeyedDataObjectCollectionBase<DEContainerAndPermission, DEContainerAndPermission>
	{
		public DEContainerAndPermissionCollection()
		{
		}

		protected DEContainerAndPermissionCollection(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}

		/// <summary>
		/// 判断指定的ContaienrID和Permission组合是否存在
		/// </summary>
		/// <param name="containerID"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool ContainsKey(string containerID, string containerPermission)
		{
			return base.ContainsKey(DEContainerAndPermission.Construct(containerID, containerPermission));
		}

		protected override DEContainerAndPermission GetKeyForItem(DEContainerAndPermission item)
		{
			return item;
		}

		class DictionaryDebugView
		{
			private DEContainerAndPermissionCollection collection = null;

			public DictionaryDebugView(DEContainerAndPermissionCollection collection)
			{
				this.collection = collection;
			}

			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ListKeyAndValue[] Objects
			{
				get
				{
					ListKeyAndValue[] keys = new ListKeyAndValue[this.collection.Count];

					int i = 0;
					foreach (DEContainerAndPermission obj in collection)
					{
						keys[i] = new ListKeyAndValue(this.collection, obj.ContainerID, obj.ContainerPermission);
						i++;
					}

					return keys;
				}
			}


		}
	}
}
