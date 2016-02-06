using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Actions;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Locks;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Permissions;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Actions;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Security.Executors;
using System.Transactions;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects.Dynamics.Contract;

namespace MCS.Library.SOA.DataObjects.Dynamics.Executors
{
    public class DEObjectOperations : IDEObjectOperations
    {
        #region
        private bool _NeedCheckPermissions = false;
        private bool _NeedExecuteActions = false;
        private bool _NeedValidationAndStatusCheck = true;
        private bool _AddLock = false;

        /// <summary>
        /// 表示<see cref="DEObjectOperations"/>的实例，此字段为只读
        /// </summary>
        public static readonly IDEObjectOperations Instance = new DEObjectOperations();

        /// <summary>
        /// 没有权限和状态检查的实例
        /// </summary>
        public static readonly IDEObjectOperations InstanceWithoutValidationAndStatusCheck = new DEObjectOperations() { NeedValidationAndStatusCheck = false };

        /// <summary>
        /// 带权限检查的Operation的实例
        /// </summary>
        public static readonly IDEObjectOperations InstanceWithPermissions = new DEObjectOperations(true);

        /// <summary>
        /// 不带权限检查的Operation的实例
        /// </summary>
        public static readonly IDEObjectOperations InstanceWithoutPermissions = new DEObjectOperations(false);

        /// <summary>
        /// 不带权限检查和锁检查的实例
        /// </summary>
        public static readonly IDEObjectOperations InstanceWithoutPermissionsAndLockCheck = new DEObjectOperations(false) { AddLock = false };

        private DEObjectOperations()
        {
        }

        private DEObjectOperations(bool needCheckPermissions)
        {
            this._NeedCheckPermissions = needCheckPermissions;
            this._NeedExecuteActions = true;
            this._AddLock = needCheckPermissions && DELockSettings.GetConfig().Enabled;
        }

        #endregion

        #region Properties
        public bool AddLock
        {
            get
            {
                return this._AddLock;
            }
            set
            {
                this._AddLock = value;
            }
        }

        /// <summary>
        /// 是否需要执行Operation后的Action
        /// </summary>
        public bool NeedExecuteActions
        {
            get
            {
                return this._NeedExecuteActions;
            }
            set
            {
                this._NeedExecuteActions = value;
            }
        }

        /// <summary>
        /// 是否需要执行校验和状态检查
        /// </summary>
        public bool NeedValidationAndStatusCheck
        {
            get
            {
                return this._NeedValidationAndStatusCheck;
            }
            set
            {
                this._NeedValidationAndStatusCheck = value;
            }
        }
        #endregion

        #region Action Wrapper
        private void ExecuteWithActions(DEOperationType operationType, Action action)
        {
            DEDataOperationLockContext.Current.DoAddLockAction(this._AddLock, EnumItemDescriptionAttribute.GetDescription(operationType), () =>
            {
                if (this._NeedExecuteActions && action != null)
                {
                    DEObjectOperationActionCollection actions = DEObjectOperationActionSettings.GetConfig().GetActions();

                    actions.BeforeExecute(operationType);

                    action();

                    actions.AfterExecute(operationType);
                }
                else
                    action();
            });
        }
        #endregion

        #region Methods

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(DynamicEntity entity)
        {
            DEObjectExecutor executor = null;
            entity.NullCheck<ArgumentNullException>("待添加的实体不能为Null");

            if (this._NeedCheckPermissions)
                CheckPermissions(DEOperationType.AddEntity, null, "AddEntity", string.Empty);

            //生成CodeName
            entity.BuidCodeName();

            //验证实体数据
            entity.Validate();

            executor = new DEMemberCollectionRelativeExecutor(DEOperationType.AddEntity, entity, entity.Fields.ToSchemaObjects()) { SaveContainerData = true, NeedValidation = this.NeedValidationAndStatusCheck };

            ExecuteWithActions(DEOperationType.AddEntity, () => SCActionContext.Current.DoActions(() => executor.Execute()));

        }

        #region 注释掉添加实体映射方法，不需要外部实体。by 王雷平 2015-8-13
        public void AddEntityMapping(EntityMapping entityMapping)
        { }

        ///// <summary>
        ///// 添加实体映射(新)
        ///// </summary>
        //public void AddEntityMapping(EntityMapping entityMapping)
        //{
        //    DEObjectExecutor executor = null;

        //    if (this._NeedCheckPermissions)
        //        CheckPermissions(DEOperationType.AddEntityMapping, null, "AddEntityMapping", string.Empty);

        //    OuterEntity outerEntity = new OuterEntity();
        //    OuterEntityFieldCollection outerEntityFields = new OuterEntityFieldCollection();

        //    using (TransactionScope scope = TransactionScopeFactory.Create())
        //    {
        //        #region [实体与外部实体映射][外部实体]入库
        //        if (entityMapping.InnerEntity.OuterEntities.Any(p => p.ID.Equals(entityMapping.OuterEntityID)))
        //        {
        //            var outentity = entityMapping.InnerEntity.OuterEntities.FirstOrDefault(p => p.ID.Equals(entityMapping.OuterEntityID));
        //            outentity.Name = entityMapping.OuterEntityName;
        //            outentity.CustomType = entityMapping.OuterEntityInType;
        //            outerEntity = outentity;
        //        }
        //        else
        //        {
        //            outerEntity = new OuterEntity() { ID = entityMapping.OuterEntityID, Name = entityMapping.OuterEntityName, CustomType = entityMapping.OuterEntityInType };
        //            entityMapping.InnerEntity.OuterEntities.Add(outerEntity);
        //        }

        //        executor = new DEMemberCollectionRelativeExecutor
        //        (
        //            DEOperationType.AddEntityMapping,
        //            entityMapping.InnerEntity,
        //            entityMapping.InnerEntity.OuterEntities.ToSchemaObjects(),
        //            DEStandardObjectSchemaType.DynamicEntityMapping
        //        ) { SaveContainerData = false, NeedValidation = this.NeedValidationAndStatusCheck };

        //        ExecuteWithActions(DEOperationType.AddEntityMapping, () => SCActionContext.Current.DoActions(() => executor.Execute()));
        //        #endregion

        //        #region 实体字段与外部实体字段映射入库
        //        entityMapping.EntityFieldMappingCollection.Where(field => field.OuterFieldName.IsNotEmpty()).ForEach(field =>
        //        {
        //            DynamicEntityField container = DESchemaObjectAdapter.Instance.Load(field.FieldID) as DynamicEntityField;

        //            var outerFiled = container.OuterEntityFields.FirstOrDefault(of => of.OuterEntity.ID.Equals(entityMapping.OuterEntityID));//&& of.ID.Equals(field.OuterFieldID)
        //            if (outerFiled != null)
        //            {
        //                outerFiled.Name = field.OuterFieldName;
        //                outerEntityFields.Add(outerFiled);
        //            }
        //            else
        //            {
        //                OuterEntityField new_outerField = new OuterEntityField() { ID = string.IsNullOrEmpty(field.OuterFieldID) ? Guid.NewGuid().ToString() : field.OuterFieldID, Name = field.OuterFieldName };
        //                container.OuterEntityFields.Add(new_outerField);

        //                outerEntityFields.Add(new_outerField);
        //            }

        //            executor = new DEMemberCollectionRelativeExecutor(DEOperationType.AddEntityFieldMapping, container, container.OuterEntityFields.ToSchemaObjects(), DEStandardObjectSchemaType.DynamicEntityFieldMapping) { SaveContainerData = false, NeedValidation = this.NeedValidationAndStatusCheck };

        //            ExecuteWithActions(DEOperationType.AddEntityFieldMapping, () => SCActionContext.Current.DoActions(() => executor.Execute()));
        //        });
        //        #endregion

        //        #region 外部实体与外部实体属性关系入库
        //        executor = new DEMemberCollectionRelativeExecutor(DEOperationType.AddOuterEntityFieldMapping, outerEntity, outerEntityFields.ToSchemaObjects(), DEStandardObjectSchemaType.OuterEntityFieldMapping) { SaveContainerData = false, SaveMemberData = false, NeedValidation = this.NeedValidationAndStatusCheck };

        //        ExecuteWithActions(DEOperationType.AddOuterEntityFieldMapping, () => SCActionContext.Current.DoActions(() => executor.Execute()));
        //        #endregion
        //        scope.Complete();

        //    }
        //}

        #endregion

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntity(DynamicEntity entity)
        {
            entity.Status = SchemaObjectStatus.Deleted;
            entity.Fields.ForEach(p => p.Status = SchemaObjectStatus.DeletedByContainer);

            // 查询出引用实体地址的字段 然后删除该字段和mapping 关系
            //WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            //builder.AppendItem("SchemaType", "DynamicEntityField");
            //builder.AppendItem("CodeName",entity.CodeName);
            //DESchemaObjectAdapter.Instance.Load(builder);
            // DeleteEntityField(DynamicEntityField entityField)

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                //删除映射关系 注释掉。2015-8-13 王雷平
                //DeleteEntityMapping(entity, entity.OuterEntities);

                DEObjectExecutor executor = null;

                if (this._NeedCheckPermissions)
                    CheckPermissions(DEOperationType.DeleteEntity, null, "DeleteEntity", string.Empty);

                executor = new DEMemberCollectionRelativeExecutor(DEOperationType.DeleteEntity, entity, entity.Fields.ToSchemaObjects()) { SaveContainerData = true, NeedValidation = this.NeedValidationAndStatusCheck };

                ExecuteWithActions(DEOperationType.DeleteEntity, () => SCActionContext.Current.DoActions(() => executor.Execute()));

                scope.Complete();
            }

        }

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

                //外部实体与外部实体字段之间的关系
                var fRelationOuterEntity = f.AllMemberOfRelations.Where(rl => oEntity.ID == rl.ContainerID).FirstOrDefault();
                if (fRelationOuterEntity != null)
                {
                    DEMemberRelationAdapter.Instance.UpdateStatus(fRelationOuterEntity, SchemaObjectStatus.Deleted);
                }

                //删除外部字段
                DESchemaObjectAdapter.Instance.UpdateStatus(f, SchemaObjectStatus.Deleted);
            });

            //删除外部实体
            DESchemaObjectAdapter.Instance.UpdateStatus(oEntity, SchemaObjectStatus.Deleted);
        }

        /// <summary>
        /// 删除实体跟外部结构关联
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntityMapping(DynamicEntity entity, OuterEntityCollection oEntities)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                oEntities.ForEach(oEntity =>
                {
                    DeleteEntityMapping(entity, oEntity);
                });
                scope.Complete();
            }

        }

        /// <summary>
        /// 删除实体跟外部结构关联
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntityMapping(string entityID, params string[] outerEntityIDs)
        {
            var entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                outerEntityIDs.ForEach(id =>
                {
                    var oEntity = DESchemaObjectAdapter.Instance.Load(id) as OuterEntity;
                    DeleteEntityMapping(entity, oEntity);
                });
                scope.Complete();
            }

        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntity(params string[] ids)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                ids.ForEach(id =>
                {
                    var entity = DESchemaObjectAdapter.Instance.Load(id);
                    if (entity != null)
                    {
                        DeleteEntity(entity as DynamicEntity);
                    }
                });
                scope.Complete();
            }

        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateEntity(DynamicEntity entity)
        {
            DEObjectExecutor executor = null;

            if (this._NeedCheckPermissions)
                CheckPermissions(DEOperationType.UpdateEntity, null, "UpdateEntity", string.Empty);

            executor = new DEMemberCollectionRelativeExecutor(DEOperationType.UpdateEntity, entity, entity.Fields.ToSchemaObjects()) { SaveContainerData = true, NeedValidation = this.NeedValidationAndStatusCheck };

            ExecuteWithActions(DEOperationType.UpdateEntity, () => SCActionContext.Current.DoActions(() => executor.Execute()));

            //DeleteEntityMapping(entity, entity.OuterEntities);
        }

        /// <summary>
        /// 添加实体属性
        /// </summary>
        /// <param name="entityField"></param>
        public void AddEntityField(DynamicEntityField entityField)
        {
            DEObjectExecutor executor = null;

            if (this._NeedCheckPermissions)
                CheckPermissions(DEOperationType.AddEntityField, null, "AddEntityField", string.Empty);

            executor = new DEObjectExecutor(DEOperationType.AddEntityField, entityField);
            ExecuteWithActions(DEOperationType.AddEntityField, () => SCActionContext.Current.DoActions(() => executor.Execute()));
        }

        /// <summary>
        /// 删除实体属性
        /// </summary>
        /// <param name="entityField"></param>
        public void DeleteEntityField(DynamicEntityField entityField)
        {
            entityField.Status = SchemaObjectStatus.Deleted;
            DEObjectExecutor executor = null;

            if (this._NeedCheckPermissions)
                CheckPermissions(DEOperationType.DeleteEntityField, null, "DeleteEntityField", string.Empty);

            executor = new DEObjectExecutor(DEOperationType.DeleteEntityField, entityField);
            ExecuteWithActions(DEOperationType.DeleteEntityField, () => SCActionContext.Current.DoActions(() => executor.Execute()));
        }

        /// <summary>
        /// 更新实体属性
        /// </summary>
        /// <param name="entityField"></param>
        public void UpdateEntityField(DynamicEntityField entityField)
        {
            DEObjectExecutor executor = null;

            if (this._NeedCheckPermissions)
                CheckPermissions(DEOperationType.UpdateEntityField, null, "UpdateEntityField", string.Empty);

            executor = new DEObjectExecutor(DEOperationType.UpdateEntityField, entityField);
            ExecuteWithActions(DEOperationType.UpdateEntityField, () => SCActionContext.Current.DoActions(() => executor.Execute()));
        }

        /// <summary>
        /// 根据录屏结果生成实体及映射
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <param name="recordCollection">录屏集合</param>
        public string RecordResultGenerate(string categoryID, RecordResultCollection recordCollection)
        {
            categoryID.CheckStringIsNullOrEmpty<ArgumentNullException>("categoryID");
            recordCollection.NullCheck<ArgumentNullException>("录屏集合不能为空!");

            string masterEntityID = string.Empty;

            #region 验证
            //只验证主表，子表可以为空
            recordCollection.Any().FalseThrow("没有找到信息项，请重新填写!");

            //验证CodeName唯一性
            recordCollection.Select(p => p.EntityName).Distinct().ForEach(p =>
                DESchemaObjectAdapter.Instance.CheckCodeNameExist(categoryID, p).TrueThrow(string.Format("已存在同名[{0}]", p)));
            #endregion

            List<string> listFullName = recordCollection.Select(p => p.TempFullPath).Distinct().ToList();

            //按长度倒序排序，确保从最子级节点开始添加实体
            listFullName = listFullName.OrderByDescending(p => p.Split('/').Length).ToList();

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                listFullName.ForEach(fullName =>
                {
                    //准备数据
                    var entityRecordList = recordCollection.Where(r => r.TempFullPath.Equals(fullName));
                    var entityResultColl = new RecordResultCollection();
                    entityResultColl.CopyFrom(entityRecordList);

                    //实体入库
                    var childEntity = entityResultColl.BuildEntity(categoryID);
                    DEObjectOperations.InstanceWithoutPermissions.DoOperation(SCObjectOperationMode.Add, childEntity, null);

                    //为父实体指定“引用实体”属性值
                    if (fullName.Contains("/"))
                    {
                        string parentFullName = fullName.Substring(0, fullName.LastIndexOf("/"));
                        string parentFieldName = fullName.Replace(parentFullName + "/", "");
                        recordCollection.First(pe => pe.TempFullPath.Equals(parentFullName) && pe.FieldName.Equals(parentFieldName)).ReferenceEntityCodeName =
                            childEntity.CodeName;
                    }

                    //映射入库
                    EntityMapping childMapping = BuildEntityMapping(childEntity);
                    DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(childMapping);

                    //最后一个被添加的实体为主实体
                    masterEntityID = childEntity.ID;
                });

                scope.Complete();

                #region 以前方法，先注释掉
                ///////////////////////////////////////////////////////////////////////////////////////
                //#region 实体定义
                ////先入子表
                //recordCollection.Where(p => !p.IsMasterTable).GroupBy(p => p.EntityName).ToList().ForEach(p =>
                //{
                //    RecordResultCollection child = new RecordResultCollection();
                //    child.CopyFrom(p.ToList());

                //    var childEntity = child.BuildEntity(categoryID);
                //    DEObjectOperations.InstanceWithoutPermissions.DoOperation(SCObjectOperationMode.Add, childEntity, null);

                //    //添加子类引用
                //    recordCollection.Add(new Dynamics.Objects.RecordResult()
                //    {
                //        EntityName = recordCollection.FirstOrDefault(m => m.IsMasterTable).EntityName,
                //        EntityDesc = recordCollection.FirstOrDefault(m => m.IsMasterTable).EntityName,
                //        IsMasterTable = true,
                //        FieldName = p.Key,
                //        FieldType = FieldTypeEnum.Collection,
                //        ReferenceEntityCodeName = childEntity.CodeName,
                //        FieldDesc = p.Key,
                //        FieldLength = 99999,
                //        SortNo = recordCollection.Count(m => m.IsMasterTable) + 1,
                //        IsStruct = p.All(f => f.EntityDesc == "STRUCTURE")
                //    });
                //});

                //// 主表
                //RecordResultCollection master = new RecordResultCollection();
                //master.CopyFrom(recordCollection.Where(p => p.IsMasterTable).ToList());
                //var masterEntity = master.BuildEntity(categoryID);

                //DEObjectOperations.InstanceWithoutPermissions.DoOperation(SCObjectOperationMode.Add, masterEntity, null);
                //#endregion

                //// 主表映射
                //EntityMapping masterMapping = BuildEntityMapping(masterEntity);
                //DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(masterMapping);

                ////子表映射
                //masterEntity.Fields.Where(p => p.FieldType == FieldTypeEnum.Collection).ForEach(f =>
                //{
                //    DynamicEntity childEntity = DEDynamicEntityAdapter.Instance.LoadByCodeName(f.ReferenceEntityCodeName) as DynamicEntity;
                //    EntityMapping childMapping = BuildEntityMapping(childEntity);

                //    DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(childMapping);
                //});

                //masterEntityID = masterEntity.ID;
                //scope.Complete();
                #endregion
            }

            return masterEntityID;
        }

        /// <summary>
        /// 复制多个实体到多个类别下
        /// </summary>
        /// <param name="entitiesIDs">实体的id集合</param>
        /// <param name="categories">目标类别id集合</param>
        public void CopyEntities(List<string> entitiesIDs, List<string> categories)
        {
            //筛选出有主子关系的id
            string error = string.Empty;
            List<string> copyIDs = new List<string>();
            copyIDs.AddRange(entitiesIDs);

            List<string> mainIDs = new List<string>();

            #region 删除带有主表的 子表id
            foreach (var entityID in entitiesIDs)
            {
                if (!copyIDs.Contains(entityID))
                {
                    continue;
                }

                var entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
                var childs = entity.Fields.Where(p => p.FieldType == FieldTypeEnum.Collection);
                if (childs.Count() > 0)
                {
                    mainIDs.Add(entityID);
                    foreach (var item in childs)
                    {
                        copyIDs.Remove(item.ReferenceEntity.ID);
                    }
                }
            }

            #endregion

            #region 循环类别，向类别中复制实体

            foreach (var category in categories)
            {
                //获取当前类别
                //var currentCategory = CategoryAdapter.Instance.GetByID(category);
                foreach (var entityID in copyIDs)
                {
                    try
                    {
                        using (TransactionScope scope = TransactionScopeFactory.Create())
                        {
                            //复制操作
                            CopyChildEntity(entityID, category);

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        error += e.Message + "\r\n";
                    }
                }

            }
            #endregion
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
        }
        /// <summary>
        /// 复制一个实体
        /// </summary>
        /// <param name="entityID">实体ID</param>
        /// <param name="category">类别ID</param>
        /// <returns></returns>
        private string CopyChildEntity(string entityID, string category)
        {
            string codeName = string.Empty;

            #region 实体创建新实体及其子实体，。
            //待复制的实体
            DynamicEntity oldEntity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            //要复制的实体
            DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            List<FieldIDMapping> listMappingField = new List<FieldIDMapping>();
            //复制指标实体
            foreach (var item in entity.Fields)
            {
                if (item.FieldType == FieldTypeEnum.Collection)
                {
                    DynamicEntity childEntity = DEDynamicEntityAdapter.Instance.LoadByCodeName(item.ReferenceEntityCodeName) as DynamicEntity;
                    item.ReferenceEntityCodeName = CopyChildEntity(childEntity.ID, category);
                }
            }
            //从新NEW当前实体及其子表的ID和VersionTime，从而创建一个新的实体
            entity.BuildNewEntity(category);
            #endregion

            //新实体入库
            this.AddEntity(entity);
            codeName = entity.CodeName;
            return codeName;
        }

        #region 注释掉原有的CopyChildEntity方法，不需要复制外部实体及其关系。By 王雷平 2013-8-13
        ///// <summary>
        ///// 复制一个实体
        ///// </summary>
        ///// <param name="entityID">实体ID</param>
        ///// <param name="category">类别ID</param>
        ///// <returns></returns>
        //private string CopyChildEntity(string entityID, string category)
        //{
        //    string codeName = string.Empty;

        //    #region 实体创建新实体及其子实体，。
        //    //待复制的实体
        //    DynamicEntity oldEntity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
        //    //要复制的实体
        //    DynamicEntity entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
        //    List<FieldIDMapping> listMappingField = new List<FieldIDMapping>();
        //    //记录外部实体
        //    List<OuterEntity> outers = new List<OuterEntity>();
        //    List<OuterEntity> oldOuters = new List<OuterEntity>();
        //    //记录字段与外部字段的mapping
        //    foreach (var item in entity.OuterEntities)
        //    {
        //        outers.Add(item);
        //    }

        //    //记录字段与外部字段的mapping
        //    foreach (var item in oldEntity.OuterEntities)
        //    {
        //        oldOuters.Add(item);
        //    }

        //    //复制指标实体
        //    foreach (var item in entity.Fields)
        //    {
        //        if (item.FieldType == FieldTypeEnum.Collection)
        //        {
        //            DynamicEntity childEntity = DEDynamicEntityAdapter.Instance.LoadByCodeName(item.ReferenceEntityCodeName) as DynamicEntity;
        //            item.ReferenceEntityCodeName = CopyChildEntity(childEntity.ID, category);
        //        }
        //    }
        //    //从新NEW当前实体及其子表的ID和VersionTime，从而创建一个新的实体
        //    entity.BuildNewEntity(category);

        //    #endregion

        //    #region 复制外部实体和Mapping
        //    foreach (OuterEntity outerEntity in outers)
        //    {
        //        //构建新的外部实体
        //        outerEntity.BuildNewEntity();
        //        //构建新的实体映射关系
        //        EntityMapping entityMapping = new EntityMapping()
        //        {
        //            InnerEntity = entity,
        //            OuterEntityID = outerEntity.ID,
        //            OuterEntityName = outerEntity.Name,
        //            OuterEntityInType = outerEntity.CustomType,
        //            EntityFieldMappingCollection = new List<EntityFieldMapping>()
        //        };
        //        //循环新实体中每个字段，如果有外部实体字段则创建实体和字段映射关系
        //        foreach (DynamicEntityField newField in entity.Fields)
        //        {
        //            if (string.IsNullOrEmpty(newField.ReferenceEntityCodeName) || 
        //                newField.OuterEntityFields == null || 
        //                newField.OuterEntityFields.Count==0)
        //            {
        //                continue;
        //            }

        //            OuterEntityField newOutField = newField.OuterEntityFields[0];
        //            newOutField.ID = Guid.NewGuid().ToString();
        //            //构建新的字段映射关系
        //            EntityFieldMapping fieldMapping = new EntityFieldMapping()
        //            {
        //                FieldID = newField.ID,
        //                FieldDefaultValue = newField.DefaultValue,
        //                FieldDesc = newField.Description,
        //                FieldLength = newField.Length,
        //                FieldName = newField.Name,
        //                FieldTypeName = newField.FieldType.ToString(),
        //                //OuterFieldID = newOutField.ID,
        //                //OuterFieldName = newOutField.Name,
        //                SortNo = newField.SortNo
        //            };

        //            entityMapping.EntityFieldMappingCollection.Add(fieldMapping);
        //        }
        //        this.AddEntityMapping(entityMapping);

        //    }
        //    #endregion

        //    #region 复制外部实体
        //    /*
        //    foreach (OuterEntity outerEntity in outers)
        //    {
        //        //旧的外部实体
        //        OuterEntity oldOuterEntity = oldOuters.Where(p => p.Name == outerEntity.Name).FirstOrDefault();
        //        if (oldOuterEntity != null && oldOuterEntity.Fields != null)
        //        {
        //            //构建新的外部实体
        //            outerEntity.BuildNewEntity();
        //            //字段之间的mapping入库
        //            foreach (var oldField in oldEntity.Fields)
        //            {
        //                foreach (var item in oldOuterEntity.Fields)
        //                {
        //                    var mappingField = oldField.OuterEntityFields.Where(p => p.ID == item.ID).FirstOrDefault();
        //                    if (mappingField != null)
        //                    {

        //                        var newField = entity.Fields.Where(p => p.ID == oldField.ID).FirstOrDefault();
        //                        var newOuterField = outerEntity.Fields.Where(p => p.Name == mappingField.Name).FirstOrDefault();
        //                        FieldIDMapping mapping = new FieldIDMapping();
        //                        mapping.OldFieldID = oldField.ID;
        //                        mapping.NewFieldID = newField.ID;
        //                        mapping.OldOuterFieldID = mappingField.ID;
        //                        mapping.NewOuterFieldID = newOuterField.ID;
        //                        listMappingField.Add(mapping);
        //                    }
        //                }
        //            }
        //        }

        //        #region 构建Mapping
        //        List<EntityFieldMapping> entityFieldMappingCollection = new List<EntityFieldMapping>();
        //        foreach (var item in entity.Fields)
        //        {
        //            var idMapping = listMappingField.Where(p => p.NewFieldID == item.ID).FirstOrDefault();
        //            var oField = outerEntity.Fields.Where(p => p.ID == idMapping.NewOuterFieldID).FirstOrDefault();
        //            if (oField == null)
        //            {
        //                continue;
        //            }
        //            string outerFieldName = oField.Name;
        //            EntityFieldMapping fieldMapping = new EntityFieldMapping()
        //            {
        //                FieldID = idMapping.NewFieldID,
        //                FieldDefaultValue = item.DefaultValue,
        //                FieldDesc = item.Description,
        //                FieldLength = item.Length,
        //                FieldName = item.Name,
        //                FieldTypeName = item.FieldType.ToString(),
        //                OuterFieldID = idMapping.NewOuterFieldID,
        //                OuterFieldName = outerFieldName,
        //                SortNo = item.SortNo
        //            };

        //            entityFieldMappingCollection.Add(fieldMapping);
        //        }

        //        EntityMapping entityMapping = new EntityMapping()
        //        {
        //            InnerEntity = entity,
        //            OuterEntityID = outerEntity.ID,
        //            OuterEntityName = outerEntity.Name,
        //            OuterEntityInType = outerEntity.CustomType,
        //            EntityFieldMappingCollection = entityFieldMappingCollection
        //        };

        //        this.AddEntityMapping(entityMapping);
        //        #endregion
        //    }
        //     */

        //    #endregion

        //    //新实体入库
        //    this.AddEntity(entity);
        //    codeName = entity.CodeName;
        //    return codeName;
        //}

        #endregion

        /// <summary>
        /// 复制多个实体到多个类别下
        /// </summary>
        /// <param name="entitiesIDs">实体的id集合</param>
        /// <param name="categories">目标类别id集合</param>
        public void MoveEntities(List<string> entitiesIDs, List<string> categories)
        {
            //todo:@海军此处加参数非空校验

            //筛选出有主子关系的id
            string error = string.Empty;
            List<string> copyIDs = new List<string>();
            copyIDs.AddRange(entitiesIDs);

            List<string> mainIDs = new List<string>();

            #region 删除带有主表的 子表id
            foreach (var entityID in entitiesIDs)
            {
                if (!copyIDs.Contains(entityID))
                {
                    continue;
                }

                var entity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
                var childs = entity.Fields.Where(p => p.FieldType == Library.SOA.DataObjects.Dynamics.Enums.FieldTypeEnum.Collection);
                if (childs.Count() > 0)
                {
                    mainIDs.Add(entityID);
                    foreach (var item in childs)
                    {
                        copyIDs.Remove(item.ReferenceEntity.ID);
                    }
                }
            }

            #endregion

            #region 循环类别，向类别中复制实体

            #region 移动实体
            foreach (var category in categories)
            {
                //获取当前类别
                //copyIDs是待操作的主实体编码
                foreach (var entityID in copyIDs)
                {
                    try
                    {
                        using (TransactionScope scope = TransactionScopeFactory.Create())
                        {
                            //复制实体（包含子实体）
                            CopyChildEntity(entityID, category);

                            //删除实体（包含子实体）
                            #region

                            //取出待删除的主实体
                            DynamicEntity delEntity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;

                            DynamicEntityCollection collToDel = new DynamicEntityCollection();

                            collToDel.Add(delEntity);

                            delEntity.Fields.Where(p => p.FieldType == FieldTypeEnum.Collection && p.ReferenceEntityCodeName.IsNotEmpty()).ForEach(item =>
                            {
                                DynamicEntity childEntity = DEDynamicEntityAdapter.Instance.LoadByCodeName(item.ReferenceEntityCodeName) as DynamicEntity;

                                DeleteEntity(childEntity.ID);
                            });

                            DeleteEntity(delEntity);
                            #endregion

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        error += e.Message + "\r\n";
                    }
                }

            }
            #endregion

            #endregion
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
        }

        /// <summary>
        /// 一个结构，字段id与新字段id关系，新外部结构字段的id
        /// </summary>
        private struct FieldIDMapping
        {
            /// <summary>
            /// 旧的字段ID
            /// </summary>
            public string OldFieldID
            {
                get;
                set;
            }

            /// <summary>
            /// 新的字段ID
            /// </summary>
            public string NewFieldID
            {
                get;
                set;
            }

            /// <summary>
            /// 旧的外部字段ID
            /// </summary>
            public string OldOuterFieldID
            {
                get;
                set;
            }

            /// <summary>
            /// 新的外部字段ID
            /// </summary>
            public string NewOuterFieldID
            {
                get;
                set;
            }
        }
        #endregion

        public DESchemaObjectBase DoOperation(SCObjectOperationMode opMode, DESchemaObjectBase data, DESchemaObjectBase parent, bool deletedByContainer = false)
        {
            data.NullCheck("data");

            DESchemaOperationDefine sod = data.Schema.Operations[opMode];

            (sod != null).FalseThrow("不能找到Schema类型为{0}，操作为{1}的方法定义", data.SchemaType, opMode);

            return sod.DoOperation(this, data, parent);
        }

        private void CheckPermissions(DEOperationType opType, DESchemaDefine schemaInfo, string permissionName, params string[] containerIDs)
        {
            if (NeedCheckPermissionAndCurrentUserIsNotSupervisor && DeluxePrincipal.Current.HasPermissions(permissionName, containerIDs) == false)
                throw DEAclPermissionCheckException.CreateException(opType, schemaInfo, permissionName);
        }

        /// <summary>
        /// 根据实体定义生成实体映射
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private EntityMapping BuildEntityMapping(DynamicEntity entity)
        {
            EntityMapping mapping = new EntityMapping();
            mapping.InnerEntity = entity;
            mapping.OuterEntityID = Guid.NewGuid().ToString();
            mapping.OuterEntityName = entity.Name;
            mapping.OuterEntityInType = InType.CustomInterface;
            mapping.EntityFieldMappingCollection = new List<EntityFieldMapping>();

            var index = 0;
            entity.Fields.ForEach(p => mapping.EntityFieldMappingCollection.Add(new EntityFieldMapping()
            {
                SortNo = ++index,
                FieldID = p.ID,
                FieldName = p.Name,
                FieldDesc = p.Description,
                FieldTypeName = p.FieldType.ToString(),
                FieldLength = p.Length,
                FieldDefaultValue = p.DefaultValue,
                //OuterFieldID = Guid.NewGuid().ToString(),
                //OuterFieldName = p.Name
            }));

            return mapping;
        }

        /// <summary>
        /// 是否需要权限检查且当前人员不是超级管理员
        /// </summary>
        private bool NeedCheckPermissionAndCurrentUserIsNotSupervisor
        {
            get
            {
                return this._NeedCheckPermissions && DeluxePrincipal.Current.IsSupervisor() == false;
            }
        }
    }
}
