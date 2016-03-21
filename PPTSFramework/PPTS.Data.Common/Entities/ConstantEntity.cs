using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [Serializable]
    [ORTableMapping("MT.Constants")]
    public class ConstantEntity : BaseConstantEntity
    {
        [ORFieldMapping("Category", PrimaryKey = true)]
        public string Category
        {
            get;
            set;
        }

        [ORFieldMapping("Key", PrimaryKey = true)]
        public override string Key
        {
            get;
            set;
        }

        [ORFieldMapping("Value")]
        public override string Value
        {
            get;
            set;
        }

        [ORFieldMapping("ParentKey")]
        public override string ParentKey
        {
            get;
            set;
        }

        [ORFieldMapping("SortNo")]
        public int SortNo
        {
            get;
            set;
        }

        [ORFieldMapping("IsValidate")]
        public bool IsValidate
        {
            get;
            set;
        }
    }

    [Serializable]
    public class ConstantEntityCollection : EditableDataObjectCollectionBase<ConstantEntity>
    {
        public IEnumerable<BaseConstantEntity> ToSimpleEntity()
        {
            List<BaseConstantEntity> result = new List<BaseConstantEntity>();

            this.ForEach(entity => result.Add(new BaseConstantEntity(entity)));

            return result;
        }
    }

    /// <summary>
    /// 在一个类别内的常量实体集合
    /// </summary>
    [Serializable]
    public class ConstantEntityInCategoryCollection : SerializableEditableKeyedDataObjectCollectionBase<string, ConstantEntity>
    {
        public ConstantEntityInCategoryCollection()
        {
        }

        public ConstantEntityInCategoryCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public IEnumerable<BaseConstantEntity> ToSimpleEntity()
        {
            List<BaseConstantEntity> result = new List<BaseConstantEntity>();

            this.ForEach(entity => result.Add(new BaseConstantEntity(entity)));

            return result;
        }

        protected override string GetKeyForItem(ConstantEntity item)
        {
            return item.Key;
        }
    }
}
