using Microsoft.Playwright;

namespace Tests.Ui.Pages
{
    public class AddWishesPage(IPage page) : BasePage(page)
    {
        public async Task AddWishAsync()
        {
            await ClickButtonAsync("Add Wish");
        }

        public async Task<bool> IsAddWishButtonPresentAsync()
        {
            return await Page.Locator("xpath=.//button[.='Add Wish']").CountAsync() > 0;
        }
    }
}
