using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MCS.Library.Configuration
{
    /// <summary>
    /// 额外的加载逻辑
    /// </summary>
    public static partial class ConfigurationBroke
    {
        /// <summary>
        /// 从XmlReaer加载配置节。此配置节不使用缓存
        /// </summary>
        /// <param name="section">配置节的实例</param>
        /// <param name="reader">XmlReader对象</param>
        /// <param name="sectionName">配置节名称</param>
        /// <param name="checkNullSection">如果配置节为空，是否抛出异常</param>
        /// <returns></returns>
        public static bool LoadSection(this ConfigurationSection section, XmlReader reader, string sectionName, bool checkNullSection = false)
        {
            section.NullCheck("section");
            reader.NullCheck("reader");
            sectionName.CheckStringIsNullOrEmpty("sectionName");

            bool result = false;

            if (reader.ReadToNextSibling(sectionName))
            {
                MethodInfo mi = typeof(ConfigurationSection).GetMethod("DeserializeElement", BindingFlags.Instance | BindingFlags.NonPublic);

                mi.NullCheck<ConfigurationException>("Without DeserializeElement method in type {0}.", section.GetType().FullName);

                mi.Invoke(section, new object[] { reader, false });

                result = true;
            }
            else
            {
                if (checkNullSection)
                    section.CheckSectionNotNull(sectionName);
            }

            return result;
        }

        /// <summary>
        /// 从字符串加载配置节。此配置节不使用缓存
        /// </summary>
        /// <param name="section">配置节的实例</param>
        /// <param name="sectionInfo">配置信息的字符串</param>
        /// <param name="sectionName">配置节名称</param>
        /// <param name="checkNullSection">如果配置节为空，是否抛出异常</param>
        /// <returns></returns>
        public static bool LoadSection(this ConfigurationSection section, string sectionInfo, string sectionName, bool checkNullSection = false)
        {
            sectionInfo.CheckStringIsNullOrEmpty("sectionInfo");

            using (StringReader reader = new StringReader(sectionInfo))
            {
                return section.LoadSection(XmlReader.Create(reader), sectionName, checkNullSection);
            }
        }
    }
}
