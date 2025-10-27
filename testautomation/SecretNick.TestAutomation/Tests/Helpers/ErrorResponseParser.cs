using System.Text.Json;
using Serilog;

namespace Tests.Helpers
{
    public static class ErrorResponseParser
    {
        static readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static (bool hasValidationErrors, Dictionary<string, string[]>? errors)
            ParseValidationErrors(string errorBody)
        {
            try
            {
                var doc = JsonDocument.Parse(errorBody);

                if (doc.RootElement.TryGetProperty("errors", out var errorsElement))
                {
                    var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                        errorsElement.GetRawText(),
                        _options);
                    return (true, errors);
                }

                return (false, null);
            }
            catch
            {
                return (false, null);
            }
        }

        public static (bool hasValidationErrors, Dictionary<string, string[]>? errors, string? detail)
            ParseErrorResponse(string errorBody)
        {
            try
            {
                var doc = JsonDocument.Parse(errorBody);
                var root = doc.RootElement;

                string? detail = null;
                if (root.TryGetProperty("detail", out var detailElement))
                {
                    detail = detailElement.GetString();
                }

                if (root.TryGetProperty("errors", out var errorsElement))
                {
                    var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                        errorsElement.GetRawText(),
                        _options);

                    Log.Debug("Parsed validation errors: {Errors}",
                        string.Join(", ", errors?.Keys ?? new Dictionary<string, string[]>().Keys));

                    return (true, errors, detail);
                }

                return (false, null, detail);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to parse error response: {ErrorBody}", errorBody);
                return (false, null, null);
            }
        }

        public static bool ContainsFieldError(string errorBody, string fieldName)
        {
            try
            {
                // Handle JSON parsing errors specifically
                if (errorBody.Contains("JSON value could not be converted") &&
                    errorBody.Contains(fieldName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
                var (hasErrors, errors, detail) = ParseErrorResponse(errorBody);

            if (hasErrors && errors != null)
            {
                var found = errors.Keys.Any(k =>
                    k.Equals(fieldName, StringComparison.OrdinalIgnoreCase) ||
                    k.Contains(fieldName, StringComparison.OrdinalIgnoreCase));

                if (found)
                {
                    Log.Debug("Found field '{Field}' in validation errors", fieldName);
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(detail))
            {
                if (detail.Contains(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    Log.Debug("Found field '{Field}' in detail message", fieldName);
                    return true;
                }
            }

            var foundInBody = errorBody.Contains(fieldName, StringComparison.OrdinalIgnoreCase);
            if (foundInBody)
            {
                Log.Debug("Found field '{Field}' in error body", fieldName);
            }

            return foundInBody;
            }
            catch (Exception)
            {
                // Log the exception for debugging
                return false;
            }
        }

        public static string GetErrorSummary(string errorBody)
        {
            var (hasErrors, errors, detail) = ParseErrorResponse(errorBody);

            if (hasErrors && errors != null)
            {
                return $"Validation errors for fields: {string.Join(", ", errors.Keys)}";
            }

            if (!string.IsNullOrEmpty(detail))
            {
                return $"Error: {detail}";
            }

            return $"Raw error: {errorBody[..Math.Min(200, errorBody.Length)]}...";
        }
    }
}
