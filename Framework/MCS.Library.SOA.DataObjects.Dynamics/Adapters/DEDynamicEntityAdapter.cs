using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library.Script;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Caching;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// DEDynamicEntityAdapter
    /// </summary>
    public class DEDynamicEntityAdapter : DESchemaObjectAdapterBase<DESchemaObjectBase>
    {
        public static readonly DEDynamicEntityAdapter Instance = new DEDynamicEntityAdapter();

        private DEDynamicEntityAdapter()
        {
        }

        /// <summary>
        /// 根据ID载入数据
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <param name="loadNormalStatus">对象状态为Normal</param>
        /// <returns><see cref="DESchemaObjectBase"/>的派生类型的实例</returns>
        public DynamicEntity LoadWithCache(string id, bool loadNormalStatus = true)
        {
            id.CheckStringIsNullOrEmpty<ArgumentNullException>("id");

            DynamicEntity result = null;

            result = DESchemaObjectAdapter.Instance.Load(id, loadNormalStatus) as DynamicEntity;
            return result;
        }

        private DynamicEntity LoadByCodeNameInner(string codeName, DateTime timePoint)
        {
            string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

            InSqlClauseBuilder inbuilder = new InSqlClauseBuilder("CodeName");
            inbuilder.AppendItem(codeName);

            WhereSqlClauseBuilder wherebuilder = new WhereSqlClauseBuilder();
            wherebuilder.AppendItem("Status", 1);

            ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And);

            connectiveBuilder.Add(inbuilder);
            connectiveBuilder.Add(wherebuilder);


            var timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);
            connectiveBuilder.Add(timePointBuilder);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT TOP 1 ID FROM {0} WHERE {1}",
                                "DE.SchemaObjectSnapshot",
                                connectiveBuilder.ToSqlString(TSqlBuilder.Instance)
                            );


            string _EntityID = Convert.ToString(DbHelper.RunSqlReturnScalar(sql.ToString(), this.GetConnectionName()));

            DynamicEntity entity = null;
            if (_EntityID.IsNotEmpty())
                entity = this.LoadWithCache(_EntityID);

            return entity;
        }

        public DynamicEntity LoadByCodeName(string codeName, DateTime timePoint)
        {
            string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

            DynamicEntity entity = null;

            entity = this.LoadByCodeNameInner(codeName, timePoint);

            return entity;
        }

        public bool ExistByCodeName(string codeName, DateTime timePoint)
        {
            string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

            return this.LoadByCodeNameInner(codeName, timePoint) != null;
        }

        public DynamicEntity LoadByCodeName(string codeName)
        {
            string.IsNullOrEmpty(codeName).TrueThrow("codeName不得为空");

            return this.LoadByCodeName(codeName, DateTime.MinValue);
        }
    }
}
