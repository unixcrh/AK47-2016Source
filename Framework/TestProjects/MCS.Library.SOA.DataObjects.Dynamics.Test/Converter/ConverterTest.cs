using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Mock;
using MCS.Web.Library.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Converter
{
    /// <summary>
    /// ConverterTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ConverterTest
    {
        public ConverterTest()
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

        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            DESchemaObjectAdapter.Instance.ClearAllData();
            DESchemaObjectAdapter.Instance.InitAllData();
        }
        #endregion
        #region 单实体不带字表序列化和反序列化

        [TestCategory("EntitySerialize"), TestMethod]
        [Description("JSON序列化DynamicEntity测试")]
        public void EntitySerialize()
        {
            DynamicEntity sourceEntity = MockData.CreateEntityWithReferenceEntity();
            string json = JSONSerializerExecute.Serialize(sourceEntity);

            sourceEntity.Fields.ForEach(f =>
            {
                Assert.IsTrue(json.Contains(f.Name), string.Format("不能再序列化的JSON中找到属性名{0}", f.Name));
            });
        }

        [TestCategory("EntitySerialize"), TestMethod]
        [Description("JSON序列化DynamicEntityCollection测试")]
        public void EntityCollectionSerialize()
        {
            DynamicEntityCollection sourceEntity = MockData.CreateRelationDynamicEntityCollection();
            string json = JSONSerializerExecute.Serialize(sourceEntity);

            sourceEntity.ForEach(e => e.Fields.ForEach(f =>
            {
                Assert.IsTrue(json.Contains(f.Name), string.Format("不能再序列化的JSON中找到属性名{0}", f.Name));
            }));
        }

        [TestCategory("EntitySerialize"), TestMethod]
        [Description("JSON反序列化DynamicEntity测试")]
        public void EntityDeserializeTest()
        {
            DynamicEntity sourceEntity = MockData.CreateEntityWithReferenceEntity();

            string json = JSONSerializerExecute.Serialize(sourceEntity);

            DynamicEntity targetEntity = JSONSerializerExecute.Deserialize<DynamicEntity>(json);

            Assert.IsNotNull(targetEntity);
            Assert.IsNotNull(targetEntity.Fields);

            Assert.AreEqual(sourceEntity.Fields.Count, targetEntity.Fields.Count);

            for (int i = 0; i < sourceEntity.Fields.Count; i++)
            {
                Assert.AreEqual(sourceEntity.Fields[i].Name, targetEntity.Fields[i].Name);
            }
        }
        #endregion

        #region 单实体带引用性字段的序列化和反序列化
        [TestCategory("EntitySerialize"), TestMethod]
        [Description("JSON序列化DynamicEntity，里面包含若干引用型字段的测试")]
        public void EntityAndChildSerializeTest()
        {
            DynamicEntity entity = MockData.CreateEntityAndChildEntity();
            string json = JSONSerializerExecute.Serialize(entity);

            var fields = entity.Fields.Where(p => p.FieldType == FieldTypeEnum.Collection);

            fields.ForEach(f =>
            {
                Assert.IsTrue(json.Contains(f.CodeName), string.Format("不能再序列化的JSON中找到属性名{0}", f.CodeName));
            });

            Assert.IsTrue(json.Contains(entity.CodeName), string.Format("不能再序列化的JSON中找到属性名{0}", entity.CodeName));
        }

        [TestCategory("EntitySerialize"), TestMethod]
        [Description("JSON反序列化DynamicEntity，里面包含若干引用型字段的测试")]
        public void EntityAndChildDeSerializeTest()
        {
            DynamicEntity entity = MockData.CreateEntityAndChildEntity();

            string json = JSONSerializerExecute.Serialize(entity);
            DynamicEntity deDynamicEntity = JSONSerializerExecute.Deserialize<DynamicEntity>(json);

            Assert.AreEqual(entity.CodeName, deDynamicEntity.CodeName);
            Assert.AreEqual(entity.Fields.Count, deDynamicEntity.Fields.Count);

            IEnumerable<DynamicEntityField> sourceFields = entity.Fields.Where(p => p.FieldType == FieldTypeEnum.Collection);

            sourceFields.ForEach(sf =>
            {
                DynamicEntityField def = deDynamicEntity.Fields[sf.ID];

                Assert.AreEqual(FieldTypeEnum.Collection, def.FieldType);
                Assert.AreEqual(sf.CodeName, def.CodeName);
            });
        }

        #endregion

        /// <summary>
        /// 序列化实体实例
        /// </summary>
        [TestCategory("InstanceSerialize"), TestMethod]
        [Description("不带子成员实例的DyanmicEntity实例序列化测试")]
        public void InstanceSerializeTest()
        {
            DynamicEntity entity = MockData.CreateEntityAndChildEntity();

            DEEntityInstanceBase instance = entity.CreateInstance();

            string json = JSONSerializerExecute.Serialize(instance);

            instance.Fields.ForEach(f =>
            {
                if (f.Definition.FieldType == FieldTypeEnum.Collection)
                {
                    foreach (var item in f.Definition.ReferenceEntity.Fields)
                        Assert.IsTrue(json.Contains(item.Name));
                }
                else
                    Assert.IsTrue(json.Contains(f.Definition.Name));
            });
        }

        //序列化实体实例，带数据
        [TestCategory("InstanceSerialize"), TestMethod]
        [Description("主表加子表实例的序列化测试，子表是包含实例化数据的")]
        public void InstanceSerializeWithDataTest()
        {
            bool flag = true;

            DEEntityInstanceBase instance = MockData.CreateEntityInstance();

            string json = JSONSerializerExecute.Serialize(instance);

            Console.WriteLine(json);

            instance.Fields.ForEach(f =>
            {
                if (f.Definition.FieldType == FieldTypeEnum.Collection)
                {
                    foreach (var item in f.Definition.ReferenceEntity.Fields)
                    {
                        Assert.IsTrue(json.Contains(item.Name));
                        Assert.IsTrue(json.Contains(item.Name + "Value"));
                    }
                }
                else
                {
                    Assert.IsTrue(json.Contains(f.Definition.Name));
                    Assert.IsTrue(json.Contains(f.Definition.Name + "Value"));
                }
            });

            Assert.IsTrue(flag, "序列化实体实例出错");
        }

        //反序列化实体实例
        [TestCategory("InstanceSerialize"), TestMethod]
        [Description("不带子成员实例的DyanmicEntity实例反序列化测试")]
        public void InstanceDeserialize()
        {
            DynamicEntity entity = MockData.CreateEntityAndChildEntity();

            DEEntityInstanceBase sourceInstance = entity.CreateInstance();

            //序列化之后的数据
            string json = JSONSerializerExecute.Serialize(sourceInstance);

            //反序列化
            DEEntityInstanceBase deserializedInstance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(json);

            foreach (var sourceField in sourceInstance.Fields)
            {
                EntityFieldValue deserializedFieldValue = deserializedInstance.Fields.Where(p => p.Definition.Name == sourceField.Definition.Name).FirstOrDefault();

                Assert.IsNotNull(deserializedFieldValue);

                if (sourceField.Definition.FieldType == FieldTypeEnum.Collection)
                    Assert.AreEqual(sourceField.Definition.ReferenceEntityCodeName, deserializedFieldValue.Definition.ReferenceEntityCodeName);
                else
                    Assert.AreEqual(sourceField.StringValue, deserializedFieldValue.StringValue);
            }
        }

        //反序列化实体实例，带数据
        [TestCategory("InstanceSerialize"), TestMethod]
        [Description("主表加子表实例的反序列化测试，子表是包含实例化数据的")]
        public void InstanceDeserializeWithData()
        {
            DEEntityInstanceBase sourceInstance = MockData.CreateEntityInstance();

            string json = JSONSerializerExecute.Serialize(sourceInstance);

            DEEntityInstanceBase deserializedInstance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(json);

            foreach (var sourceField in sourceInstance.Fields)
            {
                EntityFieldValue deserializedField = deserializedInstance.Fields.Where(p => p.Definition.Name == sourceField.Definition.Name).FirstOrDefault();

                Assert.IsNotNull(deserializedField);

                if (sourceField.Definition.FieldType == FieldTypeEnum.Collection)
                {
                    DEEntityInstanceBaseCollection sourceChildren = sourceField.GetRealValue() as DEEntityInstanceBaseCollection;
                    DEEntityInstanceBaseCollection deserializedChildren = deserializedField.GetRealValue() as DEEntityInstanceBaseCollection;

                    foreach (var sourceChild in sourceChildren)
                    {
                        var deserializedChild = deserializedChildren[sourceChild.ID];

                        Assert.IsNotNull(deserializedChild);
                    }
                }
                else
                {
                    Assert.AreEqual(sourceField.StringValue, deserializedField.StringValue);
                }
            }
        }
    }
}
