using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    public class DEInstenceSnapshotAdapter : DEInstenceSnapshotAdapterBase<DEEntityInstanceBase>
    {

        /// <summary>
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的字符串</returns>
        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }

    }
}
