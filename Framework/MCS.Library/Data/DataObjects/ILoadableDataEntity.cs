using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.DataObjects
{
    /// <summary>
    /// 可以被加载的数据实体
    /// </summary>
    public interface ILoadableDataEntity
    {
        /// <summary>
        /// 是否从数据库加载
        /// </summary>
        [NoMapping]
        bool Loaded
        {
            get;
            set;
        }
    }
}
