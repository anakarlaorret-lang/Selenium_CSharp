Feature: DemoQA widgets
  As a QA engineer
  I want to validate interactive widgets
  So that I can demonstrate reliable Selenium interaction with dynamic controls

  Scenario: Select a date from the date picker
    Given the user is on the Date Picker page
    When the user selects February 15, 2020
    Then the selected date should be "02/15/2020"

  Scenario: Set the slider to its maximum value
    Given the user is on the Slider page
    When the user sets the slider to its maximum
    Then the slider value should be "100"
