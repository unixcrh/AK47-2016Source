using System.Collections.Generic;

namespace PPTS.WebAPI.Customer
{
    public class PagedList<T>
    {
        public PagedList(int page, int limit) 
            : this(new PagedParam(page, limit) { })
        {

        }

        public PagedList(PagedParam pagedParam)
        {
            this.Data = new List<T>();
            this.PagedParam = pagedParam;
        }

        public PagedParam PagedParam { get; set; }
        public List<T> Data { get; set; }
    }
}