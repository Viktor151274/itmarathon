using Microsoft.Playwright;
using Tests.Helpers;

namespace Tests.Ui.Pages
{
    public class JoinPage(IPage page) : BasePage(page)
    {
        public async Task<bool> IsWelcomeTitleVisibleAsync(string roomName)
        {
            return await IsVisibleAsync($".//*[contains(@class,'_title')][.='Welcome to the {roomName}!']");
        }

        public async Task<string> GetExchangeDateAsync()
        {
            var locator = Page.Locator("xpath=.//*[contains(@class,'room-data-card')][contains(.,'Exchange Date')]//*[@class='card__content'] | .//*[contains(@class,'info-card')][contains(.,'Exchange Date')]//*[@class='info-card__description']").First;
            return await locator.GetTextSafeAsync();
        }

        public async Task<string> GetGiftBudgetAsync()
        {
            var locator = Page.Locator("xpath=.//*[contains(@class,'room-data-card')][contains(.,'Gift Budget')]//p | .//*[contains(@class,'info-card')][contains(.,'Gift Budget')]//*[@class='info-card__description']").First;
            return await locator.GetTextSafeAsync();
        }
    }
}
