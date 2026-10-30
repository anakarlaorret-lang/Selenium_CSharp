using DemoTest.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DemoTest.Steps
{
    [Binding]
    public sealed class WidgetsSteps
    {
        private readonly WidgetsPage widgetsPage;

        public WidgetsSteps(IWebDriver driver)
        {
            widgetsPage = new WidgetsPage(driver);
        }

        [Given(@"the user is on the Date Picker page")]
        public void GivenTheUserIsOnTheDatePickerPage()
        {
            widgetsPage.NavigateToDatePickerPage();
            Assert.That(widgetsPage.Driver.Url, Does.EndWith("/date-picker"));
        }

        [When(@"the user selects February 15, 2020")]
        public void WhenTheUserSelectsFebruary15th2020()
        {
            widgetsPage.SelectFebruary15th2020();
        }

        [Then(@"the selected date should be ""([^""]*)""")]
        public void ThenTheSelectedDateShouldBe(string expectedDate)
        {
            Assert.That(widgetsPage.GetSelectedDate(), Is.EqualTo(expectedDate));
        }

        [Given(@"the user is on the Slider page")]
        public void GivenTheUserIsOnTheSliderPage()
        {
            widgetsPage.NavigateToSliderPage();
            Assert.That(widgetsPage.Driver.Url, Does.EndWith("/slider"));
        }

        [When(@"the user sets the slider to its maximum")]
        public void WhenTheUserSetsTheSliderToItsMaximum()
        {
            widgetsPage.SetSliderToMaximum();
        }

        [Then(@"the slider value should be ""([^""]*)""")]
        public void ThenTheSliderValueShouldBe(string expectedValue)
        {
            Assert.That(widgetsPage.GetSliderValue(), Is.EqualTo(expectedValue));
        }
    }
}