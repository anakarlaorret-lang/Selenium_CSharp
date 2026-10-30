using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DemoTest.Utils;

namespace DemoTest.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static ExtentReports? extent;
        private IWebDriver? driver;
        private ExtentTest? scenario;

        [BeforeTestRun]
        public static void SetupReport()
        {
            var sparkReporter = new ExtentSparkReporter("report.html");
            var report = new ExtentReports();
            report.AttachReporter(sparkReporter);
            extent = report;
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = (extent ?? throw new InvalidOperationException("ExtentReports has not been initialized."))
                .CreateTest(scenarioContext.ScenarioInfo.Title);
            var browser = Environment.GetEnvironmentVariable("BROWSER");
            var scenarioDriver = DriverFactory.CreateDriver(
                string.IsNullOrWhiteSpace(browser) ? "chrome" : browser);
            driver = scenarioDriver;

            try
            {
                scenarioDriver.Manage().Window.Maximize();
                scenarioDriver.Navigate().GoToUrl(ConfigReader.GetBaseUrl());
                scenarioContext.ScenarioContainer.RegisterInstanceAs<IWebDriver>(scenarioDriver);
            }
            catch
            {
                scenarioDriver.Quit();
                driver = null;
                throw;
            }
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var currentScenario = scenario
                ?? throw new InvalidOperationException("The scenario report has not been initialized.");

            if (scenarioContext.TestError == null)
                currentScenario.Pass(scenarioContext.StepContext.StepInfo.Text);
            else
                currentScenario.Fail(scenarioContext.StepContext.StepInfo.Text + " - " + scenarioContext.TestError.Message);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var scenarioDriver = driver;
            driver = null;
            scenario = null;
            scenarioDriver?.Quit();
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent?.Flush();
        }
    }
}
