namespace Tests.Helpers
{
    public static class PhoneExtensions
    {
        public static string TruncatePhone(this string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return string.Empty;

            var cleaned = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            if (cleaned.StartsWith("+380") && cleaned.Length >= 13)
            {
                return cleaned[4..];
            }

            return cleaned;
        }
    }

}
