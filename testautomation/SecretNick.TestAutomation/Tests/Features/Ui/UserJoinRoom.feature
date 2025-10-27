@ui
Feature: User Join Room
    As a user
    I want to join a Secret Nick room
    So that I can participate in the gift exchange

Background:
    Given the API is available
    And a room exists via API

Rule: Join room process

Scenario Outline: User joins room with <PreferenceType>
    Given I am on the home page
    When I navigate to join page with invitation code
    Then I should see welcome heading with room name
    And I should see room exchange date
    And I should see room gift budget as in request
    
    When I click "Join the Room" button
    And I fill user form with generated data
    And I click outside any element
    And I click "Continue" button
    
    When I select "<GiftPreference>" option
    And I add <WishCount> wishes with generated names
    And I click outside any element
    And I click "Complete" button
    
    Then I should see heading "You Have Joined the Room! 🎅"
    And I should see personal link

  Examples:
    | PreferenceType | GiftPreference                              | WishCount |
    | wishes         | I have gift ideas! (add up to 5 gift ideas) | 2         |
    | surprise       | I want a surprise gift                      | 0         |
