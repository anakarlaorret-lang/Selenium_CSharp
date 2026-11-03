# Hypothetical bug report example

> This is a training example based on a deliberately incorrect test expectation. It does not report a confirmed defect in DemoQA.

## Issue overview

The date-picker scenario fails because the assertion expects a date different from the one selected by the test.

## Environment

- Operating system: Windows
- .NET SDK: .NET 8
- Browser: Chrome
- Test: `Select a date from the date picker`

## Steps to reproduce

1. Run the `SelectADateFromTheDatePicker` scenario.
2. Navigate to the Date Picker page.
3. Select February 15, 2020.
4. Compare the selected input value with the expected value.

## Expected behavior

The input should contain `02/16/2020` in this intentionally faulty test setup.

## Actual behavior

The input contains `02/15/2020`, which matches the date selected by the scenario. The assertion fails because its expected value is deliberately incorrect.

## Impact

The test reports a failure even though the date picker behaves consistently with the requested selection. This is a test expectation issue, not evidence of an application defect.

## Logs or screenshots

```text
Expected: "02/16/2020"
But was:  "02/15/2020"
```

## Resolution in this example

Restore the assertion to compare against the expected value supplied by the Gherkin scenario, `02/15/2020`.
