using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DemoTest.Pages;

namespace DemoTest.Tests.FunctionalTests_UI
{
    [TestFixture]
    public class SauceTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Teardown()
        {
            driver.Dispose();
            driver.Quit();
        }

        [TestCase("standard_user", "secret_sauce", true, TestName = "Valid Login")]
        [TestCase("locked_out_user", "secret_sauce", false, TestName = "Locked Out User")]
        [TestCase("standard_user", "wrong_password", false, TestName = "Invalid Password")]
        [TestCase("", "", false, TestName = "Empty Credentials")]
        public void SauceDemoLoginTests(string username, string password, bool expectedResult)
        {
            var saucePage = new SauceLoginPage(driver);
            saucePage.NavigateToLogin();
            saucePage.Login(username, password);

            if (expectedResult)
            {
                Assert.IsTrue(saucePage.IsLoggedIn(),
                    "User should be redirected to inventory page after valid login.");
            }
            else
            {
                Assert.IsNotNull(saucePage.GetErrorMessage(),
                    "Error message should be displayed for invalid login.");
            }
        }
    }
}