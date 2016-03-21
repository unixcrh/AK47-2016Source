using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    /// <summary>
    /// 分页请求参数的接口类
    /// </summary>
    public interface IPageRequestParams
    {
        /// <summary>
        /// 页码，从1开始计数
        /// </summary>
        int PageIndex
        {
            get;
        }

        /// <summary>
        /// 每页的行数。默认为10
        /// </summary>
        int PageSize
        {
            get;
        }

        /// <summary>
        /// 总行数。默认为-1，表示没有总行数
        /// </summary>
        int TotalCount
        {
            get;
        }

        /// <summary>
        /// 转换为行号
        /// </summary>
        /// <returns></returns>
        int ToRowIndex();
    }
}
