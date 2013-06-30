using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UpboatMe.Imaging.Tests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Approvals.Verify("test");
        }
    }
}
