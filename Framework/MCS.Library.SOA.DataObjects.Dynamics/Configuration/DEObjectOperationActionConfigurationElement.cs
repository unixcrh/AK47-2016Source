using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MCS.Library.Configuration;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Configuration
{
	public class DEObjectOperationActionConfigurationElement : TypeConfigurationElement
	{
		[ConfigurationProperty("operation", IsRequired = false)]
		public string Operation
		{
			get
			{
				return (string)this["operation"];
			}
		}
	}

	public class DEOperationActionConfigurationElementCollection : NamedConfigurationElementCollection<DEObjectOperationActionConfigurationElement>
	{
	}
}
