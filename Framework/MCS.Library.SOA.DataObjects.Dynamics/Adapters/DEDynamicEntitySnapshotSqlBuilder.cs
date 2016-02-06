using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System.Collections.Generic;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// 动态实体对应的快照表SQL创建处理类Adapter
    /// </summary>
    public class DEDynamicEntitySnapshotSqlBuilder
    {
        public static readonly DEDynamicEntitySnapshotSqlBuilder Instance = new DEDynamicEntitySnapshotSqlBuilder();

        public DEDynamicEntitySnapshotSqlBuilder()
        {
        }

        /// <summary>
        /// 获取动态实体的快照表字段定义
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>动态实体的快照表字段定义</returns>
        public virtual Dictionary<string, DEDynamicEntitySnapshotField> GetSnapshotTableFields(DynamicEntity obj)
        {
            Dictionary<string, DEDynamicEntitySnapshotField> dicFields = new Dictionary<string, DEDynamicEntitySnapshotField>();

            #region 初始化默认字段值
            //主键ID
            dicFields["ID"] = new DEDynamicEntitySnapshotField("ID", FieldTypeEnum.String, 36);
            //创建人ID
            dicFields["CREATORID"] = new DEDynamicEntitySnapshotField("CreatorID", FieldTypeEnum.String, 36);
            //创建人姓名
            dicFields["CREATORNAME"] = new DEDynamicEntitySnapshotField("CreatorName", FieldTypeEnum.String, 36);
            //创建时间
            dicFields["CREATETIME"] = new DEDynamicEntitySnapshotField("CreateTime", FieldTypeEnum.DateTime, 8);
            
            #endregion

            foreach (DynamicEntityField field in obj.Fields)
            {
                if (dicFields.ContainsKey(field.Name.ToUpper()) || field.IsInSnapshot == false)
                {
                    continue;
                }

                dicFields.Add(field.Name.ToUpper(), new DEDynamicEntitySnapshotField(field.Name, field.FieldType, field.Length));
            }

            return dicFields;
        }

        /// <summary>
        /// 准备添加快招标字段SQL语句
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>添加快招标字段SQL语句</returns>
        public virtual string PrepareAddTableFieldsSqlClause(DynamicEntity obj)
        {
            StringBuilder sql = new StringBuilder();
            Dictionary<string, DEDynamicEntitySnapshotField> dicFields = GetSnapshotTableFields(obj);
            foreach (KeyValuePair<string, DEDynamicEntitySnapshotField> kvp in dicFields)
            {
                //如果有是ID主键，则设置为不能为空
                if (kvp.Key.Equals("ID"))
                {
                    sql.Append("[ID] [nvarchar](36) NOT NULL,");
                }
                else
                { 
                    if (kvp.Value.FieldType == FieldTypeEnum.String)
                    {
                        sql.AppendFormat("[{0}] [nvarchar]({1}) NULL,", kvp.Value.FieldName, kvp.Value.FieldLength);
                    }
                    else if (kvp.Value.FieldType == FieldTypeEnum.Int)
                    {
                        sql.AppendFormat("[{0}] [int] NULL,", kvp.Value.FieldName);
                    }
                    else if (kvp.Value.FieldType == FieldTypeEnum.Bool)
                    {
                        sql.AppendFormat("[{0}] [bit] NULL,", kvp.Value.FieldName);
                    }
                    else if (kvp.Value.FieldType == FieldTypeEnum.DateTime)
                    {
                        sql.AppendFormat("[{0}] [datetime] NULL,", kvp.Value.FieldName);
                    }
                    else if (kvp.Value.FieldType == FieldTypeEnum.Decimal)
                    {
                        sql.AppendFormat("[{0}] [decimal]({1},2) NULL,", kvp.Value.FieldName, kvp.Value.FieldLength);
                    }
                }
                
            }
            return sql.ToString().TrimEnd(',');
        }
        /// <summary>
        /// 准备添加快招标字段SQL语句
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>添加快招标字段SQL语句</returns>
        public virtual string PrepareUpdateTableNewFieldsSqlClause(List<DEDynamicEntitySnapshotField> newFields)
        {
            StringBuilder sql = new StringBuilder();
            foreach (DEDynamicEntitySnapshotField field in newFields)
            {
                if (field.FieldType == FieldTypeEnum.String)
                {
                    sql.AppendFormat("[{0}] [nvarchar]({1}) NULL,", field.FieldName, field.FieldLength);
                }
                else if (field.FieldType == FieldTypeEnum.Int)
                {
                    sql.AppendFormat("[{0}] [int] NULL,", field.FieldName);
                }
                else if (field.FieldType == FieldTypeEnum.Bool)
                {
                    sql.AppendFormat("[{0}] [bit] NULL,", field.FieldName);
                }
                else if (field.FieldType == FieldTypeEnum.DateTime)
                {
                    sql.AppendFormat("[{0}] [datetime] NULL,", field.FieldName);
                }
                else if (field.FieldType == FieldTypeEnum.Decimal)
                {
                    sql.AppendFormat("[{0}] [decimal]({1},2) NULL,", field.FieldName, field.FieldLength);
                }
            }
            return sql.ToString().TrimEnd(',');
        }
        /// <summary>
        /// 准备添加快招标字段SQL语句
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>添加快招标字段SQL语句</returns>
        public virtual string PrepareUpdateTableModifyFieldsSqlClause(string tableName,List<DEDynamicEntitySnapshotField> modifyFields)
        {
            StringBuilder sql = new StringBuilder();
            foreach (DEDynamicEntitySnapshotField field in modifyFields)
            {
                if (field.FieldType == FieldTypeEnum.String)
                {
                    sql.AppendFormat(@"
ALTER TABLE {0} ALTER COLUMN [{1}] [nvarchar]({2});", tableName, field.FieldName, field.FieldLength);
                }
            }
            return sql.ToString();
        }
        /// <summary>
        /// 生成新增快照表的SQL
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>新增快照表的SQL</returns>
        public virtual string PrepareAddTableSql(DynamicEntity obj)
        {
            return string.Format(@"CREATE TABLE {0}({1}
PRIMARY KEY CLUSTERED ([ID] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
", GetSnapshotTableName(obj), PrepareAddTableFieldsSqlClause(obj));
        }

        /// <summary>
        /// 生成更新快照表的SQL
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <param name="newFields">新增的字段</param>
        /// <returns>更新快照表的SQL</returns>
        public virtual string PrepareUpdateTableSql(DynamicEntity obj, List<DEDynamicEntitySnapshotField> newFields, List<DEDynamicEntitySnapshotField> modifyFields)
        {
            StringBuilder sb = new StringBuilder();
            if (newFields.Count > 0)
            {
                sb.AppendFormat(@"ALTER TABLE {0} ADD {1};", GetSnapshotTableName(obj), PrepareUpdateTableNewFieldsSqlClause(newFields));
            }
            if (modifyFields.Count > 0)
            {
                sb.Append(PrepareUpdateTableModifyFieldsSqlClause(GetSnapshotTableName(obj), modifyFields));
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// 生成验证快照表是否已在数据库中存在SQL
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>验证快照表是否已在数据库中存在SQ</returns>
        public virtual string PrepareCheckTableExistsSql(DynamicEntity obj)
        {
            return this.PrepareCheckTableExistsSql(GetSnapshotTableName(obj));
        }

        /// <summary>
        /// 生成验证快照表是否已在数据库中存在SQL
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>验证快照表是否已在数据库中存在SQ</returns>
        public string PrepareCheckTableExistsSql(string tableName)
        {
            return string.Format("SELECT COUNT(ID)  AS RecordCount FROM SYSOBJECTS WHERE ID = OBJECT_ID('{0}') AND TYPE = 'U'", tableName);
        }


        /// <summary>
        /// 生成验证快照表是否已在数据库中存在SQL
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>验证快照表是否已在数据库中存在SQ</returns>
        public virtual string PrepareGetTableColumnsSql(DynamicEntity obj)
        {
            return this.PrepareGetTableColumnsSql(GetSnapshotTableName(obj));
        }

        /// <summary>
        /// 生成验证快照表是否已在数据库中存在SQL
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>验证快照表是否已在数据库中存在SQ</returns>
        public string PrepareGetTableColumnsSql(string tableName)
        {
            return string.Format("SELECT NAME,LENGTH/2 AS LENGTH,TYPE FROM SYSCOLUMNS WHERE ID=OBJECT_ID('{0}')", tableName);
        }

        /// <summary>
        /// 获取实体对应的快照表名称
        /// </summary>
        /// <param name="obj">动态实体定义</param>
        /// <returns>快照表名称</returns>
        protected virtual string GetSnapshotTableName(DynamicEntity obj)
        {
            obj.NullCheck("DynamicEntity Object");
            obj.SnapshotTable.NullCheck("DynamicEntity Object SnapshotTable");
            return obj.SnapshotTable;
        }
    }

    /// <summary>
    /// 动态实体快照表字段定义
    /// </summary>
    public class DEDynamicEntitySnapshotField
    {
        public DEDynamicEntitySnapshotField(string name, FieldTypeEnum type, int length)
        {
            this.FieldName = name;
            this.FieldType = type;
            this.FieldLength = length;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public FieldTypeEnum FieldType { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int FieldLength { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
