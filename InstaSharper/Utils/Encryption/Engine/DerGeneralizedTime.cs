using System;
using System.Globalization;
using System.Text;

namespace InstaSharper.Utils.Encryption.Engine;

/**
     * Generalized time object.
     */
internal class DerGeneralizedTime
    : Asn1Object
{
    /**
         * The correct format for this is YYYYMMDDHHMMSS[.f]Z, or without the Z
         * for local time, or Z+-HHMM on the end, for difference between local
         * time and UTC time. The fractional second amount f must consist of at
         * least one number with trailing zeroes removed.
         *
         * @param time the time string.
         * @exception ArgumentException if string is an illegal format.
         */
    public DerGeneralizedTime(
        string time)
    {
        TimeString = time;

        try
        {
            ToDateTime();
        }
        catch (FormatException e)
        {
            throw new ArgumentException("invalid date string: " + e.Message);
        }
    }

    /**
         * base constructor from a local time object
         */
    public DerGeneralizedTime(
        DateTime time)
    {
#if PORTABLE
            this.time = time.ToUniversalTime().ToString(@"yyyyMMddHHmmss\Z");
#else
        TimeString = time.ToString(@"yyyyMMddHHmmss\Z");
#endif
    }

    internal DerGeneralizedTime(
        byte[] bytes)
    {
        //
        // explicitly convert to characters
        //
        TimeString = Strings.FromAsciiByteArray(bytes);
    }

    /**
         * Return the time.
         * @return The time string as it appeared in the encoded object.
         */
    public string TimeString { get; }

    private bool HasFractionalSeconds => TimeString.IndexOf('.') == 14;

    /**
         * return a generalized time from the passed in object
         *
         * @exception ArgumentException if the object cannot be converted.
         */
    public static DerGeneralizedTime GetInstance(
        object obj)
    {
        if (obj == null || obj is DerGeneralizedTime) return (DerGeneralizedTime)obj;

        throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
    }

    /**
     * return a Generalized Time object from a tagged object.
     * 
     * @param obj the tagged object holding the object we want
     * @param explicitly true if the object is meant to be explicitly
     * tagged false otherwise.
     * @exception ArgumentException if the tagged object cannot
     * be converted.
     */
    public static DerGeneralizedTime GetInstance(
        Asn1TaggedObject obj,
        bool isExplicit)
    {
        var o = obj.GetObject();

        if (isExplicit || o is DerGeneralizedTime) return GetInstance(o);

        return new DerGeneralizedTime(((Asn1OctetString)o).GetOctets());
    }

    /**
     * return the time - always in the form of
     * YYYYMMDDhhmmssGMT(+hh:mm|-hh:mm).
     * <p>
     *     Normally in a certificate we would expect "Z" rather than "GMT",
     *     however adding the "GMT" means we can just use:
     *     <pre>
     *         dateF = new SimpleDateFormat("yyyyMMddHHmmssz");
     *     </pre>
     *     To read in the time and Get a date which is compatible with our local
     *     time zone.
     * </p>
     */
    public string GetTime()
    {
        //
        // standardise the format.
        //
        if (TimeString[TimeString.Length - 1] == 'Z')
            return TimeString.Substring(0, TimeString.Length - 1) + "GMT+00:00";

        var signPos = TimeString.Length - 5;
        var sign = TimeString[signPos];
        if (sign == '-' || sign == '+')
            return TimeString.Substring(0, signPos)
                   + "GMT"
                   + TimeString.Substring(signPos, 3)
                   + ":"
                   + TimeString.Substring(signPos + 3);

        signPos = TimeString.Length - 3;
        sign = TimeString[signPos];
        if (sign == '-' || sign == '+')
            return TimeString.Substring(0, signPos)
                   + "GMT"
                   + TimeString.Substring(signPos)
                   + ":00";

        return TimeString + CalculateGmtOffset();
    }

    private string CalculateGmtOffset()
    {
        var sign = '+';
        var time = ToDateTime();

#if SILVERLIGHT || PORTABLE
            long offset = time.Ticks - time.ToUniversalTime().Ticks;
            if (offset < 0)
            {
                sign = '-';
                offset = -offset;
            }
            int hours = (int)(offset / TimeSpan.TicksPerHour);
            int minutes = (int)(offset / TimeSpan.TicksPerMinute) % 60;
#else
        // Note: GetUtcOffset incorporates Daylight Savings offset
        var offset = TimeZoneInfo.Local.GetUtcOffset(time);
        if (offset.CompareTo(TimeSpan.Zero) < 0)
        {
            sign = '-';
            offset = offset.Duration();
        }

        var hours = offset.Hours;
        var minutes = offset.Minutes;
#endif

        return "GMT" + sign + Convert(hours) + ":" + Convert(minutes);
    }

    private static string Convert(
        int time)
    {
        if (time < 10) return "0" + time;

        return time.ToString();
    }

    public DateTime ToDateTime()
    {
        string formatStr;
        var d = TimeString;
        var makeUniversal = false;

        if (Platform.EndsWith(d, "Z"))
        {
            if (HasFractionalSeconds)
            {
                var fCount = d.Length - d.IndexOf('.') - 2;
                formatStr = @"yyyyMMddHHmmss." + FString(fCount) + @"\Z";
            }
            else
            {
                formatStr = @"yyyyMMddHHmmss\Z";
            }
        }
        else if (TimeString.IndexOf('-') > 0 || TimeString.IndexOf('+') > 0)
        {
            d = GetTime();
            makeUniversal = true;

            if (HasFractionalSeconds)
            {
                var fCount = Platform.IndexOf(d, "GMT") - 1 - d.IndexOf('.');
                formatStr = @"yyyyMMddHHmmss." + FString(fCount) + @"'GMT'zzz";
            }
            else
            {
                formatStr = @"yyyyMMddHHmmss'GMT'zzz";
            }
        }
        else
        {
            if (HasFractionalSeconds)
            {
                var fCount = d.Length - 1 - d.IndexOf('.');
                formatStr = @"yyyyMMddHHmmss." + FString(fCount);
            }
            else
            {
                formatStr = @"yyyyMMddHHmmss";
            }

            // TODO?
//				dateF.setTimeZone(new SimpleTimeZone(0, TimeZone.getDefault().getID()));
        }

        return ParseDateString(d, formatStr, makeUniversal);
    }

    private string FString(
        int count)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < count; ++i) sb.Append('f');

        return sb.ToString();
    }

    private DateTime ParseDateString(string s,
                                     string format,
                                     bool makeUniversal)
    {
        /*
         * NOTE: DateTime.Kind and DateTimeStyles.AssumeUniversal not available in .NET 1.1
         */
        var style = DateTimeStyles.None;
        if (Platform.EndsWith(format, "Z"))
        {
            try
            {
                style = (DateTimeStyles)Enums.GetEnumValue(typeof(DateTimeStyles), "AssumeUniversal");
            }
            catch (Exception)
            {
            }

            style |= DateTimeStyles.AdjustToUniversal;
        }

        var dt = DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, style);

        return makeUniversal ? dt.ToUniversalTime() : dt;
    }

    private byte[] GetOctets()
    {
        return Strings.ToAsciiByteArray(TimeString);
    }

    internal override void Encode(
        DerOutputStream derOut)
    {
        derOut.WriteEncoded(Asn1Tags.GeneralizedTime, GetOctets());
    }

    protected override bool Asn1Equals(
        Asn1Object asn1Object)
    {
        var other = asn1Object as DerGeneralizedTime;

        if (other == null)
            return false;

        return TimeString.Equals(other.TimeString);
    }

    protected override int Asn1GetHashCode()
    {
        return TimeString.GetHashCode();
    }
}