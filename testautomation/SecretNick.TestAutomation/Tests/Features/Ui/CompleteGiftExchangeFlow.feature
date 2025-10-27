@ui @e2e
Feature: Complete Gift Exchange Flow
    As users of the Secret Nick application
    We want to complete a full gift exchange cycle
    So that we can verify the entire user journey

Background:
    Given the API is available

Rule: Complete flow from creation to drawing

Scenario Outline: Complete gift exchange with wish list
    # Create room via UI
    Given I am on the home page
    When I click "Create Your Room" button
    Then I should see heading "Create Your Secret Nick Room"
    
    # Fill room details
    When I fill the following fields:
        | Field            | Value                 |
        | Room Name        | Holiday Exchange 2025 |
        | Room Description | Annual gift exchange  |
        | Gift Budget      | 500                   |
    And I select today as exchange date
    And I click outside any element
    And I click "Continue" button
    
    # Fill admin details
    When I fill the following fields:
        | Field      | Value             |
        | First Name | Admin             |
        | Last Name  | User              |
        | Phone      | 123456789         |
        | Email      | admin@example.com |
        | Address    | 123 Admin Street  |
    And I click outside any element
    And I click "Continue" button
    
    # Add wishes
    When I select "I have gift ideas! (add up to 5 gift ideas)" option
    And I fill the following fields:
        | Field      | Value                      |
        | Wish Name  | Book Set                   |
        | Wish Link  | https://bookstore.com/set  |
    And I click "Add Wish" button
    And I fill the following fields:
        | Field      | Value        |
        | Wish Name  | Coffee Maker |
    And I click "Add Wish" button
    And I fill the following fields:
        | Field      | Value      |
        | Wish Name  | Warm Socks |
    And I click outside any element
    And I click "Complete" button
    
    # Verify room created
    Then I should see room success with name "Holiday Exchange 2025"
    And I should see room link
    And I should see personal link
    And I should see invitation text:
        """
        Hey!

        Join our Secret Nick and make this holiday season magical! 🎄

        You‘ll get to surprise someone with a gift — and receive one too. 🎅✨

        Let the holiday fun begin! 🌟

        🎁 Join here:

        {room_link}
        """

    # Navigate to room
    When I click "Visit Your Room" button
    Then I should see room name "Holiday Exchange 2025"
    And I should see exchange date as today
    And I should see gift budget "500 UAH"
    And I should see participants count 1
    And I should see "Draw Names" button disabled
    
    When I click "Invite New Members" button
    Then I should see heading "Invite New Members"
    
    # Add participants via API
    When I add 2 participants via API
    And I refresh the page
    Then I should see participants count 3
    
    # Draw names
    When I click "Draw Names" button
    Then I should see heading "Look Who You Got!"
    And I should see recipient name
    
    When I click "Read Details" button
    Then I should see recipient details in modal

Rule: Room with special preferences

Scenario Outline: Create room with unlimited budget and surprises
    Given I am on the home page
    When I click "Create Your Room" button
    
    # Create room with zero budget
    When I fill the following fields:
        | Field            | Value               |
        | Room Name        | Surprise Party      |
        | Room Description | Everyone surprises! |
        | Gift Budget      | 0                   |
    Then I should see "0 means unlimited budget" text
    
    When I select today as exchange date
    And I click outside any element
    And I click "Continue" button
    
    # Use generated admin data
    When I fill user details with generated data
    And I click outside any element
    And I click "Continue" button
    
    # Admin wants surprise
    When I select "I want a surprise gift" option
    And I fill the following fields:
        | Field      | Value                          |
        | Interests  | Books, coffee, and cozy things |
    And I click outside any element
    And I click "Complete" button
    
    Then I should see room success with name "Surprise Party"
    When I click "Visit Your Room" button
    Then I should see gift budget "Unlimited"
