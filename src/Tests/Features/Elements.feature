Feature: Elements interactions
  As a QA engineer
  I want to validate the behavior of common UI elements
  So that I can ensure correct functionality and showcase automation skills

  # Text Box
  Scenario: Submit text box with valid data
    Given the user is on the Text Box page
    When the user fills the text box with valid data
    And clicks the submit button
    Then the output should display the entered data

  Scenario: Submit text box with invalid email
    Given the user is on the Text Box page
    When the user fills the text box with an invalid email
    And clicks the submit button
    Then the email field should be highlighted as error

  # Check Box
  Scenario: Select a single checkbox
    Given the user is on the Check Box page
    When the user expands the tree and selects the "Notes" checkbox
    Then the selected checkbox should be "Notes"

  Scenario: Select multiple checkboxes
    Given the user is on the Check Box page
    When the user expands the tree and selects "Desktop" and "Documents"
    Then the selected checkboxes should include "Desktop" and "Documents"

  # Radio Button
  Scenario: Select Yes radio button
    Given the user is on the Radio Button page
    When the user selects the "Yes" radio button
    Then the radio result should be "You have selected Yes"

  Scenario: Attempt to select No radio button
    Given the user is on the Radio Button page
    When the user tries to select the "No" radio button
    Then the radio button should be disabled

  # Web Tables
  Scenario: Add a new record to the table
    Given the user is on the Web Tables page
    When the user adds a new record
    Then the record should appear in the table

  Scenario: Edit an existing record in the table
    Given the user is on the Web Tables page
    When the user edits the record for "Cierra" with a new first name
    Then the updated record should appear in the table

  Scenario: Delete a record from the table
    Given the user is on the Web Tables page
    When the user deletes the record for "Alden"
    Then the record should no longer appear in the table

  # Buttons
  Scenario: Perform a double click
    Given the user is on the Buttons page
    When the user double clicks the button
    Then the result should display "You have done a double click"

  Scenario: Perform a dynamic click
    Given the user is on the Buttons page
    When the user clicks the dynamic button
    Then the result should display "You have done a dynamic click"

  # Links
  Scenario: Validate a working link
    Given the user is on the Links page
    When the user clicks the "Created" link
    Then the displayed link response should contain status code 201

  Scenario: Validate a bad request link
    Given the user is on the Links page
    When the user clicks the "Bad Request" link
    Then the displayed link response should contain status code 400