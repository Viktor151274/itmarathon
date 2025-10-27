using Microsoft.Playwright;

namespace Tests.Helpers
{
    public static class WaitHelper
    {
        public static async Task<bool> WaitForConditionAsync(
            Func<Task<bool>> condition,
            int timeoutMs = 5000,
            int pollIntervalMs = 200)
        {
            var endTime = DateTime.Now.AddMilliseconds(timeoutMs);

            while (DateTime.Now < endTime)
            {
                if (await condition())
                    return true;

                await Task.Delay(pollIntervalMs);
            }

            return false;
        }

        public static async Task WaitForSpaTransitionAsync(IPage page)
        {
            try
            {
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 3000 });
            }
            catch
            {
            }

            await Task.Delay(200);
        }
    }
}
