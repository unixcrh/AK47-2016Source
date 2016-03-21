using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace MCS.Library.Test.Logging
{
    [TestClass]
    public class TraceListernerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ts = new TraceSource("Test", SourceLevels.Warning);
            ts.TraceEvent(TraceEventType.Warning, 1, "Hello");

            ts = new TraceSource("Test", SourceLevels.Warning);
            ts.TraceEvent(TraceEventType.Warning, 1, "Hello");
        }
    }
}
