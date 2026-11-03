# Selenium_CSharp
DemoQA UI Automation Project (https://demoqa.com/)

1. Overview

This project demonstrates UI automation with Selenium + C# using the DemoQA site as a playground.
It showcases framework design, CI/CD integration, and professional QA practices for portfolio purposes.

2. Features

* Functional Testing: Selenium automation for core flows (forms, elements, alert, widgets).
* Code Quality: .gitignore and .editorconfig management, clean architecture with Page Object Model.
* CI/CD: GitHub Actions runs the Selenium UI suite on .NET 8 with Chrome, Firefox, and Edge.
* Reporting: ExtentReports for HTML reports.
* Documentation: Clear README and structures repo for recruiters and collaborators.
* BDD with Gherkin: Test cases described in natural language (Given/When/Then).
* SpecFlow BDD: `.feature` scenarios are bound to C# step definitions.

3. Setup & Installation

* Prerequisites
    - .NET 8 SDK
    - Git
    - Visual studio/VS Code

* Installation
```powershell
git clone https://github.com/ana-karla-orret/Selenium_CSharp.git
cd Selenium_CSharp
dotnet restore
```

4. Running Tests
```powershell
dotnet build --configuration Release
dotnet test --configuration Release
```

By default, UI tests use Chrome. To select a supported browser locally:

```powershell
$env:BROWSER = "firefox" # chrome, firefox, or edge
dotnet test --configuration Release
Remove-Item Env:BROWSER
```

The WebDriver is created per scenario. Selenium Manager resolves the matching browser driver.
The GitHub Actions workflow runs the UI suite once per browser and uploads a separate TRX result artifact for each run.
ExtentReports generates `report.html` in the test output directory, for example `bin/Release/net8.0/report.html`.

To open the HTML report automatically after a local test run, set `OPEN_EXTENT_REPORT` to `true`:

```powershell
$env:OPEN_EXTENT_REPORT = "true"
dotnet test --configuration Release
Remove-Item Env:OPEN_EXTENT_REPORT
```

Automatic opening is opt-in and disabled in CI. The report is still generated when the option is unset.

### ExtentReports preview

Save a screenshot of the report as `docs/images/extent-report.png` to display it here:

![ExtentReports execution report](docs/images/extent-report.png)

See the [hypothetical bug report example](src/Docs/BUG_REPORT_EXAMPLE.md) for a sample report based on a deliberately incorrect test expectation.

5. Test Types
* Smoke testing: Verify site loads and essential elements are visible.
* Functional testing: Validate navigation, form submission, table interactions, and alert handling.
* Validation testing: Ensure proper error handling and input validation.
* Regression testing: Re-run critical flows after updates.
* Cross-Browser testing: Confirm compatibility across Chrome, Firefox, and Edge.

6. CI/CD Integration
* GitHub Actions pipeline runs functional tests on each push.
* Browser matrix executes the Selenium UI suite on Chrome, Firefox, and Edge.
* Badges for builds status and coverage included.

7. Highlights
* Demonstrates QA best practices end-to-end.
* Combines UI automation and CI/CD.
* Clear documentation for recruiters and collaborators.

## Professional Portfolio

This project was created as part of my professional portfolio to demonstrate UI test automation with Selenium WebDriver, C#, and .NET.

**Ana K. Orret**  
Senior Software QA Engineer | Manual & Automation | Test plan & Strategy  
[GitHub profile](https://github.com/ana-karla-orret)

This project is not intended for commercial use without prior authorization from the author. DemoQA and third-party dependencies remain subject to their respective terms and licenses.

8. License

No license for commercial use is granted by this repository. Please contact the author for authorization.