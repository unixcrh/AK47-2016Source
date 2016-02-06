using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters
{
    public class EntityInstanceUpdateStatusSqlBuilder : NoVersionStrategyUpdateSqlBuilder<NoVersionedEntityInstanceObjectBase>
    {
        public static readonly EntityInstanceUpdateStatusSqlBuilder Instance = new EntityInstanceUpdateStatusSqlBuilder();

        private EntityInstanceUpdateStatusSqlBuilder()
        {
        }

        protected override string PrepareInsertSql(NoVersionedEntityInstanceObjectBase obj, ORMappingItemCollection mapping)
        {
            List<string> selectFieldNames = new List<string>(ORMapping.GetSelectFieldsName(mapping, "Status"));

            selectFieldNames.Add("Data");

            List<string> insertFieldNames = new List<string>(selectFieldNames);

            //insertFieldNames.Add("VersionStartTime");
            //insertFieldNames.Add("VersionEndTime");
            insertFieldNames.Add("Status");

            StringBuilder strB = new StringBuilder();

            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

            strB.AppendFormat("INSERT INTO {0}({1})", GetTableName(obj, mapping), string.Join(",", insertFieldNames));
            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
            strB.AppendFormat("SELECT {0},{1} FROM {2} WHERE {3}",
                string.Join(",", selectFieldNames),
                //"@currentTime",
                //TSqlBuilder.Instance.FormatDateTime(ConnectionDefine.MaxVersionEndTime),
                (int)obj.Status,
                GetTableName(obj, mapping),
                this.PrepareWhereSqlBuilder(obj, mapping).ToSqlString(TSqlBuilder.Instance));

            return strB.ToString();
        }

        protected override string GetTableName(NoVersionedEntityInstanceObjectBase obj, ORMappingItemCollection mapping)
        {
            return mapping.TableName;
        }
    }
}
