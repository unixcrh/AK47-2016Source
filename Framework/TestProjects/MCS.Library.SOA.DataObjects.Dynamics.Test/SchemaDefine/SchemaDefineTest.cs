using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.SchemaDefine
{
    /// <summary>
    /// SchemaDefineTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SchemaDefineTest
    {
        public SchemaDefineTest()
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

        /// <summary>
        /// 带SortNo的对象构造测试
        /// </summary>
        [TestMethod]
        [Description("通过SchemaType的名称创建对象的测试")]
        public void CreateObjectWithSortNo()
        {
            try
            {
                DynamicEntityField entity = SchemaExtensions.CreateObject("DynamicEntityField") as DynamicEntityField;

                Assert.AreEqual(0, entity.SortNo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
