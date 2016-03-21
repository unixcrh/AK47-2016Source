using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class PhoneAdapter : CustomerAdapterBase<Phone, PhoneCollection>
    {
        public static readonly PhoneAdapter Instance = new PhoneAdapter();

        private PhoneAdapter()
        {
        }

        public PhoneCollection LoadByOwnerID(string ownerID)
        {
            return this.Load(builder => builder.AppendItem("OwnerID", ownerID));
        }

        public void UpdateByOwnerIDInContext(string ownerID, PhoneCollection phones)
        {
            ownerID.CheckStringIsNullOrEmpty("ownerID");
            phones.NullCheck("phones");

            Dictionary<string, object> context = new Dictionary<string, object>();

            using (DbContext dbContext = this.GetDbContext())
            {
                this.DeleteInContext(builder => builder.AppendItem("OwnerID", ownerID));

                foreach (Phone phone in phones)
                {
                    dbContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
                    this.InnerInsertInContext(phone, dbContext, context);
                }
            }
        }
    }
}
