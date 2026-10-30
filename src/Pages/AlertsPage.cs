using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DemoTest.Pages
{
    public class AlertsPage : BasePage
    {
        private readonly By AlertsCardLocator = By.CssSelector("a[href='/alertsWindows']");
        private readonly By AlertsLinkLocator = By.CssSelector("a[href='/alerts']");
        private readonly By AlertButtonLocator = By.Id("alertButton");

        public AlertsPage(IWebDriver driver) : base(driver) { }

        public void NavigateToAlertsPage()
        {
            NavigateToCard(AlertsCardLocator, AlertsLinkLocator, 10);
            WaitForElement(AlertsLinkLocator, 10).Click();
            WaitForElement(AlertButtonLocator, 10);
        }

        public void TriggerButtonAlert() => WaitForElement(AlertButtonLocator).Click();

        public string AcceptButtonAlert()
        {
            var alert = new WebDriverWait(Driver, TimeSpan.FromSeconds(5))
                .Until(ExpectedConditions.AlertIsPresent());
            var text = alert.Text;
            alert.Accept();
            return text;
        }
    }
}
