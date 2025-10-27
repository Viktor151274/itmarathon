using Microsoft.Playwright;
using Tests.Helpers;

namespace Tests.Ui.Pages
{
    public class CreateRoomSuccessPage(IPage page) : BaseSuccessPage(page)
    {
        public override async Task<bool> IsOnSuccessPageAsync()
        {
            // Check if ANY success indicator is present
            var titleVisible = await IsSuccessTitleVisibleAsync();
            if (titleVisible)
                return true;

            var buttonVisible = await base.IsOnSuccessPageAsync();
            return buttonVisible;
        }

        public async Task<bool> IsSuccessTitleVisibleAsync(string roomName)
        {
            var locator = Page.Locator($"xpath=.//*[contains(@class,'_title')][.='Your {roomName} Room is Ready!']");
            return await locator.IsVisibleSafeAsync(5000);
        }

        public async Task<bool> IsSuccessTitleVisibleAsync()
        {
            var locator = Page.Locator("xpath=.//*[contains(@class,'_title')][contains(.,'Room is Ready!')]");
            return await locator.IsVisibleSafeAsync(5000);
        }

        public async Task<string> GetRoomLinkAsync()
        {
            return await ClickOnCopyAndGetClipboardText("Your Room Link");
        }

        public async Task<string> GetInvitationNoteAsync()
        {
            return await ClickOnCopyAndGetClipboardText("Invitation Note");
        }
    }
}
