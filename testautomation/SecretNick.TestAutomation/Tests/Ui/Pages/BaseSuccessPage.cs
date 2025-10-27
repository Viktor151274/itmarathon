using Microsoft.Playwright;

namespace Tests.Ui.Pages
{
    public class BaseSuccessPage(IPage page) : BasePage(page)
    {
        public async Task<string> GetPersonalLinkAsync()
        {
            return await ClickOnCopyAndGetClipboardText("Your Personal Participant Link");
        }

        public virtual async Task<bool> IsOnSuccessPageAsync()
        {
            return await IsButtonVisibleAsync("Visit Your Room");
        }
    }
}
