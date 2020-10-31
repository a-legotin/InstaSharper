using System;

namespace InstaSharper.Utils
{
    internal static class DateTimeHelper
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetUnixTimestampMilliseconds(DateTime dt) => (dt - UnixEpoch).TotalMilliseconds;

        public static DateTime UnixTimestampToDateTime(double unixTime) => ((long) unixTime).FromUnixTimeSeconds();

        public static DateTime UnixTimestampToDateTime(string unixTime) => unixTime.Length <= 10
            ? ((long) Convert.ToDouble(unixTime)).FromUnixTimeSeconds()
            : UnixTimestampMillisecondsToDateTime(unixTime);

        private static DateTime UnixTimestampMillisecondsToDateTime(string unixTime) =>
            ((long) Convert.ToDouble(unixTime) / 1000000L).FromUnixTimeSeconds();

        private static DateTime FromUnixTimeSeconds(this long unixTime) => UnixEpoch.AddSeconds(unixTime);

        public static DateTime FromUnixTimeMilliSeconds(this long unixTime) => UnixEpoch.AddMilliseconds(unixTime);

        public static long ToUnixTime(this DateTime date) => Convert.ToInt64((date - UnixEpoch).TotalSeconds);

        public static long ToUnixTimeMilliSeconds(this DateTime date) =>
            Convert.ToInt64((date - UnixEpoch).TotalMilliseconds);

        public static long GetUnixTimestampSeconds() => (long) (DateTime.UtcNow - UnixEpoch).TotalSeconds;
    }
}