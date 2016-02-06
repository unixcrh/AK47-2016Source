using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects.Dynamics
{
    /// <summary>
    /// 表示标准对象模式的类型
    /// </summary>
    public enum DEStandardObjectSchemaType
    {
        /// <summary>
        /// 实体对象
        /// </summary>
        DynamicEntity = 1,

        /// <summary>
        /// 实体对象字段
        /// </summary>
        DynamicEntityField = 2,

        /// <summary>
        /// 外部对象
        /// </summary>
        OuterEntity = 3,

        /// <summary>
        /// 实体与外部结构对应关系
        /// </summary>
        DynamicEntityMapping = 4,

        /// <summary>
        /// 外部结构字段
        /// </summary>
        OuterEntityField = 5,

        /// <summary>
        /// 外部实体与外部属性对应关系
        /// </summary>
        OuterEntityFieldMapping = 6,

        /// <summary>
        /// 实体字段与外部结构字段对应关系
        /// </summary>
        DynamicEntityFieldMapping = 8,

        /// <summary>
        /// 实体与实体字段关联关系
        /// </summary>
        Entity_FieldsRelation = 16,


        #region 抽数实体类型
        

        /// <summary>
        /// 抽数实体
        /// </summary>
        ETLEntity = 17,

        /// <summary>
        /// 抽数实体字段
        /// </summary>
        ETLEntityField = 18,

        /// <summary>
        /// 抽数实体和抽数字段关系
        /// </summary>
        ETLEntity_FieldsRelation= 24,

        /// <summary>
        /// 外部抽数实体
        /// </summary>
        OuterETLEntity = 19,

        /// <summary>
        /// 外部抽数实体字段
        /// </summary>
        OuterETLEntityField = 20,

        /// <summary>
        /// 抽数实体和外部实体对应关系
        /// </summary>
        ETLEntityMapping = 21,

        /// <summary>
        /// 外部抽数实体和外部实体字段对应关系
        /// </summary>
        OutETLEntityFieldMapping = 22,

        /// <summary>
        /// 抽数实体字段与外部结构字段对应关系
        /// </summary>
        ETLEntityFieldMapping = 23,

        /// <summary>
        /// 抽数实体与子实体之间的对应关系
        /// </summary>
        ETLEntityChildMapping = 25,
        #endregion


    }

    /// <summary>
    /// 表示用户集合中按照状态筛选的枚举
    /// </summary>
    public enum DESchemaObjectStatusFilterTypes
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 2,

        /// <summary>
        /// 全部
        /// </summary>
        All = 3
    }

    /// <summary>
    /// 表示查询快照时的查询ID的类型
    /// </summary>
    public enum DESnapshotQueryIDType
    {
        /// <summary>
        /// 按对象ID
        /// </summary>
        [EnumItemDescription("对象的ID", ShortName = "ID")]
        Guid,

        /// <summary>
        /// 按对象的代码名称
        /// </summary>
        [EnumItemDescription("Code", ShortName = "CodeName")]
        CodeName
    }

    /// <summary>
    /// 内置函数使用的对象查询的ID类型
    /// </summary>
    public enum DEBuiltInFunctionIDType
    {
        /// <summary>
        /// 按对象ID
        /// </summary>
        [EnumItemDescription("对象的ID", ShortName = "ID")]
        Guid,

        /// <summary>
        /// 按对象的代码名称
        /// </summary>
        [EnumItemDescription("代码名称", ShortName = "CodeName")]
        CodeName,

        /// <summary>
        /// 按对象的全路径
        /// </summary>
        [EnumItemDescription("全路径", ShortName = "FullPath")]
        FullPath
    }
}
