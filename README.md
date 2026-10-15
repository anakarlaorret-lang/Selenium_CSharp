# Selenium_CSharp
📖 Overview

White Tower is a demo project designed to showcase modern QA practices, combining functional automation, quality strategy, and performance testing. It demonstrates how to build a maintainable, professional QA framework integrated with CI/CD pipelines.

🚀 Features

Functional Testing: Selenium automation for core flows.

API Testing: Postman

Code Quality: ESLint integration, pre-commit hooks, .gitignore and .eslintignore management.

CI/CD: GitHub Actions workflows for automated builds and test execution.

Performance Testing: k6 scripts for load, spike, and ramp-up scenarios.

Documentation: Clear README and structured repo for recruiters and collaborators.

🛠️ Setup & Installation

Prerequisites

Node.js & npm

.NET SDK (for Selenium project)

Git

k6 (installed via Chocolatey or winget)

Manually
https://k6.io/

🧪 Running Tests

Functional and API Tests

dotnet test

Performance Tests

k6 run PerformanceTests/load_baseline.js
k6 run PerformanceTests/load_extended.js
k6 run PerformanceTests/load_ramp.js

📊 Performance Scenarios

Baseline Load Test: 20 VUs, 30s → validate normal traffic.

Extended Load Test: 50 VUs, 2m → measure sustained stability.

Ramp-Up Test: 10 → 50 VUs in stages → observe scalability.

Metrics analyzed:

Latency (http_req_duration, p95)

Throughput (http_reqs)

Error rate (http_req_failed)

🔗 CI/CD Integration

GitHub Actions pipeline runs functional tests on each push.

Performance tests can be triggered manually or scheduled.

Badges for build status and coverage included.

📌 Highlights

Demonstrates QA best practices end-to-end.

Combines automation, performance, and CI/CD.

Clear documentation for recruiters and collaborators.

📜 License

License – free to use and adapt.