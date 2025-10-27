using Microsoft.Playwright;
using Reqnroll;
using Tests.Api.Models.Responses;

namespace Tests.Ui.Steps
{
    [Binding]
    public class ComplexFlowSteps(
            ScenarioContext scenarioContext,
            IPage page,
            InteractionSteps interactionSteps) : UiStepsBase(page)
    {
        [When("user {int} joins room with generated data and surprise gift")]
        public async Task WhenUserJoinsRoomWithGeneratedDataAndSurpriseGift(int userNumber)
        {
            await NavigateToJoinPage();
            await interactionSteps.WhenIClickButton("Join the Room");
            await interactionSteps.WhenIFillUserFormWithGeneratedData();
            await interactionSteps.WhenIClickOutsideAnyElement();

            await interactionSteps.WhenIClickButton("Continue");
            await interactionSteps.WhenISelectOption("I want a surprise gift");

            await GetJoinPage().FillFieldAsync("Interests", $"User {userNumber} likes many things");
            await interactionSteps.WhenIClickOutsideAnyElement();
            await interactionSteps.WhenIClickButton("Complete");
        }

        [When("user {int} joins room with generated data and {int} wishes")]
        public async Task WhenUserJoinsRoomWithGeneratedDataAndWishes(int userNumber, int wishCount)
        {
            await NavigateToJoinPage();
            await interactionSteps.WhenIClickButton("Join the Room");
            await interactionSteps.WhenIFillUserFormWithGeneratedData();
            await interactionSteps.WhenIClickOutsideAnyElement();
            await interactionSteps.WhenIClickButton("Continue");
            await interactionSteps.WhenISelectOption("I have gift ideas! (add up to 5 gift ideas)");

            var wishNames = Enumerable.Range(1, wishCount)
                .Select(i => $"User{userNumber}Wish{i}")
                .ToList();
            await interactionSteps.WhenIAddWishesWithNames(wishCount, string.Join(",", wishNames));
            await interactionSteps.WhenIClickOutsideAnyElement();
            await interactionSteps.WhenIClickButton("Complete");
        }

        private async Task NavigateToJoinPage()
        {
            var baseUrl = scenarioContext.Get<string>("baseUrl");
            string invitationCode;

            if (scenarioContext.ContainsKey("RoomLink"))
            {
                var roomLink = scenarioContext.Get<string>("RoomLink");
                invitationCode = roomLink.Contains('/') ? roomLink.Split('/').Last() : roomLink;
            }

            else if (scenarioContext.ContainsKey("InvitationCode"))
            {
                invitationCode = scenarioContext.Get<string>("InvitationCode");
            }
            else
            {
                var response = scenarioContext.Get<RoomCreationResponse>("RoomCreationResponse");
                invitationCode = response.Room.InvitationCode!;
            }

            await page.GotoAsync($"{baseUrl}/join/{invitationCode}");
        }
    }
}
