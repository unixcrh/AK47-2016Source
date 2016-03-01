using MCS.Library.Configuration;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MCS.Web.WebControls
{
    public sealed class DropdownPropertyDataSourceSettings : ConfigurationSection
    {
        public static DropdownPropertyDataSourceSettings GetConfig()
        {
            DropdownPropertyDataSourceSettings settings = (DropdownPropertyDataSourceSettings)ConfigurationBroker.GetSection("dropdownPropertyDataSourceSettings");

            if (settings == null)
                settings = new DropdownPropertyDataSourceSettings();

            return settings;
        }

        private DropdownPropertyDataSourceSettings()
        {
        }

        [ConfigurationProperty("propertySources", IsRequired = false)]
        public DropdownPropertyDataSourceConfigurationCollection PropertySources
        {
            get
            {
                return (DropdownPropertyDataSourceConfigurationCollection)this["propertySources"];
            }
        }
    }

    public sealed class DropdownPropertyDataSourceConfigurationElement : TypeConfigurationElement
    {
        [ConfigurationProperty("bindingValue", IsRequired = true)]
        public string BindingValue
        {
            get
            {
                return (string)this["bindingValue"];
            }
        }

        [ConfigurationProperty("bindingText", IsRequired = true)]
        public string BindingText
        {
            get
            {
                return (string)this["bindingText"];
            }
        }

        [ConfigurationProperty("method", IsRequired = true)]
        public string Method
        {
            get
            {
                return (string)this["method"];
            }
        }

        [ConfigurationProperty("methodParemeter", IsRequired = false)]
        public string MethodParemeter
        {
            get
            {
                return (string)this["methodParemeter"];
            }
        }

        [ConfigurationProperty("inlineItems", IsRequired = false)]
        public InlineDropdownPropertyDataSourceItemConfigurationElementCollection InlineItems
        {
            get
            {
                return (InlineDropdownPropertyDataSourceItemConfigurationElementCollection)this["inlineItems"];
            }
        }

        public object GenerateDataSource()
        {
            this.Type.CheckStringIsNullOrEmpty("type");
            this.Method.CheckStringIsNullOrEmpty("method");

            Type type = this.GetTypeInfo();

            MethodInfo mi = type.GetMethod(this.Method);

            (mi != null).FalseThrow("不能在类型{0}中找到方法{1}", type.FullName, this.Method);

            List<object> parameters = new List<object>();

            if (this.MethodParemeter.IsNotEmpty())
                parameters.Add(this.MethodParemeter);

            return mi.Invoke(this.CreateInstance(), parameters.ToArray());
        }
    }

    public sealed class DropdownPropertyDataSourceConfigurationCollection : NamedConfigurationElementCollection<DropdownPropertyDataSourceConfigurationElement>
    {
    }

    public sealed class InlineDropdownPropertyDataSourceItemConfigurationElement : NamedConfigurationElement
    {
    }

    public sealed class InlineDropdownPropertyDataSourceItemConfigurationElementCollection : NamedConfigurationElementCollection<InlineDropdownPropertyDataSourceItemConfigurationElement>
    {
    }
}
