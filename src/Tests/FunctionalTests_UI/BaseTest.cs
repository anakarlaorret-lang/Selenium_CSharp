using NUnit.Framework;
using OpenQA.Selenium;  
using DemoTest.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace DemoTest.Tests
{
    /// <summary>
    /// BaseTest provides setup and teardown logic
    /// for all test classes. It initializes WebDriver
    /// and loads configuration data.
    /// </summary>
    public abstract class BaseTest
    {
        protected IWebDriver driver;
       protected static AventStack.ExtentReports.ExtentReports extent;
        protected AventStack.ExtentReports.ExtentTest? test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            string reportsDir = Path.Combine(projectRoot, "reports");
            Directory.CreateDirectory(reportsDir);
           
            // Initialize ExtentReports
            var htmlReporter = new ExtentHtmlReporter(Path.Combine(reportsDir, "TestReport.html"));
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

       [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent?.Flush();
        }

        [SetUp]
        public void Setup()
        {
            // Load configuration from TestData.json
            ConfigReader.LoadConfig();

            // Initialize WebDriver using DriverFactory
            driver = DriverFactory.CreateDriver();
            driver.Manage().Window.Maximize();

            // Initialize ExtentReports
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void Teardown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            var message = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)? "" : string.Format("{0}", TestContext.CurrentContext.Result.Message);

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                    string reportsDir = Path.Combine(projectRoot, "reports");
                    Directory.CreateDirectory(reportsDir);

                    string rawName = TestContext.CurrentContext.Test.Name;
                    foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    {
                        rawName = rawName.Replace(c, '_');
                    }

                    string filePath = Path.Combine(reportsDir, $"{rawName}.png");
                    screenshot.SaveAsFile(filePath);

                    test?.Fail($"Test failed: {message}")
                        .AddScreenCaptureFromPath(filePath)
                        .Fail($"Stacktrace: {stacktrace}");
                        
                }
                catch (Exception ex)
                {
                    test?.Fail($"Screenshot failed: {ex.Message}");
                }
            }

            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test?.Pass("Test passed");
            }

            else
            {
                test?.Skip("Test skipped");
            }

            driver?.Quit();
            driver?.Dispose();
        }
    }
}