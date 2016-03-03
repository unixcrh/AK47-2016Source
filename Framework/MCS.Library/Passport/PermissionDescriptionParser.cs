using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Passport
{
    /// <summary>
    /// 角色描述信息的分析器
    /// </summary>
    public static class PermissionDescriptionParser
    {
        /// <summary>
        /// 根据权限对象的描述(应用的名称1:角色名称11,角色名称12,...;应用名称2:角色名称21,角色名称22,...)
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ApplicationAndPermissionObjectsCollection ParseApplicationAndPermissionObjects(string description)
        {
            ApplicationAndPermissionObjectsCollection pods = new ApplicationAndPermissionObjectsCollection();

            if (description.IsNotEmpty())
            {
                string[] appRoles = description.Split(';');

                for (int i = 0; i < appRoles.Length; i++)
                {
                    string[] oneAppRoles = appRoles[i].Split(':');

                    if (oneAppRoles.Length == 2)
                    {
                        string appName = oneAppRoles[0].Trim();

                        ApplicationAndPermissionObjects pod = pods[appName];

                        if (pod == null)
                        {
                            pod = new ApplicationAndPermissionObjects(appName);
                            pods.Add(pod);
                        }

                        string[] pos = oneAppRoles[1].Split(',');

                        for (int j = 0; j < pos.Length; j++)
                        {
                            string roleName = pos[j].Trim();

                            pod.PermissionObjectCodeNames.Add(roleName);
                        }
                    }
                }
            }

            return pods;
        }
    }
}
