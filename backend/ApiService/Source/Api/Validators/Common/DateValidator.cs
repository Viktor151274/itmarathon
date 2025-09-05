using System.Globalization;

namespace Epam.ItMarathon.ApiService.Api.Validators.Common
{
    public static class DateValidators
    {
        public static bool DateNotPastUtcIso(string date)
        {
            try
            {
                var parsed = DateTime.Parse(
                    date,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
