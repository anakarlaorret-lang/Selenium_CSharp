using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace DemoTest.Pages
{   
    public class FormsPage : BasePage
    {
        public FormsPage(IWebDriver driver) : base(driver) { }
        
        // Locators
        private readonly By FirstNameLocator = By.Id("firstName");
        private readonly By LastNameLocator = By.Id("lastName");
        private readonly By EmailLocator = By.Id("userEmail");
        private readonly By GenderMaleLocator = By.Id("gender-radio-1");
        private readonly By PhoneLocator = By.Id("userNumber");
        private readonly By SubmitButtonLocator = By.Id("submit");
        private readonly By SuccessModalLocator = By.Id("example-modal-sizes-title-lg");
        private readonly By BannerLocator = By.CssSelector("img.banner-image[alt*='Selenium']");
        private readonly By FormCardLocator = By.CssSelector("a[href='/forms']");
        private readonly By PracticeFormLinkLocator = By.CssSelector("a[href='/automation-practice-form']");

        // Métodos
        public void FillForm(string firstName, string lastName, string email, string phone)
        {
            var firstNameField = WaitForElement(FirstNameLocator);
            firstNameField.Clear();
            firstNameField.SendKeys(firstName);

            var lastNameField = WaitForElement(LastNameLocator);
            lastNameField.Clear();
            lastNameField.SendKeys(lastName);

            var emailField = WaitForElement(EmailLocator);
            emailField.Clear();
            emailField.SendKeys(email);

            var genderMaleRadio = WaitForElement(GenderMaleLocator);
            genderMaleRadio.Click();

            var phoneField = WaitForElement(PhoneLocator);
            phoneField.Clear();
            phoneField.SendKeys(phone);
        }

        public void SubmitForm()
        {
            var submitButton = WaitForElement(SubmitButtonLocator, 10);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", submitButton);
        }

        public string GetSuccessMessage() => WaitForElement(SuccessModalLocator).Text;

        public bool IsEmailInvalid()
        {
            var emailField = WaitForElement(EmailLocator);
            return !(bool)((IJavaScriptExecutor)Driver)
                .ExecuteScript("return arguments[0].checkValidity();", emailField);
        }

        public bool IsBannerVisible()
        {
            try
            {
                var banner = WaitForElement(BannerLocator, 10);
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", banner);
                return banner.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsFormLoaded()
        {
            try
            {
                var firstNameField = WaitForElement(FirstNameLocator, 10);
                return firstNameField.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        
        public void GoToFormsPage()
        {
            // Usa el método genérico de BasePage con locators
            NavigateToCard(FormCardLocator, PracticeFormLinkLocator, 10);

            // Click en el link interno
            var practiceFormLink = WaitForElement(PracticeFormLinkLocator, 10);
            practiceFormLink.Click();

            // Validación final
            WaitForElement(FirstNameLocator, 10);
        }
    }
}
