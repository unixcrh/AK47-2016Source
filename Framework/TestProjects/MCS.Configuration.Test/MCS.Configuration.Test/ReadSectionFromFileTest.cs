using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Configuration;
using MCS.Library.Configuration;

namespace MCS.Configuration.Test
{
    [TestClass]
    public class ReadSectionFromFileTest
    {
        [TestMethod]
        public void ReadSection()
        {
            SimpleSection section = GetSectionFromFile("simpleSection1.xml", "simpleSection");

            Console.WriteLine("Section Name: {0}, Items Count: {1}", section.Name, section.Items.Count);
            Assert.AreEqual("Shen Zheng", section.Name);
        }

        [TestMethod]
        public void MergeSection()
        {
            SimpleSection section = GetSectionFromFile("simpleSection1.xml", "simpleSection");

            ReadSection(section, "simpleSection2.xml", "simpleSection");

            Console.WriteLine("Section Name: {0}, Items Count: {1}", section.Name, section.Items.Count);
            //Assert.AreEqual("Shen Rong", section2.Name);
        }

        [TestMethod]
        public void MergeAppConfig()
        {
            SimpleSection appSection = (SimpleSection)ConfigurationBroker.GetSection("simpleSection");

            ReadSection(appSection, "simpleSection1.xml", "simpleSection");

            Console.WriteLine("Section Name: {0}, Items Count: {1}", appSection.Name, appSection.Items.Count);
        }

        private static void ReadSection(ConfigurationSection section, string fileName, string sectionName)
        {
            XmlReader reader = XmlReader.Create(fileName);

            reader.ReadToNextSibling(sectionName);

            MethodInfo mi = typeof(SimpleSection).GetMethod("DeserializeElement", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(mi);

            mi.Invoke(section, new object[] { reader, false });
        }

        private static SimpleSection GetSectionFromFile(string fileName, string sectionName)
        {
            SimpleSection section = new SimpleSection();

            ReadSection(section, fileName, sectionName);

            return section;
        }
    }
}
