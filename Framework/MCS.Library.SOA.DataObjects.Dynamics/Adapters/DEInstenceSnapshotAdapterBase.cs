using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System.Data;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// 动态实体实例快照表操作Adapter
    /// </summary>
    /// <typeparam name="T">动态实体实例类型</typeparam>
    public abstract class DEInstenceSnapshotAdapterBase<T> where T : DEEntityInstanceBase
    {
        /// <summary>
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的字符串</returns>
        protected abstract string GetConnectionName();

        /// <summary>
        /// 更新对象的快照
        /// </summary>
        /// <param name="obj">动态实体实例</param>
        protected virtual int UpdateSnapshot(T obj)
        {
            //obj.NullCheck("obj");
            //obj.EntityDefine.SnapshotTable.CheckStringIsNullOrEmpty("SnapshotTable");
            string sql = DEInstanceSnapshotSqlBuilder.Instance.PrepareUpdateSql(obj);
            return DbHelper.RunSql(sql, this.GetConnectionName());
        }

        /// <summary>
        /// 新增对象的快照
        /// </summary>
        /// <param name="obj">动态实体实例</param>
        protected virtual int InsertSnapshot(T obj)
        {
            //obj.NullCheck("obj");
            //obj.EntityDefine.SnapshotTable.CheckStringIsNullOrEmpty("SnapshotTable");
            string sql = DEInstanceSnapshotSqlBuilder.Instance.PrepareInsertSql(obj);
            return DbHelper.RunSql(sql, this.GetConnectionName());

        }
        /// <summary>
        /// 更新动态实体实例信息
        /// </summary>
        /// <param name="obj">动态实体实例信息</param>
        public virtual void Update(T obj)
        {
            obj.NullCheck("obj");
            obj.EntityDefine.SnapshotTable.CheckStringIsNullOrEmpty("SnapshotTable");

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                if (UpdateSnapshot(obj) == 0)
                {
                    InsertSnapshot(obj);
                }
                scope.Complete();
            }
        }

        public virtual DataView Load(T obj)
        {
            obj.NullCheck("obj");
            obj.EntityDefine.SnapshotTable.CheckStringIsNullOrEmpty("SnapshotTable");
            string sql = DEInstanceSnapshotSqlBuilder.Instance.PrepareLoadSql(obj);

            DataSet ds = DbHelper.RunSPReturnDS(sql, this.GetConnectionName());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0].DefaultView;
            else
                return null;
        }

        /// <summary>
        /// 更新对象的状态
        /// </summary>
        /// <param name="obj">动态实体实例</param>
        public virtual void UpdateSnapshotStatus(T obj)
        {
            obj.NullCheck("obj");
            obj.EntityDefine.SnapshotTable.CheckStringIsNullOrEmpty("SnapshotTable");
            string sql = "";

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DbHelper.RunSql(sql, this.GetConnectionName());
                scope.Complete();
            }
        }
    }
}
