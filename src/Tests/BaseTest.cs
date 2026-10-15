using NUnit.Framework;
using OpenQA.Selenium;  
using DemoTest.Utils;

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

        [SetUp]
        public void Setup()
        {
            // Load configuration from TestData.json
            ConfigReader.LoadConfig();

            // Initialize WebDriver using DriverFactory
            driver = DriverFactory.CreateDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
public void Teardown()
{
    if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
    {
        try
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            string reportsDir = Path.Combine(AppContext.BaseDirectory, "reports");
            Directory.CreateDirectory(reportsDir); // Ensure folder exists

            string filePath = Path.Combine(reportsDir, $"{TestContext.CurrentContext.Test.Name}.png");
            screenshot.SaveAsFile(filePath);

            TestContext.AddTestAttachment(filePath, "Screenshot on failure");
        }
        catch
        {
            // Avoid blocking teardown if screenshot fails
        }
    }

    driver?.Quit();
    driver?.Dispose();
}
    }
}