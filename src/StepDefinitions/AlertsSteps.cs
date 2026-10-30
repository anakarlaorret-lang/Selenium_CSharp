using DemoTest.Pages;
using DemoTest.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DemoTest.Steps
{
    [Binding]
    public sealed class AlertsSteps
    {
        private readonly IWebDriver driver;
        private readonly AlertsPage alertsPage;
        private string? acceptedAlertText;

        public AlertsSteps(IWebDriver driver)
        {
            this.driver = driver;
            alertsPage = new AlertsPage(driver);
        }

        [Given(@"the DemoQA site is accessible")]
        public void GivenTheDemoQASiteIsAccessible()
        {
            var baseUrl = ConfigReader.GetBaseUrl().TrimEnd('/');
            Assert.That(driver.Url.TrimEnd('/'), Is.EqualTo(baseUrl),
                "The browser should start on the configured DemoQA base URL.");
        }

        [Given(@"the user is on the Alerts page")]
        public void GivenTheUserIsOnTheAlertsPage()
        {
            alertsPage.NavigateToAlertsPage();
            Assert.That(driver.Url, Does.EndWith("/alerts"));
        }

        [When(@"the user triggers an alert")]
        public void WhenTheUserTriggersAnAlert()
        {
            alertsPage.TriggerButtonAlert();
        }

        [When(@"the user accepts the alert")]
        public void WhenTheUserAcceptsTheAlert()
        {
            acceptedAlertText = alertsPage.AcceptButtonAlert();
        }

        [Then(@"the alert should be handled successfully")]
        public void ThenTheAlertShouldBeHandledSuccessfully()
        {
            Assert.That(acceptedAlertText, Is.EqualTo("You clicked a button"),
                "The browser alert should display the expected message before it is accepted.");
            Assert.That(driver.Url, Does.EndWith("/alerts"),
                "Accepting the alert should leave the browser on the Alerts page.");
        }
    }
}