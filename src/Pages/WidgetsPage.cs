using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoTest.Pages
{
    public class WidgetsPage : BasePage
    {
        private readonly By WidgetsCardLocator = By.CssSelector("a[href='/widgets']");
        private readonly By DatePickerLinkLocator = By.CssSelector("a[href='/date-picker']");
        private readonly By SliderLinkLocator = By.CssSelector("a[href='/slider']");
        private readonly By DatePickerLocator = By.Id("datePickerMonthYearInput");
        private readonly By SliderLocator = By.Id("slider");

        public WidgetsPage(IWebDriver driver) : base(driver) { }

        public void NavigateToDatePickerPage()
        {
            NavigateToWidget(DatePickerLinkLocator, DatePickerLocator);
        }

        public void NavigateToSliderPage()
        {
            NavigateToWidget(SliderLinkLocator, SliderLocator);
        }

        private void NavigateToWidget(By widgetLink, By readyLocator)
        {
            NavigateToCard(WidgetsCardLocator, widgetLink, 10);
            var link = WaitForElement(widgetLink, 10);
            link.SendKeys(Keys.Enter);
            WaitForElement(readyLocator, 10);
        }

        public void SelectFebruary15th2020()
        {
            ClickAfterLayoutStabilizes(DatePickerLocator);
            new SelectElement(WaitForElement(By.ClassName("react-datepicker__month-select")))
                .SelectByValue("1");
            new SelectElement(WaitForElement(By.ClassName("react-datepicker__year-select")))
                .SelectByValue("2020");
            WaitForElement(By.CssSelector(
                ".react-datepicker__day--015:not(.react-datepicker__day--outside-month)")).Click();
        }

        private void ClickAfterLayoutStabilizes(By locator)
        {
            var element = WaitForElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteAsyncScript(
                "const element = arguments[0]; " +
                "const done = arguments[arguments.length - 1]; " +
                "element.scrollIntoView({ block: 'center', inline: 'nearest' }); " +
                "let previousPosition; " +
                "let stableFrames = 0; " +
                "const waitForStablePosition = () => { " +
                "const rect = element.getBoundingClientRect(); " +
                "const position = [rect.left, rect.top, rect.width, rect.height].join(':'); " +
                "stableFrames = position === previousPosition ? stableFrames + 1 : 0; " +
                "previousPosition = position; " +
                "if (stableFrames >= 3) done(); " +
                "else requestAnimationFrame(waitForStablePosition); " +
                "}; " +
                "requestAnimationFrame(waitForStablePosition);",
                element);
            Driver.FindElement(locator).Click();
        }

        public string GetSelectedDate() => WaitForElement(DatePickerLocator).GetAttribute("value")
            ?? throw new InvalidOperationException("The selected date input has no value.");

        public void SetSliderToMaximum() => WaitForElement(SliderLocator).SendKeys(Keys.End);

        public string GetSliderValue() => WaitForElement(SliderLocator).GetAttribute("value")
            ?? throw new InvalidOperationException("The slider input has no value.");
    }
}
