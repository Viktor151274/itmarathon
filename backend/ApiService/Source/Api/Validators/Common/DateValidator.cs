using System.Globalization;

namespace Epam.ItMarathon.ApiService.Api.Validators.Common
{
    /// <summary>
    /// Validation for the dates.
    /// </summary>
    public static class DateValidators
    {
        /// <summary>
        /// Validation if the date is in correct UTC ISO format.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Returns true if format is correct, other way - false.</returns>
        public static bool DateCorrectUtcIsoFormat(string date)
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
