using Microsoft.Playwright;

namespace Tests.Helpers
{
    public static class LocatorExtensions
    {
        public static async Task<string> GetTextSafeAsync(
            this ILocator locator,
            int timeoutMs = 5000)
        {
            await locator.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });
            return await locator.TextContentAsync() ?? String.Empty;
        }

        public static async Task<bool> IsVisibleSafeAsync(
            this ILocator locator,
            int timeoutMs = 2000)
        {
            try
            {
                await locator.WaitForAsync(new()
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeoutMs
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task ClickSafeAsync(
            this ILocator locator,
            int timeoutMs = 5000)
        {
            await locator.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });
            await locator.ClickAsync();
        }

        public static async Task FillSafeAsync(
            this ILocator locator,
            string value,
            int timeoutMs = 5000)
        {
            await locator.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });
            await locator.FillAsync(value);
        }
    }
}
