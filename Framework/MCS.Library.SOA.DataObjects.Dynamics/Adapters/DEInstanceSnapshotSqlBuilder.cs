using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    public class DEInstanceSnapshotSqlBuilder 
    {
		private string _TableName = null;

        public static readonly DEInstanceSnapshotSqlBuilder Instance = new DEInstanceSnapshotSqlBuilder();

		public DEInstanceSnapshotSqlBuilder()
		{
		}

        public DEInstanceSnapshotSqlBuilder(string tableName)
		{
			this._TableName = tableName;
		}
        /// <summary>
        /// 快照表名称
        /// </summary>
        public string TableName
        {
            get
            {
                return this._TableName;
            }

            set
            {
                this._TableName = value;
            }
        }

        /// <summary>
        ///  创建现在SQL语句生成器
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected virtual InsertSqlClauseBuilder PrepareInsertSqlBuilder(DEEntityInstanceBase obj)
        {
            InsertSqlClauseBuilder builder = new InsertSqlClauseBuilder();
            foreach (var filed in obj.Fields)
            {
                if (filed.Definition.IsInSnapshot)
                {
                    builder.AppendItem(filed.Definition.Name,filed.GetRealValue());
                }
            }
            if (!builder.ContainsDataField("ID"))
            {
                builder.AppendItem("ID",obj.ID);
            }
            if (!builder.ContainsDataField("CreateTime"))
            {
                builder.AppendItem("CreateTime", obj.CreateDate);
            }
            if (!builder.ContainsDataField("CreatorID"))
            {
                builder.AppendItem("CreatorID", obj.Creator.ID);
            }
            if (!builder.ContainsDataField("CreatorName"))
            {
                builder.AppendItem("CreatorName", obj.Creator.Name);
            }
            return builder;
        }
        /// <summary>
        /// 创建更新SQL语句生成器
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual UpdateSqlClauseBuilder PrepareUpdateSqlBuilder(DEEntityInstanceBase obj)
        {
            UpdateSqlClauseBuilder builder = new UpdateSqlClauseBuilder();
            foreach (var filed in obj.Fields)
            {
                if (filed.Definition.IsInSnapshot)
                {
                    builder.AppendItem(filed.Definition.Name, filed.GetRealValue());
                }
            }
            return builder;
        }
        /// <summary>
        /// 创建Where条件SQL语句生成器
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual WhereSqlClauseBuilder PrepareWhereSqlBuilder(DEEntityInstanceBase obj)
        { 
            WhereSqlClauseBuilder primaryKeyBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj);
            return primaryKeyBuilder;
        }
        /// <summary>
        /// 生成插入SQL
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public virtual string PrepareInsertSql(DEEntityInstanceBase obj)
        {
            InsertSqlClauseBuilder builder = PrepareInsertSqlBuilder(obj);

            return string.Format("INSERT INTO {0}{1}", GetTableName(obj), builder.ToSqlString(TSqlBuilder.Instance));
        }
        /// <summary>
        /// 生成更新SQL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual string PrepareUpdateSql(DEEntityInstanceBase obj)
        {
            WhereSqlClauseBuilder primaryKeyBuilder = PrepareWhereSqlBuilder(obj);
            UpdateSqlClauseBuilder updateBuilder = PrepareUpdateSqlBuilder(obj);

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                    GetTableName(obj),
                    updateBuilder.ToSqlString(TSqlBuilder.Instance),
                    primaryKeyBuilder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 生成查询SQL
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public virtual string PrepareLoadSql(DEEntityInstanceBase obj,WhereSqlClauseBuilder whereClauseBuilder)
        {
            InsertSqlClauseBuilder builder = PrepareInsertSqlBuilder(obj);
            return string.Format("SELECT * FROM {0} WHERE {1}", GetTableName(obj), whereClauseBuilder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 生成查询SQL
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public virtual string PrepareLoadSql(DEEntityInstanceBase obj)
        {
            InsertSqlClauseBuilder builder = PrepareInsertSqlBuilder(obj);
            WhereSqlClauseBuilder primaryKeyBuilder = PrepareWhereSqlBuilder(obj);
            return string.Format("SELECT * FROM {0} WHERE {1}", GetTableName(obj), primaryKeyBuilder.ToSqlString(TSqlBuilder.Instance));
        }


        /// <summary>
        /// 获取实体对应的快照表名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual string GetTableName(DEEntityInstanceBase obj)
        {
            string table = this._TableName;
            if (table.IsNullOrEmpty())
            {
                table = GetTableName(obj.EntityDefine);
            }
            return table;
        }

        protected string GetTableName(DynamicEntity obj)
        {
            obj.NullCheck("DynamicEntity Object");
            return obj.SnapshotTable;
        }
    }
}
