using System.Text.Json;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Epam.ItMarathon.ApiService.Domain.Shared
{
    /// <summary>
    /// Static extension class for <see cref="ValidationResult"/>
    /// </summary>
    public static class ValidationResultExtensions
    {
        /// <summary>
        /// Extension method for parsing result of Validation to JSON format.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ParseErrorsToJson(this ValidationResult result)
        {
            var errorDict = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList()
                    );
            return JsonSerializer.Serialize(errorDict);
        }
    }
}
