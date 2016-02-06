using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance
{
	/// <summary>
	/// Update和Insert语句的构造器
	/// </summary>
    public class NoVersionStrategyUpdateSqlBuilder<T> //where T : IVersionDataObject
	{
		public string ToUpdateSql(T obj, ORMappingItemCollection mapping)
		{
            return PrepareUpdateSql(obj, mapping);

		}
        public string ToInsertSql(T obj, ORMappingItemCollection mapping)
        {
            return PrepareInsertSql(obj, mapping);

        }

		protected virtual string GetTableName(T obj, ORMappingItemCollection mapping)
		{
			return mapping.TableName;
		}

		protected virtual InsertSqlClauseBuilder PrepareInsertSqlBuilder(T obj, ORMappingItemCollection mapping)
		{
			InsertSqlClauseBuilder builder = ORMapping.GetInsertSqlClauseBuilder(obj, mapping);

            //string vsFieldName = GetPropertyFieldName("VersionStartTime", mapping);
            //string veFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            //builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == vsFieldName);
            //builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == veFieldName);

            //builder.AppendItem(vsFieldName, "@currentTime", "=", true);
            //builder.AppendItem(veFieldName, ConnectionDefine.MaxVersionEndTime);

			return builder;
		}

		protected virtual UpdateSqlClauseBuilder PrepareUpdateSqlBuilder(T obj, ORMappingItemCollection mapping)
		{
			//UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            UpdateSqlClauseBuilder updateBuilder = ORMapping.GetUpdateSqlClauseBuilder(obj, mapping);
			//updateBuilder.AppendItem(GetPropertyFieldName("VersionEndTime", mapping), "@currentTime", "=", true);

			return updateBuilder;
		}

		protected virtual WhereSqlClauseBuilder PrepareWhereSqlBuilder(T obj, ORMappingItemCollection mapping)
		{
			WhereSqlClauseBuilder primaryKeyBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj);

			//string vsFieldName = GetPropertyFieldName("VersionStartTime", mapping);

			//if (primaryKeyBuilder.Exists(item => ((SqlClauseBuilderItemIUW)item).DataField == vsFieldName) == false)
			//	primaryKeyBuilder.AppendItem(vsFieldName, obj.VersionStartTime);

			return primaryKeyBuilder;
		}

		protected virtual string PrepareInsertSql(T obj, ORMappingItemCollection mapping)
		{
			InsertSqlClauseBuilder builder = PrepareInsertSqlBuilder(obj, mapping);

			return string.Format("INSERT INTO {0}{1}", GetTableName(obj, mapping), builder.ToSqlString(TSqlBuilder.Instance));
		}

		protected virtual string PrepareUpdateSql(T obj, ORMappingItemCollection mapping)
		{
			WhereSqlClauseBuilder primaryKeyBuilder = PrepareWhereSqlBuilder(obj, mapping);
			UpdateSqlClauseBuilder updateBuilder = PrepareUpdateSqlBuilder(obj, mapping);

			return string.Format("UPDATE {0} SET {1} WHERE {2}",
					GetTableName(obj, mapping),
					updateBuilder.ToSqlString(TSqlBuilder.Instance),
					primaryKeyBuilder.ToSqlString(TSqlBuilder.Instance));
		}

		protected static string GetPropertyFieldName(string propertyName, ORMappingItemCollection mapping)
		{
			ORMappingItem item = mapping[propertyName];

			(item != null).FalseThrow("不能在{0}的OR Mapping信息中找到属性{1}", mapping.TableName, propertyName);

			return item.DataFieldName;
		}
	}
}
