using NUnit.Framework;
using DemoTest.Pages;
using DemoTest.Utils;

namespace DemoTest.Tests
{
    [TestFixture]
    public class CareersPageTests : BaseTest
    {
        [Test]
        public void VerifyCareersMenuIsActive()
        {
            driver.Navigate().GoToUrl(ConfigReader.GetCareersUrl());
            var careersPage = new CareersPage(driver);

            Assert.That(careersPage.IsCareersMenuActive(), Is.True, "Careers menu link should be active");
        }

        [Test]
        public void VerifyOpenPositionsSectionIsVisible()
        {
            driver.Navigate().GoToUrl(ConfigReader.GetCareersUrl());
            var careersPage = new CareersPage(driver);

            bool sectionVisible = careersPage.IsOpenPositionsVisible();
            Assert.That(sectionVisible, Is.True, "Open Positions section should be visible.");
        }
    }
}