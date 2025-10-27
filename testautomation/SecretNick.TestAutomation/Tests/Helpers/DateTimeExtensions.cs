namespace Tests.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime Simplify(this DateTime complexDateTime)
        {
            return new DateTime(
                complexDateTime.Year, 
                complexDateTime.Month, 
                complexDateTime.Day, 
                complexDateTime.Hour, 
                complexDateTime.Minute, 
                complexDateTime.Second, 
                DateTimeKind.Unspecified);
        }
    }
}
