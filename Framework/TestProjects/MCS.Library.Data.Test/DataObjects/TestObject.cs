using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.Data.Test.DataObjects
{
    [ORTableMapping("TEST_TABLE")]
    [Serializable]
    public class TestObject
    {
        [ORFieldMapping("ID", PrimaryKey = true)]
        public string ID
        {
            get;
            set;
        }

        [ORFieldMapping("NAME")]
        public string Name
        {
            get;
            set;
        }

        [PropertyEncryption]
        [ORFieldMapping("AMOUNT")]
        public Decimal Amount
        {
            get;
            set;
        }

        [ORFieldMapping("LOCAL_TIME")]
        public DateTime LocalTime
        {
            get;
            set;
        }

        [ORFieldMapping("UTC_TIME", UtcTimeToLocal = true)]
        public DateTime UtcTime
        {
            get;
            set;
        }
    }

    [Serializable]
    public class TestObjectCollection : EditableDataObjectCollectionBase<TestObject>
    {

    }
}
