Test Plan – DemoQA automation portfolio
https://demoqa.com/

1. Introduction

This Test Plan defines the objectives, scope, approach, and resources for automating UI tests on the DemoQA site using Selenium + C#. It ensures consistency, maintainability, and professional QA practices aligned with portfolio goals.

2. Test Items

* Forms Module: Input fields, validation, submission.
* Elements Module: Tables, checkboxes, radio buttons.
* Alerts Module: Alert handling, confirmation dialogs.
* Widgets Module: Date pickers, sliders, tooltips.

3. Features to be Tested

* Navigation across DemoQA modules.
* Form submission with valid and invalid data.
* Alert handling and confirmation.
* Table interactions (add, edit, delete rows).
* Cross-browser compatibility (Chrome, Firefox, Edge).

3.1. Features Not to be Tested

* Authentication flows (DemoQA has no login).
* Backend API validations.
* Advanced non-functional testing (security, scalability).

4. Test Case Prioritization

* High Priority: Smoke tests, form submission, alert handling.
* Medium Priority: Validation tests, table interactions.
* Low Priority: Exploratory widget interactions.

5. Test Environment

Local: Windows 10, .NET 8 SDK, Visual Studio/VS Code.

CI/CD: GitHub Actions pipeline with a Chrome, Firefox, and Edge browser matrix.

Data: Dummy credentials and simulated form inputs.

Reporting: ExtentReports integrated into CI/CD.

6. Preconditions

* DemoQA site is accessible and stable.
* .NET 8 SDK installed.
* Selenium WebDriver dependencies restored; Selenium Manager resolves the browser driver.
* Test data prepared in JSON/CSV fixtures.
* GitHub Actions runner configured with required dependencies.
* reporting tool integrated.

7. Detailed Test Case Table
![alt text](image.png)

8. Schedule / Execution Plan

* Local Execution:
    - Smoke tests -> before each commit.
    - Functional + validation tests -> daily execution.

* CI/CD Execution (GitHub Actions):
    - Full regression suite -> on each push to main
    - Cross-browser matrix -> nightly build

* Performance Tests (k6):
    - On-demand or scheduled weekly.

9. Performance Testing (k6)

Baseline Load Test: 20 VUs, 30s → validate normal traffic.

Extended Load Test: 50 VUs, 2m → measure sustained stability.

Ramp-Up Test: 10 → 50 VUs in stages → observe scalability.

Metrics analyzed: Latency (http_req_duration, p95), Throughput (http_reqs), Error rate (http_req_failed).

Execution:

k6 run PerformanceTests/load_baseline.js
k6 run PerformanceTests/load_extended.js
k6 run PerformanceTests/load_ramp.js