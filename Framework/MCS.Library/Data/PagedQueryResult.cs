using MCS.Library.Data.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    /// <summary>
    /// 分页查询的返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public class PagedQueryResult<T, TCollection> where T : new() where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public PagedQueryResult()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.TotalCount = -1;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="prp"></param>
        public PagedQueryResult(IPageRequestParams prp)
        {
            this.PageIndex = prp.PageIndex;
            this.PageSize = prp.PageSize;
            this.TotalCount = prp.TotalCount;
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
        /// 查询的单页结果
        /// </summary>
        public TCollection PagedData
        {
            get;
            set;
        }
    }
}
