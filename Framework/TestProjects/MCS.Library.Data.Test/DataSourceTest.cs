using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Data.Test.DataObjects;
using MCS.Library.Core;
using MCS.Library.Data.Test.Adapters;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class DataSourceTest
    {
        [TestMethod]
        public void QueryUserTheFirstPageTest()
        {
            UserAdapter.Instance.ClearAll();

            PrepareUsers(10);

            UserCondition condition = new UserCondition() { UserName = "沈" };

            UserDataSource dataSource = new UserDataSource();

            var result = dataSource.Query(condition.PageParams, condition, condition.OrderByBuilder);

            Console.WriteLine("PageIndex: {0}, PageSize: {1}, TotalCount: {2}",
                result.PageIndex, result.PageSize, result.TotalCount);

            Assert.AreEqual(10, result.TotalCount);
            Assert.AreEqual(1, result.PageIndex);
            Assert.AreEqual(10, result.PagedData.Count);
        }

        [TestMethod]
        public void QueryUserTheSecondPageTest()
        {
            UserAdapter.Instance.ClearAll();

            PrepareUsers(18);

            UserCondition condition = new UserCondition() { UserName = "沈" };

            UserDataSource dataSource = new UserDataSource();

            condition.PageParams.PageIndex = 2;

            var result = dataSource.Query(condition.PageParams, condition, condition.OrderByBuilder);

            Console.WriteLine("PageIndex: {0}, PageSize: {1}, TotalCount: {2}",
                result.PageIndex, result.PageSize, result.TotalCount);

            Assert.AreEqual(18, result.TotalCount);
            Assert.AreEqual(2, result.PageIndex);
            Assert.AreEqual(8, result.PagedData.Count);
        }

        [TestMethod]
        public void QueryUserExceedPageTest()
        {
            UserAdapter.Instance.ClearAll();

            PrepareUsers(10);

            UserCondition condition = new UserCondition() { UserName = "沈" };

            UserDataSource dataSource = new UserDataSource();

            condition.PageParams.PageIndex = 2;

            var result = dataSource.Query(condition.PageParams, condition, condition.OrderByBuilder);

            Console.WriteLine("PageIndex: {0}, PageSize: {1}, TotalCount: {2}",
                result.PageIndex, result.PageSize, result.TotalCount);

            Assert.AreEqual(10, result.TotalCount);
            Assert.AreEqual(1, result.PageIndex);
            Assert.AreEqual(10, result.PagedData.Count);
        }

        private static void PrepareUsers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                User user = new User();

                user.UserID = UuidHelper.NewUuidString();
                user.UserName = "沈峥";
                user.Gender = GenderType.Male;

                UserAdapter.Instance.Update(user);
            }
        }
    }
}
