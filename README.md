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
* Performance Testing: k6 scripts for load, spike, and ramp-up scenarios.
* BDD with Gherkin: Test cases described in natural language (Given/When/Then).
* Cucumber integration: Execution of `.feature` files with step definitions

3. Setup & Installation

* Prerequisites
    - .NET 8 SDK
    - Git
    - k6 (installed via Chocolatey or winget)
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
ExtentReports generates `report.html` in the test working directory.

5. Test Types
* Smoke testing: Verify site loads and essential elements are visible.
* Functional testing: Validate navigation, form submission, table interactions, and alert handling.
* Validation testing: Ensure proper error handling and input validation.
* Regression testing: Re-run critical flows after updates.
* Cross-Browser testing: Confirm compatibility across Chrome, Firefox, and Edge.
* Performance testing (k6):
    - Baseline Load Test: 20 VUs, 30s → validate normal traffic.
    - Extended Load Test: 50 VUs, 2m → measure sustained stability.
    - Ramp-Up Test: 10 → 50 VUs in stages → observe scalability.
    - Metrics analyzed: Latency (http_req_duration, p95), Throughput (http_reqs), Error rate (http_req_failed).

    Execution:

    k6 run PerformanceTests/load_baseline.js
    k6 run PerformanceTests/load_extended.js
    k6 run PerformanceTests/load_ramp.js

6. CI/CD Integration
* GitHub Actions pipeline runs functional tests on each push.
* Browser matrix executes the Selenium UI suite on Chrome, Firefox, and Edge.
* Badges for builds status and coverage included.

Performance tests can be triggered manually or scheduled.

7. Highlights
* Demonstrates QA best practices end-to-end.
* Combines automation, performance, and CI/CD.
* Clear documentation for recruiters and collaborators.

8. License

Free to use and adapt.