using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

using MCS.Library.Core;
using MCS.Library.Data.DataObjects;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// 条件表达式和对象映射条目集合
    /// </summary>
    public class ConditionMappingItemCollection : DataObjectCollectionBase<ConditionMappingItemBase>
    {
        /// <summary>
        /// 添加一个条件项
        /// </summary>
        /// <param name="item"></param>
        public void Add(ConditionMappingItemBase item)
        {
            InnerAdd(item);
        }

        /// <summary>
        /// 按照索引添加或设置一个条件项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ConditionMappingItemBase this[int index]
        {
            get
            {
                return (ConditionMappingItemBase)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// 删除一个条件项
        /// </summary>
        /// <param name="item"></param>
        public void Remove(ConditionMappingItemBase item)
        {
            List.Remove(item);
        }

        /// <summary>
        /// 根据类型进行筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> FilterByType<T>() where T: ConditionMappingItemBase
        {
            foreach(ConditionMappingItemBase item in this)
            {
                if (item is T)
                {
                    yield return (T)item;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected override void OnValidate(object value)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(value != null, "value");
        }
    }
}
