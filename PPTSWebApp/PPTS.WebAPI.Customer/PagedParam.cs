using System;

namespace PPTS.WebAPI.Customer
{
    public class PagedParam
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int PageCount { get; set; }
        public string Message { get; set; }
        public PagedParam(int page = 1, int limit = 10)
        {
            this.Page = page;
            this.Limit = limit;
        }

        public void Initialize(int totalcount)
        {
            this.TotalCount = totalcount;
            if (this.TotalCount > 0)
            {
                var a = this.TotalCount / this.Limit;
                var b = this.TotalCount % this.Limit;

                this.PageCount = (b == 0) ? a : a + 1;
                this.Message = "共" + this.TotalCount.ToString() + "条数据，当前显示" + ((this.Page - 1) * this.Limit + 1).ToString() + "到" + Math.Min(this.Page * this.Limit, this.TotalCount).ToString() + "条";
            }
        }
    }
}
