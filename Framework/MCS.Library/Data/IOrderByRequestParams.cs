using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    /// <summary>
    /// 排序的请求参数接口
    /// </summary>
    public interface IOrderByRequestItem
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        string DataField
        {
            get;
        }

        /// <summary>
        /// 排序方向
        /// </summary>
        FieldSortDirection SortDirection
        {
            get;
        }
    }
}
