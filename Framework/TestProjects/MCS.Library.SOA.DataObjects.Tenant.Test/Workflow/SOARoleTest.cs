using MCS.Library.OGUPermission;
using MCS.Library.Passport;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Tenant.Test.Workflow.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Tenant.Test.Workflow
{
    /// <summary>
    /// SOA Role相关的单元测试和示例
    /// </summary>
    [TestClass]
    public class SOARoleTest
    {
        /// <summary>
        /// 角色矩阵包含的人员测试
        /// </summary>
        [TestMethod]
        public void RoleMatrixUsersTest()
        {
            IRole role = GetTestRole();

            SOARole soaRole = SOARoleHelper.PrepareSOARole(role);

            SOARolePropertyRowUsersCollection rowsUsers = soaRole.Rows.GenerateRowsUsers();

            //输出每一行中的每一个人，也包括指定的列
            Output(rowsUsers);
        }

        [TestMethod]
        public void OperatorBelongToRoleMatrixTest()
        {
            IRole role = GetTestRole();

            SOARole soaRole = SOARoleHelper.PrepareSOARole(role);

            IUser user = OguMechanismFactory.GetMechanism().GetObjects<IUser>(SearchOUIDType.LogOnName, "fanhy").First();

            List<string> roleIDs = SOARolePropertiesAdapter.Instance.OperatorBelongToRoleIDsDirectly(user.ID);

            foreach(string roleID in roleIDs)
            {
                SOARolePropertyRowCollection rows = SOARolePropertiesAdapter.Instance.GetByRoleID(roleID);

                Output(rows);
            }
            
        }

        private static void Output(SOARolePropertyRowCollection rows)
        {
            foreach (SOARolePropertyRow row in rows)
            {
                foreach(SOARolePropertyValue v in row.Values)
                    Console.Write("{0}: {1} ", v.Column.Name, v.Value);

                Console.WriteLine();
            }
        }

        private static void Output(SOARolePropertyRowUsersCollection rowsUsers)
        {
            foreach (SOARolePropertyRowUsers rowUsers in rowsUsers)
            {
                Console.Write("CostCenter: {0} ", rowUsers.Row.Values.GetValue("CostCenter", string.Empty));

                foreach (IUser user in rowUsers.Users)
                    Console.Write("User Name: {0} ", user.DisplayName);

                Console.WriteLine();
            }
        }

        private static IRole GetTestRole()
        {
            IRole[] roles = DeluxePrincipal.GetRoles(RolesDefineConfig.GetConfig().RolesDefineCollection["testRole"].Roles);

            return roles[0];
        }
    }
}
