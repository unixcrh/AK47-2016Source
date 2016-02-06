using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.DataObjects;
using System.Data;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Organizations
{
	/// <summary>
	/// 表示成员关系
	/// </summary>
	[Serializable]
	public class DEMemberRelation : DESimpleRelationBase
	{
		/// <summary>
		/// 初始化<see cref="DEMemberRelation"/>的新实例
		/// </summary>
		public DEMemberRelation() :
			base(DEStandardObjectSchemaType.Entity_FieldsRelation.ToString())
		{
		}

		/// <summary>
		/// 使用指定的容器对象和成员对象 初始化<see cref="DEMemberRelation"/>的新实例
		/// </summary>
		/// <param name="container">容器对象</param>
		/// <param name="member">成员对象</param>
		public DEMemberRelation(DESchemaObjectBase container, DESchemaObjectBase member) :
            base(container, member, DEStandardObjectSchemaType.Entity_FieldsRelation.ToString())
		{
		}

        /// <summary>
        /// 使用指定的容器对象和成员对象 初始化<see cref="DEMemberRelation"/>的新实例
        /// </summary>
        /// <param name="container">容器对象</param>
        /// <param name="member">成员对象</param>
        public DEMemberRelation(DESchemaObjectBase container, DESchemaObjectBase member, DEStandardObjectSchemaType relationType) :
            base(container, member, relationType.ToString())
        {
        }
	}
}
