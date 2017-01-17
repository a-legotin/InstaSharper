using System;

namespace InstaSharper.Helpers
{
    internal static class DateTimeHelper
    {
        public static DateTime UnixTimestampToDateTime(double unixTime)
        {
            var time = (long) unixTime;
            return time.FromUnixTimeSeconds();
        }

        public static DateTime UnixTimestampToDateTime(string unixTime)
        {
            var time = (long) Convert.ToDouble(unixTime);
            return time.FromUnixTimeSeconds();
        }

        public static DateTime UnixTimestampMilisecondsToDateTime(string unixTime)
        {
            var time = (long) Convert.ToDouble(unixTime) / 1000000;
            return time.FromUnixTimeSeconds();
        }

        public static DateTime FromUnixTimeSeconds(this long unixTime)
        {
            try
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(unixTime);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime FromUnixTimeMiliSeconds(this long unixTime)
        {
            try
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddMilliseconds(unixTime);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static long ToUnixTime(this DateTime date)
        {
            try
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return Convert.ToInt64((date - epoch).TotalSeconds);
            }
            catch
            {
                return 0;
            }
        }
    }
}