using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Actions
{
    /// <summary>
    /// 表示操作的类型
    /// </summary>
    public enum DEOperationType
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None = 0,

        /// <summary>
        /// 添加实体
        /// </summary>
        [EnumItemDescription("添加实体")]
        AddEntity,

        /// <summary>
        /// 删除实体
        /// </summary>
        [EnumItemDescription("删除实体")]
        DeleteEntity,

        /// <summary>
        /// 修改实体
        /// </summary>
        [EnumItemDescription("修改实体")]
        UpdateEntity,

        /// <summary>
        /// 添加实体字段
        /// </summary>
        [EnumItemDescription("添加实体字段")]
        AddEntityField,

        /// <summary>
        /// 删除实体字段
        /// </summary>
        [EnumItemDescription("删除实体字段")]
        DeleteEntityField,

        /// <summary>
        /// 修改实体字段
        /// </summary>
        [EnumItemDescription("修改实体字段")]
        UpdateEntityField,

        /// <summary>
        /// 添加实体映射
        /// </summary>
        [EnumItemDescription("添加实体映射")]
        AddEntityMapping,

        /// <summary>
        /// 添加实体字段映射
        /// </summary>
        [EnumItemDescription("添加实体字段映射")]
        AddEntityFieldMapping,

        /// <summary>
        /// 添加外部实与外部体字段映射
        /// </summary>
        [EnumItemDescription("添加外部实与外部体字段映射")]
        AddOuterEntityFieldMapping,

        /// <summary>
        /// 删除外部实体
        /// </summary>
        [EnumItemDescription("删除外部实体")]
        DeleteOuterEntity,

        /// <summary>
        /// 添加ETL实体任务
        /// </summary>
        [EnumItemDescription("添加ETL实体任务")]
        AddEntityJob,

        /// <summary>
        /// 修改ETL实体任务
        /// </summary>
        [EnumItemDescription("修改ETL实体任务")]
        EditEntityJob,

        /// <summary>
        /// 删除ETL实体任务
        /// </summary>
        [EnumItemDescription("删除ETL实体任务")]
        DeleteEntityJob,
        /// <summary>
        /// 添加ETL实体
        /// </summary>
        [EnumItemDescription("添加ETL实体")]
        AddETLEntity,

        /// <summary>
        /// 修改ETL实体
        /// </summary>
        [EnumItemDescription("修改ETL实体")]
        UpdateETLEntity,

        /// <summary>
        /// 删除ETL实体
        /// </summary>
        [EnumItemDescription("删除ETL实体")]
        DeleteETLEntity,


        /// <summary>
        /// 添加ETL实体映射
        /// </summary>
        [EnumItemDescription("添加ETL实体映射")]
        AddETLEntityMapping,

        /// <summary>
        /// 添加ETL实体字段映射
        /// </summary>
        [EnumItemDescription("添加ETL实体字段映射")]
        AddETLEntityFieldMapping,


        /// <summary>
        /// 添加外部ETL实体字段映射
        /// </summary>
        [EnumItemDescription("添加ETL外部实体字段映射")]
        AddOutETLEntityFieldMapping,

        ///// <summary>
        ///// 增加用户
        ///// </summary>
        //[EnumItemDescription("增加用户")]
        //AddUser,

        ///// <summary>
        ///// 修改用户
        ///// </summary>
        //[EnumItemDescription("修改用户")]
        //UpdateUser,

        ///// <summary>
        ///// 删除用户
        ///// </summary>
        //[EnumItemDescription("删除用户")]
        //DeleteUser,

        ///// <summary>
        ///// 改变对象的所有者
        ///// </summary>
        //[EnumItemDescription("改变所有者")]
        //ChangeOwner,

        ///// <summary>
        ///// 在组织中添加用户
        ///// </summary>
        //[EnumItemDescription("在组织中添加用户")]
        //AddUserToOrganization,

        ///// <summary>
        ///// 从组织中删除用户
        ///// </summary>
        //[EnumItemDescription("从组织中删除用户")]
        //RemoveUserFromOrganization,

        ///// <summary>
        ///// 设置用户的默认组织
        ///// </summary>
        //[EnumItemDescription("设置用户的默认组织")]
        //SetUserDefaultOrganization,

        ///// <summary>
        ///// 增加组织
        ///// </summary>
        //[EnumItemDescription("增加组织")]
        //AddOrganization,

        ///// <summary>
        ///// 修改组织
        ///// </summary>
        //[EnumItemDescription("修改组织")]
        //UpdateOrganization,

        ///// <summary>
        ///// 删除组织
        ///// </summary>
        //[EnumItemDescription("删除组织")]
        //DeleteOrganization,

        ///// <summary>
        ///// 移动对象
        ///// </summary>
        //[EnumItemDescription("移动对象")]
        //MoveObject,

        ///// <summary>
        ///// 增加群组
        ///// </summary>
        //[EnumItemDescription("增加群组")]
        //AddGroup,

        ///// <summary>
        ///// 修改群组
        ///// </summary>
        //[EnumItemDescription("修改群组")]
        //UpdateGroup,

        ///// <summary>
        ///// 删除群组
        ///// </summary>
        //[EnumItemDescription("删除群组")]
        //DeleteGroup,

        ///// <summary>
        ///// 将用户添加到群组
        ///// </summary>
        //[EnumItemDescription("将用户添加到群组")]
        //AddUserToGroup,

        ///// <summary>
        ///// 将用户从到群组中删除
        ///// </summary>
        //[EnumItemDescription("将用户从到群组中删除")]
        //RemoveUserFromGroup,

        //[EnumItemDescription("为用户指定秘书")]
        //AddSecretaryToUser,

        //[EnumItemDescription("为用户撤销秘书")]
        //RemoveSecretaryFromUser,

        ///// <summary>
        ///// 增加应用
        ///// </summary>
        //[EnumItemDescription("增加应用")]
        //AddApplication,

        ///// <summary>
        ///// 修改应用
        ///// </summary>
        //[EnumItemDescription("修改应用")]
        //UpdateApplication,

        ///// <summary>
        ///// 删除应用
        ///// </summary>
        //[EnumItemDescription("删除应用")]
        //DeleteApplication,

        ///// <summary>
        ///// 增加角色
        ///// </summary>
        //[EnumItemDescription("增加角色")]
        //AddRole,

        ///// <summary>
        ///// 修改角色
        ///// </summary>
        //[EnumItemDescription("修改角色")]
        //UpdateRole,

        ///// <summary>
        ///// 删除角色
        ///// </summary>
        //[EnumItemDescription("删除角色")]
        //DeleteRole,

        ///// <summary>
        ///// 在角色中添加成员
        ///// </summary>
        //[EnumItemDescription("在角色中添加成员")]
        //AddMemberToRole,

        ///// <summary>
        ///// 从角色中删除成员
        ///// </summary>
        //[EnumItemDescription("从角色中删除成员")]
        //RemoveMemberFromRole,

        ///// <summary>
        ///// 增加权限
        ///// </summary>
        //[EnumItemDescription("增加权限")]
        //AddPermission,

        ///// <summary>
        ///// 修改权限
        ///// </summary>
        //[EnumItemDescription("修改权限")]
        //UpdatePermission,

        ///// <summary>
        ///// 删除权限
        ///// </summary>
        //[EnumItemDescription("删除权限")]
        //DeletePermission,

        ///// <summary>
        ///// 关联角色和权限
        ///// </summary>
        //[EnumItemDescription("关联角色和权限")]
        //JoinRoleAndPermission,

        ///// <summary>
        ///// 解除角色和权限的关联
        ///// </summary>
        //[EnumItemDescription("解除角色和权限的关联")]
        //DisjoinRoleAndPermission,

        //[EnumItemDescription("修改角色的条件")]
        //UpdateRoleConditions,

        //[EnumItemDescription("修改群组的条件")]
        //UpdateGroupConditions,

        //[EnumItemDescription("递归删除对象")]
        //DeleteObjectsRecursively,

        //[EnumItemDescription("递归替换权限")]
        //ReplaceAclRecursively,

        //[EnumItemDescription("修改对象的访问控制权限")]
        //UpdateObjectAcl,

        //[EnumItemDescription("修改对象的图片")]
        //UpdateObjectImage
    }
}
