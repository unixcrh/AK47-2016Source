using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects
{
    public class ExtendedPropertiesAdapter : UpdatableAndLoadableAdapterBase<ExtendedProperties, ExtendedPropertiesCollection>
    {
        public static readonly ExtendedPropertiesAdapter Instance = new ExtendedPropertiesAdapter();

        protected ExtendedPropertiesAdapter()
        {
        }

        public ExtendedProperties Load(string id)
        {
            id.CheckStringIsNullOrEmpty("id");

            return this.LoadByInBuilder(builder => builder.AppendItem(id), "ID").FirstOrDefault();
        }

        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }
    }
}
