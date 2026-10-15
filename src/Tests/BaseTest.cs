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
             // Captura evidencia si el test falla
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    string filePath = Path.Combine("reports", $"{TestContext.CurrentContext.Test.Name}.png");
                    screenshot.SaveAsFile(filePath);
                }
                catch
                {
                    // Avoid an issue/error on the screenshot blocks the teardown process.
                }
            }

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Explicit dispose to satisfy NUnit analyzer
            }
        }
    }
}