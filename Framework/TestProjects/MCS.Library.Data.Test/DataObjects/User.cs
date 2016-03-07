using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public enum GenderType
    {
        Unknown,
        Male,
        Female
    }

    [ORTableMapping("Users")]
    [Serializable]
    public class User
    {
        [ORFieldMapping("UserID", PrimaryKey = true)]
        public string UserID
        {
            get;
            set;
        }

        [ORFieldMapping("UserName")]
        public string UserName
        {
            get;
            set;
        }

        [ORFieldMapping("Gender")]
        [SqlBehavior(EnumUsage = EnumUsageTypes.UseEnumString)]
        public GenderType Gender
        {
            get;
            set;
        }
    }

    [Serializable]
    public class UserCollection : EditableDataObjectCollectionBase<User>
    {
    }
}
