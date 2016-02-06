using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Tenant.Test.DataObjects
{
    [TestClass]
    public class PropertyAndValidators
    {
        [TestMethod]
        public void ReadValidatorSettingsTest()
        {
            ValidatorSettings settings = ValidatorSettings.GetConfig();

            foreach (ValidatorTypeConfigurationElement element in settings.Validators)
            {
                Console.WriteLine("Name: {0}, Type: {1}", element.Name, element.Type);

                if (element.Parameters.Count > 0)
                {
                    foreach(ValidatorParameterConfigurationElement paramElem in element.Parameters)
                    {
                        Console.WriteLine("Name: {0}, Type: {1}", paramElem.Name, paramElem.DataType);
                    }
                }
            }

            Assert.IsTrue(settings.Validators.Count > 0);
        }

        [TestMethod]
        public void ValidatorParamsToProperties()
        {
            ValidatorSettings settings = ValidatorSettings.GetConfig();

            foreach (ValidatorTypeConfigurationElement element in settings.Validators)
            {
                Console.WriteLine("Name: {0}, Type: {1}", element.Name, element.Type);

                PropertyValueCollection properties = element.ToPropertyValues();

                if (properties.Count > 0)
                {
                    foreach (PropertyValue pv in properties)
                    {
                        Console.WriteLine("Name: {0}, Type: {1}", pv.Definition.Name, pv.Definition.DataType);
                    }
                }
            }
        }
    }
}
