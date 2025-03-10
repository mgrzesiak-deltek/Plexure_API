using AventStack.ExtentReports;
using log4net;
using NUnit.Framework;

namespace PlexureAPITest.Loggers
{
    public class Log
    {
        public ILog _log;
        protected ExtentTest _extentReportsTest;

        public Log(ExtentTest extentReportsTest)
        {
            this._extentReportsTest = extentReportsTest;
            _log = LogManager.GetLogger(TestContext.CurrentContext.Test.Name);
        }

        public void Info(string message)
        {
            _extentReportsTest.Log(Status.Info, message);
            _log.Info(message);
        }
    }
}