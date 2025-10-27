@ui
Feature: Room Creation
    As a user
    I want to create a Secret Nick room
    So that I can organize a gift exchange

Background:
    Given the API is available

Rule: Create room with different preferences

Scenario Outline: Create room with <PreferenceType>
    Given I am on the home page
    When I click "Create Your Room" button
    Then I should see heading "Create Your Secret Nick Room"
    
    When I fill room form with generated data
    And I click outside any element
    And I click "Continue" button
    
    When I fill user form with generated data
    And I click outside any element
    And I click "Continue" button
    
    When I select "<GiftPreference>" option
    And I add <WishCount> wishes with generated names
    And I click outside any element
    And I click "Complete" button
    
    Then I should see room success page

  Examples:
    | PreferenceType | GiftPreference                              | WishCount |
    | wishes         | I have gift ideas! (add up to 5 gift ideas) | 3         |
    | surprise       | I want a surprise gift                      | 0         |
