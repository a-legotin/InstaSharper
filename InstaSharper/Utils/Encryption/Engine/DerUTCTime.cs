using System;
using System.Globalization;

namespace InstaSharper.Utils.Encryption.Engine
{
    /**
     * UTC time object.
     */
    internal class DerUtcTime
        : Asn1Object
    {
        private readonly string time;

        /**
         * The correct format for this is YYMMDDHHMMSSZ (it used to be that seconds were
         * never encoded. When you're creating one of these objects from scratch, that's
         * what you want to use, otherwise we'll try to deal with whatever Gets read from
         * the input stream... (this is why the input format is different from the GetTime()
         * method output).
         * <p>
         * @param time the time string.</p>
         */
        public DerUtcTime(
            string time)
        {
            if (time == null)
                throw new ArgumentNullException("time");

            this.time = time;

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
         * base constructor from a DateTime object
         */
        public DerUtcTime(
            DateTime time)
        {
#if PORTABLE
            this.time = time.ToUniversalTime().ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
#else
            this.time = time.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
#endif
        }

        internal DerUtcTime(
            byte[] bytes) =>
            //
            // explicitly convert to characters
            //
            time = Strings.FromAsciiByteArray(bytes);

        /**
         * return the time - always in the form of
         *  YYMMDDhhmmssGMT(+hh:mm|-hh:mm).
         * <p>
         * Normally in a certificate we would expect "Z" rather than "GMT",
         * however adding the "GMT" means we can just use:
         * <pre>
         *     dateF = new SimpleDateFormat("yyMMddHHmmssz");
         * </pre>
         * To read in the time and Get a date which is compatible with our local
         * time zone.</p>
         * <p>
         * <b>Note:</b> In some cases, due to the local date processing, this
         * may lead to unexpected results. If you want to stick the normal
         * convention of 1950 to 2049 use the GetAdjustedTime() method.</p>
         */
        public string TimeString
        {
            get
            {
                //
                // standardise the format.
                //
                if (time.IndexOf('-') < 0 && time.IndexOf('+') < 0)
                {
                    if (time.Length == 11)
                    {
                        return time.Substring(0, 10) + "00GMT+00:00";
                    }

                    return time.Substring(0, 12) + "GMT+00:00";
                }

                var index = time.IndexOf('-');
                if (index < 0)
                {
                    index = time.IndexOf('+');
                }

                var d = time;

                if (index == time.Length - 3)
                {
                    d += "00";
                }

                if (index == 10)
                {
                    return d.Substring(0, 10) + "00GMT" + d.Substring(10, 3) + ":" + d.Substring(13, 2);
                }

                return d.Substring(0, 12) + "GMT" + d.Substring(12, 3) + ":" +  d.Substring(15, 2);
            }
        }

        [Obsolete("Use 'AdjustedTimeString' property instead")]
        public string AdjustedTime => AdjustedTimeString;

        /// <summary>
        /// Return a time string as an adjusted date with a 4 digit year.
        /// This goes in the range of 1950 - 2049.
        /// </summary>
        public string AdjustedTimeString
        {
            get
            {
                var d = TimeString;
                var c = d[0] < '5' ? "20" : "19";

                return c + d;
            }
        }

        /**
         * return an UTC Time from the passed in object.
         *
         * @exception ArgumentException if the object cannot be converted.
         */
        public static DerUtcTime GetInstance(
            object obj)
        {
            if (obj == null || obj is DerUtcTime)
            {
                return (DerUtcTime) obj;
            }

            throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
        }

        /**
         * return an UTC Time from a tagged object.
         *
         * @param obj the tagged object holding the object we want
         * @param explicitly true if the object is meant to be explicitly
         *              tagged false otherwise.
         * @exception ArgumentException if the tagged object cannot
         *               be converted.
         */
        public static DerUtcTime GetInstance(
            Asn1TaggedObject obj,
            bool isExplicit)
        {
            var o = obj.GetObject();

            if (isExplicit || o is DerUtcTime)
            {
                return GetInstance(o);
            }

            return new DerUtcTime(((Asn1OctetString) o).GetOctets());
        }

//		public DateTime ToDateTime()
//		{
//			string tm = this.AdjustedTimeString;
//
//			return new DateTime(
//				Int16.Parse(tm.Substring(0, 4)),
//				Int16.Parse(tm.Substring(4, 2)),
//				Int16.Parse(tm.Substring(6, 2)),
//				Int16.Parse(tm.Substring(8, 2)),
//				Int16.Parse(tm.Substring(10, 2)),
//				Int16.Parse(tm.Substring(12, 2)));
//		}

        /**
		 * return the time as a date based on whatever a 2 digit year will return. For
		 * standardised processing use ToAdjustedDateTime().
		 *
		 * @return the resulting date
		 * @exception ParseException if the date string cannot be parsed.
		 */
        public DateTime ToDateTime() => ParseDateString(TimeString, @"yyMMddHHmmss'GMT'zzz");

        /**
		* return the time as an adjusted date
		* in the range of 1950 - 2049.
		*
		* @return a date in the range of 1950 to 2049.
		* @exception ParseException if the date string cannot be parsed.
		*/
        public DateTime ToAdjustedDateTime() => ParseDateString(AdjustedTimeString, @"yyyyMMddHHmmss'GMT'zzz");

        private DateTime ParseDateString(
            string dateStr,
            string formatStr)
        {
            var dt = DateTime.ParseExact(
                dateStr,
                formatStr,
                DateTimeFormatInfo.InvariantInfo);

            return dt.ToUniversalTime();
        }

        private byte[] GetOctets() => Strings.ToAsciiByteArray(time);

        internal override void Encode(
            DerOutputStream derOut)
        {
            derOut.WriteEncoded(Asn1Tags.UtcTime, GetOctets());
        }

        protected override bool Asn1Equals(
            Asn1Object asn1Object)
        {
            var other = asn1Object as DerUtcTime;

            if (other == null)
                return false;

            return time.Equals(other.time);
        }

        protected override int Asn1GetHashCode() => time.GetHashCode();

        public override string ToString() => time;
    }
}