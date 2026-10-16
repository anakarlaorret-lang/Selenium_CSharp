using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoTest.Pages
{
    public class SauceLoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // Constructor
        public SauceLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locators
        private By UsernameField => By.Id("user-name");
        private By PasswordField => By.Id("password");
        private By LoginButton => By.Id("login-button");
        private By ErrorMessage => By.CssSelector("h3[data-test='error']");

        // Actions
        public void NavigateToLogin()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void EnterUsername(string username)
        {
            driver.FindElement(UsernameField).Clear();
            driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(PasswordField).Clear();
            driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClickLogin()
        {
            driver.FindElement(LoginButton).Click();
        }

        // Combined action
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        // Validations
        public bool IsLoggedIn()
        {
            wait.Until(d => d.Url.Contains("inventory.html"));
            return driver.Url.Contains("inventory.html");
        }

        public string GetErrorMessage()
        {
            return driver.FindElement(ErrorMessage).Text;
        }
    }
}