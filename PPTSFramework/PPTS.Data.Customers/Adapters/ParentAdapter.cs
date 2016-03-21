using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class ParentAdapter : CustomerAdapterBase<Parent, ParentCollection>
    {
        public static readonly ParentAdapter Instance = new ParentAdapter();

        private ParentAdapter()
        {
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="account"></param>
        /*
		public void Insert(Account account)
		{
			this.InnerInsert(account, new Dictionary<string, object>());
		}
		*/

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public Parent Load(string parentID)
        {
            return this.Load(builder => builder.AppendItem("ParentID", parentID)).SingleOrDefault();
        }

        protected override void BeforeInnerUpdateInContext(Parent data, DbContext dbContext, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("P");
        }

        protected override void BeforeInnerUpdate(Parent data, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("P");
        }
    }
}