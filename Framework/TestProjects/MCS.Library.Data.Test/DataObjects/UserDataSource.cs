using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public class UserDataSource : ObjectDataSourceQueryAdapterBase<User, UserCollection>
    {
        protected override string GetConnectionName()
        {
            return Common.ConnectionName;
        }
    }
}
