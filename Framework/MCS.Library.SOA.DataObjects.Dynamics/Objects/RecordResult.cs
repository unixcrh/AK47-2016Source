using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 录屏结果对象
    /// </summary>
    [Serializable]
    public class RecordResult
    {
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }

        public string TempFullPath { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 实体描述
        /// </summary>
        public string EntityDesc { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 主表标识
        /// </summary>
        public bool IsMasterTable { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public FieldTypeEnum FieldType { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string FieldDesc { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int FieldLength { get; set; }

        public int DecimalLength { get; set; }

        /// <summary>
        /// 引用实体的CodeName
        /// </summary>
        public string ReferenceEntityCodeName { get; set; }

        public bool IsStruct { get; set; }

        public ParamDirectionEnum ParamDirection { get; set; }

    }

    [Serializable]
    public class RecordResultCollection : EditableDataObjectCollectionBase<RecordResult>
    {
        /// <summary>
        /// 生成实体定义
        /// </summary>
        /// <returns></returns>
        public DynamicEntity BuildEntity(string categoryID)
        {
            DynamicEntity result = new DynamicEntity();

            this.Any().FalseThrow("当前集合不能为空!");

            //这个提示有点扯蛋
            (this.Select(p => p.EntityName).Distinct().Count() == 1).FalseThrow("录屏结果不能生成多个实体!");

            result.ID = Guid.NewGuid().ToString();
            result.Name = this.FirstOrDefault().EntityName;
            result.Description = this.FirstOrDefault().EntityDesc;
            result.CategoryID = categoryID;
            result.Fields = new DynamicEntityFieldCollection();
            result.SortNo = 0;

            //result.BuidCodeName();

            var index = 0;
            this.OrderBy(p => p.SortNo).ForEach(p => result.Fields.Add(new DynamicEntityField()
            {
                ID = Guid.NewGuid().ToString(),
                Name = p.FieldName,
                Description = p.FieldDesc,
                Length = p.FieldLength,
                FieldType = p.FieldType,
                DefaultValue = p.DefaultValue,
                SortNo = index++,
                ReferenceEntityCodeName = p.ReferenceEntityCodeName,
                IsStruct = p.IsStruct,
                ParamDirection = p.ParamDirection
            }));

            return result;
        }


        // public ETLEntity
    }
}
