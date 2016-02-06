using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Adapter
{
    /// <summary>
    /// DEMemberRelationAdapterTest 的摘要说明
    /// </summary> 
    [TestClass]
    public class DEMemberRelationAdapterTest
    {
        public DEMemberRelationAdapterTest()
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
        //
        //在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }
        #endregion

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据成员的ID查找关系")]
        public void LoadByMemberIDTest()
        {
            var newEntity = CreateEntity();

            var fieldDesc = "New Field Description";

            newEntity.Fields[0].Description = fieldDesc;

            var fieldID = newEntity.Fields[0].ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.LoadByMemberID(fieldID).FirstOrDefault();

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(fieldID, relation.ID);
            Assert.AreEqual(fieldDesc, ((DynamicEntityField)relation.Member).Description);
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据成员的ID和SchemaType查找关系")]
        public void LoadByMemberIDAndContainerSchemaTypeTest()
        {
            var newEntity = CreateEntity();

            var fieldDesc = "New Field Description";

            newEntity.Fields[0].Description = fieldDesc;

            var fieldID = newEntity.Fields[0].ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.LoadByMemberID(fieldID, newEntity.SchemaType).FirstOrDefault();

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(fieldID, relation.ID);
            Assert.AreEqual(fieldDesc, ((DynamicEntityField)relation.Member).Description);
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据成员的ID和时间查找关系")]
        public void LoadByMemberIDAndTimeTest()
        {
            var newEntity = CreateEntity();

            var fieldDesc = "New Field Description";

            newEntity.Fields[0].Description = fieldDesc;

            var fieldID = newEntity.Fields[0].ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.LoadByMemberID(fieldID, DateTime.MinValue).FirstOrDefault();

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(fieldID, relation.ID);
            Assert.AreEqual(fieldDesc, ((DynamicEntityField)relation.Member).Description);
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据容器的ID查找关系")]
        public void LoadByContainerIDTest()
        {
            var newEntity = CreateEntity();

            var description = "New Description";

            newEntity.Description = description;

            var containerID = newEntity.ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.LoadByContainerID(containerID).FirstOrDefault();

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(description, ((DynamicEntity)relation.Container).Description);
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据容器的ID和子成员的SchemaType查找关系")]
        public void LoadByContainerIDAndMemberSchemaTypeTest()
        {
            var newEntity = CreateEntity();

            var description = "New Description";

            newEntity.Description = description;

            var containerID = newEntity.ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.LoadByContainerID(containerID, "DynamicEntityField").FirstOrDefault();

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(description, ((DynamicEntity)relation.Container).Description);
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据容器的ID和时间查找关系")]
        public void LoadByContainerIDAndTimeTest()
        {
            var newEntity = CreateEntity();

            var description = "New Description";

            newEntity.Description = description;

            var containerID = newEntity.ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relations = DEMemberRelationAdapter.Instance.LoadByContainerID(containerID, DateTime.MinValue);

            var relation = relations.FirstOrDefault();

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(description, ((DynamicEntity)relation.Container).Description);
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据容器的ID和成员的ID来加载关系")]
        public void LoadByID()
        {
            var newEntity = CreateEntity();

            var description = "New Description";

            var fieldDescription = "New Field Description";

            newEntity.Description = description;

            newEntity.Fields[0].Description = fieldDescription;

            var fieldID = newEntity.Fields[0].ID;

            var containerID = newEntity.ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.Load(containerID, fieldID);

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(fieldID, relation.ID);

            Assert.AreEqual(description, ((DynamicEntity)relation.Container).Description);
            Assert.AreEqual(fieldDescription, relation.Member.Properties.GetValue("Description", string.Empty));
        }

        [TestCategory("DEMemberRelationAdapter"), TestMethod]
        [Description("根据容器的ID、成员的ID和时间来加载关系")]
        public void LoadByIDAndTime()
        {
            var newEntity = CreateEntity();

            var description = "New Description";

            var fieldDescription = "New Field Description";

            newEntity.Description = description;

            newEntity.Fields[0].Description = fieldDescription;

            var fieldID = newEntity.Fields[0].ID;

            var containerID = newEntity.ID;

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(newEntity);

            var relation = DEMemberRelationAdapter.Instance.Load(containerID, fieldID, DateTime.MinValue);

            Assert.AreEqual(newEntity.ID, relation.ContainerID);
            Assert.AreEqual(fieldID, relation.ID);

            Assert.AreEqual(description, ((DynamicEntity)relation.Container).Description);
            Assert.AreEqual(fieldDescription, relation.Member.Properties.GetValue("Description", string.Empty));
        }

        #region 辅助方法

        /// <summary>
        /// 创建实体字段
        /// </summary>
        /// <returns></returns>
        private static DynamicEntityField CreateEntityField(string flag = "new")
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
        private static DynamicEntity CreateEntity()
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
                Fields = new DynamicEntityFieldCollection(),
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
                SortNo = 0
            };

            for (var i = 0; i < 2; i++)
            {
                var field = CreateEntityField();
                entity.Fields.Add(field);
            }
            return entity;
        }

        #endregion
    }
}
