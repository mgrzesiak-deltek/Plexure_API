using AventStack.ExtentReports;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PlexureAPITest.Loggers;

namespace PlexureAPITest
{
    public abstract class EachTestRunner
    {
        protected Log Log;
        private ExtentTest ExtentReportsTest;
        [SetUp]
        public void SetUpBeforeEveryTest()
        {
            ExtentReportsTest = GlobalSetup._extentReports.CreateTest(PrepareTestName());
            Log = new Log(ExtentReportsTest);
            Log.Info($"Start running test under {TestConfig1.BaseUrl} address.");
        }

        [TearDown]
        public void TearDownAfterEveryTest()
        {
            GlobalSetup._extentReportsManager.PrepareTestReport(ExtentReportsTest);
            GlobalSetup._extentReports?.Flush();
        }

        private string PrepareTestName()
        {
            string testPath = TestExecutionContext.CurrentContext.CurrentTest.ClassName.Replace("PlexureAPITest.", "");
            var splitTestPath = testPath.Split('.');
            return " Test class: " + splitTestPath[1] + " ; Test name: " + TestContext.CurrentContext.Test.Name;
        }
    }
}
