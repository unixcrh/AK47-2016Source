using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Actions;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters
{
    /// <summary>
    /// 表示模式对象适配器的基类
    /// </summary>
    /// <typeparam name="T"><see cref="DEEntityInstanceBase"/>的派生类型。</typeparam>
    public abstract class DEInstanceAdapterBase<T> where T : DEEntityInstanceBase
    {
        /// <summary>
        /// 获取Action的集合
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected EntityInstanceUpdateActionCollection GetActions(string actionName)
        {
            return EntityInstanceUpdateActionSettings.GetConfig().GetActions(actionName);
        }

        /// <summary>
        /// 将模式对象的修改提交到数据库
        /// </summary>
        /// <param name="obj">对其进行更新的<typeparamref name="T"/>对象。</param>
        public void Update(T obj)
        {
            obj.NullCheck("obj");

            this.MergeExistsObjectInfo(obj);

            EntityInstanceUpdateActionCollection actions = GetActions("Update");

            actions.Prepare(obj);

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DEEntityInstanceBase existData = GetExistedObject(obj);
                string sql = string.Empty;
                if (existData != null)
                {
                    sql = EntityInstanceUpdateSqlBuilder.Instance.ToUpdateSql(obj, this.GetMappingInfo());
                    DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());
                }
                else
                {
                    sql = EntityInstanceUpdateSqlBuilder.Instance.ToInsertSql(obj, this.GetMappingInfo());
                    DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());
                }

                //执行update  如果update失败执行insert
                //string sql = EntityInstanceUpdateSqlBuilder.Instance.ToUpdateSql(obj, this.GetMappingInfo());
                //try
                //{
                //    DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());
                //    DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());
                //}
                //catch (Exception)
                //{
                //    sql = EntityInstanceUpdateSqlBuilder.Instance.ToInsertSql(obj, this.GetMappingInfo());
                //    DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());
                //}

                //SCInstanceActionContext.Current.TimePoint.IsMinValue(() => SCInstanceActionContext.Current.TimePoint = dt);
                //string sql = EntityInstanceUpdateSqlBuilder.Instance.ToInsertSql(obj, this.GetMappingInfo());

                actions.Persist(obj);

                scope.Complete();
            }
        }

        /// <summary>
        /// 更新模式对象的状态到数据库
        /// </summary>
        /// <param name="obj">对其进行更新的<typeparamref name="T"/>对象。</param>
        /// <param name="status">表示状态的<see cref="SchemaObjectStatus"/>值之一。</param>
        public void UpdateStatus(T obj, SchemaObjectStatus status)
        {
            obj.Status = status;

            string sql = EntityInstanceUpdateStatusSqlBuilder.Instance.ToUpdateSql(obj, this.GetMappingInfo());

            EntityInstanceUpdateActionCollection actions = GetActions("UpdateStatus");

            actions.Prepare(obj);

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DateTime dt = (DateTime)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());

                SCInstanceActionContext.Current.TimePoint.IsMinValue(() => SCInstanceActionContext.Current.TimePoint = dt);

                actions.Persist(obj);

                scope.Complete();
            }
        }

        /// <summary>
        /// 合并现有对象信息
        /// </summary>
        /// <param name="obj">对其进行更新的<typeparamref name="T"/>对象。</param>
        public void MergeExistsObjectInfo(T obj)
        {
            if (SCInstanceActionContext.Current.OriginalObject != null &&
                SCInstanceActionContext.Current.OriginalObject.EntityCode == obj.EntityCode &&
                SCInstanceActionContext.Current.OriginalObject.ID == obj.ID)
            {
                obj.Creator = SCInstanceActionContext.Current.OriginalObject.Creator;
                obj.CreateDate = SCInstanceActionContext.Current.OriginalObject.CreateDate;
                //obj.VersionStartTime = SCInstanceActionContext.Current.OriginalObject.VersionStartTime;
            }
            else
            {
                DEEntityInstanceBase existedInfo = this.GetExistedObject(obj);

                if (existedInfo != null)
                {
                    obj.CreateDate = existedInfo.CreateDate;
                    obj.Creator = existedInfo.Creator;
                    //obj.VersionStartTime = existedInfo.VersionStartTime;
                }
            }
        }

        private DEEntityInstanceBase GetExistedObject(T obj)
        {
            WhereSqlClauseBuilder keyBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj, this.GetMappingInfo());

            string sql = string.Format("SELECT TOP 1 {0} FROM {1} WHERE {2} ORDER BY CreateDate DESC",
                string.Join(",", ORMapping.GetSelectFieldsName(this.GetMappingInfo(), "Data")),
                this.GetMappingInfo().TableName,
                keyBuilder.ToSqlString(TSqlBuilder.Instance));

            DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

            DEEntityInstanceBase result = null;

            if (table.Rows.Count > 0)
            {
                result = this.CreateSimpleObject(table.Rows[0]["EntityCode"].ToString());
                ORMapping.DataRowToObject(table.Rows[0], result);
            }

            return result;
        }

        /// <summary>
        /// 创建一个简单对象
        /// </summary>
        /// <returns>一个<see cref="DEEntityInstance"/>对象。</returns>
        protected virtual DEEntityInstanceBase CreateSimpleObject(string entityID)
        {
            DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            DEEntityInstanceBase result = entity.CreateInstance();

            result.ID = Guid.NewGuid().ToString();
            return result;
        }

        /// <summary>
        /// 在派生类中重写时， 获取映射信息的集合
        /// </summary>
        /// <returns><see cref="ORMappingItemCollection"/>，表示映射信息</returns>
        protected virtual ORMappingItemCollection GetMappingInfo()
        {
            return ORMapping.GetMappingInfo(typeof(T));
        }

        /// <summary>
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的<see cref="string"/>。</returns>
        protected string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }
    }
}
