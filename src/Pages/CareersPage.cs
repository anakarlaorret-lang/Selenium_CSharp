using OpenQA.Selenium;

namespace DemoTest.Pages
{
    public class CareersPage : BasePage
    {
        #region Locators
        private readonly By jobTitle = By.CssSelector("h2.job-title");
        private readonly By careersMenuLink = By.CssSelector("a.active-link[href='/careers']");
        private readonly By openPositionsSection = By.Id("open-positions");
        private readonly By jobListingsLocator = By.CssSelector(".job-listing");

        #endregion

        public CareersPage(IWebDriver driver) : base(driver) {}

        public string GetFirstJobTitle()
        {
            return driver.FindElement(jobTitle).Text;
        }
        public bool IsCareersMenuActive()
        {
            var element = driver.FindElement(careersMenuLink);
            return element.Displayed && element.GetAttribute("class").Contains("active-link");
        }

        public bool IsOpenPositionsVisible()
        {
            return driver.FindElement(openPositionsSection).Displayed;
        }

        public IList<IWebElement> GetJobListings()
        {
            return driver.FindElements(jobListingsLocator);
        }
    }
}