using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// 动态实体实例快照表操作Adapter
    /// </summary>
    /// <typeparam name="T">动态实体实例类型</typeparam>
    public abstract class DEDynamicEntitySnapshotAdapterBase<T> where T : DynamicEntity
    {
        /// <summary>
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的字符串</returns>
        protected abstract string GetConnectionName();

        /// <summary>
        /// 更新对象的快照
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>是否成功</returns>
        public virtual bool CreateSnapshot(T obj)
        {
            obj.NullCheck("obj");
            obj.SnapshotTable.NullCheck("SnapshotTable");
            if (this.CheckTableExists(obj.SnapshotTable))
            {
                return false;
            }
            else
            {
                string sql = DEDynamicEntitySnapshotSqlBuilder.Instance.PrepareAddTableSql(obj);
                DbHelper.RunSql(sql, this.GetConnectionName());
                return true;
            }

        }

        /// <summary>
        /// 更新对象的快照
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        public virtual void UpdateSnapshot(T obj)
        {
            obj.NullCheck("obj");
            obj.SnapshotTable.NullCheck("SnapshotTable");
            string sql = string.Empty;
            if (this.CheckTableExists(obj.SnapshotTable))
            {
                //修改后的实体字段
                Dictionary<string, DEDynamicEntitySnapshotField> dicFields = DEDynamicEntitySnapshotSqlBuilder.Instance.GetSnapshotTableFields(obj);
                if (dicFields.Count > 0)
                {
                    //当前数据库中的字段
                    Dictionary<string, DEDynamicEntitySnapshotField> currentFields = GetSnapshotTableColumns(obj);
                    List<DEDynamicEntitySnapshotField> newFields = new List<DEDynamicEntitySnapshotField>();
                    List<DEDynamicEntitySnapshotField> modifyFields = new List<DEDynamicEntitySnapshotField>();
                    foreach (KeyValuePair<string, DEDynamicEntitySnapshotField> kvp in dicFields)
                    {
                        if (currentFields.ContainsKey(kvp.Key))
                        {
                            //如果数据库字段和修改后的字段都是字符串类型，且数据库总的字段长度小于修改后的字段长度，则需要修改数据库字段长度
                            if (currentFields[kvp.Key].FieldType == FieldTypeEnum.String
                                && kvp.Value.FieldType == FieldTypeEnum.String
                                && kvp.Value.FieldLength > currentFields[kvp.Key].FieldLength)
                            {
                                modifyFields.Add(kvp.Value);
                            }
                        }
                        else
                        {
                            newFields.Add(kvp.Value);
                        }
                    }
                    if (newFields.Count > 0 || modifyFields.Count > 0)
                    {
                        sql = DEDynamicEntitySnapshotSqlBuilder.Instance.PrepareUpdateTableSql(obj, newFields, modifyFields);
                    }
                }
            }
            else
            {
                //如果没有改快照表，则新增
                sql = DEDynamicEntitySnapshotSqlBuilder.Instance.PrepareAddTableSql(obj);
            }

            if (sql.IsNotEmpty())
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    DbHelper.RunSql(sql, this.GetConnectionName());
                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// 获取表所有字段名称
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>表字段名称列表</returns>
        public Dictionary<string, DEDynamicEntitySnapshotField> GetSnapshotTableColumns(T obj)
        {
            obj.NullCheck("obj");
            obj.SnapshotTable.NullCheck("SnapshotTable");
            return this.GetTableColumns(obj.SnapshotTable);
        }

        /// <summary>
        /// 获取表的所有字段定义
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>字段定义</returns>
        public Dictionary<string, DEDynamicEntitySnapshotField> GetTableColumns(string tableName)
        {
            tableName.NullCheck("tableName");
            Dictionary<string, DEDynamicEntitySnapshotField> columns = new Dictionary<string, DEDynamicEntitySnapshotField>();
            string sql = DEDynamicEntitySnapshotSqlBuilder.Instance.PrepareGetTableColumnsSql(tableName);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    columns.Add(dr["NAME"].ToString().ToUpper(),
                        new DEDynamicEntitySnapshotField(dr["NAME"].ToString()
                            , ConvertToFieldTypeEnum(dr["TYPE"].ToString())
                            , Convert.ToInt32(dr["LENGTH"]))
                        );
                }
            }
            return columns;
        }

        /// <summary>
        /// 验证快照表是否已在数据库中存在
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>快照表是否已在数据库中存在</returns>
        protected virtual bool CheckSnapshotTableExists(T obj)
        {
            obj.NullCheck("obj");
            obj.SnapshotTable.NullCheck("SnapshotTable");
            return this.CheckTableExists(obj.SnapshotTable);
        }

        /// <summary>
        /// 验证快照表是否已在数据库中存在
        /// </summary>
        /// <param name="tableName">快照表名称</param>
        /// <returns>快照表是否已在数据库中存在</returns>
        protected bool CheckTableExists(string tableName)
        {
            tableName.NullCheck("tableName");
            string sql = DEDynamicEntitySnapshotSqlBuilder.Instance.PrepareCheckTableExistsSql(tableName);
            object result = DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());
            if (result == null)
            {
                return false;
            }
            else
            {
                return Convert.ToInt32(result) > 0;
            }
        }

        /// <summary>
        /// 将数据库数据类型转换为字段类型枚举
        /// </summary>
        /// <param name="code">数据库数据类型编码</param>
        /// <returns>字段类型枚举</returns>
        protected FieldTypeEnum ConvertToFieldTypeEnum(string code)
        {
            FieldTypeEnum type = FieldTypeEnum.String;
            switch (code)
            {
                case "39":
                    type = FieldTypeEnum.String;
                    break;
                case "111":
                    type = FieldTypeEnum.DateTime;
                    break;
                case "38":
                    type = FieldTypeEnum.Int;
                    break;
                case "106":
                    type = FieldTypeEnum.Decimal;
                    break;
                case "50":
                    type = FieldTypeEnum.Bool;
                    break;
            }
            return type;
        }
    }
}
