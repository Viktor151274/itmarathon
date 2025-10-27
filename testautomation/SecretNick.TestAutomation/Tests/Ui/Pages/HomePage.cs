using Microsoft.Playwright;

namespace Tests.Ui.Pages
{
    public class HomePage(IPage page) : BasePage(page)
    {
        public async Task<string> GetTitleTextAsync()
        {
            return await GetTextAsync(".//*[@class='home-page__title']");
        }

        public async Task<bool> IsTitleVisibleAsync()
        {
            return await IsVisibleAsync(".//*[@class='home-page__title']");
        }
    }
}
