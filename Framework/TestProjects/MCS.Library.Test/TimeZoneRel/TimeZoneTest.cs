using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Core;

namespace MCS.Library.Test.TimeZoneRel
{
    [TestClass]
    public class TimeZoneTest
    {
        [TestMethod]
        public void ChinaTimeConvert()
        {
            TimeZoneInfo chinaTZ = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

            DateTime utcTime = new DateTime(2015, 12, 22, 0, 0, 0, DateTimeKind.Utc);

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, chinaTZ);

            Console.WriteLine(localTime);

            Assert.AreEqual(8, localTime.Hour);
        }

        [TestMethod]
        public void CustomTimeConvertFromUtc()
        {
            TimeZoneInfo chinaTZ = TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(480), "TimeZoneInfoContext", "TimeZoneInfoContext");

            DateTime utcTime = new DateTime(2015, 12, 22, 0, 0, 0, DateTimeKind.Unspecified);

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, chinaTZ);

            Console.WriteLine(localTime);

            Assert.AreEqual(8, localTime.Hour);
        }

        [TestMethod]
        public void CustomTimeConvertToUtc()
        {
            TimeZoneContext.Current.CurrentTimeZone = TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(480), "TimeZoneInfoContext", "TimeZoneInfoContext");
            
            DateTime localTime = new DateTime(2015, 12, 22, 16, 0, 0, DateTimeKind.Local);

            DateTime utcTime = TimeZoneContext.Current.ConvertTimeToUtc(localTime);

            Console.WriteLine(utcTime);

            Assert.AreEqual(8, utcTime.Hour);
        }

        [TestMethod]
        public void LocalTimeConvert()
        {
            DateTime utcTime = new DateTime(2015, 12, 22, 0, 0, 0, DateTimeKind.Utc);

            Console.WriteLine(TimeZoneInfo.Local.Id);

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.Local);

            Console.WriteLine(localTime);

            Assert.AreEqual(8, localTime.Hour);
        }

        [TestMethod]
        public void TimeZoneContextLocalTimeConvert()
        {
            TimeZoneContext.Current.CurrentTimeZone = TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(480), "TimeZoneInfoContext", "TimeZoneInfoContext");

            DateTime utcTime = new DateTime(2015, 12, 22, 0, 0, 0, DateTimeKind.Utc);

            Console.WriteLine(TimeZoneContext.Current.CurrentTimeZone.Id);

            DateTime localTime = TimeZoneContext.Current.ConvertTimeFromUtc(utcTime);

            Console.WriteLine(localTime);

            Assert.AreEqual(8, localTime.Hour);
        }
    }
}
