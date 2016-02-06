using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters
{
    /// <summary>
    /// EntityInstance对象SQL生成器
    /// </summary>
    public class EntityInstanceUpdateSqlBuilder : NoVersionStrategyUpdateSqlBuilder<NoVersionedEntityInstanceObjectBase>
    {
        public static readonly EntityInstanceUpdateSqlBuilder Instance = new EntityInstanceUpdateSqlBuilder();

        private EntityInstanceUpdateSqlBuilder()
        {
        }

        protected override InsertSqlClauseBuilder PrepareInsertSqlBuilder(NoVersionedEntityInstanceObjectBase obj, ORMappingItemCollection mapping)
        {
         
            NoVersionedEntityInstanceObjectBase schemaObj = (NoVersionedEntityInstanceObjectBase)obj;

            if (OguBase.IsNullOrEmpty(schemaObj.Creator))
            {
                if (DeluxePrincipal.IsAuthenticated)
                    schemaObj.Creator = DeluxeIdentity.CurrentUser;
            }

            InsertSqlClauseBuilder builder = base.PrepareInsertSqlBuilder(obj, mapping);

            builder.AppendItem("Data", obj.ToString());

            return builder;
        }

        protected override UpdateSqlClauseBuilder PrepareUpdateSqlBuilder(NoVersionedEntityInstanceObjectBase obj, ORMappingItemCollection mapping)
        {
            NoVersionedEntityInstanceObjectBase schemaObj = (NoVersionedEntityInstanceObjectBase)obj;

            if (OguBase.IsNullOrEmpty(schemaObj.Creator))
            {
                if (DeluxePrincipal.IsAuthenticated)
                    schemaObj.Creator = DeluxeIdentity.CurrentUser;
            }

            UpdateSqlClauseBuilder builder = base.PrepareUpdateSqlBuilder(obj, mapping);

            builder.AppendItem("Data", obj.ToString());

            return builder;
        }

    }
}
