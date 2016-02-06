using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.InstanceBefore
{
    /// <summary>
    /// InstanceBeforeTest 的摘要说明
    /// </summary>
    [TestClass]
    public class EntityObjectBeforeTest
    {
        public EntityObjectBeforeTest()
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }
        #endregion

        /// <summary>
        /// 查询已删除和修改过的实体定义
        /// </summary>
        [Description("根据ID查询实体"), TestMethod]
        public void LoadInTest()
        {
            bool flag = true;
            string msg = string.Empty;
            try
            {
                DynamicEntity dey = MockData.CreateEntityWithReferenceEntity();
                DateTime dt1 = DateTime.Now;

                DynamicEntity d = DESchemaObjectAdapter.Instance.Load(dey.ID) as DynamicEntity;

                d.Name = "这是一个新的Name";
                DEObjectOperations.InstanceWithoutPermissions.UpdateEntity(d);

                d = DESchemaObjectAdapter.Instance.Load(d.ID, dt1, false) as DynamicEntity;
                DynamicEntity d2 = DESchemaObjectAdapter.Instance.Load(dey.ID) as DynamicEntity;

                if (d.Name == dey.Name && d2.Name == "这是一个新的Name")
                    flag = true;

            }
            catch (Exception ex)
            {
                flag = false;
                msg = ex.Message;
            }

            Assert.IsTrue(flag, "时间穿梭失败" + msg);
        }
    }
}
