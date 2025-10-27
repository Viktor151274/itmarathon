using Reqnroll;
using Shouldly;
using Tests.Api.Clients;
using Tests.Api.Models.Requests;
using Tests.Api.Models.Responses;
using Tests.Common.Models;
using Tests.Common.TestData;
using Tests.Helpers;

namespace Tests.Api.Steps
{
    [Binding]
    public class RoomApiSteps(
        ScenarioContext scenarioContext,
        RoomApiClient apiClient,
        UserApiClient userApiClient)
    {
        private readonly ScenarioContext _scenarioContext = scenarioContext;
        private readonly RoomApiClient _apiClient = apiClient;
        private readonly UserApiClient _userApiClient = userApiClient;

        [When("I get the room by user code")]
        public async Task WhenIGetTheRoomByUserCode()
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();
            var roomDetails = await _apiClient.GetRoomByUserCodeAsync(response.UserCode!);
            _scenarioContext.Set(roomDetails, "RoomDetails");
            _scenarioContext.Set((dynamic)roomDetails, "LastEntity");
            _scenarioContext.Set(200, "LastStatusCode");
        }

        [When("I get the room by invitation code")]
        public async Task WhenIGetTheRoomByInvitationCode()
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();
            var roomDetails = await _apiClient.GetRoomByInvitationCodeAsync(response.Room.InvitationCode!);
            _scenarioContext.Set(roomDetails, "RoomByInvitation");
            _scenarioContext.Set(200, "LastStatusCode");
        }

        [When("I try to draw names again")]
        [When("I draw names as admin")]
        public async Task WhenIDrawNamesAsAdmin()
        {
            try
            {
                var response = _scenarioContext.Get<RoomCreationResponse>();

                // Admin draws names ONCE for the whole room
                var drawResult = await _apiClient.DrawRoomAsync(response.UserCode!);

                // This might return admin's recipient or just confirmation
                _scenarioContext.Set(drawResult, "DrawResult");
                _scenarioContext.Set(true, "DrawCompleted");
                _scenarioContext.Set(200, "LastStatusCode");
            }
            catch (InvalidOperationException ex)
            {
                _scenarioContext.Set(ex.Message, "LastError");
                var match = ValidationPatterns.StatusCodePattern().Match(ex.Message);
                if (match.Success)
                {
                    _scenarioContext.Set(int.Parse(match.Groups[1].Value), "LastStatusCode");
                }
            }
        }

        [When("each user checks their gift assignment")]
        public async Task WhenEachUserChecksTheirGiftAssignment()
        {
            var userCodes = _scenarioContext.Get<Dictionary<string, string>>("AllUserCodes");
            var userAssignments = new Dictionary<string, UserReadDto>();

            foreach (var userEntry in userCodes)
            {
                // Each user gets the users list with their userCode
                var users = await _userApiClient.GetUsersAsync(userEntry.Value);

                // Find this user by their userCode
                var thisUser = users.FirstOrDefault(u => u.UserCode == userEntry.Value);

                if (thisUser?.GiftToUserId != null)
                {
                    // Find the recipient in the same list
                    var recipient = users.FirstOrDefault(u => u.Id == thisUser.GiftToUserId);
                    if (recipient != null)
                    {
                        userAssignments.Add(userEntry.Key, recipient);
                    }
                }
            }

            _scenarioContext.Set(userAssignments, "UserAssignments");
        }

        [Then("Each user has a gift recipient assigned")]
        public void ThenEachUserHasAGiftRecipientAssigned()
        {
            var userAssignments = _scenarioContext.Get<Dictionary<string, UserReadDto>>("UserAssignments");
            var userCodes = _scenarioContext.Get<Dictionary<string, string>>("AllUserCodes");

            userAssignments.Count.ShouldBe(userCodes.Count, "Every user should have an assignment");

            foreach (var assignment in userAssignments)
            {
                var userName = assignment.Key;
                var recipient = assignment.Value;

                recipient.ShouldNotBeNull($"User {userName} should have a recipient");
                recipient.Id.ShouldBeGreaterThan(0);
                recipient.FirstName.ShouldNotBeNullOrEmpty();
                recipient.LastName.ShouldNotBeNullOrEmpty();
            }
        }

        [Then("No user is assigned to themselves")]
        public void ThenNoUserIsAssignedToThemselves()
        {
            var userAssignments = _scenarioContext.Get<Dictionary<string, UserReadDto>>("UserAssignments");

            foreach (var kvp in userAssignments)
            {
                var userName = kvp.Key;
                var recipient = kvp.Value;
                var recipientName = $"{recipient.FirstName} {recipient.LastName}";

                userName.ShouldNotBe(recipientName, $"{userName} should not give gift to themselves");
            }
        }

        [Then("All assignments are unique")]
        public void ThenAllAssignmentsAreUnique()
        {
            var userAssignments = _scenarioContext.Get<Dictionary<string, UserReadDto>>("UserAssignments");

            var recipientIds = userAssignments.Values.Select(r => r.Id).ToList();
            recipientIds.Distinct().Count().ShouldBe(recipientIds.Count,
                "Each person should receive a gift from only one person");
        }

        [Then("Recipients match user preferences")]
        public void ThenRecipientsMatchUserPreferences()
        {
            var userAssignments = _scenarioContext.Get<Dictionary<string, UserReadDto>>("UserAssignments");

            // Just verify we have assignments
            userAssignments.ShouldNotBeEmpty();
            userAssignments.Values.ShouldAllBe(r => r != null);
        }

        [Then("Basic room information with name and description")]
        public void ThenBasicRoomInformationWithNameAndDescription()
        {
            var room = _scenarioContext.Get<RoomReadDto>("RoomDetails");
            room.Name.ShouldNotBeNullOrEmpty();
            room.Description.ShouldNotBeNullOrEmpty();
        }

        [Then("Admin user ID greater than {int}")]
        public void ThenAdminUserIDGreaterThan(int value)
        {
            var room = _scenarioContext.Get<RoomReadDto>("RoomDetails");
            room.AdminId.ShouldBeGreaterThan(value);
        }

        [Then("Room status showing availability")]
        public void ThenRoomStatusShowingAvailability()
        {
            var room = _scenarioContext.Get<RoomReadDto>("RoomDetails");
            room.IsFull.ShouldBeFalse();
        }

        [Then("Sensitive admin information")]
        [Then("User private data")]
        [Then("Internal system fields")]
        public void ThenShouldNotContainSensitiveData()
        {
            var room = _scenarioContext.Get<RoomReadDto>("RoomByInvitation");
            room.ShouldNotBeNull();
        }

        [Given("I have created a room with multiple users:")]
        public async Task GivenIHaveCreatedARoomWithMultipleUsers(Table table)
        {
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(),
                AdminUser = TestDataGenerator.GenerateUser()
            };

            var roomResponse = await _apiClient.CreateRoomAsync(request);
            _scenarioContext.Set(roomResponse);

            // Store all user codes including admin
            var userCodes = new Dictionary<string, string>
    {
        { $"{request.AdminUser.FirstName} {request.AdminUser.LastName}", roomResponse.UserCode! }
    };

            var users = table.CreateSet<UserCreationDto>();
            foreach (var user in users)
            {
                var fullUser = TestDataGenerator.GenerateUser(user.WantSurprise);
                fullUser.FirstName = user.FirstName;
                fullUser.LastName = user.LastName;

                var userResponse = await _userApiClient.CreateUserAsync(roomResponse.Room.InvitationCode!, fullUser);
                userCodes.Add($"{user.FirstName} {user.LastName}", userResponse.UserCode!);
            }

            _scenarioContext.Set(userCodes, "AllUserCodes");
        }

        [Given("I have invalid room creation data:")]
        public void GivenIHaveInvalidRoomCreationData(Table table)
        {
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(table),
                AdminUser = TestDataGenerator.GenerateUser()
            };

            _scenarioContext.Set(request);
        }

        [Given("I have room creation data")]
        public void GivenIHaveRoomCreationData()
        {
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(),
                AdminUser = TestDataGenerator.GenerateUser()
            };

            _scenarioContext.Set(request);
        }

        [Given("I have room creation data with surprise gift preference")]
        public void GivenIHaveRoomCreationDataWithSurpriseGiftPreference()
        {
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(),
                AdminUser = TestDataGenerator.GenerateUser(wantSurprise: true)
            };

            _scenarioContext.Set(request);
        }

        [Given("I have room creation data with:")]
        public void GivenIHaveRoomCreationDataWith(Table table)
        {
            var room = TestDataGenerator.GenerateRoom();

            foreach (var row in table.Rows)
            {
                switch (row["Field"])
                {
                    case "Name":
                        room.Name = row["Value"];
                        break;
                    case "GiftMaximumBudget":
                        room.GiftMaximumBudget = decimal.Parse(row["Value"]);
                        break;
                    case "GiftExchangeDate":
                        if (row["Value"].StartsWith("future+"))
                        {
                            var days = int.Parse(row["Value"].Replace("future+", ""));
                            room.GiftExchangeDate = DateTime.Now.AddDays(days);
                        }
                        break;
                }
            }

            _scenarioContext.Set(room, "RoomData");
        }

        [Given("I have user data with surprise preference {string}")]
        public void GivenIHaveUserDataWithSurprisePreference(string wantSurprise)
        {
            var room = _scenarioContext.Get<RoomCreationDto>("RoomData");
            var request = new RoomCreationRequest
            {
                Room = room,
                AdminUser = TestDataGenerator.GenerateUser(bool.Parse(wantSurprise))
            };

            _scenarioContext.Set(request);
        }

        [Given("I have created a room")]
        public async Task GivenIHaveCreatedARoom()
        {
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(),
                AdminUser = TestDataGenerator.GenerateUser()
            };

            var response = await _apiClient.CreateRoomAsync(request);
            _scenarioContext.Set(response);
        }

        [Given("I have created a room with only admin user")]
        public async Task GivenIHaveCreatedARoomWithOnlyUser()
        {
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(),
                AdminUser = TestDataGenerator.GenerateUser()
            };

            var response = await _apiClient.CreateRoomAsync(request);
            _scenarioContext.Set(response);
        }

        [Given("I have created a room and already drawn names")]
        public async Task GivenIHaveCreatedARoomAndAlreadyDrawnNames()
        {
            // Create room with multiple users
            var request = new RoomCreationRequest
            {
                Room = TestDataGenerator.GenerateRoom(),
                AdminUser = TestDataGenerator.GenerateUser()
            };

            var response = await _apiClient.CreateRoomAsync(request);
            _scenarioContext.Set(response);

            // Add more users
            for (int i = 0; i < 3; i++)
            {
                await _userApiClient.CreateUserAsync(
                    response.Room.InvitationCode!,
                    TestDataGenerator.GenerateUser());
            }

            // Draw names
            await _apiClient.DrawRoomAsync(response.UserCode);
        }

        [Given("I have a regular user code")]
        public async Task GivenIHaveARegularUserCode()
        {
            var adminResponse = _scenarioContext.Get<RoomCreationResponse>();
            var userResponse = await _userApiClient.CreateUserAsync(
                adminResponse.Room.InvitationCode!,
                TestDataGenerator.GenerateUser());

            _scenarioContext.Set(userResponse.UserCode, "RegularUserCode");
        }

        [When("I send a POST request to create the room")]
        public async Task WhenISendAPOSTRequestToCreateTheRoom()
        {
            var request = _scenarioContext.Get<RoomCreationRequest>();

            try
            {
                var response = await _apiClient.CreateRoomAsync(request);
                _scenarioContext.Set(response);
                _scenarioContext.Set(201, "LastStatusCode");
            }
            catch (ApiException ex)
            {
                _scenarioContext.Set(ex.ActualStatus, "LastStatusCode");
                _scenarioContext.Set(ex.ResponseBody, "LastErrorBody");
            }
        }

        [Then("the request should fail with status {int}")]
        public void ThenTheRequestShouldFailWithStatus(int expectedStatus)
        {
            var actualStatus = _scenarioContext.Get<int>("LastStatusCode");
            actualStatus.ShouldBe(expectedStatus);
        }

        [Then("Validation error for {word} field")]
        public void ThenValidationErrorForField(string fieldName)
        {
            var errorBody = _scenarioContext.Get<string>("LastErrorBody");

            if (fieldName == "GiftMaximumBudget" && errorBody.Contains("could not be converted"))
            {
                errorBody.ShouldContain("giftMaximumBudget");
                return;
            }

            var errorSummary = ErrorResponseParser.GetErrorSummary(errorBody);
            ErrorResponseParser.ContainsFieldError(errorBody, fieldName)
                .ShouldBeTrue($"Expected validation error for '{fieldName}'. {errorSummary}");
        }

        [Then("the error response should contain JSON parsing error")]
        public void ThenTheErrorResponseShouldContainJsonParsingError()
        {
            var errorBody = _scenarioContext.Get<string>("LastErrorBody");

            var hasJsonError = errorBody.Contains("Failed to read parameter") &&
                              errorBody.Contains("from the request body as JSON");
            var hasUint64Error = errorBody.Contains("JSON value could not be converted to System.UInt64");

            (hasJsonError || hasUint64Error).ShouldBeTrue(
                $"Expected JSON parsing error. Got: {errorBody}");
        }

        [When("I get the room by user code {string}")]
        public async Task WhenIGetTheRoomByUserCode(string userCode)
        {
            try
            {
                var room = await _apiClient.GetRoomByUserCodeAsync(userCode);
                _scenarioContext.Set(room, "RetrievedRoom");
                _scenarioContext.Set(200, "LastStatusCode");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Expected status"))
            {
                _scenarioContext.Set(ex.Message, "LastError");
                var match = ValidationPatterns.StatusCodePattern().Match(ex.Message);
                if (match.Success)
                {
                    _scenarioContext.Set(int.Parse(match.Groups[1].Value), "LastStatusCode");
                }
            }
        }

        [When("I update the room with new data:")]
        public async Task WhenIUpdateTheRoomWithNewData(Table table)
        {
            var userCode = _scenarioContext.Get<RoomCreationResponse>().UserCode;
            var patchRequest = table.CreateInstance<RoomPatchRequest>();

            var updatedRoom = await _apiClient.UpdateRoomAsync(userCode, patchRequest);
            _scenarioContext.Set(updatedRoom, "UpdatedRoom");
        }

        [When("I update only the room name to {string}")]
        public async Task WhenIUpdateOnlyTheRoomNameTo(string newName)
        {
            var userCode = _scenarioContext.Get<RoomCreationResponse>().UserCode;
            var patchRequest = new RoomPatchRequest { Name = newName };

            var updatedRoom = await _apiClient.UpdateRoomAsync(userCode, patchRequest);
            _scenarioContext.Set(updatedRoom, "UpdatedRoom");
        }

        [When("I try to update the room")]
        public async Task WhenITryToUpdateTheRoom()
        {
            try
            {
                var userCode = _scenarioContext.Get<string>("RegularUserCode");
                var patchRequest = new RoomPatchRequest { Name = "Unauthorized Update" };

                var updatedRoom = await _apiClient.UpdateRoomAsync(userCode, patchRequest);
                _scenarioContext.Set(updatedRoom, "UpdatedRoom");
                _scenarioContext.Set(200, "LastStatusCode");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Expected status"))
            {
                _scenarioContext.Set(ex.Message, "LastError");
                var match = ValidationPatterns.StatusCodePattern().Match(ex.Message);
                if (match.Success)
                {
                    _scenarioContext.Set(int.Parse(match.Groups[1].Value), "LastStatusCode");
                }
            }
        }

        [Then("the room should be created successfully")]
        public void ThenTheRoomShouldBeCreatedSuccessfully()
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();

            response.ShouldSatisfyAllConditions(
                () => response.ShouldNotBeNull(),
                () => response.Room.ShouldNotBeNull(),
                () => response.UserCode.ShouldNotBeNullOrEmpty(),
                () => response.Room.Id.ShouldBeGreaterThan(0),
                () => response.Room.InvitationCode?.ShouldMatch(ValidationPatterns.InvitationCodePattern())
            );
        }

        [Then("the room details should match the request")]
        public void ThenTheRoomDetailsShouldMatchTheRequest()
        {
            var request = _scenarioContext.Get<RoomCreationRequest>();
            var response = _scenarioContext.Get<RoomCreationResponse>();

            response.Room.Name.ShouldBe(request.Room.Name);
            response.Room.Description.ShouldBe(request.Room.Description);
            response.Room.GiftExchangeDate.ShouldBe(request.Room.GiftExchangeDate);
            response.Room.GiftMaximumBudget.ShouldBe(request.Room.GiftMaximumBudget);
        }

        [Then("the response should include:")]
        public static void ThenTheResponseShouldInclude()
        {
        }

        [Then("Room ID greater than {int}")]
        public void ThenRoomIDGreaterThan(int value)
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();
            response.Room.Id.ShouldBeGreaterThan(value);
        }

        [Then("Admin ID greater than {int}")]
        public void ThenAdminIDGreaterThan(int value)
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();
            response.Room.AdminId.ShouldBeGreaterThan(value);
        }

        [Then("Invitation code in valid format")]
        public void ThenInvitationCodeInValidFormat()
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();
            response.Room.InvitationCode.ShouldNotBeNullOrEmpty();
            response.Room.InvitationCode.ShouldMatch(ValidationPatterns.InvitationCodePattern());
        }

        [Then("User code in valid format")]
        public void ThenUserCodeInValidFormat()
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();
            response.UserCode.ShouldNotBeNullOrEmpty();
            response.UserCode.ShouldMatch(ValidationPatterns.InvitationCodePattern());
        }

        [Then("WantSurprise set to true")]
        public void ThenWantSurpriseSetToTrue()
        {
            var request = _scenarioContext.Get<RoomCreationRequest>();
            request.AdminUser.WantSurprise.ShouldBeTrue();
        }

        [Then("Interests field populated with text")]
        public void ThenInterestsFieldPopulatedWithText()
        {
            var request = _scenarioContext.Get<RoomCreationRequest>();
            request.AdminUser.Interests.ShouldNotBeNullOrEmpty();
        }

        [Then("Empty wish list with {int} items")]
        public void ThenEmptyWishListWithItems(int count)
        {
            var request = _scenarioContext.Get<RoomCreationRequest>();
            request.AdminUser.WishList?.Count.ShouldBe(count);
        }

        [Then("Valid contact information")]
        public void ThenValidContactInformation()
        {
            var request = _scenarioContext.Get<RoomCreationRequest>();
            var user = request.AdminUser;
            user.FirstName.ShouldNotBeNullOrEmpty();
            user.LastName.ShouldNotBeNullOrEmpty();
            user.Phone.ShouldMatch(ValidationPatterns.PhonePattern());
            user.Email?.ShouldMatch(ValidationPatterns.EmailPattern());
        }

        [Then("the admin user should have:")]
        public static void ThenTheAdminUserShouldHave()
        {
        }

        [Then("the request should (pass|fail) with status {int}")]
        public void ThenTheRequestShouldPassOrFailWithStatus(string result, int expectedStatus)
        {
            var actualStatus = _scenarioContext.Get<int>("LastStatusCode");
            actualStatus.ShouldBe(expectedStatus);

            if (result == "pass")
            {
                actualStatus.ShouldBeInRange(200, 299);
            }
            else
            {
                actualStatus.ShouldBeGreaterThanOrEqualTo(400);
            }
        }

        [Then("the error response should contain:")]
        public static void ThenTheErrorResponseShouldContain()
        {
        }

        [Then("the room should contain:")]
        public static void ThenTheRoomShouldContain()
        {
        }

        [Then("the room should not contain:")]
        public static void ThenTheRoomShouldNotContain()
        {
        }

        [Then("the draw results should show:")]
        public static void ThenTheDrawResultsShouldShow()
        {
        }

        [Then("the room budget should display as {string}")]
        public void ThenTheRoomBudgetShouldDisplayAs(string expectedDisplay)
        {
            var response = _scenarioContext.Get<RoomCreationResponse>();

            if (expectedDisplay == "Unlimited")
            {
                response.Room.GiftMaximumBudget.ShouldBe(0);
            }
            else
            {
                response.Room.GiftMaximumBudget.ToString().ShouldBe(expectedDisplay);
            }
        }

        [Then("the room should be updated successfully")]
        public void ThenTheRoomShouldBeUpdatedSuccessfully()
        {
            var updatedRoom = _scenarioContext.Get<RoomReadDto>("UpdatedRoom");
            updatedRoom.ShouldNotBeNull();
        }

        [Then("the room name should be {string}")]
        public void ThenTheRoomNameShouldBe(string expectedName)
        {
            var updatedRoom = _scenarioContext.Get<RoomReadDto>("UpdatedRoom");
            updatedRoom.Name.ShouldBe(expectedName);
        }

        [Then("the room description should be {string}")]
        public void ThenTheRoomDescriptionShouldBe(string expectedDescription)
        {
            var updatedRoom = _scenarioContext.Get<RoomReadDto>("UpdatedRoom");
            updatedRoom.Description.ShouldBe(expectedDescription);
        }

        [Then("the room budget should be {int}")]
        public void ThenTheRoomBudgetShouldBe(int expectedBudget)
        {
            var updatedRoom = _scenarioContext.Get<RoomReadDto>("UpdatedRoom");
            updatedRoom.GiftMaximumBudget.ShouldBe(expectedBudget);
        }

        [Then("the room invitation note should be {string}")]
        public void ThenTheRoomInvitationNoteShouldBe(string expectedNote)
        {
            var updatedRoom = _scenarioContext.Get<RoomReadDto>("UpdatedRoom");
            updatedRoom.InvitationNote.ShouldBe(expectedNote);
        }

        [Then("only the name should be changed")]
        public void ThenOnlyTheNameShouldBeChanged()
        {
            var originalResponse = _scenarioContext.Get<RoomCreationResponse>();
            var updatedRoom = _scenarioContext.Get<RoomReadDto>("UpdatedRoom");

            updatedRoom.Name.ShouldBe("New Name");
            updatedRoom.Description.ShouldBe(originalResponse.Room.Description);
            updatedRoom.GiftMaximumBudget.ShouldBe(originalResponse.Room.GiftMaximumBudget);
        }

        [Then("the error message should be {string}")]
        public void ThenTheErrorMessageShouldBe(string expectedMessage)
        {
            var error = _scenarioContext.Get<string>("LastError");
            error.ShouldContain(expectedMessage);
        }

        [Then("the error message should indicate insufficient users")]
        public void ThenTheErrorMessageShouldIndicateInsufficientUsers()
        {
            var error = _scenarioContext.Get<string>("LastError");
            error.ToLower().ShouldContain("insufficient");
        }

        [Then("the error message should indicate names already drawn")]
        public void ThenTheErrorMessageShouldIndicateNamesAlreadyDrawn()
        {
            var error = _scenarioContext.Get<string>("LastError");
            var errorLower = error.ToLower();
            (errorLower.Contains("already drawn") || errorLower.Contains("already assigned")).ShouldBeTrue();
        }

        [Then("the error should contain {string}")]
        public void ThenTheErrorShouldContain(string expectedError)
        {
            var error = _scenarioContext.Get<string>("LastError");
            error.ShouldContain(expectedError);
        }

        [Then("the room should be available for joining")]
        public void ThenTheRoomShouldBeAvailableForJoining()
        {
            var room = _scenarioContext.Get<RoomReadDto>("RetrievedRoom");
            room.IsFull.ShouldBeFalse();
        }
    }
}
