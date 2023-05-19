namespace RothschildHouse.Application.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime? ToStartDateTime(this DateTime? dt)
            => new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, 0, 0, 0, 0);

        public static DateTime? ToEndDateTime(this DateTime? dt)
            => new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, 23, 59, 59, 59);
    }
}
