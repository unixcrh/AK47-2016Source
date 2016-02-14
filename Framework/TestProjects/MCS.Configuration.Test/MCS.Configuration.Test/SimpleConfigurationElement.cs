using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Configuration.Test
{
    public class SimpleConfigurationElement : NamedConfigurationElement
    { 
    }

    public class SimpleConfigurationElementCollection : NamedConfigurationElementCollection<SimpleConfigurationElement>
    {
    }
}
