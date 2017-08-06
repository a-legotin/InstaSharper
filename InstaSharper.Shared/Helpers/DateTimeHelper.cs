using System;

namespace InstaSharper.Helpers
{
    internal static class DateTimeHelper
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        public static double GetUnixTimestampMilliseconds(DateTime dt)
        {
            var span = dt - UnixEpoch;
            return span.TotalMilliseconds;
        }

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
                return UnixEpoch.AddSeconds(unixTime);
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
                return UnixEpoch.AddMilliseconds(unixTime);
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
                return Convert.ToInt64((date - UnixEpoch).TotalSeconds);
            }
            catch
            {
                return 0;
            }
        }
    }
}