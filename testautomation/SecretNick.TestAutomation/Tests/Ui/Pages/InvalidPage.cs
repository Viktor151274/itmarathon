using Microsoft.Playwright;
using Tests.Helpers;

namespace Tests.Ui.Pages
{
    public class InvalidPage(IPage page) : BasePage(page)
    {
        public async Task<bool> IsErrorPageVisibleAsync()
        {
            var h1Locator = Page.Locator("xpath=.//h1");
            var h1Text = await h1Locator.GetTextSafeAsync(2000);

            if (h1Text.Contains("Oops! Page Not Found"))
                return true;

            var h2Locator = Page.Locator("xpath=.//h2");
            var h2Text = await h2Locator.GetTextSafeAsync(2000);

            return h2Text.Contains("Oops! Page Not Found");
        }

        public async Task ClickGoHomeAsync()
        {
            await ClickButtonAsync("Go Home");
        }
    }
}
