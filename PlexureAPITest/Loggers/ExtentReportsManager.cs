using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using PlexureAPITest;
using System.IO;
using System;

namespace PlexureAPITest.Loggers
{
    public class ExtentReportsManager
    {
        private static ExtentReports _instance;
        private const string FullDataFormat = "dd_MM_yyy_HH_mm_ss";
        private const string HourDataFormat = "HH_mm_ss";
        private const string BeginOfDocumentName = "TestReport_";
        private const string ScreenshotFileFormat = ".png";
        private const string ReportsFileFormat = ".html";
        private const string ReportsName = "PlexureAPITest - Test Report";
        private const string UsernameInformationComment = "User name";
        private const string EndTestInformation = "Test ended with ";
        private const string HttpNewLine = "<br>";
        private readonly DateTime _timeFromBeingTests = DateTime.Now;
        private static readonly object Lock = new object();

        public ExtentReports PrepareExtentReports()
        {
            lock (Lock)
            {
                if (_instance == null)
                {
                    string reportFolderPath = GlobalSetup.GetProjectPath() + TestConfig1.ReportNameFolder; // C:\\Test-Automation + TestConfig1.ReportNameFolder
                    Directory.CreateDirectory(reportFolderPath);
                    ExtentSparkReporter sparkReporter = new ExtentSparkReporter(reportFolderPath + BeginOfDocumentName +
                        _timeFromBeingTests.ToString(FullDataFormat) + ReportsFileFormat);
                    sparkReporter.Config.Theme = Theme.Dark;
                    sparkReporter.Config.DocumentTitle = BeginOfDocumentName + _timeFromBeingTests.ToString(FullDataFormat);
                    sparkReporter.Config.TimelineEnabled = true;
                    sparkReporter.Config.ReportName = ReportsName;
                    ExtentReports extentReports = new ExtentReports();
                    extentReports.AttachReporter(sparkReporter);
                    extentReports.AddSystemInfo(UsernameInformationComment, Environment.UserName);
                    _instance = extentReports;
                }
            }

            return _instance;
        }

        public void PrepareTestReport(ExtentTest extentReports)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : TestContext.CurrentContext.Result.Message + HttpNewLine + HttpNewLine +
                  TestContext.CurrentContext.Result.StackTrace;
            Status logStatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logStatus = Status.Fail;
                    var time = DateTime.Now;
                    extentReports.Log(Status.Fail, "Fail");
                    break;
                case TestStatus.Inconclusive:
                    logStatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logStatus = Status.Skip;
                    break;
                default:
                    logStatus = Status.Pass;
                    break;
            }
            extentReports.Log(logStatus, EndTestInformation + logStatus + stacktrace);
        }
    }
}