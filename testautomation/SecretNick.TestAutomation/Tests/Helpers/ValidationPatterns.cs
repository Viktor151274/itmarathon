using System.Text.RegularExpressions;

namespace Tests.Helpers
{
    public static partial class ValidationPatterns
    {
        [GeneratedRegex(@"^[a-f0-9]{32}$")]
        public static partial Regex InvitationCodePattern();

        [GeneratedRegex(@"^\+380\d{9}$")]
        public static partial Regex PhonePattern();

        [GeneratedRegex(@"^[\w\.-]+@[\w\.-]+\.\w+$")]
        public static partial Regex EmailPattern();

        [GeneratedRegex(@"got (\d+)")]
        public static partial Regex StatusCodePattern();

        [GeneratedRegex(@"\d+")]
        public static partial Regex Participants();

        [GeneratedRegex(@"http://[^\s]+")]
        public static partial Regex Link();
    }
}
