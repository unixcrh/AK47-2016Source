using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Adapter
{
    /// <summary>
    /// DEConnectionDefine 的摘要说明
    /// </summary>
    [TestClass]
    public class DEConnectionDefineTest
    {
        public DEConnectionDefineTest()
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
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }
        #endregion

        [TestCategory("DEConnectionDefine"), TestMethod]
        [Description("测试连接串映射是不是为空")]
        public void DBConnectionNameTest()
        {
            Assert.IsTrue(DEConnectionDefine.DBConnectionName.IsNotEmpty());
        }
    }
}
