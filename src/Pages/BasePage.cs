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
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "arguments[0].scrollIntoView({ block: 'center', inline: 'nearest' });",
                element);
            return element;
        }

        protected void ScrollToStableElement(
            IWebElement element,
            string blockAlignment = "center",
            string inlineAlignment = "nearest")
        {
            ((IJavaScriptExecutor)Driver).ExecuteAsyncScript(
                "const element = arguments[0]; " +
                "const done = arguments[arguments.length - 1]; " +
                "element.scrollIntoView({ block: arguments[1], inline: arguments[2] }); " +
                "let previousPosition; " +
                "let stableFrames = 0; " +
                "const waitForStablePosition = () => { " +
                "const rect = element.getBoundingClientRect(); " +
                "const position = [rect.left, rect.top, rect.width, rect.height].join(':'); " +
                "stableFrames = position === previousPosition ? stableFrames + 1 : 0; " +
                "previousPosition = position; " +
                "if (stableFrames >= 3) done(); " +
                "else requestAnimationFrame(waitForStablePosition); " +
                "}; " +
                "requestAnimationFrame(waitForStablePosition);",
                element,
                blockAlignment,
                inlineAlignment);
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
