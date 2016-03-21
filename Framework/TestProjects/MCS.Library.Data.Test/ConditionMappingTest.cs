using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Data.Test.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class ConditionMappingTest
    {
        [TestMethod]
        public void BasicConditionMappingTest()
        {
            ConditionObject condition = PrepareData();

            IConnectiveSqlClause builder = ConditionMapping.GetWhereSqlClauseBuilder(condition);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        [TestMethod]
        public void BooksConditionMappingTest()
        {
            ConditionWithInObject condition = PrepareInData();

            IConnectiveSqlClause builder = ConditionMapping.GetConnectiveClauseBuilder(condition);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        private static ConditionObject PrepareData()
        {
            ConditionObject condition = new ConditionObject();

            condition.Subject = "Hello world !";
            condition.FullTextTerm = "钱";
            condition.Gender = GenderType.Female;
            condition.StartTime = DateTime.Now.AddDays(-1);
            condition.EndTime = DateTime.Now.AddDays(1);

            return condition;
        }

        private static ConditionWithInObject PrepareInData()
        {
            ConditionWithInObject condition = new ConditionWithInObject();

            condition.Subject = "Hello world !";
            condition.FullTextTerm = "钱";
            condition.Gender = GenderType.Female;
            condition.StartTime = DateTime.Now.AddDays(-1);
            condition.EndTime = DateTime.Now.AddDays(1);

            condition.Books = new string[] { "青铜骑士", "乡村教师", "三体" };
            condition.Chairs = new int[] { 1, 5 };

            return condition;
        }
    }
}
