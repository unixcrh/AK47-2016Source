using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class PageRequestParams : IPageRequestParams
    {
        /// <summary>
        /// 构造方法。默认页码从1开始计数，每页10行
        /// </summary>
        public PageRequestParams()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.TotalCount = -1;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的行数</param>
        public PageRequestParams(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = -1;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的行数</param>
        /// <param name="totalCount">总行数。默认为-1</param>
        public PageRequestParams(int pageIndex, int pageSize, int totalCount)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
        }

        /// <summary>
        /// 页码，从1开始计数
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 每页的行数。默认为10
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 总行数。默认为-1，表示没有总行数
        /// </summary>
        public int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 转换为行号
        /// </summary>
        /// <returns></returns>
        public int ToRowIndex()
        {
            return (this.PageIndex - 1) * this.PageSize;
        }
    }
}
