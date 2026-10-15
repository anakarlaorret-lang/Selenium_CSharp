using NUnit.Framework;
using DemoTest.Pages;
using DemoTest.Utils;

namespace DemoTest.Tests
{
    [TestFixture]
    public class HomePageTests : BaseTest
    {
        [Test]
        public void VerifyHomePageTitle()
        {
            driver.Navigate().GoToUrl(ConfigReader.GetBaseUrl());
            var homePage = new HomePage(driver);

            string title = homePage.GetHomeTitle();
            Assert.That(title, Does.Contain(ConfigReader.GetHomeTitle()), 
                "Home page title should contain company name.");
        }

        [Test]
        public void VerifyMainBannerIsVisible()
        {
            driver.Navigate().GoToUrl(ConfigReader.GetBaseUrl());
            var homePage = new HomePage(driver);

            Assert.That(homePage.IsMainBannerVisible(), Is.True, 
                "Main banner should be visible on Home page.");
        }
   
        [Test]
        public void VerifyNavigationToCareersPage()
        {
            driver.Navigate().GoToUrl(ConfigReader.GetBaseUrl());
            var homePage = new HomePage(driver);

            homePage.ClickCareersLink();
            Assert.That(driver.Url, Is.EqualTo(ConfigReader.GetCareersUrl()), 
                "Careers link should navigate to Careers page.");
        }
    }
}
