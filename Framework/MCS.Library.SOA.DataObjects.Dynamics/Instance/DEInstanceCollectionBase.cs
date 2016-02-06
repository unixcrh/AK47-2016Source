using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Schemas
{
    [Serializable]
    public abstract class DEInstanceCollectionBase<T, TFilterResult> :
        EditableDataObjectCollectionBase<T>
        where T : DEEntityInstanceBase
        where TFilterResult : EditableDataObjectCollectionBase<T>
    {
        /// <summary>
        /// 创建过滤结果的集合
        /// </summary>
        /// <returns></returns>
        protected abstract TFilterResult CreateFilterResultCollection();

        /// <summary>
        /// 获取按状态进行过滤的结果的集合
        /// </summary>
        /// <param name="filter"><see cref="SchemaObjectStatusFilterTypes"/>值之一，表示过滤的类型</param>
        /// <returns></returns>
        public TFilterResult FilterByStatus(DESchemaObjectStatusFilterTypes filter)
        {
            TFilterResult result = CreateFilterResultCollection();

            foreach (T obj in this)
            {
                if ((filter & DESchemaObjectStatusFilterTypes.Normal) != DESchemaObjectStatusFilterTypes.None &&
                    obj.Status == SchemaObjectStatus.Normal)
                {
                    result.Add(obj);
                }

                if ((filter & DESchemaObjectStatusFilterTypes.Deleted) != DESchemaObjectStatusFilterTypes.None &&
                    obj.Status == SchemaObjectStatus.Deleted)
                {
                    result.Add(obj);
                }
            }

            return result;
        }

        public void LoadFromDataView(DataView view)
        {
            foreach (DataRowView drv in view)
            {
                T obj = (T)SchemaExtensions.CreateInstanceBaseObject((string)drv["EntityID"]);

                obj.FromString((string)drv["Data"]);

                ORMapping.DataRowToObject(drv.Row, obj);

                this.Add(obj);
            }
        }
    }

    [Serializable]
    public abstract class DEInstanceEditableKeyedCollectionBase<T, TFilterResult> :
        SerializableEditableKeyedDataObjectCollectionBase<string, T>
        where T : DEEntityInstanceBase
        where TFilterResult : EditableKeyedDataObjectCollectionBase<string, T>
    {
        public DEInstanceEditableKeyedCollectionBase()
            : base(100)
        {
        }

        public DEInstanceEditableKeyedCollectionBase(int capacity)
            : base(capacity)
        {
        }

        protected DEInstanceEditableKeyedCollectionBase(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        /// <summary>
        /// 创建过滤结果的集合
        /// </summary>
        /// <returns></returns>
        protected abstract TFilterResult CreateFilterResultCollection();

        public TFilterResult FilterByStatus(DESchemaObjectStatusFilterTypes filter)
        {
            TFilterResult result = CreateFilterResultCollection();

            foreach (T obj in this)
            {
                if ((filter & DESchemaObjectStatusFilterTypes.Normal) != DESchemaObjectStatusFilterTypes.None &&
                    obj.Status == SchemaObjectStatus.Normal)
                {
                    result.Add(obj);
                }

                if ((filter & DESchemaObjectStatusFilterTypes.Deleted) != DESchemaObjectStatusFilterTypes.None &&
                    obj.Status == SchemaObjectStatus.Deleted)
                {
                    result.Add(obj);
                }
            }

            return result;
        }

        public void LoadFromDataView(DataView view, Action<DataRow, T> action)
        {
            Dictionary<string, ObjectSchemaConfigurationElement> schemaElements = new Dictionary<string, ObjectSchemaConfigurationElement>(StringComparer.OrdinalIgnoreCase);

            ObjectSchemaSettings settings = ObjectSchemaSettings.GetConfig();

            foreach (DataRowView drv in view)
            {
                string schemaType = (string)drv["SchemaType"];

                ObjectSchemaConfigurationElement schemaElement = null;

                if (schemaElements.TryGetValue(schemaType, out schemaElement) == false)
                {
                    schemaElement = settings.Schemas[schemaType];

                    schemaElements.Add(schemaType, schemaElement);
                }

                if (schemaElement != null)
                {
                    T obj = (T)schemaElement.CreateInstance();

                    obj.FromString((string)drv["Data"]);

                    ORMapping.DataRowToObject(drv.Row, obj);

                    if (action != null)
                        action(drv.Row, obj);

                    if (this.ContainsKey(obj.ID) == false)
                        this.Add(obj);
                }
            }
        }

        public void LoadFromDataView(DataView view)
        {
            LoadFromDataView(view, null);
        }

        public void LoadFromDataReader(DataView dataView)
        {
            Dictionary<string, DynamicEntity> schemaElements = new Dictionary<string, DynamicEntity>(StringComparer.OrdinalIgnoreCase);

            DynamicEntity dynamicEntity = null;

            DataTable table = dataView.ToTable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string entityCode = table.Rows[i]["EntityCode"].ToString();
                string data = table.Rows[i]["Data"].ToString();

                if (schemaElements.TryGetValue(entityCode, out dynamicEntity) == false)
                {
                    dynamicEntity = DESchemaObjectAdapter.Instance.Load(entityCode) as DynamicEntity;
                    schemaElements.Add(entityCode, dynamicEntity);
                }

                if (dynamicEntity != null)
                {
                    T obj = (T)dynamicEntity.CreateInstance();

                    obj.FromString(data);

                    ORMapping.DataRowToObject(table.Rows[i], obj);
                    
                    if (this.ContainsKey(obj.ID) == false)
                        this.Add(obj);
                }
            }
        }

        public string[] ToIDArray()
        {
            List<string> result = new List<string>(this.Count);

            this.ForEach(s => result.Add(s.ID));

            return result.ToArray();
        }
    }
}
