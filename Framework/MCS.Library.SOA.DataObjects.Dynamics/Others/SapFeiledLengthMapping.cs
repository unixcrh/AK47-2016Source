using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics.Others
{
    public class SapFeiledLengthMapping
    {
        public int UEPFiledLengthAdd(int filedLenth, int addNumber)
        {
            return filedLenth + addNumber;

        }

        public int UEPFiledLengthMul(int filedLenth, int addNumber)
        {
            return filedLenth * addNumber;
        }


        public int UEPFiledLenth(int filedLenth, int addNumber)
        {
            return filedLenth;
        }

    }

    public class SapTableStructLengthMapping
    {
        public int UEPFiledLengthAdd(int filedLenth, int addNumber)
        {
            return 999999;

        }

        public int UEPFiledLengthMul(int filedLenth, int addNumber)
        {
            return 999999;
        }


        public int UEPFiledLenth(int filedLenth, int addNumber)
        {
            return 999999;
        }

    }
}