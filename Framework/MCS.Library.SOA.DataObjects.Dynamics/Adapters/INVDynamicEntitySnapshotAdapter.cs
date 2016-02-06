using MCS.Library.SOA.DataObjects.Dynamics.Objects;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    public class INVDynamicEntitySnapshotAdapter : DEDynamicEntitySnapshotAdapterBase<DynamicEntity>
    {
        public static readonly INVDynamicEntitySnapshotAdapter Instance = new INVDynamicEntitySnapshotAdapter();

         private INVDynamicEntitySnapshotAdapter()
        {
        }

        /// <summary>
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的字符串</returns>
        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBInvitationConnectionName;
        }

    }
}
