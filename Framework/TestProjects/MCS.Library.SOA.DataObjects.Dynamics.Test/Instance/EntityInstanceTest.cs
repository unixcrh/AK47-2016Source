using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Contract;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Mock;
using MCS.Library.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Instance
{
    /// <summary>
    /// EntityInstanceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class EntityInstanceTest
    {
        public EntityInstanceTest()
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
        //在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }
        #endregion

        [Description("实体实例"), TestMethod]
        public void AddEntityInstance()
        {
            DEEntityInstanceBase instance = MockData.CreateInstanceWithData();

            DEEntityInstanceBase result = DEInstanceAdapter.Instance.Load(instance.ID);

            var coll = result.Fields.FirstOrDefault(p => p.Definition.FieldType == FieldTypeEnum.Collection).GetRealValue() as DEEntityInstanceBaseCollection;

            decimal totalAmount = coll.Select(p => Convert.ToDecimal(p.Fields["单价"].StringValue)).Sum();
            Assert.AreEqual(200, totalAmount);
        }

        /// <summary>
        /// 实体映射暂不需要此测试没用
        /// </summary>
        [Description("实体实例映射"), TestMethod]
        public void EntityInstanceMapping()
        {
            DEEntityInstanceBase instance = MockData.CreateInstanceWithData();

            List<SapValue> result = instance.ToParams("Tcode_test");
            //这儿的断言太扯，回头改
            Assert.IsNotNull(result, "实体实例转KeyValue报错");
        }

        [Description("实体实例默认值测试"), TestMethod]
        public void EntityInstanceDefaultValueTest()
        {
            DynamicEntity emptyEntity = MockData.CreateEntityWithReferenceEntity();

            var emptyInstance = emptyEntity.CreateInstance();

            Assert.AreEqual(99, Convert.ToDecimal(emptyInstance.Fields["总金额"].GetRealValue()), "实体实例默认值测试失败");
        }

        [Description("实体实例获取强类型值测试"), TestMethod]
        public void EntityInstanceGetRealValueTest()
        {
            DEEntityInstanceBase instance = MockData.CreateInstaceWithAllTypeData();

            bool flag = Convert.ToBoolean(instance.Fields["Bool"].GetRealValue()) == true &&
                        Convert.ToDateTime(instance.Fields["DateTime"].GetRealValue()).ToString("yyyyMMdd") == "20140303" &&
                        Convert.ToDecimal(instance.Fields["Decimal"].GetRealValue()) == 99 &&
                        Convert.ToInt32(instance.Fields["Int"].GetRealValue()) == 99 &&
                        Convert.ToString(instance.Fields["String"].GetRealValue()) == "haoyk";

            Assert.IsTrue(flag, "实体实例获取强类型值失败");
        }

        [TestMethod]
        [Description("实例的验证器测试")]
        public void InstanceValidatorTest()
        {
            DEEntityInstanceBase instance = MockData.CreateInstaceWithAllTypeData() as DEEntityInstance;

            ValidationResults result = instance.Validate();

            Assert.IsTrue(result.ResultCount == 0);
        }
    }
}
