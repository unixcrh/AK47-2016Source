using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Permissions;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
//using MCS.Library.SOA.DataObjects.Security.Permissions;

namespace MCS.Library.SOA.DataObjects.Dynamics
{
	public interface IDEMemberObject
	{
		DEObjectContainerRelationCollection GetCurrentMemberOfRelations();
	}

    ///// <summary>
    ///// 应用程序所包含的对象，包括角色和权限
    ///// </summary>
    //public interface ISCApplicationMember
    //{
    //    /// <summary>
    //    /// 所属的应用
    //    /// </summary>
    //    SCApplication CurrentApplication
    //    {
    //        get;
    //    }
    //}

	public interface IDEContainerObject
	{
		DEObjectMemberRelationCollection GetCurrentMembersRelations();
		DESchemaObjectCollection GetCurrentMembers();
	}

    ///// <summary>
    ///// 用户容器需要实现的接口
    ///// </summary>
    //public interface ISCUserContainerObject
    //{
    //    /// <summary>
    //    /// 得到所有的用户，这个实现开销有可能很大
    //    /// </summary>
    //    /// <returns></returns>
    //    DESchemaObjectCollection GetCurrentUsers();
    //}


    ///// <summary>
    ///// 关系类容器需要实现的接口，例如组织
    ///// </summary>
    //public interface IDERelationContainer
    //{
    //    [NoMapping]
    //    [ScriptIgnore]
    //    DEChildrenRelationObjectCollection AllChildrenRelations
    //    {
    //        get;
    //    }

    //    [NoMapping]
    //    [ScriptIgnore]
    //    DEChildrenRelationObjectCollection CurrentChildrenRelations
    //    {
    //        get;
    //    }

    //    [NoMapping]
    //    [ScriptIgnore]
    //    SchemaObjectCollection AllChildren
    //    {
    //        get;
    //    }

    //    [NoMapping]
    //    [ScriptIgnore]
    //    SchemaObjectCollection CurrentChildren
    //    {
    //        get;
    //    }
    //}

	/// <summary>
	/// Acl容器所必须实现的接口
	/// </summary>
	public interface IDEAclContainer
	{
		/// <summary>
		/// 得到Acl中的成员
		/// </summary>
		/// <returns></returns>
		DEAclMemberCollection GetAclMembers();
	}

	/// <summary>
	/// Acl成员所必须实现的接口
	/// </summary>
	public interface IDEAclMember
	{
		DEAclContainerCollection GetAclContainers();
	}
}
