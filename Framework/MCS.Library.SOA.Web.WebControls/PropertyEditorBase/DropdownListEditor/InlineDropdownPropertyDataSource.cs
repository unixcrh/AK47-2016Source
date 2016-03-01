using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.WebControls
{
    public class InlineDropdownPropertyDataSource
    {
        public object GetDataSource(string dataSourceID)
        {
            List<InlineDataSourceItem> result = new List<InlineDataSourceItem>();

            if (dataSourceID.IsNotEmpty())
            {
                DropdownPropertyDataSourceConfigurationElement configElement = DropdownPropertyDataSourceSettings.GetConfig().PropertySources[dataSourceID];

                if (configElement != null)
                {
                    foreach (InlineDropdownPropertyDataSourceItemConfigurationElement inlineItem in configElement.InlineItems)
                    {
                        InlineDataSourceItem item = new InlineDataSourceItem() { Key = inlineItem.Name, Name = inlineItem.Description };

                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}
