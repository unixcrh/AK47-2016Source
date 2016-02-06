using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;

namespace MCS.Library.SOA.DataObjects.Dynamics.Permissions
{
    /// <summary>
    /// 当前Principal的扩展类，用于进行权限判断
    /// </summary>
    public static class DEPrincipalExtension
    {
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool IsSupervisor(this IPrincipal principal)
        {
            bool result = false;

            //Modified by Haoyk
            //2015-02-02
            //动态实体项目单设管理员
            if (principal != null)
            {
                result = DEPrincipalCache.Instance.GetOrAddNewValue(principal.Identity.Name, (cache, key) =>
                {
                    bool innerResult = false;

                    if (ConfigurationManager.AppSettings["DEAdmin"] != null && ConfigurationManager.AppSettings["DEAdmin"].IsNotEmpty())
                    {
                        string roleStr = ConfigurationManager.AppSettings["DEAdmin"].Trim();

                        if (string.IsNullOrEmpty(ObjectSchemaSettings.GetConfig().AdminRoleFullCodeName))
                        {
                            innerResult = true;
                        }
                        else
                        {
                            IRole role = new OguRole(ObjectSchemaSettings.GetConfig().AdminRoleFullCodeName);
                            if (role.ObjectsInRole.Count == 0)
                                innerResult = true;
                            else
                                innerResult = principal.IsInRole(roleStr);

                            //如果不属于DEAdmin，则进一步判断是否是超级管理员
                            if (innerResult == false)
                            {
                                innerResult = IsSuperAdmin(principal);
                            }
                        }
                    }
                    else
                    {
                        //如果没有配置则都认为人人都是管理员
                        innerResult = true;
                    }

                    cache.Add(key, innerResult);
                    return innerResult;
                });
            }

            return result;
        }

        private static bool IsSuperAdmin(IPrincipal principal)
        {
            return DEPrincipalCache.Instance.GetOrAddNewValue(principal.Identity.Name, (cache, key) =>
            {
                bool innerResult = false;
                if (ObjectSchemaSettings.GetConfig().AdminRoleFullCodeName.IsNotEmpty())
                {
                    IRole role = new OguRole(ObjectSchemaSettings.GetConfig().AdminRoleFullCodeName);

                    if (role.ObjectsInRole.Count == 0)
                        innerResult = true;
                    else
                        innerResult = principal.IsInRole(ObjectSchemaSettings.GetConfig().AdminRoleFullCodeName);
                }
                else
                    innerResult = true;

                cache.Add(key, innerResult);

                return innerResult;
            });
        }

        /// <summary>
        /// 是否拥有指定的权限
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="permissions"></param>
        /// <param name="permissionName"></param>
        /// <param name="containerIDs"></param>
        /// <returns></returns>
        public static bool HasPermissions(this IPrincipal principal, DEContainerAndPermissionCollection permissions, string permissionName, params string[] containerIDs)
        {
            bool result = IsSupervisor(principal);

            if (result == false)
            {
                if (principal != null)
                {
                    permissions.NullCheck("permissions");

                    foreach (string containerID in containerIDs)
                    {
                        if (permissions.ContainsKey(containerID, permissionName))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 是否拥有指定的权限
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="permissionName"></param>
        /// <param name="containerIDs"></param>
        /// <returns></returns>
        public static bool HasPermissions(this IPrincipal principal, string permissionName, params string[] containerIDs)
        {
            bool result = IsSupervisor(principal);

            if (result == false)
            {
                if (principal != null)
                {
                    result = HasPermissions(principal, GetPrincipalPermissions(principal, containerIDs), permissionName, containerIDs);
                }
            }

            return result;
        }

        private static DEContainerAndPermissionCollection GetPrincipalPermissions(IPrincipal principal, params string[] containerIDs)
        {
            string calculatedKey = CalculatePrincipalAndPermissionKey(principal, containerIDs);

            return (DEContainerAndPermissionCollection)ObjectContextCache.Instance.GetOrAddNewValue(calculatedKey, (cache, key) =>
            {
                DEContainerAndPermissionCollection permissions = DEAclAdapter.Instance.LoadCurrentContainerAndPermissions(GetUserID(principal), containerIDs);

                cache.Add(key, permissions);

                return permissions;
            });
        }

        private static string CalculatePrincipalAndPermissionKey(IPrincipal principal, params string[] containerIDs)
        {
            StringBuilder strB = new StringBuilder();

            strB.Append(GetUserID(principal));
            strB.Append("-");

            foreach (string containerID in containerIDs)
            {
                strB.Append("-");
                strB.Append(containerID);
            }

            return strB.ToString();
        }

        private static string GetUserID(IPrincipal principal)
        {
            string result = principal.Identity.Name;

            if (principal is DeluxePrincipal)
            {
                result = DeluxeIdentity.CurrentUser.ID;
            }

            return result;
        }
    }
}
