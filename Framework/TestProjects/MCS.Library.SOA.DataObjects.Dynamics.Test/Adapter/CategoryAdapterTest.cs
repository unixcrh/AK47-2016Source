using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Adapter
{
    /// <summary>
    /// CategoryAdapter 的摘要说明
    /// </summary>
    [TestClass]
    public class CategoryAdapterTest
    {
        public CategoryAdapterTest()
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

        [TestCategory("CategoryAdapter"), TestMethod]
        [Description("获取类别的根测试")]
        public void GetRootTest()
        {
            DECategory root = CategoryAdapter.Instance.GetRoot();

            Assert.IsNotNull(root, "获取根类别失败！");
        }

        [TestCategory("CategoryAdapter"), TestMethod]
        [Description("根据父类别的Code获取子类别")]
        public void GetCategoryByParentTest()
        {
            CategoryCollection root = CategoryAdapter.Instance.GetByParentCode(CategoryAdapter.Instance.GetRoot().Code);

            Assert.IsTrue(root.Count > 0, "根据父类别获取类别信息失败！");
        }
    }
}
