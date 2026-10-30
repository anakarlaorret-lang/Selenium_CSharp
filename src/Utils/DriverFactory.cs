using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace DemoTest.Utils
{
    /// <summary>
    /// DriverFactory centralizes WebDriver creation.
    /// It allows switching between browsers easily.
    /// </summary>
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver(string browser = "chrome")
        {
            return browser.Trim().ToLowerInvariant() switch
            {
                "chrome" => new ChromeDriver(),
                "firefox" => new FirefoxDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException(
                    $"Unsupported browser '{browser}'. Supported browsers: chrome, firefox, edge.",
                    nameof(browser))
            };
        }
    }
}