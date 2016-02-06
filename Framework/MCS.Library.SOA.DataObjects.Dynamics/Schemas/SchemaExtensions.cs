using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics
{
    public static class SchemaExtensions
    {
        public static DESchemaObjectBase CreateObject(string schemaTypeString)
        {
            ObjectSchemaSettings settings = ObjectSchemaSettings.GetConfig();

            ObjectSchemaConfigurationElement schemaElement = settings.Schemas[schemaTypeString];
            (schemaElement != null).FalseThrow<NotSupportedException>("不支持的对象类型: {0}", schemaTypeString);

            DESchemaObjectBase result = null;

            if (schemaElement.GetTypeInfo() == typeof(DEGenericObject))
            {
                result = new DEGenericObject(schemaTypeString);
            }
            else
            {
                result = (DESchemaObjectBase)schemaElement.CreateInstance();
            }

            return result;
        }

        public static DEEntityInstanceBase CreateInstanceBaseObject(string entityID)
        {
            entityID.CheckStringIsNullOrEmpty<ArgumentNullException>("entityID");

            DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            entity.NullCheck<ArgumentNullException>("找不到编码为{0}的实体");

            DEEntityInstanceBase result = entity.CreateInstance();

            return result;
        }

        public static PropertyDataType ToPropertyDataType(this FieldTypeEnum fieldType)
        {
            PropertyDataType pdt = PropertyDataType.String;

            switch (fieldType)
            {
                case FieldTypeEnum.Bool:
                    pdt = PropertyDataType.Boolean;
                    break;
                case FieldTypeEnum.DateTime:
                    pdt = PropertyDataType.DateTime;
                    break;
                case FieldTypeEnum.Decimal:
                    pdt = PropertyDataType.Decimal;
                    break;
                case FieldTypeEnum.Int:
                    pdt = PropertyDataType.Integer;
                    break;
                case FieldTypeEnum.String:
                    pdt = PropertyDataType.String;
                    break;
                case FieldTypeEnum.Collection:
                    pdt = PropertyDataType.DataObject;
                    break;
            }

            return pdt;
        }
    }
}
