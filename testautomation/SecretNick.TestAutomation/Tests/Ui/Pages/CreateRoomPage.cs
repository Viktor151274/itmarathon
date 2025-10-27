using Microsoft.Playwright;
using Tests.Helpers;

namespace Tests.Ui.Pages
{
    public class CreateRoomPage(IPage page) : BasePage(page)
    {
        public async Task SelectDateAsync()
        {
            var locator = Page.Locator("xpath=.//*[@placeholder='Select date']");
            await locator.ClickSafeAsync();
        }

        public async Task SelectTodayAsync()
        {
            var locator = Page.Locator("xpath=.//a[normalize-space()='Today']");
            await locator.ClickSafeAsync();
        }

        public async Task SelectCustomDateAsync(DateTime date)
        {
            var nextButton = Page.Locator("xpath=.//button[@class='ant-picker-header-next-btn']");
            await nextButton.ClickSafeAsync();

            var dateString = date.ToString("MM-dd");
            var dateCell = Page.Locator($"xpath=.//td[contains(@title,'{dateString}')]");
            await dateCell.ClickSafeAsync();
        }

        public async Task SelectCustomDateAsync(string date)
        {
            await SelectCustomDateAsync(DateTime.Parse(date));
        }

        public async Task<bool> IsUnlimitedBudgetTextVisibleAsync()
        {
            var locator1Visible = await IsVisibleAsync(".//p[normalize-space()='0 means unlimited budget']");
            var locator2Visible = await IsVisibleAsync(".//span[normalize-space()='0 means unlimited budget']");
            return locator1Visible || locator2Visible;
        }

        public async Task<bool> IsBackButtonVisibleAsync()
        {
            return await IsVisibleAsync(".//button[.='Back to the previous step']");
        }

        public async Task<string> GetBudgetTextAsync()
        {
            return await GetTextAsync(".//p[contains(.,'Gift Budget')]");
        }
    }
}
