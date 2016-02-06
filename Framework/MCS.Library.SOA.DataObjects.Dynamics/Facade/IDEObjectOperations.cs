using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
//using MCS.Library.SOA.DataObjects.Security.Conditions;
//using MCS.Library.SOA.DataObjects.Security.Permissions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
    public interface IDEObjectOperations
    {
        void AddEntity(DynamicEntity entity);

        void DeleteEntity(DynamicEntity entity);

        void UpdateEntity(DynamicEntity entity);

        void AddEntityField(DynamicEntityField entityField);

        void DeleteEntityField(DynamicEntityField entityField);

        void UpdateEntityField(DynamicEntityField entityField);

        void DeleteEntity(params string[] ids);

        //void AddEntityMapping(DynamicEntity entity);

        void AddEntityMapping(EntityMapping entityMapping);

        void DeleteEntityMapping(DynamicEntity entity,OuterEntity oEntity);

        void DeleteEntityMapping(DynamicEntity entity, OuterEntityCollection oEntities);

        void DeleteEntityMapping(string entityID, params string[] outerEntityIDs);

        string RecordResultGenerate(string categoryID, RecordResultCollection recordCollection);

        DESchemaObjectBase DoOperation(SCObjectOperationMode opMode, DESchemaObjectBase data, DESchemaObjectBase parent, bool deletedByContainer = false);

        void CopyEntities(List<string> entitiesIDs, List<string> categories);

        void MoveEntities(List<string> entitiesIDs, List<string> categories);
    }
}
