using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Adapter
{
    /// <summary>
    /// CategoryAdapter 的摘要说明
    /// </summary>
    [TestClass]
    public class DEDynamicEntityFieldSnapShotAdapterTest
    {
        public DEDynamicEntityFieldSnapShotAdapterTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
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

        [TestCategory("DEDynamicEntityFieldSnapShotAdapter"), TestMethod]
        public void GetByRefferanceCodeNameTest()
        {
            // /集团公司/管道板块/运输
            string categoryID = "48BE753C-630D-42F4-A02D-D2B50818F817";
            var entity = CreatEntity(categoryID);
            var childEntity = creatChildEntity(categoryID);
            //子表入库
            DEObjectOperations.InstanceWithoutPermissions.AddEntity(childEntity);
            //子表CodeName
            string childCodeName = childEntity.CodeName;
            //主表字段跟子表关联
            entity.Fields[0].FieldType = FieldTypeEnum.Collection;
            entity.Fields[0].ReferenceEntityCodeName = childCodeName;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);

            Assert.AreEqual(1, DEDynamicEntityFieldSnapShotAdapter.Instance.LoadByRefferanceCodeName(childEntity.CodeName).Count);
        }

        #region 辅助方法
        /// <summary>
        /// 创建实体字段
        /// </summary>
        /// <returns></returns>
        private static DynamicEntityField CreatEntityField(int sortNo, string flag = "new")
        {
            var field = new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "字段",
                Description = "描述" + flag,
                Length = 2,
                DefaultValue = "默认值",
                FieldType = FieldTypeEnum.Decimal,
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
                SortNo = sortNo
            };

            return field;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <returns></returns>
        private static DynamicEntity CreatEntity(string categoryID)
        {
            string entityId = Guid.NewGuid().ToString();

            var entity = new DynamicEntity
            {
                ID = entityId,
                Name = "实体1",
                Description = "描述",
                CreateDate = DateTime.Now,
                CategoryID = categoryID,//已存在的分类编码
                Fields = new DynamicEntityFieldCollection(),
                SortNo = 0,
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
            };

            for (var i = 0; i < 2; i++)
            {
                var field = CreatEntityField(i);
                entity.Fields.Add(field);
            }

            return entity;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <returns></returns>
        private DynamicEntity creatChildEntity(string categoryID, int randomParam = 0)
        {
            string entityId = Guid.NewGuid().ToString();

            var entity = new DynamicEntity
            {
                ID = entityId,
                Name = "行项目实体" + randomParam.ToString(),
                Description = "描述",
                CreateDate = DateTime.Now,
                CategoryID = categoryID,//已存在的分类编码
                Fields = new DynamicEntityFieldCollection(),
                SortNo = 0,
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
            };

            for (var i = 0; i < 2; i++)
            {
                var field = CreatEntityField(i);
                entity.Fields.Add(field);
            }
            return entity;
        }

        /// <summary>
        /// 创建外部实体字段
        /// </summary>
        /// <returns></returns>
        private OuterEntityField creatChildEntity()
        {
            string fieldId = Guid.NewGuid().ToString();

            var newOuterEntityField = new OuterEntityField()
            {
                ID = fieldId,
                Name = "OuterEntityField",

            };
            return newOuterEntityField;
        }

        #endregion


    }
}
