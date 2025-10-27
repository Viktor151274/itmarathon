using Microsoft.Playwright;
using Tests.Helpers;

namespace Tests.Ui.Pages
{
    public class JoinSuccessPage(IPage page) : BaseSuccessPage(page)
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

        public async Task<bool> IsSuccessTitleVisibleAsync()
        {
            var text1Locator = Page.Locator("xpath=.//h1[@class='page-layout__title'][.='You Have Joined the Room!']");
            if (await text1Locator.CountAsync() > 0 && await text1Locator.IsVisibleSafeAsync(5000))
                return true;

            var text2Locator = Page.Locator("xpath=.//h3[@class='form-wrapper__title'][.='You Have Joined the Room!']");
            if (await text2Locator.CountAsync() > 0 && await text2Locator.IsVisibleSafeAsync(5000))
                return true;

            return false;
        }
    }
}
