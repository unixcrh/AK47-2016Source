using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Data.Test.DataObjects;
using MCS.Library.Core;
using System.Linq;

namespace MCS.Library.Data.Test.Adapters
{
    [TestClass]
    public class AdapterTest
    {
        [TestMethod]
        public void UpdateUserTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            UserAdapter.Instance.Update(user);

            User userLoaded = UserAdapter.Instance.LoadByInBuilder(builder => builder.AppendItem(user.UserID), "UserID").Single();

            AssertEqual(user, userLoaded);
        }

        [TestMethod]
        public void UpdateUserInContextTest()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            using (DbContext context = UserAdapter.Instance.GetDbContext())
            {
                UserAdapter.Instance.UpdateInContext(user);

                Console.WriteLine(context.GetSqlInContext());

                context.ExecuteNonQuerySqlInContext();

                User userLoaded = UserAdapter.Instance.LoadByInBuilder(builder => builder.AppendItem(user.UserID), "UserID").Single();

                AssertEqual(user, userLoaded);
            }
        }

        private static void AssertEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.Gender, actual.Gender);
        }
    }
}
