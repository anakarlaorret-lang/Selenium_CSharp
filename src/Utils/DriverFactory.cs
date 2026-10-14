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
            return browser.ToLower() switch
            {
                "chrome" => CreateChromeDriver(),
                "firefox" => new FirefoxDriver(),
                "edge" => new EdgeDriver(),
                _ => CreateChromeDriver()
            };
        }

        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); 
            return new ChromeDriver(options);
        }
    }
}