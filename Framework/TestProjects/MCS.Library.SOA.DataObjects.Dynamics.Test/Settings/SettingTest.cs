using MCS.Library.Data;
using MCS.Library.Logging;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class SettingTest
    {
        public SettingTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性

        #endregion

        [TestCategory("SettingTest"), TestMethod]
        [Description("测试读取objectSchemaSettings配置节")]
        public void ObjectSchemaSettingsTestMethod()
        {
            var setting = ObjectSchemaSettings.GetConfig();

            Assert.IsTrue(setting.Schemas.Count > 0, "ObjectSchemaSettings配置节出错！");
        }

        [TestCategory("Setting"), TestMethod]
        [Description("测试读取LoggingSettings配置节")]
        public void LoggingSectionTest()
        {
            var config = LoggingSection.GetConfig();

            Assert.IsNotNull(config.Loggers);
        }

        [TestCategory("Setting"), TestMethod]
        [Description("从配置文件中获取连接串的配置信息")]
        public void GetConnectionStringTest()
        {
            var config = DbConnectionManager.GetConnectionString("DynamicsEntity");

            Assert.IsTrue(config != null);
        }

        [TestCategory("Setting"), TestMethod]
        [Description("验证读取DEObjectSnapshotAction和DEObjectUpdateStatusAction配置节")]
        public void SchemaObjectUpdateActionSettingsTest()
        {
            var deObjectSnapshotAction = SchemaObjectUpdateActionSettings.GetConfig().GetActions("DEObjectSnapshotAction");

            var deObjectUpdateStatusAction = SchemaObjectUpdateActionSettings.GetConfig().GetActions("DEObjectUpdateStatusAction");

            Assert.IsNotNull(deObjectSnapshotAction);
            Assert.IsNotNull(deObjectUpdateStatusAction);
        }

        [TestCategory("Setting"), TestMethod]
        [Description("验证读取schemaPropertyGroupSettings配置节")]
        public void SchemaPropertyGroupSettingsTest()
        {
            var groupSettings = SchemaPropertyGroupSettings.GetConfig();

            Assert.IsTrue(groupSettings.Groups.Count > 0);
        }
    }
}
