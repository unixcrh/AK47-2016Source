using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    public class DEDynamicEntityFieldSnapShotAdapter
    {
        public static readonly DEDynamicEntityFieldSnapShotAdapter Instance = new DEDynamicEntityFieldSnapShotAdapter();

        const string TableName = "DE.EntityFieldSnapshot";

        /// <summary>
        /// 通过实体的CodeName加载实体字段对象集合
        /// </summary>
        /// <param name="codeName">实体的CodeName</param>
        /// <returns>该CodeName对应的实体字段对象集合</returns>
        public List<DynamicEntityField> LoadByRefferanceCodeName(string codeName)
        {
            List<DynamicEntityField> entityFields = new List<DynamicEntityField>();

            string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

            InSqlClauseBuilder inbuilder = new InSqlClauseBuilder("ReferenceEntityCodeName");
            inbuilder.AppendItem(codeName);

            WhereSqlClauseBuilder wherebuilder = new WhereSqlClauseBuilder();
            wherebuilder.AppendItem("Status", 1);

            ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And);

            connectiveBuilder.Add(inbuilder);
            connectiveBuilder.Add(wherebuilder);


            var timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder();
            connectiveBuilder.Add(timePointBuilder);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT ID FROM {0} WHERE {1}",
                                TableName,
                                connectiveBuilder.ToSqlString(TSqlBuilder.Instance)
                            );

            DataSet fieldIDs = DbHelper.RunSqlReturnDS(sql.ToString(), this.GetConnectionName());

            if (fieldIDs.Tables.Count != 0)
            {
                foreach (DataRow item in fieldIDs.Tables[0].Rows)
                {
                    var id = item[0].ToString();
                    var dbResult = DESchemaObjectAdapter.Instance.Load(id) as DynamicEntityField;
                    entityFields.Add(dbResult);
                }

            }

            return entityFields;
        }

        /// <summary>
        /// 获取连接数据库配置节点名称
        /// </summary>
        /// <returns></returns>
        public string GetConnectionName()
        {
            return ConnectionNameMappingSettings.GetConfig().GetConnectionName("DynamicsEntity", "DynamicsEntity");
        }

    }
}
