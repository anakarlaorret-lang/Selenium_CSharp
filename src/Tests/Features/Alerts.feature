Feature: DemoQA alerts
  As a QA engineer
  I want to handle browser alerts
  So that I can validate Selenium's native alert support

  Scenario: Accept a button alert
    Given the DemoQA site is accessible
    And the user is on the Alerts page
    When the user triggers an alert
    And the user accepts the alert
    Then the alert should be handled successfully
