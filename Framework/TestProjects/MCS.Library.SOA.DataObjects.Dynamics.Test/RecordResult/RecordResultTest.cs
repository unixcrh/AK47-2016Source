using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.RecordResult
{
    /// <summary>
    /// 此测试类作废。沈峥 2015/7/22
    /// RecordResultTest 的摘要说明
    /// </summary>
    //[TestClass]
    public class RecordResultTest
    {
        public RecordResultTest()
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }
        #endregion

        [TestCategory("录屏结果生成实体及映射"), TestMethod]
        public void RecordResultGenTest()
        {
            string categoryID = "48BE753C-630D-42F4-A02D-D2B50818F817";//集团公司/管道板块/运输
            RecordResultCollection data = GetMockData();
            string masterEntityID = DEObjectOperations.InstanceWithoutPermissions.RecordResultGenerate(categoryID, data);

            DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(masterEntityID) as DynamicEntity;
            //Assert.IsTrue(entity != null && entity.Fields.Any() && entity.OuterEntities.Any(), "生成实体及映射失败!");
            Assert.IsTrue(entity != null && entity.Fields.Any(), "生成实体及映射失败!");
        }

        [TestCategory("没有子表的录屏结果生成实体及映射"), TestMethod]
        public void RecordResultNoneChildTest()
        {
            string categoryID = "48BE753C-630D-42F4-A02D-D2B50818F817";//集团公司/管道板块/运输
            RecordResultCollection data = new RecordResultCollection();

            //只取出主表数据
            data.CopyFrom(GetMockData().Where(p => p.IsMasterTable));

            string masterEntityID = DEObjectOperations.InstanceWithoutPermissions.RecordResultGenerate(categoryID, data);

            DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(masterEntityID) as DynamicEntity;
            //Assert.IsTrue(entity != null && entity.Fields.Any() && entity.OuterEntities.Any(), "生成实体及映射失败!");
            Assert.IsTrue(entity != null && entity.Fields.Any(), "生成实体及映射失败!");
        }

        /// <summary>
        /// 生成模拟数据
        /// </summary>
        /// <returns></returns>
        private RecordResultCollection GetMockData()
        {
            RecordResultCollection result = new RecordResultCollection();

            //主表
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-KUNNR", FieldType = FieldTypeEnum.String, FieldDesc = "客户", FieldLength = 255, SortNo = 1 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-OIC_MOT", FieldType = FieldTypeEnum.String, FieldDesc = "运输方式", FieldLength = 255, SortNo = 2 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-BUKRS", FieldType = FieldTypeEnum.String, FieldDesc = "公司代码", FieldLength = 255, SortNo = 3 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-VKORG", FieldType = FieldTypeEnum.String, FieldDesc = "销售主题", FieldLength = 255, SortNo = 4 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-CZDAT", FieldType = FieldTypeEnum.String, FieldDesc = "单据日期", FieldLength = 255, SortNo = 5 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-VTWEG", FieldType = FieldTypeEnum.String, FieldDesc = "销售方式", FieldLength = 255, SortNo = 6 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-VKBUR", FieldType = FieldTypeEnum.String, FieldDesc = "销售部门", FieldLength = 255, SortNo = 7 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-YSHHS", FieldType = FieldTypeEnum.String, FieldDesc = "提货方式", FieldLength = 255, SortNo = 8 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-NAME", FieldType = FieldTypeEnum.String, FieldDesc = "" });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521", EntityDesc = "销售订单", IsMasterTable = true, FieldName = "ZR5_SS02-RID_XJXS", FieldType = FieldTypeEnum.String, FieldDesc = "" });

            //子表
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521_Items1", EntityDesc = "销售订单详细", IsMasterTable = false, FieldName = "ZR5_SS02P-MATNR", FieldType = FieldTypeEnum.String, FieldDesc = "油品代码", FieldLength = 255, SortNo = 9 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521_Items1", EntityDesc = "销售订单详细", IsMasterTable = false, FieldName = "ZR5_SS02P-KWMENG", FieldType = FieldTypeEnum.String, FieldDesc = "数量", FieldLength = 255, SortNo = 10 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521_Items1", EntityDesc = "销售订单详细", IsMasterTable = false, FieldName = "ZR5_SS02P-WERKS", FieldType = FieldTypeEnum.String, FieldDesc = "业务单元代码", FieldLength = 255, SortNo = 11 });
            result.Add(new Dynamics.Objects.RecordResult() { EntityName = "ZR521_Items1", EntityDesc = "销售订单详细", IsMasterTable = false, FieldName = "ZR5_SS02P-LGORT", FieldType = FieldTypeEnum.String, FieldDesc = "库存地点代码", FieldLength = 255, SortNo = 12 });

            return result;
        }
    }
}
