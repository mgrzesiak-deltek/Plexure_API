using AventStack.ExtentReports;
using log4net.Config;
using NUnit.Framework;
using PlexureAPITest.Helpers;
using PlexureAPITest.Loggers;
using System;
using Environment = System.Environment;

namespace PlexureAPITest
{
    [SetUpFixture]
    public class GlobalSetup
    {
        public static string _token;
        public static string _baseUrl;
        public static ExtentReportsManager _extentReportsManager;
        public static ExtentReports _extentReports;
        private const string Log4NetConfigPath = "Configuration/log4net.config";

        [OneTimeSetUp]
        public void RunBeforeAllTests()
        {
            _extentReportsManager = new ExtentReportsManager();
            _extentReports = _extentReportsManager.PrepareExtentReports();
            XmlConfigurator.Configure(new Uri(GetProjectPath() + Log4NetConfigPath));
            _token = TestConfig1.GetAuthenticationToken();
            _baseUrl = $"{TestConfig1.BaseUrl}/api";
        }

        public static string GetProjectPath()
        {
            int binFolderPathIndex = TestConfig1.AssemblyPath.LastIndexOf("bin", StringComparison.Ordinal);
            string actualPath = TestConfig1.AssemblyPath.Substring(0,binFolderPathIndex);
            string projectPath = new Uri(actualPath).LocalPath;
            return projectPath;
        }

        [OneTimeTearDown]
        public void TestEndDeclaration()
        {
            Console.Write("Test execution ended. Check result in newly generated report.");
            ApiHelper.RestClientDispose();
        }
    }
}