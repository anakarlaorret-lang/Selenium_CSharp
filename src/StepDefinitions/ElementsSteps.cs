using TechTalk.SpecFlow;
using NUnit.Framework;
using DemoTest.Pages;
using OpenQA.Selenium;

namespace DemoTest.Steps
{
    [Binding]
    public class ElementsSteps
    {
        private readonly ElementsPage elementsPage;

        public ElementsSteps(IWebDriver driver)
        {
            elementsPage = new ElementsPage(driver);
        }

        // --- Given ---
        [Given(@"the user is on the Text Box page")]
        public void GivenTheUserIsOnTheTextBoxPage()
        {
            elementsPage.NavigateToTextBoxPage();
            Assert.That(elementsPage.Driver.Url, Does.EndWith("/text-box"));
            Assert.That(elementsPage.FullName.Displayed, Is.True,
                "The full-name field should be visible after navigation.");
        }

        [Given(@"the user is on the Check Box page")]
        public void GivenTheUserIsOnTheCheckBoxPage()
        {
            elementsPage.NavigateToCheckBoxPage();
            Assert.That(elementsPage.Driver.Url, Does.EndWith("/checkbox"));
        }

        [Given(@"the user is on the Radio Button page")]
        public void GivenTheUserIsOnTheRadioButtonPage()
        {
            elementsPage.NavigateToRadioButtonPage();
            Assert.That(elementsPage.Driver.Url, Does.EndWith("/radio-button"));
        }

        [Given(@"the user is on the Web Tables page")]
        public void GivenTheUserIsOnTheWebTablesPage()
        {
            elementsPage.NavigateToWebTablesPage();
            Assert.That(elementsPage.Driver.Url, Does.EndWith("/webtables"));
        }

        [Given(@"the user is on the Buttons page")]
        public void GivenTheUserIsOnTheButtonsPage()
        {
            elementsPage.NavigateToButtonsPage();
            Assert.That(elementsPage.Driver.Url, Does.EndWith("/buttons"));
        }

        [Given(@"the user is on the Links page")]
        public void GivenTheUserIsOnTheLinksPage()
        {
            elementsPage.NavigateToLinksPage();
            Assert.That(elementsPage.Driver.Url, Does.EndWith("/links"));
        }

        // --- Text Box ---
        [When(@"the user fills the text box with valid data")]
        public void WhenTheUserFillsTheTextBoxWithValidData()
        {
            elementsPage.FillTextBox("Ana QA", "ana.qa@test.com", "Montevideo", "Uruguay");
        }

        [When(@"the user fills the text box with an invalid email")]
        public void WhenTheUserFillsTheTextBoxWithAnInvalidEmail()
        {
            elementsPage.FillTextBox("Ana QA", "invalid-email", "Montevideo", "Uruguay");
        }

        [When(@"clicks the submit button")]
        public void WhenClicksTheSubmitButton()
        {
            elementsPage.SubmitTextBoxForm();
        }

        [Then(@"the output should display the entered data")]
        public void ThenTheOutputShouldDisplayTheEnteredData()
        {
            var output = elementsPage.GetOutputText();
            Assert.That(output, Does.Contain("Ana QA"), "The submitted name should appear in the output.");
            Assert.That(output, Does.Contain("ana.qa@test.com"), "The submitted email should appear in the output.");
            Assert.That(output, Does.Contain("Montevideo"), "The submitted current address should appear in the output.");
            Assert.That(output, Does.Contain("Uruguay"), "The submitted permanent address should appear in the output.");
        }

        [Then(@"the email field should be highlighted as error")]
        public void ThenTheEmailFieldShouldBeHighlightedAsError()
        {
            Assert.That(elementsPage.IsEmailInvalid(), Is.True,
                "The browser should reject the malformed email address.");
        }

        // --- Check Box ---
        [When(@"the user expands the tree and selects the ""([^""]*)"" checkbox")]
        public void WhenTheUserExpandsTheTreeAndSelectsASingleCheckBox(string checkbox)
        {
            elementsPage.ExpandCheckBoxNode("Home");
            elementsPage.ExpandCheckBoxNode("Desktop");
            elementsPage.SelectCheckBox(checkbox);
        }

        [When(@"the user expands the tree and selects ""([^""]*)"" and ""([^""]*)""")]
        public void WhenTheUserExpandsTheTreeAndSelectsTwoCheckBoxes(string firstCheckbox, string secondCheckbox)
        {
            elementsPage.ExpandCheckBoxNode("Home");
            elementsPage.SelectCheckBox(firstCheckbox);
            elementsPage.SelectCheckBox(secondCheckbox);
        }

        [Then(@"the selected checkbox should be ""([^""]*)""")]
        public void ThenTheSelectedCheckBoxShouldBe(string checkbox)
        {
            Assert.That(elementsPage.IsCheckBoxSelected(checkbox), Is.True,
                $"Expected checkbox '{checkbox}' to be selected.");
            Assert.That(elementsPage.IsCheckBoxSelected("Commands"), Is.False,
                "Selecting a leaf checkbox should not select its sibling.");
            var selectedItems = elementsPage.GetCheckBoxResult().ToLowerInvariant()
                .Split(new[] { ' ', '\r', '\n', ':', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(selectedItems, Does.Contain(checkbox.ToLowerInvariant()),
                $"Expected the result to include '{checkbox}'.");
        }

        [Then(@"the selected checkboxes should include ""([^""]*)"" and ""([^""]*)""")]
        public void ThenTheSelectedCheckBoxesShouldInclude(string firstCheckbox, string secondCheckbox)
        {
            var result = elementsPage.GetCheckBoxResult().ToLowerInvariant()
                .Split(new[] { ' ', '\r', '\n', ':', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(elementsPage.IsCheckBoxSelected(firstCheckbox), Is.True,
                $"Expected checkbox '{firstCheckbox}' to be selected.");
            Assert.That(elementsPage.IsCheckBoxSelected(secondCheckbox), Is.True,
                $"Expected checkbox '{secondCheckbox}' to be selected.");
            Assert.That(result, Does.Contain(firstCheckbox.ToLowerInvariant()));
            Assert.That(result, Does.Contain(secondCheckbox.ToLowerInvariant()));
            Assert.That(elementsPage.IsCheckBoxSelected("Downloads"), Is.False,
                "The unrelated Downloads checkbox should remain unselected.");
        }

        [When(@"the user selects the ""([^""]*)"" radio button")]
        public void WhenTheUserSelectsTheRadioButton(string option)
        {
            elementsPage.SelectRadioButton(option);
        }

        [Then(@"the radio result should be ""([^""]*)""")]
        public void ThenTheRadioResultShouldBe(string expectedResult)
        {
            Assert.That(elementsPage.IsRadioButtonSelected("Yes"), Is.True,
                "Expected the Yes radio button to be selected.");
            Assert.That(elementsPage.IsRadioButtonSelected("No"), Is.False,
                "The mutually exclusive No radio button should remain unselected.");
            Assert.That(elementsPage.GetRadioResult(), Is.EqualTo(expectedResult));
        }

        [When(@"the user tries to select the ""([^""]*)"" radio button")]
        public void WhenTheUserTriesToSelectTheRadioButton(string option)
        {
            elementsPage.SelectRadioButton(option);
        }

        [Then(@"the radio button should be disabled")]
        public void ThenTheRadioButtonShouldBeDisabled()
        {
            Assert.That(elementsPage.IsRadioButtonEnabled("No"), Is.False,
                "Expected the No radio button to be disabled.");
            Assert.That(elementsPage.IsRadioButtonSelected("No"), Is.False,
                "A disabled radio button must not become selected.");
        }

        // --- Web Tables ---
        [When(@"the user adds a new record")]
        public void WhenTheUserAddsANewRecord()
        {
            elementsPage.AddNewRecord();
        }

        [Then(@"the record should appear in the table")]
        public void ThenTheRecordShouldAppearInTheTable()
        {
            Assert.That(elementsPage.TableContains("Automation"), Is.True,
                "The newly added record should be visible in the table.");
            Assert.That(elementsPage.TableContains("automation.portfolio@example.com"), Is.True,
                "The newly added record should include its email.");
        }

        [When(@"the user edits the record for ""([^""]*)"" with a new first name")]
        public void WhenTheUserEditsTheRecordWithANewFirstName(string firstName)
        {
            elementsPage.EditRecord(firstName, "Updated QA");
        }

        [Then(@"the updated record should appear in the table")]
        public void ThenTheUpdatedRecordShouldAppearInTheTable()
        {
            Assert.That(elementsPage.TableContainsCellValue("Updated QA"), Is.True,
                "The edited first name should be visible in the table.");
            Assert.That(elementsPage.TableContainsCellValue("Cierra"), Is.False,
                "The original first name should no longer be present.");
        }

        [When(@"the user deletes the record for ""([^""]*)""")]
        public void WhenTheUserDeletesTheRecord(string firstName)
        {
            elementsPage.DeleteRecord(firstName);
        }

        [Then(@"the record should no longer appear in the table")]
        public void ThenTheRecordShouldNoLongerAppearInTheTable()
        {
            Assert.That(elementsPage.TableContains("Alden"), Is.False,
                "The deleted record should no longer be visible in the table.");
        }

        // --- Buttons ---
        [When(@"the user double clicks the button")]
        public void WhenTheUserDoubleClicksTheButton()
        {
            elementsPage.DoubleClickButton();
        }

        [When(@"the user clicks the dynamic button")]
        public void WhenTheUserClicksTheDynamicButton()
        {
            elementsPage.DynamicClickButton();
        }

        [Then(@"the result should display ""([^""]*)""")]
        public void ThenTheResultShouldDisplay(string expectedResult)
        {
            var interaction = expectedResult switch
            {
                "You have done a double click" => "double click",
                "You have done a dynamic click" => "dynamic click",
                _ => throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, "Unsupported button result.")
            };

            Assert.That(elementsPage.GetButtonResult(interaction), Is.EqualTo(expectedResult));
        }

        // --- Links ---
        [When(@"the user clicks the ""([^""]*)"" link")]
        public void WhenTheUserClicksTheLink(string linkName)
        {
            elementsPage.ClickLink(linkName);
        }

        [Then(@"the displayed link response should contain status code (.*)")]
        public void ThenTheDisplayedLinkResponseShouldContainStatusCode(int expectedStatusCode)
        {
            Assert.That(elementsPage.GetLinkResponse(), Does.Contain(expectedStatusCode.ToString()),
                $"The displayed link response should include HTTP status code {expectedStatusCode}.");
        }

    }
}
