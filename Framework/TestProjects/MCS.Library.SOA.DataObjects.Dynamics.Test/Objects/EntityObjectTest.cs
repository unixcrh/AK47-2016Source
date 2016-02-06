using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Common;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Mock;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Objects
{
    /// <summary>
    /// EntityObjectTest 的摘要说明
    /// </summary>
    [TestClass]
    public class EntityObjectTest
    {
        public EntityObjectTest()
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

        #region 操作实体
        [TestCategory("EntityObject"), TestMethod]
        [Description("增加一个实体定义，然后加载测试")]
        public void AddEntityTest()
        {
            var entity = CreatEntity();

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);

            var entityLoaded = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

            Assert.AreEqual(entity.ID, entityLoaded.ID);

            Assert.AreEqual(entity.Fields.Count, entityLoaded.Fields.Count);

            AssertFields(entity.Fields, entityLoaded.Fields);

            Assert.AreEqual(entity.AllMembersRelations.Count, entityLoaded.AllMembersRelations.Count);

            foreach (var relation in entity.AllMembersRelations)
            {
                var relationLoaded = entityLoaded.AllMembersRelations[relation.ID];

                Assert.IsNotNull(relationLoaded);
                Assert.AreEqual(entityLoaded.ID, relationLoaded.ContainerID);
            }
        }

        [TestCategory("EntityObject"), TestMethod]
        [Description("修改一个新增加的数据实体测试，包括实体的属性变更以及字段变更")]
        public void UpdateEntityTest()
        {
            var entity = CreatEntity();

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);

            var entityInserted = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

            //修改描述属性
            entityInserted.Description = "Test Update";

            //删除一个字段
            if (entityInserted.Fields.Count() > 0)
                entityInserted.Fields[0].Status = SchemaObjectStatus.Deleted;
                //entityInserted.Fields.RemoveAt(0);

            //增加一个字段
            var newField = CreatEntityField("update");

            entityInserted.Fields.Add(newField);

            DEObjectOperations.InstanceWithoutPermissions.UpdateEntity(entityInserted);

            var entityUpdated = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

            Assert.AreEqual(entityInserted.Description, entityUpdated.Description);
            AssertFields(entityInserted.Fields, entityUpdated.Fields);
        }

        [TestCategory("EntityObject"), TestMethod]
        [Description("删除一个数据实体测试")]
        public void DeleteEntityTest()
        {
            var entity = CreatEntity();

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);

            DEObjectOperations.InstanceWithoutPermissions.DeleteEntity(entity);

            var entityDeleted = DESchemaObjectAdapter.Instance.Load(entity.ID, false);

            Assert.AreEqual(SchemaObjectStatus.Deleted, entityDeleted.Status);
        }

        ///// <summary>
        ///// 沈峥注释，文不对题
        ///// </summary>
        //[TestCategory("EntityObject"), TestMethod]
        //public void DeleteEntityFromCollectionMethod()
        //{
        //    DynamicEntityCollection entities = new DynamicEntityCollection();

        //    var entity = CreatEntity();

        //    DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);

        //    var dbResult = DESchemaObjectAdapter.Instance.Load(entity.ID);

        //    Assert.IsTrue(dbResult.ID.Equals(entity.ID));
        //}

        
        #endregion

        #region 操作实体字段

        [TestCategory("EntityObject"), TestMethod]
        [Description("增加字段的测试")]
        public void AddEntityFieldTest()
        {
            var field = CreatEntityField();

            DEObjectOperations.InstanceWithoutPermissions.AddEntityField(field);

            var fieldLoaded = DESchemaObjectAdapter.Instance.Load(field.ID);

            Assert.IsTrue(fieldLoaded.ID.Equals(field.ID));
        }

        [TestCategory("EntityObject"), TestMethod]
        [Description("修改字段的测试")]
        public void UpdateEntityFieldTest()
        {
            var field = CreatEntityField();

            DEObjectOperations.InstanceWithoutPermissions.AddEntityField(field);

            var fieldInserted = DESchemaObjectAdapter.Instance.Load(field.ID) as DynamicEntityField;

            var defaultValue = "default update";
            fieldInserted.DefaultValue = defaultValue;

            DEObjectOperations.InstanceWithoutPermissions.UpdateEntityField(fieldInserted);

            var fieldUpdated = DESchemaObjectAdapter.Instance.Load(fieldInserted.ID) as DynamicEntityField;

            Assert.AreEqual(fieldInserted.DefaultValue, fieldUpdated.DefaultValue);
        }

        [TestCategory("EntityObject"), TestMethod]
        [Description("删除字段的测试")]
        public void DeleteEntityFieldTest()
        {
            var field = CreatEntityField();

            DEObjectOperations.InstanceWithoutPermissions.AddEntityField(field);

            DEObjectOperations.InstanceWithoutPermissions.DeleteEntityField(field);

            var fieldDeleted = DESchemaObjectAdapter.Instance.Load(field.ID, false) as DynamicEntityField;

            Assert.AreEqual(SchemaObjectStatus.Deleted, fieldDeleted.Status);
        }
        #endregion

        #region 操作实体集合
        [TestCategory("EntityObject"), TestMethod]
        public void EntitiyCollectionToStringTest()
        {
            DynamicEntityCollection TestData = new DynamicEntityCollection();

            //创建测试数据
            TestData.CopyFrom(MockData.CreateRelationDynamicEntityCollection());

            var result = TestData.ToString();

            Assert.IsTrue(!string.IsNullOrEmpty(result));

        }

        [TestCategory("EntityObject"), TestMethod]
        [Description("导入实体测试")]
        public void ImportEntityTest()
        {
            XElement element = ResourceHelper.LoadXElementFromResource(Assembly.GetExecutingAssembly(), "MCS.Library.SOA.DataObjects.Dynamics.Test.Objects.importTestFile.xml");

            string msg = string.Empty;

            DEDynamicEntityImportAdapter.Instance.Import(element, Define.TestCategoryID, out msg);

            Assert.IsTrue(!msg.Contains("失败"));
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 创建实体字段
        /// </summary>
        /// <returns></returns>
        private static DynamicEntityField CreatEntityField(string flag = "new")
        {
            var field = new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "字段",
                Description = "描述" + flag,
                Length = 2,
                DefaultValue = "默认值",
                FieldType = FieldTypeEnum.Decimal,
                //CodeName = "Field",
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
                SortNo = 0
            };
            return field;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <returns></returns>
        private static DynamicEntity CreatEntity()
        {
            string entityId = Guid.NewGuid().ToString();

            var entity = new DynamicEntity
            {
                ID = entityId,
                //CodeName = "Entity1",
                Name = "实体1",
                Description = "描述",
                CreateDate = DateTime.Now,
                CategoryID = "763DF7AB-4B69-469A-8A01-041DDEAB19F7",//已存在的分类编码
                SortNo = 0,
                Fields = new DynamicEntityFieldCollection(),
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),
            };

            for (var i = 0; i < 2; i++)
            {
                var field = CreatEntityField();
                entity.Fields.Add(field);
            }

            return entity;
        }

        private static void AssertFields(DynamicEntityFieldCollection expected, DynamicEntityFieldCollection actual)
        {
            int expectedCount = expected.Count(p => p.Status == SchemaObjectStatus.Normal);
            int actualCount = actual.Count(p => p.Status == SchemaObjectStatus.Normal);
            Assert.AreEqual(expectedCount, actualCount);

            foreach (DynamicEntityField field in actual )
            {
                Assert.IsTrue(expected.ContainsKey(field.ID));
            }
        }
        #endregion
    }
}
