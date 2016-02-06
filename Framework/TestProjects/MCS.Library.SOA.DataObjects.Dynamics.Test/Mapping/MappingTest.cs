using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Mapping
{
    /// <summary>
    /// 我们不需要Mapping的工程
    /// MappingTest 的摘要说明
    /// </summary>
    //[TestClass]
    public class MappingTest
    {
        public MappingTest()
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

        //[TestMethod]
        //public void TestEntityMapping()
        //{
        //    var entity = creatEntity();
        //    //添加实体
        //    DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);
        //    //获取实体
        //    var entityLoad = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

        //    //添加外部实体
        //    for (int i = 0; i < 2; i++)
        //    {
        //        var newOutEntity = createOuterEntity(i.ToString());//这没给外部实体下的属性集合赋值
        //        newOutEntity.SortNo = i;
        //        entityLoad.OuterEntities.Add(newOutEntity);
        //    }
        //    List<string> outerIDs = new List<string>();
        //    entityLoad.OuterEntities.ForEach(p => outerIDs.Add(p.ID));
        //    //添加Mapping
        //    DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(entityLoad);

        //    var dbResult = DESchemaObjectAdapter.Instance.Load(entityLoad.ID) as DynamicEntity;
        //    var fields = dbResult.Fields;
        //    var outentities = dbResult.OuterEntities;
        //    //mapping
        //    bool mappingSuccess = outerIDs.Count == dbResult.OuterEntities.Count;
        //    foreach (var outEntity in dbResult.OuterEntities)
        //    {
        //        if (!outerIDs.Contains(outEntity.ID))
        //        {
        //            mappingSuccess = false;
        //        }
        //    }

        //    //relation
        //    var mappings = dbResult.AllMembersRelations.Where(p => p.MemberSchemaType == DEStandardObjectSchemaType.OuterEntity.ToString());
        //    bool relationSuccess = mappings.Count() == outerIDs.Count;
        //    foreach (var relation in mappings)
        //    {
        //        if (relation.ContainerID != dbResult.ID)
        //        {
        //            relationSuccess = false;
        //        }
        //        if (!outerIDs.Contains(relation.ID))
        //        {
        //            relationSuccess = false;
        //        }
        //    }

        //    Assert.IsTrue(dbResult.ID.Equals(entity.ID) && mappingSuccess && relationSuccess);
        //}

        //[TestMethod]
        //public void TestEntityFieldMapping()
        //{
        //    var entity = creatEntity();
        //    //添加实体
        //    DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);
        //    //获取实体
        //    var entityLoad = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

        //    //添加外部实体
        //    for (int i = 0; i < 1; i++)
        //    {
        //        var newOutEntity = createOuterEntity(i.ToString());
        //        entityLoad.OuterEntities.Add(newOutEntity);
        //    }
        //    for (int i = 0; i < 1; i++)
        //    {
        //        var newOuterEntityField = createOuterEntityField(i.ToString());
        //        entityLoad.Fields[0].OuterEntityFields.Add(newOuterEntityField);
        //    }
        //    List<string> outerIDs = new List<string>();
        //    entityLoad.OuterEntities.ForEach(p => outerIDs.Add(p.ID));

        //    List<string> outerFieldIDs = new List<string>();
        //    entityLoad.Fields[0].OuterEntityFields.ForEach(p => outerFieldIDs.Add(p.ID));
        //    //添加Mapping
        //    DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(entityLoad);

        //    var dbResult = DESchemaObjectAdapter.Instance.Load(entityLoad.ID) as DynamicEntity;
        //    var fields = dbResult.Fields;
        //    var outentities = dbResult.OuterEntities;
        //    var outerEntityFields = fields[0].OuterEntityFields;
        //    //mapping
        //    bool mappingSuccess = outerIDs.Count == dbResult.OuterEntities.Count;
        //    foreach (var outEntity in dbResult.OuterEntities)
        //    {
        //        if (!outerIDs.Contains(outEntity.ID))
        //        {
        //            mappingSuccess = false;
        //        }
        //    }

        //    //relation
        //    var mappings = dbResult.AllMembersRelations.Where(p => p.MemberSchemaType == DEStandardObjectSchemaType.OuterEntity.ToString());
        //    bool relationSuccess = mappings.Count() == outerIDs.Count;
        //    foreach (var relation in mappings)
        //    {
        //        if (relation.ContainerID != dbResult.ID)
        //        {
        //            relationSuccess = false;
        //        }
        //        if (!outerIDs.Contains(relation.ID))
        //        {
        //            relationSuccess = false;
        //        }
        //    }

        //    bool mappingFieldSuccess = dbResult.Fields[0].OuterEntityFields.Count == 1;
        //    var fieldRelations = dbResult.Fields[0].AllMembersRelations;
        //    var fieldContainerID = dbResult.Fields[0].ID;
        //    foreach (var relation in fieldRelations)
        //    {
        //        if (relation.ContainerID != fieldContainerID)
        //        {
        //            mappingFieldSuccess = false;
        //        }
        //        if (!outerFieldIDs.Contains(relation.ID))
        //        {
        //            mappingFieldSuccess = false;
        //        }
        //    }

        //    Assert.IsTrue(dbResult.ID.Equals(entity.ID) && mappingSuccess && relationSuccess && mappingFieldSuccess);
        //}

        [TestMethod]
        public void TestDeleteEntityMapping()
        {

            var entity = creatEntity();
            //添加实体
            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);
            //获取实体
            var entityLoad = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

            //外部实体
            var newOutEntity = createOuterEntity();

            List<EntityFieldMapping> entityFieldMappingCollection = new List<EntityFieldMapping>();

            List<string> outerFieldIDs = new List<string>();
            //新建外部属性
            entity.Fields.ForEach(f =>
            {
                var guid = Guid.NewGuid().ToString();
                EntityFieldMapping mapping = new EntityFieldMapping();
                mapping.BuildFromField(f);
                //mapping.OuterFieldID = guid;
                //mapping.OuterFieldName = "Name_" + f.Name;
                outerFieldIDs.Add(guid);
                entityFieldMappingCollection.Add(mapping);
            });

            //mapping对象
            EntityMapping entityMapping = new EntityMapping()
            {
                InnerEntity = entityLoad,
                OuterEntityID = newOutEntity.ID,
                OuterEntityName = newOutEntity.Name,
                EntityFieldMappingCollection = entityFieldMappingCollection
            };

            //mapping入库
            DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(entityMapping);
            //实体
            var resultEntity = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;
            //外部实体
            var resultOEntity = DESchemaObjectAdapter.Instance.Load(newOutEntity.ID) as OuterEntity;

            //删除mapping
            DEObjectOperations.InstanceWithoutPermissions.DeleteEntityMapping(resultEntity, resultOEntity);



            //实体
            var resultEntityL = DESchemaObjectAdapter.Instance.Load(entity.ID, false) as DynamicEntity;

            //外部实体
            var resultOEntityL = DESchemaObjectAdapter.Instance.Load(newOutEntity.ID, false) as OuterEntity;

            //实体与外部结构的mapping是否删除
            bool entityMappingSuccess = true;// !(resultEntityL.OuterEntities.Any(e => e.ID == resultOEntity.ID));
            //外部实体是否删除
            bool outerEntitySuccess = resultOEntityL.Status == DataObjects.Schemas.SchemaProperties.SchemaObjectStatus.Deleted;
            //实体字段mapping是否删除
            bool fieldMapping = true;

            foreach (var f in resultEntityL.Fields)
            {
                fieldMapping = !f.AllMemberOfRelations.Any(r =>
                    outerFieldIDs.Contains(r.ID)
                );
                if (!fieldMapping)
                {
                    break;
                }
            }

            bool outerFieldSuccess = true;
            foreach (var fID in outerFieldIDs)
            {
                var fObj = DESchemaObjectAdapter.Instance.Load(fID, false) as OuterEntityField;
                if (fObj != null)
                {
                    outerFieldSuccess = fObj.Status == SchemaObjectStatus.Deleted;
                }
                if (!outerFieldSuccess)
                {
                    break;
                }
            }

            Assert.IsTrue(entityMappingSuccess && outerEntitySuccess && fieldMapping && outerFieldSuccess, "mapping删除失败");

        }

        [TestMethod]
        public void TestDeleteEntityAndMapping()
        {

            var entity = creatEntity();
            //添加实体
            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);
            //获取实体
            var entityLoad = DESchemaObjectAdapter.Instance.Load(entity.ID) as DynamicEntity;

            //外部实体
            var newOutEntity = createOuterEntity();

            List<EntityFieldMapping> entityFieldMappingCollection = new List<EntityFieldMapping>();

            List<string> outerFieldIDs = new List<string>();
            //新建外部属性
            entity.Fields.ForEach(f =>
            {
                var guid = Guid.NewGuid().ToString();
                EntityFieldMapping mapping = new EntityFieldMapping();
                mapping.BuildFromField(f);
                //mapping.OuterFieldID = guid;
                //mapping.OuterFieldName = "Name_" + f.Name;
                outerFieldIDs.Add(guid);
                entityFieldMappingCollection.Add(mapping);
            });

            //mapping对象
            EntityMapping entityMapping = new EntityMapping()
            {
                InnerEntity = entityLoad,
                OuterEntityID = newOutEntity.ID,
                OuterEntityName = newOutEntity.Name,
                EntityFieldMappingCollection = entityFieldMappingCollection
            };

            //mapping入库
            DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(entityMapping);

            //删除实体
            DEObjectOperations.InstanceWithoutPermissions.DeleteEntity(entity.ID);

            //实体
            var resultEntity = DESchemaObjectAdapter.Instance.Load(entity.ID, false) as DynamicEntity;

            //外部实体
            var resultOEntity = DESchemaObjectAdapter.Instance.Load(newOutEntity.ID, false) as OuterEntity;

            bool entitySuccess = resultEntity.Status == SchemaObjectStatus.Deleted;

            bool oEntitySuccess = resultOEntity.Status == SchemaObjectStatus.Deleted;

            Assert.IsTrue(entitySuccess && oEntitySuccess, "删除mapping失败");
        }

        #region 辅助方法

        /// <summary>
        /// 删除实体跟外部结构关联
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntityMapping(DynamicEntity entity, OuterEntity oEntity)
        {
            var relation = entity.AllMembersRelations.Where(p => p.ID == oEntity.ID).FirstOrDefault();

            if (relation != null)
            {
                //删除实体与外部结构关系
                DEMemberRelationAdapter.Instance.UpdateStatus(relation, SchemaObjectStatus.Deleted);
            }


            //删除实体字段和外部字段的关系
            oEntity.Fields.ForEach(f =>
            {

                var fRelation = f.AllMemberOfRelations.Where(rl => entity.Fields.Any(fd => fd.ID == rl.ContainerID)).FirstOrDefault();

                if (fRelation != null)
                {
                    DEMemberRelationAdapter.Instance.UpdateStatus(fRelation, SchemaObjectStatus.Deleted);
                }

                //删除外部字段
                DESchemaObjectAdapter.Instance.UpdateStatus(f, SchemaObjectStatus.Deleted);

            });

            //删除外部实体
            DESchemaObjectAdapter.Instance.UpdateStatus(oEntity, SchemaObjectStatus.Deleted);

        }


        /// <summary>
        /// 创建实体字段
        /// </summary>
        /// <returns></returns>
        private DynamicEntityField creatEntityField(string flag = "new")
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
        /// 创建外部实体
        /// </summary>
        /// <returns></returns>
        private OuterEntity createOuterEntity(string flag = "new")
        {
            var outerEntity = new OuterEntity()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "外部实体",
                Description = "描述" + flag,
                //CodeName = "OuterEntity",
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
                SortNo = 0
            };
            return outerEntity;
        }

        /// <summary>
        /// 创建外部实体
        /// </summary>
        /// <returns></returns>
        private OuterEntityField createOuterEntityField(string flag = "new")
        {
            var outerEntity = new OuterEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "外部实体字段",
                Description = "描述" + flag,
                //CodeName = "OuterEntityField",
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
            };
            return outerEntity;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <returns></returns>
        private DynamicEntity creatEntity()
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
                //OuterEntities = new OuterEntityCollection(),
                Creator = (IUser)OguBase.CreateWrapperObject(new OguUser("22c3b351-a713-49f2-8f06-6b888a280fff")),//wangli5
                SortNo = 0
            };

            for (var i = 0; i < 1; i++)
            {
                var field = creatEntityField();
                entity.Fields.Add(field);
            }
            return entity;
        }

        #endregion
    }
}
