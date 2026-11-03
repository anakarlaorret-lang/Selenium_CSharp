using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DemoTest.Utils;
using System.Diagnostics;

namespace DemoTest.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static ExtentReports? extent;
        private static string? reportPath;
        private IWebDriver? driver;
        private ExtentTest? scenario;

        [BeforeTestRun]
        public static void SetupReport()
        {
            reportPath = Path.Combine(AppContext.BaseDirectory, "report.html");
            var sparkReporter = new ExtentSparkReporter(reportPath);
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

            var openReport = string.Equals(
                Environment.GetEnvironmentVariable("OPEN_EXTENT_REPORT"),
                "true",
                StringComparison.OrdinalIgnoreCase);
            var isCi = string.Equals(
                           Environment.GetEnvironmentVariable("CI"),
                           "true",
                           StringComparison.OrdinalIgnoreCase)
                       || string.Equals(
                           Environment.GetEnvironmentVariable("GITHUB_ACTIONS"),
                           "true",
                           StringComparison.OrdinalIgnoreCase);

            if (openReport && !isCi)
            {
                var path = reportPath
                    ?? throw new InvalidOperationException("The ExtentReports path has not been initialized.");
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
        }
    }
}
