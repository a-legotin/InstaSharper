using System;

namespace InstaSharper.Utils;

internal static class DateTimeHelper
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static double GetUnixTimestampMilliseconds(DateTime dt)
    {
        return (dt - UnixEpoch).TotalMilliseconds;
    }

    public static DateTime UnixTimestampToDateTime(double unixTime)
    {
        return ((long)unixTime).FromUnixTimeSeconds();
    }

    public static DateTime UnixTimestampToDateTime(string unixTime)
    {
        return unixTime.Length <= 10
            ? ((long)Convert.ToDouble(unixTime)).FromUnixTimeSeconds()
            : UnixTimestampMillisecondsToDateTime(unixTime);
    }

    public static DateTime UnixTimestampMicrosecondsToDateTime(long unixTime)
    {
        try
        {
            var time = unixTime / 1000000;
            return time.FromUnixTimeSeconds();
        }
        catch
        {
        }

        return DateTime.Now;
    }

    private static DateTime UnixTimestampMillisecondsToDateTime(string unixTime)
    {
        return ((long)Convert.ToDouble(unixTime) / 1000000L).FromUnixTimeSeconds();
    }

    private static DateTime FromUnixTimeSeconds(this long unixTime)
    {
        return UnixEpoch.AddSeconds(unixTime);
    }

    public static DateTime FromUnixTimeMilliSeconds(this long unixTime)
    {
        return UnixEpoch.AddMilliseconds(unixTime);
    }

    public static long ToUnixTime(this DateTime date)
    {
        return Convert.ToInt64((date - UnixEpoch).TotalSeconds);
    }

    public static long ToUnixTimeMilliSeconds(this DateTime date)
    {
        return Convert.ToInt64((date - UnixEpoch).TotalMilliseconds);
    }

    public static long GetUnixTimestampSeconds()
    {
        return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
    }
}