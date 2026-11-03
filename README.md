# Selenium C# UI Automation

[![Selenium UI Tests CI](https://github.com/anakarlaorret-lang/Selenium_CSharp/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/anakarlaorret-lang/Selenium_CSharp/actions/workflows/ci.yml)

A portfolio project demonstrating browser-based functional testing with Selenium WebDriver, C#, .NET 8, NUnit, and SpecFlow against the public [DemoQA practice site](https://demoqa.com/).

## What this project demonstrates

- Page Object Model and reusable Selenium interactions.
- BDD scenarios written in Gherkin and bound to C# step definitions.
- Scenario-scoped WebDriver instances with Chrome, Firefox, and Edge support.
- Explicit assertions, waits, and test data for selected UI flows.
- GitHub Actions browser matrix and ExtentReports HTML output for local runs.

The suite covers selected Elements, Forms, Alerts, and Widgets flows. See the [test plan](src/Docs/TEST_PLAN.md) and [test strategy](src/Docs/TEST_STRATEGY.md) for details.

## Prerequisites

- .NET 8 SDK
- Git
- Chrome, Firefox, or Edge

## Run locally

```powershell
git clone https://github.com/anakarlaorret-lang/Selenium_CSharp.git
cd Selenium_CSharp
dotnet restore
dotnet test --configuration Release
```

Chrome is the default browser. To select another supported browser:

```powershell
$env:BROWSER = "firefox" # chrome, firefox, or edge
dotnet test --configuration Release
Remove-Item Env:BROWSER
```

The test hooks create and close a WebDriver for each scenario. Selenium Manager resolves the browser driver.

## ExtentReports

ExtentReports writes `report.html` to the test output directory, for example `bin/Release/net8.0/report.html`. To open it automatically after a local run:

```powershell
$env:OPEN_EXTENT_REPORT = "true"
dotnet test --configuration Release
Remove-Item Env:OPEN_EXTENT_REPORT
```

Automatic opening is opt-in and disabled in CI. The HTML report is still generated when the variable is unset. CI uploads TRX test results as workflow artifacts.

There is no committed report screenshot yet. Before adding one, remove local paths, personal data, and other environment-specific details; save the sanitized image as `docs/images/extent-report.png` and embed it here.

## Scope and limitations

- This repository demonstrates UI functional automation; it is not a load/performance or security testing suite.
- DemoQA is an external practice site, not an application maintained by this project. Its content, advertisements, availability, and layout can change and affect test stability.
- A failing browser test is not, by itself, evidence of a defect in DemoQA. Confirm the behavior independently before reporting an application issue.
- Test data is synthetic. Do not add credentials, private data, local machine paths, or unredacted reports/screenshots.

## Issues and contributions

Use the [bug report template](.github/ISSUE_TEMPLATE/bug_report.md) only for reproducible problems in this repository's framework or tests. See the [hypothetical report example](src/Docs/BUG_REPORT_EXAMPLE.md); do not use the template to report unconfirmed DemoQA defects.

This is a personal portfolio repository. External pull requests are not accepted; opening an issue does not grant permission to modify or reuse the project.

## Professional portfolio

Created as part of my professional portfolio to demonstrate Selenium WebDriver and C# UI automation.

**Ana K. Orret**  
Senior Software QA Engineer | Manual & Automation | Test plan & Strategy  
[View this project on GitHub](https://github.com/anakarlaorret-lang/Selenium_CSharp)

## Copyright and usage

Copyright (c) 2026 Ana K. Orret. This repository is shared for portfolio review; no commercial license is granted. Obtain prior written authorization before commercial use. This README is not a substitute for a formal license. Third-party components, DemoQA content, and trademarks remain subject to their respective licenses and terms.
