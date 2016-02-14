using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Configuration.Test
{
    public class SimpleSection : ConfigurationSection
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

        [ConfigurationProperty("items")]
        public SimpleConfigurationElementCollection Items
        {
            get
            {
                return (SimpleConfigurationElementCollection)this["items"];
            }
        }
    }
}
