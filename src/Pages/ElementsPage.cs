using OpenQA.Selenium;
using System;
using System.Linq;

namespace DemoTest.Pages
{
    public class ElementsPage : BasePage
    {
        public ElementsPage(IWebDriver driver) : base(driver) { }
        
        private readonly By ElementsCardLocator = By.CssSelector("a[href='/elements']");
        private readonly By TextBoxLinkLocator = By.CssSelector("a[href='/text-box']");
        private readonly By CheckBoxLinkLocator = By.CssSelector("a[href='/checkbox']");
        private readonly By RadioButtonLinkLocator = By.CssSelector("a[href='/radio-button']");
        private readonly By WebTablesLinkLocator = By.CssSelector("a[href='/webtables']");
        private readonly By ButtonsLinkLocator = By.CssSelector("a[href='/buttons']");
        private readonly By LinksLinkLocator = By.CssSelector("a[href='/links']");
        private readonly By CheckBoxTreeLocator = By.CssSelector("[role='tree'] [role='treeitem']");
        private readonly By YesRadioLabelLocator = By.CssSelector("label[for='yesRadio']");
        // --- Text Box ---
        private readonly By FullNameLocator = By.Id("userName");
        public IWebElement FullName => WaitForElement(FullNameLocator);
        private IWebElement Email => WaitForElement(By.CssSelector("input#userEmail"));
        private IWebElement CurrentAddress => WaitForElement(By.Id("currentAddress"));
        private IWebElement PermanentAddress => WaitForElement(By.Id("permanentAddress"));
        private IWebElement SubmitTextBox => WaitForElement(By.CssSelector("button#submit"));
        private IWebElement OutputBox => WaitForElement(By.CssSelector("div#output"));


        public void NavigateToTextBoxPage()
        {
            NavigateToElementsSection(TextBoxLinkLocator, FullNameLocator);
        }

        public void NavigateToCheckBoxPage()
        {
            NavigateToElementsSection(CheckBoxLinkLocator, CheckBoxTreeLocator);
        }

        public void NavigateToRadioButtonPage()
        {
            NavigateToElementsSection(RadioButtonLinkLocator, YesRadioLabelLocator);
        }

        public void NavigateToWebTablesPage()
        {
            NavigateToElementsSection(WebTablesLinkLocator, By.Id("addNewRecordButton"));
        }

        public void NavigateToButtonsPage()
        {
            NavigateToElementsSection(ButtonsLinkLocator, By.Id("doubleClickBtn"));
        }

        public void NavigateToLinksPage()
        {
            NavigateToElementsSection(LinksLinkLocator, By.LinkText("Created"));
        }

        private void NavigateToElementsSection(By sectionLinkLocator, By sectionReadyLocator)
        {
            NavigateToCard(ElementsCardLocator, sectionLinkLocator, 10);
            WaitForElement(sectionLinkLocator, 10).Click();
            WaitForElement(sectionReadyLocator, 10);
        }

        public void FillTextBox(string name, string email, string currentAddress, string permanentAddress)
        {
            FullName.Clear();
            FullName.SendKeys(name);

            Email.Clear();
            Email.SendKeys(email);

            CurrentAddress.Clear();
            CurrentAddress.SendKeys(currentAddress);

            PermanentAddress.Clear();
            PermanentAddress.SendKeys(permanentAddress);
        }

        public void SubmitTextBoxForm()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", SubmitTextBox);
            SubmitTextBox.Click();
        }

        public string GetOutputText() => OutputBox.Text;

        public bool IsEmailInvalid()
        {
            return !(bool)((IJavaScriptExecutor)Driver)
                .ExecuteScript("return arguments[0].checkValidity();", Email);
        }

        // --- Check Box ---
        private By CheckBoxTreeItemLocator(string name) =>
            By.XPath($"//*[@role='treeitem'][.//*[@role='checkbox' and @aria-label='Select {name}']]");

        public void ExpandCheckBoxNode(string name)
        {
            var treeItem = WaitForElement(CheckBoxTreeItemLocator(name));
            if (treeItem.GetAttribute("aria-expanded") != "true")
            {
                treeItem.FindElement(By.CssSelector(".rc-tree-switcher")).Click();
            }
        }

        public void SelectCheckBox(string name)
        {
            var treeItem = WaitForElement(CheckBoxTreeItemLocator(name));
            treeItem.FindElement(By.CssSelector("[role='checkbox']")).Click();
        }

        public bool IsCheckBoxSelected(string name)
        {
            var treeItem = WaitForElement(CheckBoxTreeItemLocator(name));
            return treeItem.FindElement(By.CssSelector("[role='checkbox']"))
                .GetAttribute("aria-checked") == "true";
        }

        public string GetCheckBoxResult() => WaitForElement(By.Id("result")).Text;

        // --- Radio Button ---
        public void SelectRadioButton(string option)
        {
            var radioId = GetRadioButtonId(option);
            WaitForElement(By.CssSelector($"label[for='{radioId}']")).Click();
        }

        public bool IsRadioButtonSelected(string option)
        {
            return Driver.FindElement(By.Id(GetRadioButtonId(option))).Selected;
        }

        public bool IsRadioButtonEnabled(string option) =>
            Driver.FindElement(By.Id(GetRadioButtonId(option))).Enabled;

        public string GetRadioResult() =>
            WaitForElement(By.ClassName("text-success")).FindElement(By.XPath("..")).Text;

        private static string GetRadioButtonId(string option) => option switch
        {
            "Yes" => "yesRadio",
            "No" => "noRadio",
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, "Unsupported radio button option.")
        };

        // --- Web Tables ---
        public void AddNewRecord()
        {
            WaitForElement(By.Id("addNewRecordButton")).Click();
            FillRecordField("firstName", "Automation");
            FillRecordField("lastName", "Portfolio");
            FillRecordField("userEmail", "automation.portfolio@example.com");
            FillRecordField("age", "30");
            FillRecordField("salary", "75000");
            FillRecordField("department", "Quality Engineering");
            WaitForElement(By.Id("submit")).Click();
        }

        public void EditRecord(string currentFirstName, string updatedFirstName)
        {
            var row = FindTableRow(currentFirstName);
            var editButton = row.FindElement(By.CssSelector("[title='Edit']"));
            ScrollToStableElement(editButton);
            editButton.Click();
            var firstNameField = WaitForElement(By.Id("firstName"));
            firstNameField.Clear();
            firstNameField.SendKeys(updatedFirstName);
            WaitForElement(By.Id("submit")).Click();
        }

        public void DeleteRecord(string firstName)
        {
            var deleteButton = FindTableRow(firstName).FindElement(By.CssSelector("[title='Delete']"));
            ScrollToStableElement(deleteButton);
            deleteButton.Click();
        }

        public bool TableContains(string value)
        {
            var table = Driver.FindElements(By.TagName("table")).FirstOrDefault();
            if (table == null)
            {
                throw new NoSuchElementException(
                    $"The table was not found on {Driver.Url}.");
            }

            return table.Text.Contains(value, StringComparison.OrdinalIgnoreCase);
        }

        public bool TableContainsCellValue(string value)
        {
            return Driver.FindElements(By.CssSelector("table tbody td"))
                .Any(cell => string.Equals(
                    cell.Text.Trim(),
                    value,
                    StringComparison.OrdinalIgnoreCase));
        }

        private IWebElement FindTableRow(string firstName)
        {
            return Driver.FindElements(By.CssSelector("table tbody tr"))
                .FirstOrDefault(row => row.Text.Contains(firstName, StringComparison.OrdinalIgnoreCase))
                ?? throw new NoSuchElementException(
                    $"No table row was found for '{firstName}' on {Driver.Url}.");
        }

        private void FillRecordField(string fieldId, string value)
        {
            var field = WaitForElement(By.Id(fieldId));
            field.Clear();
            field.SendKeys(value);
        }

        // --- Buttons ---
        public void DoubleClickButton()
        {
            var button = Driver.FindElement(By.Id("doubleClickBtn"));
            ((IJavaScriptExecutor)Driver).ExecuteAsyncScript(
                "const button = arguments[0]; " +
                "const done = arguments[arguments.length - 1]; " +
                "button.scrollIntoView({ block: 'center' }); " +
                "let previousPosition; " +
                "let stableFrames = 0; " +
                "const waitForStablePosition = () => { " +
                "const rect = button.getBoundingClientRect(); " +
                "const position = [rect.left, rect.top, rect.width, rect.height].join(':'); " +
                "stableFrames = position === previousPosition ? stableFrames + 1 : 0; " +
                "previousPosition = position; " +
                "if (stableFrames >= 3) done(); " +
                "else requestAnimationFrame(waitForStablePosition); " +
                "}; " +
                "requestAnimationFrame(waitForStablePosition);",
                button);
            new OpenQA.Selenium.Interactions.Actions(Driver)
                .DoubleClick(button)
                .Perform();
        }

        public void DynamicClickButton() =>
            WaitForElement(By.XPath("//button[normalize-space()='Click Me']")).Click();

        public string GetButtonResult(string interaction)
        {
            var resultId = interaction switch
            {
                "double click" => "doubleClickMessage",
                "dynamic click" => "dynamicClickMessage",
                _ => throw new ArgumentOutOfRangeException(nameof(interaction), interaction, "Unsupported button interaction.")
            };
            return WaitForElement(By.Id(resultId)).Text;
        }

        // --- Links ---
        public void ClickLink(string linkName)
        {
            if (linkName is not ("Created" or "Bad Request"))
            {
                throw new ArgumentOutOfRangeException(nameof(linkName), linkName, "Supported links are Created and Bad Request.");
            }

            WaitForElement(By.LinkText(linkName)).Click();
        }

        public string GetLinkResponse() => WaitForElement(By.Id("linkResponse")).Text;
    }
}