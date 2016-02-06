using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Tenant.Test.DataObjects
{
    [TestClass]
    public class ExtendedPropertiesTest
    {
        [TestMethod]
        public void PropertyToDataTest()
        {
            ExtendedProperties ep = PrepareData();

            ExtendedPropertiesAdapter.Instance.Update(ep);

            ExtendedProperties epLoaded = ExtendedPropertiesAdapter.Instance.Load(ep.ID);

            AssertEqual(ep, epLoaded);
        }

        private static void AssertEqual(ExtendedProperties expected, ExtendedProperties actual)
        {
            expected.NullCheck("expected");
            actual.NullCheck("actual");

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.ResourceID, actual.ResourceID);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual("xml", actual.Format);

            Assert.AreEqual(expected.Properties.Count, actual.Properties.Count);
            Assert.AreEqual(expected.Properties[0].Definition.Name, actual.Properties[0].Definition.Name);
            Assert.AreEqual(expected.Properties[1].Definition.Name, actual.Properties[1].Definition.Name);
        }

        private static ExtendedProperties PrepareData()
        {
            ExtendedProperties ep = new ExtendedProperties();

            ep.ID = UuidHelper.NewUuidString();
            ep.ResourceID = UuidHelper.NewUuidString();
            ep.Type = "Test";
            ep.Properties.CopyFrom(PrepareProperties());

            return ep;
        }

        private static PropertyValueCollection PrepareProperties()
        {
            PropertyValueCollection properties = new PropertyValueCollection();

            PropertyDefine pd1 = new PropertyDefine();

            pd1.Name = "P1";

            properties.Add(new PropertyValue(pd1));

            PropertyDefine pd2 = new PropertyDefine();

            pd2.Name = "P2";

            properties.Add(new PropertyValue(pd2));

            return properties;
        }
    }
}
