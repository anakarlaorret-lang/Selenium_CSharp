Feature: DemoQA Forms
  As a user of DemoQA
  I want to submit forms with different data
  So that I can validate input handling

  @Forms
  Scenario: Submit form with valid data
    Given the Selenium banner should be visible
    Given the user is on the Forms page
    When the user fills the form with valid data
    And submits the form
    Then a success message should be displayed

  @Forms
  Scenario: Submit form with invalid data
    Given the Selenium banner should be visible
    Given the user is on the Forms page
    When the user fills the form with invalid data
    And submits the form
    Then the email field should be highlighted as error