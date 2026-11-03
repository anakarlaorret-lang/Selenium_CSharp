using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using DemoTest.Utils;

namespace DemoTest.Pages
{
    public abstract class BasePage
    {
        public readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement WaitForElement(By locator, int timeoutSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return element;
        }

        public void NavigateToCard(By cardSelector, By elementToWait, int timeoutSeconds = 10)
        {
            // Abre la página inicial
            Driver.Navigate().GoToUrl(ConfigReader.GetBaseUrl());
            Driver.Manage().Window.Maximize();

            // Hace clic en el card específico
            var card = WaitForElement(cardSelector, timeoutSeconds);
            card.Click();

            // Espera a que el elemento clave de la siguiente página esté visible
            WaitForElement(elementToWait, timeoutSeconds);
        }
    }
}
