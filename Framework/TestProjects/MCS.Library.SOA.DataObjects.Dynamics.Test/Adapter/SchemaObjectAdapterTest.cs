using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Adapter
{
    [TestClass]
    public class SchemaObjectAdapterTest
    {
        public SchemaObjectAdapterTest()
        {
        }

        //在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }

        #region 操作实体
        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("添加动态实体测试")]
        public void SchemaObjectAdapterAddEntityMethodTest()
        {
            var newEntity = createEntity();
            DESchemaObjectAdapter.Instance.Update(newEntity);
            var loadEntity = DESchemaObjectAdapter.Instance.Load(newEntity.ID);
            Assert.IsNotNull(loadEntity, "添加动态实体测试失败！");
        }

        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("更新动态实体测试")]
        public void SchemaObjectAdapterUpdateEntityMethodTest()
        {
            var newEntity = createEntity();
            DESchemaObjectAdapter.Instance.Update(newEntity);
            //重新设置时间点
            DBTimePointActionContext.Clear();
            var loadEntity = DESchemaObjectAdapter.Instance.Load(newEntity.ID) as DynamicEntity;
            Assert.IsNotNull(loadEntity, "添加动态实体测试失败！");
            loadEntity.Description = "Update Entity";
            DESchemaObjectAdapter.Instance.Update(loadEntity);
            var resultEntity = DESchemaObjectAdapter.Instance.Load(newEntity.ID) as DynamicEntity;
            Assert.IsNotNull(resultEntity, "更新动态实体数据丢失！");
            Assert.AreEqual(resultEntity.Description, "Update Entity", "更新动态实体失败！");
        }

        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("加载动态实体测试")]
        public void SchemaObjectAdapterLoadEntityMethodTest()
        {
            var newEntity = createEntity();
            DESchemaObjectAdapter.Instance.Update(newEntity);
            var loadEntity = DESchemaObjectAdapter.Instance.Load(newEntity.ID) as DynamicEntity;
            Assert.IsNotNull(loadEntity, "加载动态实体数据丢失！");
        }


        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("删除动态实体测试")]
        public void SchemaObjectAdapterDeleteEntityMethodTest()
        {
            var newEntity = createEntity();
            DESchemaObjectAdapter.Instance.Update(newEntity);
            //重新设置时间点
            DBTimePointActionContext.Clear();
            var loadEntity = DESchemaObjectAdapter.Instance.Load(newEntity.ID);
            DESchemaObjectAdapter.Instance.UpdateStatus(loadEntity, DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Deleted);
            var resultEntity = DESchemaObjectAdapter.Instance.Load(newEntity.ID, false);
            Assert.AreEqual(resultEntity.Status, DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Deleted, "删除动态实体失败！");
        }
        #endregion

        #region 操作实体属性
        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("动态实体属性添加测试")]
        public void SchemaObjectAdapterAddFieldMethodTest()
        {
            var newEntityField = createEntityField();
            DESchemaObjectAdapter.Instance.Update(newEntityField);
            var loadEntityField = DESchemaObjectAdapter.Instance.Load(newEntityField.ID);
            Assert.IsNotNull(loadEntityField, "动态实体属性添加测试失败！");
        }

        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("动态实体属性更新测试")]
        public void SchemaObjectAdapterUpdateFieldMethodTest()
        {
            var newEntityField = createEntityField();
            DESchemaObjectAdapter.Instance.Update(newEntityField);
            //重新设置时间点
            DBTimePointActionContext.Clear();
            var loadEntityField = DESchemaObjectAdapter.Instance.Load(newEntityField.ID);
            Assert.IsNotNull(loadEntityField, "动态实体属性添加测试失败！");
            loadEntityField.Status = DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Unspecified;
            DESchemaObjectAdapter.Instance.Update(loadEntityField);
            var resultEntityField = DESchemaObjectAdapter.Instance.Load(loadEntityField.ID, false);
            Assert.IsNotNull(resultEntityField, "动态实体属性更新测试失败！");
            Assert.AreEqual(resultEntityField.Status, DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Unspecified, "动态实体属性更新测试！");
        }

        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("动态实体属性加载测试")]
        public void SchemaObjectAdapterLoadFieldMethodTest()
        {
            var newEntityField = createEntityField();
            DESchemaObjectAdapter.Instance.Update(newEntityField);
            var loadEntityField = DESchemaObjectAdapter.Instance.Load(newEntityField.ID) as DynamicEntityField;
            Assert.IsNotNull(loadEntityField, "动态实体属性加载测试失败！");
        }


        [TestCategory("SchemaObjectAdapter"), TestMethod]
        [Description("动态实体属性删除测试")]
        public void SchemaObjectAdapterDeleteFieldMethodTest()
        {
            var newEntityField = createEntityField();
            DESchemaObjectAdapter.Instance.Update(newEntityField);
            //重新设置时间点
            DBTimePointActionContext.Clear();
            var loadEntityField = DESchemaObjectAdapter.Instance.Load(newEntityField.ID) as DynamicEntityField;
            Assert.IsNotNull(loadEntityField, "动态实体属性加载测试失败！");
            DESchemaObjectAdapter.Instance.UpdateStatus(loadEntityField, DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Deleted);
            var resultEntityField = DESchemaObjectAdapter.Instance.Load(loadEntityField.ID, false);
            Assert.IsNotNull(resultEntityField, "动态实体属性加更新试失败！");
            Assert.AreEqual(resultEntityField.Status, DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Deleted, "动态实体属性删除失败！");
        }
        #endregion


        #region 辅助方法

        /// <summary>
        /// 创建实体字段
        /// </summary>
        /// <returns></returns>
        private DynamicEntityField createEntityField(string flag = "new")
        {
            var field = new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "字段",
                Description = "描述" + flag,
                Length = 2,
                DefaultValue = "默认值",
                FieldType = FieldTypeEnum.String,
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
        private DynamicEntity createEntity()
        {
            string entityId = Guid.NewGuid().ToString();
            var entity = new DynamicEntity
            {
                ID = entityId,
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
                var field = createEntityField();
                entity.Fields.Add(field);
            }
            return entity;
        }

        #endregion
    }
}
