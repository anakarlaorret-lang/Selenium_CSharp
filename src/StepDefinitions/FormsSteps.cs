using TechTalk.SpecFlow;
using NUnit.Framework;

namespace DemoTest.Pages
{
    [Binding]
    public class FormsSteps
    {
        private readonly FormsPage formsPage;

        public FormsSteps(FormsPage formsPage)
        {
            this.formsPage = formsPage;
        }

        [Given(@"the Selenium banner should be visible")]
        public void GivenTheSeleniumBannerShouldBeVisible()
        {
            Assert.That(formsPage.IsBannerVisible(), Is.True,
                "The DemoQA Selenium banner should be visible.");
        }

        [Given(@"the user is on the Forms page")]
        public void GivenTheUserIsOnTheFormsPage()
        {
            formsPage.GoToFormsPage();
            Assert.That(formsPage.IsFormLoaded(), Is.True, "The practice form should be loaded.");
        }

        [When(@"submits the form")]
        public void WhenSubmitsTheForm()
        {
            formsPage.SubmitForm();
        }
        [When(@"the user fills the form with valid data")]
        public void WhenTheUserFillsTheFormWithValidData()
        {
            var data = TestData.LoadData().validForm;
            formsPage.FillForm(
                (string)data.firstName,
                (string)data.lastName,
                (string)data.email,
                (string)data.phone
            );
        }

        [When(@"the user fills the form with invalid data")]
        public void WhenTheUserFillsTheFormWithInvalidData()
        {
            var data = TestData.LoadData().invalidForm;
            formsPage.FillForm(
                (string)data.firstName,
                (string)data.lastName,
                (string)data.email,
                (string)data.phone
            );
        }

        [Then(@"a success message should be displayed")]
        public void ThenASuccessMessageShouldBeDisplayed()
        {
            Assert.That(formsPage.GetSuccessMessage(), Does.Contain("Thanks for submitting the form"),
                "The successful submission confirmation should be displayed.");
        }

        [Scope(Feature = "DemoQA Forms")]
        [Then(@"the email field should be highlighted as error")]
        public void ThenTheEmailFieldShouldBeHighlightedAsError()
        {
            Assert.That(formsPage.IsEmailInvalid(), Is.True,
                "The browser should reject the malformed email address.");
        }
    }
}
