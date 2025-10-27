@api
Feature: System Health Check
  As a system administrator
  I want to check the system health
  So that I can ensure the API is functioning properly

  @Smoke
  Scenario: Check system health
    When I check the system health
    Then the system should be healthy
    And the response should include current date and time
    And the response should include environment name
    And the response should include build version

  @Performance
  Scenario: Check API response time
    When I check the system health 10 times
    Then all requests should succeed
    And the average response time should be less than 500ms
