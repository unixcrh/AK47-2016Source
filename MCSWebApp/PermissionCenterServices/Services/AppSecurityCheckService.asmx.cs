using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using MCS.Library.Caching;
using MCS.Library.OGUPermission;
using MCS.Library.Services.Configuration;
using MCS.Library.SOA.DataObjects.Security;
using MCS.Library.SOA.DataObjects.Security.Actions;
using MCS.Library.SOA.DataObjects.Security.Adapters;
using PermissionCenter.Adapters;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Data.Builder;

namespace PermissionCenter.Services
{
    /// <summary>
    /// Summary description for AppSecurityCheckService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AppSecurityCheckService : System.Web.Services.WebService
    {
        #region Web methods
        /// <summary>
        /// 获得所有应用系统的基本信息。
        /// </summary>
        /// <returns>所有应用系统的基本信息。</returns>
        [WebMethod]
        public DataSet GetApplications()
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Applications").ToSchemaNames();

            SchemaObjectCollection objs = SCSnapshotAdapter.Instance.QueryApplications(schemaTypes, false, DateTime.MinValue);

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(objs));

            return ds;
        }

        /// <summary>
        /// 查询指定应用系统中，指定类别的所有角色
        /// </summary>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="rightMask">权限授权类型</param>
        /// <returns>指定应用系统中，指定类别的所有角色</returns>
        [WebMethod]
        public DataSet GetRoles(string appCodeName, RightMaskType rightMask)
        {
            string[] schemaTypes = new string[] { "Roles" };

            SchemaObjectCollection objs = SCSnapshotAdapter.Instance.QueryApplicationObjectsByCodeName(schemaTypes, appCodeName, false, DateTime.MinValue);

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(objs));

            return ds;
        }

        /// <summary>
        /// 查询指定应用系统中，指定类别的所有功能
        /// </summary>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="rightMask">权限授权类型</param>
        /// <returns>指定应用系统中，指定类别的所有功能</returns>
        [WebMethod]
        public DataSet GetFunctions(string appCodeName, RightMaskType rightMask)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Permissions").ToSchemaNames();

            SchemaObjectCollection objs = SCSnapshotAdapter.Instance.QueryApplicationObjectsByCodeName(schemaTypes, appCodeName, false, DateTime.MinValue);

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(objs));

            return ds;
        }

        /// <summary>
        /// 查找指定应用中，具有指定功能的角色。
        /// </summary>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="funcCodeNames">功能的英文标识，多个时用逗号分隔</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetFunctionsRoles(string appCodeName, string funcCodeNames)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Permissions").ToSchemaNames();
            string[] permissionCodeNames = OGUReaderService.SplitObjectValues(funcCodeNames);

            SchemaObjectCollection objs = SCSnapshotAdapter.Instance.QueryPermissionRolesByCodeName(schemaTypes, appCodeName, permissionCodeNames, false, DateTime.MinValue);

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(objs));

            return ds;
        }

        /// <summary>
        /// 查询指定部门范围下，指定应用系统中，指定角色下的所有人员。这个查询是递归的
        /// </summary>
        /// <param name="orgRoot">部门范围的全路径，空串时不做限制，多个时用逗号分隔（在本服务实现中，这个参数被忽略了）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="roleCodeNames">角色的英文标识，多个时用逗号分隔</param>
        /// <param name="delegationMask">权限委派类型</param>
        /// <param name="sidelineMask">人员职位类型</param>
        /// <param name="extAttr">要求获取的扩展属性</param>
        /// <returns>指定部门范围下，指定应用系统中，指定角色下的所有人员</returns>
        [WebMethod]
        public DataSet GetRolesUsers(string orgRoot, string appCodeName, string roleCodeNames, DelegationMaskType delegationMask, SidelineMaskType sidelineMask, string extAttr)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] roleIDs = OGUReaderService.SplitObjectValues(roleCodeNames);

            bool removeDuplicateData = GetMethodSettingAttributeValue("GetChildrenInRoles", "removeDuplicateData", false);

            SCObjectAndRelationCollection relations = SCSnapshotAdapter.Instance.QueryRolesContainsUsers(schemaTypes, appCodeName, roleIDs, removeDuplicateData, false, DateTime.MinValue);

            relations.FillDetails();

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetOguTableBuilder(SchemaInfo.FilterByCategory("Users").ToSchemaNames()).Convert(relations));

            return ds;
        }

        /// <summary>
        /// 查询指定部门范围下，指定应用系统中，指定角色下的所有人员。这个查询是递归的
        /// </summary>
        /// <param name="orgRoot">部门范围的全路径，空串时不做限制，多个时用逗号分隔（在本服务实现中，这个参数被忽略了）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="roleCodeNames">角色的英文标识，多个时用逗号分隔</param>
        /// <param name="delegationMask">权限委派类型</param>
        /// <param name="sidelineMask">人员职位类型</param>
        /// <param name="extAttr">要求获取的扩展属性</param>
        /// <param name="includeMatrixUsers">是否包含矩阵成员</param>
        /// <returns>指定部门范围下，指定应用系统中，指定角色下的所有人员</returns>
        [WebMethod]
        public DataSet GetRolesUsers2(string orgRoot, string appCodeName, string roleCodeNames, DelegationMaskType delegationMask, SidelineMaskType sidelineMask, string extAttr, bool includeMatrixUsers)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] roleIDs = OGUReaderService.SplitObjectValues(roleCodeNames);

            bool removeDuplicateData = GetMethodSettingAttributeValue("GetChildrenInRoles", "removeDuplicateData", false);

            SCObjectAndRelationCollection relations = SCSnapshotAdapter.Instance.QueryRolesContainsUsers(schemaTypes, appCodeName, roleIDs, removeDuplicateData, false, DateTime.MinValue);

            if (includeMatrixUsers)
                FillRoleMatrixRelations(relations, schemaTypes, roleIDs);

            relations.FillDetails();

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetOguTableBuilder(SchemaInfo.FilterByCategory("Users").ToSchemaNames()).Convert(relations));

            return ds;
        }

        /// <summary>
        /// 查询指定用户，在指定应用系统中所拥有的角色
        /// </summary>
        /// <param name="userValue">用户身份标识（由userValueType参数指定类型）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="userValueType">人员标识类型</param>
        /// <param name="rightMask">权限授权类型</param>
        /// <param name="delegationMask">权限委派类型</param>
        /// <returns>指定用户，在指定应用系统中所拥有的角色</returns>
        [WebMethod]
        public DataSet GetUserRoles(string userValue, string appCodeName, UserValueType userValueType, RightMaskType rightMask, DelegationMaskType delegationMask)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] userIDs = OGUReaderService.SplitObjectValues(userValue);

            SCObjectAndRelationCollection users = OGUReaderService.GetSearchAdapter(GetSearchOUIDType(userValueType), SchemaInfo.FilterByCategory("Users").ToSchemaNames(), userIDs, false).QueryObjectsAndRelations();

            SchemaObjectCollection roles = SCSnapshotAdapter.Instance.QueryUserBelongToRoles(schemaTypes, appCodeName, users.ToIDArray(), false, DateTime.MinValue);

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(roles));

            return ds;
        }

        /// <summary>
        /// 查询指定用户，在指定应用系统中所拥有的角色
        /// </summary>
        /// <param name="userValue">用户身份标识（由userValueType参数指定类型）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="userValueType">人员标识类型</param>
        /// <param name="rightMask">权限授权类型</param>
        /// <param name="delegationMask">权限委派类型</param>
        /// <param name="includeMatrixUsers">是否包含矩阵成员</param>
        /// <returns>指定用户，在指定应用系统中所拥有的角色</returns>
        [WebMethod]
        public DataSet GetUserRoles2(string userValue, string appCodeName, UserValueType userValueType, RightMaskType rightMask, DelegationMaskType delegationMask, bool includeMatrixUsers)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] userIDs = OGUReaderService.SplitObjectValues(userValue);

            SCObjectAndRelationCollection users = OGUReaderService.GetSearchAdapter(GetSearchOUIDType(userValueType), SchemaInfo.FilterByCategory("Users").ToSchemaNames(), userIDs, false).QueryObjectsAndRelations();

            SchemaObjectCollection roles = SCSnapshotAdapter.Instance.QueryUserBelongToRoles(schemaTypes, appCodeName, users.ToIDArray(), false, DateTime.MinValue);

            if (includeMatrixUsers)
            {
                List<string> matachedRoleIDs = GetUserRoleIDsInMatrix(users.ToIDArray(), appCodeName);

                //过滤掉已经在之前查询过的角色
                matachedRoleIDs = matachedRoleIDs.FindAll(rID => roles.ContainsKey(rID) == false);

                if (matachedRoleIDs.Count > 0)
                {
                    InSqlClauseBuilder matrixRoleBuilder = new InSqlClauseBuilder("ID");

                    matrixRoleBuilder.AppendItem(matachedRoleIDs.ToArray());
                    SchemaObjectCollection rolesInMatrix = SchemaObjectAdapter.Instance.Load(matrixRoleBuilder, DateTime.MinValue);

                    rolesInMatrix.ForEach(rMatrix => roles.AddNotExistsItem(rMatrix, (r) => string.Compare(r.ID, rMatrix.ID, true) == 0));
                }
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(roles));

            return ds;
        }

        /// <summary>
        /// 查询指定人员，在指定应用系统具有的权限（功能）
        /// </summary>
        /// <param name="userValue">用户身份标识（由userValueType参数指定类型）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="userValueType">人员标识类型</param>
        /// <param name="rightMask">权限授权类型</param>
        /// <param name="delegationMask">权限委派类型</param>
        /// <returns>指定人员，在指定应用系统具有的权限（功能）</returns>
        [WebMethod]
        public DataSet GetUserPermissions(string userValue, string appCodeName, UserValueType userValueType, RightMaskType rightMask, DelegationMaskType delegationMask)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] userIDs = OGUReaderService.SplitObjectValues(userValue);

            SCObjectAndRelationCollection users = OGUReaderService.GetSearchAdapter(GetSearchOUIDType(userValueType), SchemaInfo.FilterByCategory("Users").ToSchemaNames(), userIDs, false).QueryObjectsAndRelations();

            SchemaObjectCollection permissions = SCSnapshotAdapter.Instance.QueryUserBelongToPermissions(schemaTypes, appCodeName, users.ToIDArray(), false, DateTime.MinValue);

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(permissions));

            return ds;
        }

        /// <summary>
        /// 查询指定人员，在指定应用系统具有的权限（功能）
        /// </summary>
        /// <param name="userValue">用户身份标识（由userValueType参数指定类型）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="userValueType">人员标识类型</param>
        /// <param name="rightMask">权限授权类型</param>
        /// <param name="delegationMask">权限委派类型</param>
        /// <param name="includeMatrixUsers">是否包含矩阵成员</param>
        /// <returns>指定人员，在指定应用系统具有的权限（功能）</returns>
        [WebMethod]
        public DataSet GetUserPermissions2(string userValue, string appCodeName, UserValueType userValueType, RightMaskType rightMask, DelegationMaskType delegationMask, bool includeMatrixUsers)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] userIDs = OGUReaderService.SplitObjectValues(userValue);

            SCObjectAndRelationCollection users = OGUReaderService.GetSearchAdapter(GetSearchOUIDType(userValueType), SchemaInfo.FilterByCategory("Users").ToSchemaNames(), userIDs, false).QueryObjectsAndRelations();

            SchemaObjectCollection permissions = SCSnapshotAdapter.Instance.QueryUserBelongToPermissions(schemaTypes, appCodeName, users.ToIDArray(), false, DateTime.MinValue);

            if (includeMatrixUsers)
            {
                string[] permissionTypes = SchemaInfo.FilterByCategory("Permissions").ToSchemaNames();

                List<string> roleIDsInMatrix = GetUserRoleIDsInMatrix(users.ToIDArray(), appCodeName);

                SCObjectAndRelationCollection permissionsInMatrixRelation = SCSnapshotAdapter.Instance.QueryObjectAndRelationByParentIDs(permissionTypes, roleIDsInMatrix.ToArray(), false, true, false, DateTime.MinValue);

                InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("ID");

                List<string> permissionInMatrixIDs = new List<string>();

                foreach (SCObjectAndRelation pim in permissionsInMatrixRelation)
                {
                    if (permissions.ContainsKey(pim.ID) == false)
                        inBuilder.AppendItem(pim.ID);
                }

                if (inBuilder.IsEmpty == false)
                {
                    SchemaObjectCollection permissionsInMatrix = SchemaObjectAdapter.Instance.Load(inBuilder, DateTime.MinValue);

                    permissions.CopyFrom(permissionsInMatrix);
                }
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetAppObjectTableBuilder(schemaTypes).Convert(permissions));

            return ds;
        }

        /// <summary>
        /// 查询指定部门下，指定应用系统中，指定角色的所有被授权对象。这个查询是不递归的。
        /// </summary>
        /// <param name="orgRoot">部门范围的全路径，空串时不做限制，多个时用逗号分隔（这个参数在本服务中没有用）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="roleCodeNames">角色的英文标识，多个时用逗号分隔</param>
        /// <param name="doesMixSort">是否采用混合排序，true:机构、组、人员混排，false:先机构，再组，后人员</param>
        /// <param name="doesSortRank">是否排序级别，true:先级别，后层次 false:只排层次</param>
        /// <param name="includeDelegate">是否包括委派权限的被授权对象，true:包括 false:不包括</param>
        /// <returns>指定部门下，指定应用系统中，指定角色的所有被授权对象</returns>
        [WebMethod]
        public DataSet GetChildrenInRoles(string orgRoot, string appCodeName, string roleCodeNames, bool doesMixSort, bool doesSortRank, bool includeDelegate)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] roleIDs = OGUReaderService.SplitObjectValues(roleCodeNames);

            bool removeDuplicateData = GetMethodSettingAttributeValue("GetChildrenInRoles", "removeDuplicateData", false);

            SCObjectAndRelationCollection relations = SCSnapshotAdapter.Instance.QueryRolesContainsMembers(schemaTypes, appCodeName, roleIDs, removeDuplicateData, false, DateTime.MinValue);

            relations.FillDetails();

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetOguTableBuilder(new string[] { "OguObject" }).Convert(relations));

            return ds;
        }

        /// <summary>
        /// 查询指定部门下，指定应用系统中，指定角色的所有被授权对象。这个查询是不递归的。
        /// </summary>
        /// <param name="orgRoot">部门范围的全路径，空串时不做限制，多个时用逗号分隔（这个参数在本服务中没有用）</param>
        /// <param name="appCodeName">应用的英文标识</param>
        /// <param name="roleCodeNames">角色的英文标识，多个时用逗号分隔</param>
        /// <param name="doesMixSort">是否采用混合排序，true:机构、组、人员混排，false:先机构，再组，后人员</param>
        /// <param name="doesSortRank">是否排序级别，true:先级别，后层次 false:只排层次</param>
        /// <param name="includeDelegate">是否包括委派权限的被授权对象，true:包括 false:不包括</param>
        /// <param name="includeMatrixUsers">是否包含矩阵的成员</param>
        /// <returns>指定部门下，指定应用系统中，指定角色的所有被授权对象</returns>
        [WebMethod]
        public DataSet GetChildrenInRoles2(string orgRoot, string appCodeName, string roleCodeNames, bool doesMixSort, bool doesSortRank, bool includeDelegate, bool includeMatrixUsers)
        {
            string[] schemaTypes = SchemaInfo.FilterByCategory("Roles").ToSchemaNames();
            string[] roleIDs = OGUReaderService.SplitObjectValues(roleCodeNames);

            bool removeDuplicateData = GetMethodSettingAttributeValue("GetChildrenInRoles", "removeDuplicateData", false);

            SCObjectAndRelationCollection relations = SCSnapshotAdapter.Instance.QueryRolesContainsMembers(schemaTypes, appCodeName, roleIDs, removeDuplicateData, false, DateTime.MinValue);

            if (includeMatrixUsers)
                FillRoleMatrixRelations(relations, schemaTypes, roleIDs);

            relations.FillDetails();

            DataSet ds = new DataSet();

            ds.Tables.Add(QueryHelper.GetOguTableBuilder(new string[] { "OguObject" }).Convert(relations));

            return ds;
        }

        [WebMethod]
        public void RemoveAllCache()
        {
            SCCacheHelper.InvalidateAllCache();
        }
        #endregion Web methods

        private static SearchOUIDType GetSearchOUIDType(UserValueType userValueType)
        {
            SearchOUIDType result = SearchOUIDType.None;

            switch (userValueType)
            {
                case UserValueType.Guid:
                    result = SearchOUIDType.Guid;
                    break;
                case UserValueType.AllPath:
                    result = SearchOUIDType.FullPath;
                    break;
                case UserValueType.LogonName:
                    result = SearchOUIDType.LogOnName;
                    break;
                default:
                    throw new NotSupportedException(string.Format("不支持的用户ID类型{0}", userValueType));
            }

            return result;
        }

        private static T GetMethodSettingAttributeValue<T>(string methodName, string attrName, T defaultValue)
        {
            T result = defaultValue;

            ServiceMethodConfigurationElement element = GetMethodSettings(methodName);

            if (element != null)
                result = element.GetValue(attrName, defaultValue);

            return result;
        }

        private static ServiceMethodConfigurationElement GetMethodSettings(string methodName)
        {
            ServiceSettings settings = ServiceSettings.GetConfig();

            ServiceMethodConfigurationElement result = null;

            ServiceConfigurationElement serviceElement = settings.Services[typeof(AppSecurityCheckService).FullName];

            if (serviceElement != null)
                result = serviceElement.Methods[methodName];

            return result ?? settings.MethodDefaultSettings;
        }

        /// <summary>
        /// 填充矩阵中的角色成员
        /// </summary>
        /// <param name="relations"></param>
        /// <param name="schemaTypes"></param>
        /// <param name="roleIDs"></param>
        private static void FillRoleMatrixRelations(SCObjectAndRelationCollection relations, string[] schemaTypes, string[] roleIDs)
        {
            SchemaObjectCollection roles = SchemaObjectAdapter.Instance.LoadByCodeNameAndSchema(schemaTypes, roleIDs, true, false, DateTime.MinValue);

            List<string> matrixUserIDs = SOARolePropertiesAdapter.Instance.RoleContainsOperatorIDsDirectly(roles.ToIDArray());

            if (matrixUserIDs.Count > 0)
            {
                InSqlClauseBuilder matrixUserIDBuilder = new InSqlClauseBuilder("SC.ID");

                matrixUserIDBuilder.AppendItem(matrixUserIDs.ToArray());

                SCObjectAndRelationCollection matrixUserRelations = SCSnapshotAdapter.Instance.QueryObjectAndRelationByBuilder(matrixUserIDBuilder, false, DateTime.MinValue, -1);

                matrixUserRelations.ForEach(mr => relations.AddNotExistsItem(mr, (r) => string.Compare(r.ID, mr.ID, true) == 0));
            }
        }

        /// <summary>
        /// 得到某个用户在矩阵中的角色ID
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="appCodeName"></param>
        /// <returns></returns>
        private static List<string> GetUserRoleIDsInMatrix(string[] userIDs, string appCodeName)
        {
            List<string> matachedRoleIDs = new List<string>();

            SCApplication app = (SCApplication)SchemaObjectAdapter.Instance.LoadByCodeNameAndSchema(SchemaInfo.FilterByCategory("Applications").ToSchemaNames(), new string[] { appCodeName }, true, false, DateTime.MinValue).SingleOrDefault();

            if (app != null)
            {
                List<string> matrixRoleIDs = SOARolePropertiesAdapter.Instance.OperatorBelongToRoleIDsDirectly(userIDs);

                //过滤掉不在当前应用中的角色
                matachedRoleIDs = matrixRoleIDs.FindAll(rID => app.CurrentRoles.ContainsKey(rID));
            }

            return matachedRoleIDs;
        }
    }
}
