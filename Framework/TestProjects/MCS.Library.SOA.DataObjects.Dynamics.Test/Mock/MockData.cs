using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Common;
using System;
using System.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Mock
{
    public static class MockData
    {
        public static DynamicEntityCollection CreateRelationDynamicEntityCollection()
        {
            DynamicEntityCollection result = new DynamicEntityCollection();

            var item = CreateDemoItem();
            var header = CreateDemoHeader();

            result.Add(item);
            result.Add(header);

            return result;
        }

        /// <summary>
        /// 创建一个主表加子表的实体定义
        /// </summary>
        /// <returns></returns>
        public static DynamicEntity CreateEntityWithReferenceEntity()
        {
            CreateDemoItem();

            return CreateDemoHeader();
        }

        /// <summary>
        /// 创建一个主表的实例化数据
        /// </summary>
        /// <returns></returns>
        public static DynamicEntity CreateEntityAndChildEntity()
        {
            return CreateDemoHeaderEntity();
        }

        /// <summary>
        /// 创建一个主表加子表的实体定义的实例化数据
        /// </summary>
        /// <returns></returns>
        public static DEEntityInstanceBase CreateEntityInstance()
        {
            //创建Mock实体定义
            DynamicEntity entity = CreateEntityWithReferenceEntity();
            //Mock实体实例
            DEEntityInstanceBase instance = entity.CreateInstance();

            //集合字段类型赋值
            #region
            var children = entity.Fields.Where(p => p.FieldType == FieldTypeEnum.Collection);

            foreach (var child in children)
            {
                DEEntityInstanceBaseCollection entityItems = new DEEntityInstanceBaseCollection();

                //创建两条字表数据
                for (int i = 0; i < 2; i++)
                {
                    DEEntityInstanceBase childInstance = child.ReferenceEntity.CreateInstance();
                    childInstance.ID = Guid.NewGuid().ToString();

                    foreach (var field in childInstance.Fields)
                    {
                        childInstance.Fields.TrySetValue(field.Definition.Name, field.Definition.Name + "Value" + i);
                    }

                    entityItems.Add(childInstance);
                }

                instance.Fields.SetValue(child.Name, entityItems);
            }
            #endregion

            //普通字段赋值
            #region
            instance.Fields.ForEach(p =>
            {
                if (p.Definition.FieldType != FieldTypeEnum.Collection)
                {
                    p.StringValue = p.Definition.Name + "Value";
                }
            });
            #endregion

            return instance;
        }

        /// <summary>
        /// 创建带数据的实体实例
        /// </summary>
        /// <returns></returns>
        public static DEEntityInstanceBase CreateInstanceWithData()
        {
            //准备实体定义
            DynamicEntity header = MockData.CreateEntityWithReferenceEntity();

            //创建空实例
            DEEntityInstanceBase instance = header.CreateInstance();

            instance.Fields.SetValue("总金额", 200);

            DynamicEntityField field = instance.EntityDefine.Fields.FirstOrDefault(p => p.FieldType == FieldTypeEnum.Collection) as DynamicEntityField;

            DEEntityInstanceBaseCollection items = new DEEntityInstanceBaseCollection();
            #region
            //实例1
            DEEntityInstanceBase itemInstance = field.ReferenceEntity.CreateInstance();
            itemInstance.ID = Guid.NewGuid().ToString();
            itemInstance.Name = "第一条数据";
            itemInstance.Fields.TrySetValue("物料名称", "鼠标");
            itemInstance.Fields.TrySetValue("物料数量", 1);
            itemInstance.Fields.TrySetValue("单价", 50);
            items.Add(itemInstance);

            //实例2
            DEEntityInstanceBase itemInstance1 = field.ReferenceEntity.CreateInstance();
            itemInstance1.ID = Guid.NewGuid().ToString();
            itemInstance1.Name = "第二条数据";
            itemInstance1.Fields.TrySetValue("物料名称", "键盘");
            itemInstance1.Fields.TrySetValue("物料数量", 1);
            itemInstance1.Fields.TrySetValue("单价", 150);
            items.Add(itemInstance1);
            #endregion
            instance.Fields.SetValue("销售明细", items);

            DEInstanceAdapter.Instance.Update(instance);

            return instance;
        }

        public static DEEntityInstanceBase CreateInstaceWithAllTypeData()
        {
            return CreatEntityWithAllTypeData().CreateInstance();
        }

        public static DynamicEntity CreatEntityWithAllTypeData()
        {
            DynamicEntity entity = new DynamicEntity();

            entity.ID = Guid.NewGuid().ToString();
            entity.Name = "测试实体";
            entity.Description = "测试实体";
            entity.CategoryID = Define.TestCategoryID;   //集团公司/管道板块/运输
            entity.BuidCodeName();

            entity.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "Bool", FieldType = FieldTypeEnum.Bool, Length = 1, DefaultValue = "true" });
            //entiy.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "Collection", FieldType = FieldTypeEnum.Collection, Length = 1 });
            entity.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "DateTime", FieldType = FieldTypeEnum.DateTime, Length = 10, DefaultValue = "2014-3-3" });
            entity.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "Decimal", FieldType = FieldTypeEnum.Decimal, Length = 10, DefaultValue = "99" });
            entity.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "Int", FieldType = FieldTypeEnum.Int, Length = 10, DefaultValue = "99" });
            entity.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "String", FieldType = FieldTypeEnum.String, Length = 10, DefaultValue = "haoyk" });

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(entity);

            return entity;
        }

        #region
        //创建测试数据
        private static DynamicEntity CreateDemoHeader()
        {
            //添加实体定义
            DynamicEntity saleOrderHeaderDefine = new DynamicEntity();

            saleOrderHeaderDefine.ID = Guid.NewGuid().ToString();
            saleOrderHeaderDefine.Name = "销售单";
            saleOrderHeaderDefine.Description = "销售单";
            saleOrderHeaderDefine.CategoryID = Define.TestCategoryID;   //集团公司/管道板块/运输

            //SaleOrderHeader.SortNo = 0;
            saleOrderHeaderDefine.BuidCodeName();

            saleOrderHeaderDefine.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "总金额", FieldType = FieldTypeEnum.Decimal, Length = 18, SortNo = 0, DefaultValue = "99" });

            DynamicEntityField field = new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                //CodeName = "销售明细",
                Name = "销售明细",
                FieldType = FieldTypeEnum.Collection,
                Length = 999,
                SortNo = 1
            };

            field.ReferenceEntityCodeName = "/集团公司/管道板块/运输/销售单明细";
            saleOrderHeaderDefine.Fields.Add(field);

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(saleOrderHeaderDefine);

            #region Mapping关系
            //抬头对应
            EntityMapping mapping = new EntityMapping();
            mapping.InnerEntity = saleOrderHeaderDefine;
            mapping.OuterEntityID = Guid.NewGuid().ToString();
            mapping.OuterEntityName = "Tcode_test";
            mapping.EntityFieldMappingCollection.ToArray();

            //saleOrderHeaderDefine.Fields.ToArray().Where(p => p.FieldType != FieldTypeEnum.Collection).ToList().ForEach(f =>
            //{
            //    var outerField = mapping.EntityFieldMappingCollection.FirstOrDefault(p => p.FieldName.Equals(f.Name));
            //    outerField.OuterFieldID = Guid.NewGuid().ToString();
            //    outerField.OuterFieldName = f.Name + "_Mapping";
            //});

            //var collfield = mapping.EntityFieldMappingCollection.FirstOrDefault(p => p.FieldTypeName == FieldTypeEnum.Collection.ToString());
            //collfield.OuterFieldID = Guid.NewGuid().ToString();
            //collfield.OuterFieldName = "Tcode_test_Item";

            DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(mapping);
            #endregion

            return saleOrderHeaderDefine;
        }

        /// <summary>
        /// 创建主表测试数据，无子表
        /// </summary>
        /// <returns></returns>
        private static DynamicEntity CreateDemoHeaderEntity()
        {
            DynamicEntity SaleOrderItem = new DynamicEntity();
            SaleOrderItem.ID = Guid.NewGuid().ToString();
            SaleOrderItem.Name = "销售单明细";
            SaleOrderItem.Description = "销售单明细";
            SaleOrderItem.CategoryID = Define.TestCategoryID;   //集团公司/管道板块/运输
            SaleOrderItem.BuidCodeName();
            SaleOrderItem.SortNo = 1;
            SaleOrderItem.CreateDate = DateTime.Now;

            DynamicEntity saleOrderEntityItem1 = CreateDemoItemEntity("第一个");
            DynamicEntity saleOrderEntityItem2 = CreateDemoItemEntity("第二个");

            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "物料名称",
                FieldType = FieldTypeEnum.String,
                Length = 255,
                SortNo = 0,
                CreateDate = SaleOrderItem.CreateDate
            });
            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "物料数量",
                FieldType = FieldTypeEnum.Int,
                Length = 4,
                SortNo = 1
            });
            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "单价",
                FieldType = FieldTypeEnum.Decimal,
                Length = 18,
                SortNo = 2
            });

            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "销售订单字表1",
                FieldType = FieldTypeEnum.Collection,
                ReferenceEntityCodeName = saleOrderEntityItem1.CodeName,
                Length = 50,
                SortNo = 3
            });

            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "销售订单字表2",
                FieldType = FieldTypeEnum.Collection,
                ReferenceEntityCodeName = saleOrderEntityItem2.CodeName,
                Length = 18,
                SortNo = 4
            });

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(SaleOrderItem);

            return SaleOrderItem;
        }

        //创建字表测试数据
        private static DynamicEntity CreateDemoItemEntity(string strlenth)
        {
            DynamicEntity SaleOrderItem = new DynamicEntity();
            SaleOrderItem.ID = Guid.NewGuid().ToString();
            SaleOrderItem.Name = "销售单明细字表" + strlenth;
            SaleOrderItem.Description = "销售单明细字表" + strlenth;
            SaleOrderItem.CategoryID = Define.TestCategoryID;   //集团公司/管道板块/运输
            SaleOrderItem.BuidCodeName();
            SaleOrderItem.SortNo = 1;
            SaleOrderItem.CreateDate = DateTime.Now;

            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "物料名称",
                FieldType = FieldTypeEnum.String,
                Length = 255,
                SortNo = 0
            });
            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "物料数量",
                FieldType = FieldTypeEnum.Int,
                Length = 4,
                SortNo = 1,
                DefaultValue = "1"
            });
            SaleOrderItem.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "单价",
                FieldType = FieldTypeEnum.Decimal,
                Length = 18,
                SortNo = 2,
                DefaultValue = "99"
            });

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(SaleOrderItem);
            return SaleOrderItem;
        }

        //创建测试数据
        private static DynamicEntity CreateDemoItem()
        {
            DynamicEntity saleOrderItemDefine = new DynamicEntity();

            saleOrderItemDefine.ID = Guid.NewGuid().ToString();
            saleOrderItemDefine.Name = "销售单明细";
            saleOrderItemDefine.Description = "销售单明细";
            saleOrderItemDefine.CategoryID = Define.TestCategoryID;   //集团公司/管道板块/运输
            saleOrderItemDefine.BuidCodeName();
            //SaleOrderItem.SortNo = 1;
            saleOrderItemDefine.CreateDate = DateTime.Now;

            saleOrderItemDefine.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "物料名称", FieldType = FieldTypeEnum.String, Length = 255, SortNo = 0, DefaultValue = "测试默认值" });
            saleOrderItemDefine.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "物料数量", FieldType = FieldTypeEnum.Int, Length = 4, SortNo = 1 });
            saleOrderItemDefine.Fields.Add(new DynamicEntityField() { ID = Guid.NewGuid().ToString(), Name = "单价", FieldType = FieldTypeEnum.Decimal, Length = 18, SortNo = 2 });

            DEObjectOperations.InstanceWithoutPermissions.AddEntity(saleOrderItemDefine);

            #region Mapping关系
            //明细对应
            EntityMapping mapping = new EntityMapping();
            mapping.InnerEntity = saleOrderItemDefine;
            mapping.OuterEntityID = Guid.NewGuid().ToString();
            mapping.OuterEntityName = "Tcode_test_Item";
            mapping.EntityFieldMappingCollection.ToArray();

            //saleOrderItemDefine.Fields.ForEach(f =>
            //{
            //    var outerField = mapping.EntityFieldMappingCollection.FirstOrDefault(p => p.FieldName.Equals(f.Name));

            //    outerField.OuterFieldID = Guid.NewGuid().ToString();
            //    outerField.OuterFieldName = f.Name + "_Mapping";
            //});

            DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(mapping);
            #endregion

            return saleOrderItemDefine;
        }

        #endregion
    }
}
