using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoTest.Pages
{
    /// <summary>
    /// BasePage provides common methods and utilities
    /// for all page objects in the project.
    /// Every page class should inherit from this class.
    /// </summary>
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        /// <summary>
        /// Constructor initializes WebDriver and WebDriverWait.
        /// </summary>
        /// <param name="driver">The WebDriver instance</param>
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Navigate to a specific URL.
        /// </summary>
        /// <param name="url">Target URL</param>
        public void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Find an element using explicit wait.
        /// </summary>
        /// <param name="locator">Locator strategy</param>
        /// <returns>IWebElement</returns>
        protected IWebElement FindElement(By locator)
        {
            return wait.Until(drv => drv.FindElement(locator));
        }

        /// <summary>
        /// Click on an element.
        /// </summary>
        /// <param name="locator">Locator strategy</param>
        protected void Click(By locator)
        {
            FindElement(locator).Click();
        }

        /// <summary>
        /// Enter text into an input field.
        /// </summary>
        /// <param name="locator">Locator strategy</param>
        /// <param name="text">Text to input</param>
        protected void EnterText(By locator, string text)
        {
            var element = FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Get the text of an element.
        /// </summary>
        /// <param name="locator">Locator strategy</param>
        /// <returns>Element text</returns>
        protected string GetText(By locator)
        {
            return FindElement(locator).Text;
        }

        /// <summary>
        /// Check if an element is visible.
        /// </summary>
        /// <param name="locator">Locator strategy</param>
        /// <returns>True if visible, false otherwise</returns>
        protected bool IsElementVisible(By locator)
        {
            try
            {
                return FindElement(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}