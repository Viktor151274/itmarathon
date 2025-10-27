using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Shouldly;

namespace Tests.Helpers
{
    public static class ShouldlyExtensions
    {
        private static string GenerateShouldMatchMessage(string actual, Regex regexObject, string shouldlyMethod, string? customMessage)
        {
            var messageObject = new ExpectedActualShouldlyMessage(
                expected: regexObject.ToString(),
                actual: actual,
                customMessage: customMessage,
                shouldlyMethod: shouldlyMethod
            );

            return messageObject.ToString();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShouldMatch(this string actual, Regex regexObject, string? customMessage = null)
        {
            if (!regexObject.IsMatch(actual))
            {
                var message = GenerateShouldMatchMessage(actual, regexObject, nameof(ShouldMatch), customMessage);

                throw new ShouldAssertException(message);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShouldNotMatch(this string actual, Regex regexObject, string? customMessage = null)
        {
            if (regexObject.IsMatch(actual))
            {
                var message = GenerateShouldMatchMessage(actual, regexObject, nameof(ShouldNotMatch), customMessage);

                throw new ShouldAssertException(message);
            }
        }
    }
}
