@api
Feature: Room Management API
  As a system user
  I want to manage rooms through the API
  So that I can organize gift exchanges

Background:
  Given the API is available

Rule: Room Creation

  @positive
  Scenario: Create a room with random data
    Given I have room creation data
    When I send a POST request to create the room
    Then the room should be created successfully
    And the room details should match the request
    And the response should include:
      * Room ID greater than 0
      * Admin ID greater than 0
      * Invitation code in valid format
      * User code in valid format

  @positive
  Scenario: Create a room with surprise gift preference
    Given I have room creation data with surprise gift preference
    When I send a POST request to create the room
    Then the room should be created successfully
    And the admin user should have:
      * WantSurprise set to true
      * Interests field populated with text
      * Empty wish list with 0 items
      * Valid contact information

  @negative
  Scenario Outline: Create a room with invalid data
    Given I have invalid room creation data:
      | Field   | Value   |
      | <Field> | <Value> |
    When I send a POST request to create the room
    Then the request should fail with status 400
    And the error should mention field "<Field>"

    Examples:
      | Field             | Value |
      | Name              |       |
      | Description       |       |
      | GiftExchangeDate  | past  |
      | GiftMaximumBudget | -100  |

Rule: Room Retrieval

  @positive
  Scenario: Retrieve room by user code
    Given I have created a room
    When I get the room by user code
    Then the request should return status 200
    And the room should contain:
      * Basic room information with name and description
      * Admin user ID greater than 0
      * Creation date within last hour
      * Modification date within last hour
      * Room status showing availability

  @positive
  Scenario: Retrieve room by invitation code
    Given I have created a room
    When I get the room by invitation code
    Then the request should return status 200
    But the room should not contain:
      * Sensitive admin information
      * User private data
      * Internal system fields

Rule: Room Drawing

  @positive
  Scenario: Draw names in a room
    Given I have created a room with multiple users:
      | FirstName | LastName | WantSurprise |
      | John      | Doe      | true         |
      | Jane      | Smith    | false        |
      | Bob       | Johnson  | true         |
      | Alice     | Williams | false        |
    When I draw names as admin
    Then the request should return status 200
    When each user checks their gift assignment
    Then Each user has a gift recipient assigned
      * No user is assigned to themselves
      * All assignments are unique
      * Recipients match user preferences
