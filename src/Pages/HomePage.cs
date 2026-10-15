using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoTest.Pages
{
    public class HomePage : BasePage
    {
        #region Locators
        
        private readonly By mainBanner = By.XPath("//h1[text()='Engineering dreams']");
        private readonly By careersLink = By.CssSelector("a[href='/careers']");
        private readonly By hamburgerMenu = By.CssSelector(".hamburger-menu");
        private readonly By naviBar = By.CssSelector(".main-nav");
      
        #endregion

        public HomePage(IWebDriver driver) : base(driver) {}

        public void GoToCareers()
        {
            driver.FindElement(careersLink).Click();
        }

        public string GetHomeTitle()
        {
            return driver.Title;
        }

        public bool IsMainBannerVisible()
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var banner = wait.Until(d =>
                {
                    var element = d.FindElement(mainBanner);
                    return (element.Displayed) ? element : null;
                });
                return banner.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        public void ClickCareersLink()
        {
            driver.FindElement(careersLink).Click();
        }
        public bool IsHamburgerMenuVisible()
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var menu = wait.Until(d =>
                {
                    var element = d.FindElement(hamburgerMenu);
                    return (element.Displayed) ? element : null;
                });
                return menu.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsNavBarVisible()
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var navBar = wait.Until(d =>
                {
                    var element = d.FindElement(naviBar);
                    return (element.Displayed) ? element : null;
                });
                return navBar.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
