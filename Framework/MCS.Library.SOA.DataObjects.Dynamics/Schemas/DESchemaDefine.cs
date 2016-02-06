using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics.Permissions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Schemas
{
    [Serializable]
    [XElementSerializable]
    [ORTableMapping("DE.SchemaDefine")]
    public class DESchemaDefine : SchemaDefineBase
    {
        private DESchemaOperationDefineCollection _Operations = null;
        private DEAclPermissionItemCollection _PermissionSet = null;

        public DESchemaDefine()
        {
            
        }

        public DESchemaDefine(ObjectSchemaConfigurationElement schemaConfig)
        {
            schemaConfig.NullCheck("schemaConfig");

            InitFromConfigurationElement(schemaConfig);
        }

        protected override void InitFromConfigurationElement(ObjectSchemaConfigurationElementBase schemaConfig)
        {
            base.InitFromConfigurationElement(schemaConfig);

            ObjectSchemaConfigurationElement configElem = (ObjectSchemaConfigurationElement)schemaConfig;
            this.CodeNameKey = configElem.CodeNameKey;
            this.CodeNameValidationMethod = configElem.CodeNameValidationMethod;
            this.FullPathValidationMethod = configElem.FullPathValidationMethod;

            this.IsUsersContainer = configElem.IsUsersContainer;
            this.IsUsersContainerMember = configElem.IsUsersContainerMember;
            this.ToSchemaObjectSnapshot = configElem.ToSchemaObjectSnapshot;

            SchemaPropertyGroupSettings groupSettings = SchemaPropertyGroupSettings.GetConfig();

            foreach (ObjectSchemaClassConfigurationElement schemaClass in configElem.Groups)
            {
                if (groupSettings.Groups.ContainsKey(schemaClass.GroupName))
                {
                    this.Properties.AppendPropertiesFromConfiguration(groupSettings.Groups[schemaClass.GroupName].AllProperties);
                }
            }

            this.Operations.LoadFromConfiguration(configElem.Operations);

            this.PermissionSet.LoadFromConfiguration(configElem.PermissionSet);
        }

        /// <summary>
        /// 使用指定的<see cref="DESchemaDefine"/>初始化<see cref="SchemaPropertyValueCollection"/>的新实例
        /// </summary>
        /// <returns></returns>
        public SchemaPropertyValueCollection ToProperties()
        {
            SchemaPropertyValueCollection properties = new SchemaPropertyValueCollection();

            this.Properties.ForEach(pd => properties.Add(new SchemaPropertyValue(pd)));

            return properties;
        }

        /// <summary>
        /// 根据schemaType构造DESchemaDefine
        /// </summary>
        /// <param name="schemaType"></param>
        /// <returns></returns>
        public static DESchemaDefine Create(string schemaType)
        {
            ObjectSchemaSettings settings = ObjectSchemaSettings.GetConfig();

            settings.Schemas.ContainsKey(schemaType).FalseThrow("不能找到{0}的SchemaType的定义", schemaType);

            return new DESchemaDefine(settings.Schemas[schemaType]);
        }

        /// <summary>
        /// 从Cache中获取DESchemaDefine，如果Cache中不存在则创建一个
        /// </summary>
        /// <param name="schemaType"></param>
        /// <returns></returns>
        public static DESchemaDefine GetSchema(string schemaType)
        {
            return (DESchemaDefine)SchemaDefineCache.Instance.GetOrAddNewValue(schemaType, (cache, key) =>
            {
                DESchemaDefine result = DESchemaDefine.Create(schemaType);
                cache.Add(schemaType, result);

                return result;
            });
        }

        /// <summary>
        /// 得到SchemaType对应的配置信息
        /// </summary>
        /// <param name="schemaType"></param>
        /// <returns></returns>
        public static ObjectSchemaConfigurationElement GetSchemaConfig(string schemaType)
        {
            return ObjectSchemaSettings.GetConfig().Schemas[schemaType];
        }

        /// <summary>
        /// CodeName的分组的Key，同一Key的对象CodeName才是唯一的
        /// </summary>
        public string CodeNameKey { get; set; }

        public SchemaObjectCodeNameValidationMethod CodeNameValidationMethod { get; set; }
        public SCRelationFullPathValidationMethod FullPathValidationMethod { get; set; }


        /// <summary>
        /// 是不是群组或角色之类的人员的容器
        /// </summary>
        public bool IsUsersContainer { get; set; }

        /// <summary>
        /// 是不是群组或角色之类的人员的容器的成员。通常人员会设置为true
        /// </summary>
        public bool IsUsersContainerMember { get; set; }

        [NoMapping]
        public DESchemaOperationDefineCollection Operations
        {
            get
            {
                if (this._Operations == null)
                    this._Operations = new DESchemaOperationDefineCollection();

                return this._Operations;
            }
        }

        [NoMapping]
        public DEAclPermissionItemCollection PermissionSet
        {
            get
            {
                if (this._PermissionSet == null)
                    this._PermissionSet = new DEAclPermissionItemCollection();

                return this._PermissionSet;
            }
        }
    }
}
