@ui
Feature: Room Management
    As a room admin
    I want to manage my Secret Nick room
    So that I can organize the gift exchange

Background:
    Given the API is available

Rule: Room viewing and management

Scenario Outline: View room with <ParticipantCount> participants
    Given a room exists with <ParticipantCount> participants via API
    And I am on the home page
    When I navigate to room page with admin code
    Then I should see room name from API
    And I should see exchange date as in request
    And I should see participants count <ParticipantCount>
    And I should see "Draw Names" button <ButtonState>
    
  Examples:
    | ParticipantCount | ButtonState |
    | 1                | disabled    |
    | 3                | enabled     |

Scenario: Draw names and view recipient
    Given a room exists with 3 participants via API
    And I am on the home page
    When I navigate to room page with admin code
    Then I should see participants count 3
    
    When I click "Draw Names" button
    Then I should see heading "Look Who You Got!"
    And I should see recipient name
    
    When I click "Read Details" button
    Then I should see recipient personal info
    And I should see recipient wishes or interests
