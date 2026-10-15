using NUnit.Framework;
using DemoTest.Pages;
using System.Drawing;

namespace DemoTest.Tests
{
    [TestFixture]
    public class ResponsiveTests : BaseTest
    {
        //[TestCase(1366, 768, "desktop")]
        [TestCase(768, 1024, "tablet")]
        [TestCase(375, 667, "mobile")]
        public void VerifyResponsiveLayout(int width, int height, string device)
        {
            driver.Manage().Window.Size = new Size(width, height);

            var homePage = new HomePage(driver);
            driver.Navigate().GoToUrl("https://towersoft.net");

            if (device == "desktop")
            {
                Assert.That(homePage.IsNavBarVisible(), Is.True,
                    "Main navigation should be visible on desktop");
            }
            else
            {
                Assert.That(homePage.IsHamburgerMenuVisible(), Is.True,
                    "Hamburger menu should be visible on mobile");
            }
        }
    }
}