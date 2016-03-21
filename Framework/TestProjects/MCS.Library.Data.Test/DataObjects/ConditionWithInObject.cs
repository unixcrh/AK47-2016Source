using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    public class ConditionWithInObject : ConditionObject
    {
        [InConditionMapping()]
        public string[] Books
        {
            get;
            set;
        }

        [NoMapping]
        public int[] Chairs
        {
            get;
            set;
        }

        [InConditionMapping()]
        public int[] Desks
        {
            get;
            set;
        }
    }
}
