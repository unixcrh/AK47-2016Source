using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Dynamics.Validators;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;

namespace MCS.Library.SOA.DataObjects.Dynamics.Schemas
{
    /// <summary>
    /// 表示模式对象的基类
    /// </summary>
    [Serializable]
    [ORTableMapping("DE.SchemaObject")]
    public abstract class DESchemaObjectBase : VersionedSchemaObjectBase, ISCStatusObject
    {
        private SchemaPropertyValueCollection _Properties = null;

        /// <summary>
        /// 初始化<see cref="DESchemaObjectBase"/>成员
        /// </summary>
        protected DESchemaObjectBase()
        {
        }

        /// <summary>
        /// 使用指定的模式名初始化<see cref="DESchemaObjectBase"/>成员
        /// </summary>
        /// <param name="schemaType">模式的名称</param>
        public DESchemaObjectBase(string schemaType)
            : base(schemaType)
        {
        }

        /// <summary>
        /// 获取模式定义
        /// </summary>
        /// <value>表示模式定义的<see cref="DESchemaDefine"/>对象</value>
        [NoMapping]
        public new DESchemaDefine Schema
        {
            get
            {
                return (DESchemaDefine)base.Schema;
            }
        }

        //[NonSerialized]
        //private SCParentsRelationObjectCollection _AllParentRelations = null;

        ///// <summary>
        ///// 获取表示所有父级关系的<see cref="SCParentsRelationObjectCollection"/>
        ///// </summary>
        //[ScriptIgnore]
        //[NoMapping]
        //public SCParentsRelationObjectCollection AllParentRelations
        //{
        //    get
        //    {
        //        if (this._AllParentRelations == null && this.ID.IsNotEmpty())
        //        {
        //            this._AllParentRelations = SchemaRelationObjectAdapter.Instance.LoadByObjectID(this.ID);
        //        }

        //        return _AllParentRelations;
        //    }
        //}

        ///// <summary>
        ///// 获取一个表示当前父级关系的<see cref="SCParentsRelationObjectCollection"/>
        ///// </summary>
        //[ScriptIgnore]
        //[NoMapping]
        //public SCParentsRelationObjectCollection CurrentParentRelations
        //{
        //    get
        //    {
        //        return (SCParentsRelationObjectCollection)AllParentRelations.FilterByStatus(SchemaObjectStatusFilterTypes.Normal);
        //    }
        //}

        //[NonSerialized]
        //private SchemaObjectCollection _AllParents = null;

        ///// <summary>
        ///// 获取一个<see cref="SchemaObjectCollection"/>，表示所有父级关系
        ///// </summary>
        //[NoMapping]
        //[ScriptIgnore]
        //public SchemaObjectCollection AllParents
        //{
        //    get
        //    {
        //        if (_AllParents == null && this.ID.IsNotEmpty())
        //        {
        //            this._AllParents = DESchemaObjectAdapter.Instance.Load(AllParentRelations.ToParentIDsBuilder());

        //            SCOrganization root = SCOrganization.GetRoot();

        //            if (AllParentRelations.Exists(r => r.ParentID == root.ID))
        //                this._AllParents.Add(root);
        //        }

        //        return this._AllParents;
        //    }
        //}

        //[NonSerialized]
        //private SchemaObjectCollection _CurrentlParents = null;

        ///// <summary>
        ///// 获取一个<see cref="SchemaObjectCollection"/>，表示所有父级。
        ///// </summary>
        //[NoMapping]
        //[ScriptIgnore]
        //public DESchemaObjectCollection CurrentParents
        //{
        //    get
        //    {
        //        if (this._CurrentlParents == null && this.ID.IsNotEmpty())
        //        {
        //            this._CurrentlParents = DESchemaObjectAdapter.Instance.Load(CurrentParentRelations.ToParentIDsBuilder());
        //            this._CurrentlParents = this._CurrentlParents.FilterByStatus(SchemaObjectStatusFilterTypes.Normal);

        //            SCOrganization root = SCOrganization.GetRoot();

        //            if (CurrentParentRelations.Exists(r => r.ParentID == root.ID))
        //                this._CurrentlParents.Add(root);
        //        }

        //        return this._CurrentlParents;
        //    }
        //}

        ///// <summary>
        ///// 生成表示当前对象的简单对象
        ///// </summary>
        ///// <returns></returns>
        //public SCSimpleObject ToSimpleObject()
        //{
        //    SCSimpleObject result = new SCSimpleObject();

        //    result.ID = this.ID;
        //    result.VersionStartTime = this.VersionStartTime;
        //    result.VersionEndTime = this.VersionEndTime;
        //    result.Tag = this.Tag;
        //    result.SchemaType = this.SchemaType;
        //    result.Status = this.Status;

        //    result.Name = this.Properties.GetValue("Name", string.Empty);
        //    result.DisplayName = this.Properties.GetValue("DisplayName", string.Empty);
        //    result.CodeName = this.Properties.GetValue("CodeName", string.Empty);

        //    if (result.DisplayName.IsNullOrEmpty())
        //        result.DisplayName = result.Name;

        //    return result;
        //}

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        public ValidationResults Validate()
        {
            DESchemaObjectValidator validator = new DESchemaObjectValidator();

            return validator.Validate(this);
        }

        /// <summary>
        /// 清除相关的数据，例如AllParents、AllChildren等
        /// </summary>
        public void ClearRelativeData()
        {
            OnIDChanged();
        }

        ///// <summary>
        ///// 处理ID的变更
        ///// </summary>
        //protected override void OnIDChanged()
        //{
        //    this._AllParentRelations = null;
        //    this._AllParents = null;
        //    this._CurrentlParents = null;
        //}

        protected override SchemaPropertyValueCollection GetProperties()
        {
            if (this._Properties == null)
                this._Properties = ((DESchemaDefine)this.Schema).ToProperties();

            return this._Properties;
        }

        protected override SchemaDefineBase GetSchema(string schemaType)
        {
            return DESchemaDefine.GetSchema(this.SchemaType);
        }
    }

    ///// <summary>
    ///// 表示平面化模式对象的集合
    ///// </summary>
    //[Serializable]
    //public class DESchemaObjectPlainCollection : DESchemaObjectCollectionBase<DESchemaObjectBase, DESchemaObjectPlainCollection>
    //{
    //    ///// <summary>
    //    ///// 获取简单对象的集合
    //    ///// </summary>
    //    ///// <returns><see cref="SCSimpleObjectCollection"/></returns>
    //    //public DESCSimpleObjectCollection ToSimpleObjects()
    //    //{
    //    //    SCSimpleObjectCollection result = new SCSimpleObjectCollection();

    //    //    this.ForEach(obj => result.Add(obj.ToSimpleObject()));

    //    //    return result;
    //    //}

    //    protected override DESchemaObjectPlainCollection CreateFilterResultCollection()
    //    {
    //        return new DESchemaObjectPlainCollection();
    //    }
    //}

    /// <summary>
    /// 表示模式对象的集合
    /// </summary>
    [Serializable]
    public class DESchemaObjectCollection : DESchemaObjectEditableKeyedCollectionBase<DESchemaObjectBase, DESchemaObjectCollection>
    {
        public DESchemaObjectCollection()
        {
        }

        public DESchemaObjectCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// 合并两个集合。结果是两个集合的并集
        /// </summary>
        /// <param name="source"></param>
        public virtual void Merge(DESchemaObjectCollection source)
        {
            if (source != null)
                source.ForEach(this.AddNotExistsItem);
        }

        /// <summary>
        /// 获取指定对象的键
        /// </summary>
        /// <param name="item">要获取其键的<see cref="DESchemaObjectBase"/>的派生类型</param>
        /// <returns>表示键的字符串</returns>
        protected override string GetKeyForItem(DESchemaObjectBase item)
        {
            return item.ID;
        }

        /// <summary>
        /// 创建过滤器结果的集合
        /// </summary>
        /// <returns></returns>
        protected override DESchemaObjectCollection CreateFilterResultCollection()
        {
            return new DESchemaObjectCollection();
        }

    }
}
