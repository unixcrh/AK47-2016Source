using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
	public static class DEConnectionDefine
	{
		public static readonly DateTime MaxVersionEndTime = new DateTime(9999, 9, 9);

        /// <summary>
        /// 连接数据库配置文件中的配置名称
        /// </summary>
		public static string DBConnectionName
		{
			get
			{
                return ConnectionNameMappingSettings.GetConfig().GetConnectionName("DynamicsEntity", "DynamicsEntity");
			}
		}


        /// <summary>
        /// 招商管理连接数据库配置文件中的配置名称
        /// </summary>
        public static string DBInvitationConnectionName
        {
            get
            {
                return ConnectionNameMappingSettings.GetConfig().GetConnectionName("Invitation", "Invitation");
            }
        }
	}
}
