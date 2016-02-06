using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Dynamics.Web.DataSource
{
    /// <summary>
    /// 查询实体映射列表
    /// </summary>
    public class EntityMappingItemDataSource : DataViewDataSourceQueryAdapterBase
    {
        public EntityMappingItemDataSource()
            : base("DE.EntityMappingItemSnapshot_Current")
        {
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = "*";
            qc.WhereClause = qc.WhereClause;
            qc.OrderByClause = "CreateDate DESC";
        }

        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }
    }
}