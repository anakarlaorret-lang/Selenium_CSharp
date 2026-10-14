using NUnit.Framework;
using OpenQA.Selenium;  
using DemoTest.Utils;
using OpenQA.Selenium.Chrome;

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
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Explicit dispose to satisfy NUnit analyzer
            }
        }
    }
}