using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Passport
{
    using MCS.Library.Core;

    /// <summary>
    /// 访问权限的检查器
    /// </summary>
    public static class AccessPermissionChecker
    {
        /// <summary>
        /// 用户是否包含某个类型上定义的权限
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UserInRolesOrPermissions(this MemberInfo mi, IUser user)
        {
            mi.NullCheck("mi");

            bool result = false;

            if (HasPermissionRestricts(mi) == false)
                result = true;
            else
                result = InnerUserHasPermissions(mi, user);

            return result;
        }

        /// <summary>
        /// 根据类型上的mi上的特性，检查用户是否属于某个角色或者拥有哪个权限
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="user"></param>
        public static void CheckUserInRolesOrPermissions(this MemberInfo mi, IUser user)
        {
            ExceptionHelper.FalseThrow(UserInRolesOrPermissions(mi, user), "您没有权限执行此操作");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleDesp"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UserInRoles(this RoleDescriptionAttribute roleDesp, IUser user)
        {
            bool result = false;

            if (roleDesp != null && user != null)
            {
                ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects(roleDesp.Description);

                foreach (ApplicationAndPermissionObjects pod in pods)
                {
                    foreach (string roleCodeName in pod.PermissionObjectCodeNames)
                    {
                        if (user.Roles[pod.ApplicationCodeName, roleCodeName] != null)
                        {
                            result = true;
                            break;
                        }
                    }

                    if (result)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionDesp"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UserHasPermissions(this PermissionDescriptionAttribute permissionDesp, IUser user)
        {
            bool result = false;

            if (permissionDesp != null && user != null)
            {
                ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects(permissionDesp.Description);

                foreach (ApplicationAndPermissionObjects pod in pods)
                {
                    foreach (string permissionCodeName in pod.PermissionObjectCodeNames)
                    {
                        if (user.Permissions[pod.ApplicationCodeName, permissionCodeName] != null)
                        {
                            result = true;
                            break;
                        }
                    }

                    if (result)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleGroups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UserInRoleGroups(this RoleGroupsAttribute roleGroups, IUser user)
        {
            bool result = false;

            if (roleGroups != null && user != null)
                result = RolesDefineConfig.GetConfig().IsCurrentUserInRoles(user, roleGroups.Parse().ToArray());

            return result;
        }

        /// <summary>
        /// 是否包含权限的限制
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        private static bool HasPermissionRestricts(MemberInfo mi)
        {
            RoleDescriptionAttribute roleDesp = AttributeHelper.GetCustomAttribute<RoleDescriptionAttribute>(mi);
            PermissionDescriptionAttribute permissionDesp = AttributeHelper.GetCustomAttribute<PermissionDescriptionAttribute>(mi);
            RoleGroupsAttribute roleGroups = AttributeHelper.GetCustomAttribute<RoleGroupsAttribute>(mi);

            return (roleDesp != null || permissionDesp != null || roleGroups != null);
        }

        private static bool InnerUserHasPermissions(MemberInfo mi, IUser user)
        {
            bool result = AttributeHelper.GetCustomAttribute<RoleDescriptionAttribute>(mi).UserInRoles(user);

            if (result == false)
                result = AttributeHelper.GetCustomAttribute<PermissionDescriptionAttribute>(mi).UserHasPermissions(user);

            if (result == false)
                result = AttributeHelper.GetCustomAttribute<RoleGroupsAttribute>(mi).UserInRoleGroups(user);

            return result;
        }
    }
}
