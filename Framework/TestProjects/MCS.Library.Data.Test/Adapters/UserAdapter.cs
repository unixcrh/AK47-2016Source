using MCS.Library.Data.Adapters;
using MCS.Library.Data.Test.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.Adapters
{
    public class UserAdapter : UpdatableAndLoadableAdapterBase<User, UserCollection>
    {
        public static readonly UserAdapter Instance = new UserAdapter();

        private UserAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return Common.ConnectionName;
        }
    }
}
