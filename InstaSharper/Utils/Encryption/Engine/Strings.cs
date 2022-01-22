using System;
using System.Text;

namespace InstaSharper.Utils.Encryption.Engine;

/// <summary> General string utilities.</summary>
internal abstract class Strings
{
    public static string ToUpperCase(string original)
    {
        var changed = false;
        var chars = original.ToCharArray();

        for (var i = 0; i != chars.Length; i++)
        {
            var ch = chars[i];
            if ('a' <= ch && 'z' >= ch)
            {
                changed = true;
                chars[i] = (char)(ch - 'a' + 'A');
            }
        }

        if (changed) return new string(chars);

        return original;
    }


    internal static bool IsOneOf(string s,
                                 params string[] candidates)
    {
        foreach (var candidate in candidates)
            if (s == candidate)
                return true;

        return false;
    }

    public static string FromByteArray(
        byte[] bs)
    {
        var cs = new char[bs.Length];
        for (var i = 0; i < cs.Length; ++i) cs[i] = Convert.ToChar(bs[i]);

        return new string(cs);
    }

    public static byte[] ToByteArray(
        char[] cs)
    {
        var bs = new byte[cs.Length];
        for (var i = 0; i < bs.Length; ++i) bs[i] = Convert.ToByte(cs[i]);

        return bs;
    }

    public static byte[] ToByteArray(
        string s)
    {
        var bs = new byte[s.Length];
        for (var i = 0; i < bs.Length; ++i) bs[i] = Convert.ToByte(s[i]);

        return bs;
    }

    public static string FromAsciiByteArray(
        byte[] bytes)
    {
#if SILVERLIGHT || PORTABLE
            // TODO Check for non-ASCII bytes in input?
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
#else
        return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
#endif
    }

    public static byte[] ToAsciiByteArray(
        char[] cs)
    {
#if SILVERLIGHT || PORTABLE
            // TODO Check for non-ASCII characters in input?
            return Encoding.UTF8.GetBytes(cs);
#else
        return Encoding.ASCII.GetBytes(cs);
#endif
    }

    public static byte[] ToAsciiByteArray(
        string s)
    {
#if SILVERLIGHT || PORTABLE
            // TODO Check for non-ASCII characters in input?
            return Encoding.UTF8.GetBytes(s);
#else
        return Encoding.ASCII.GetBytes(s);
#endif
    }

    public static string FromUtf8ByteArray(
        byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }

    public static byte[] ToUtf8ByteArray(
        char[] cs)
    {
        return Encoding.UTF8.GetBytes(cs);
    }

    public static byte[] ToUtf8ByteArray(
        string s)
    {
        return Encoding.UTF8.GetBytes(s);
    }
}