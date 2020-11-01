 using System;

 namespace InstaSharper.Utils.Encryption.Lib
{
    internal class Check
    {
        internal static void DataLength(bool condition, string msg)
        {
            if (condition)
                throw new Exception(msg);
        }

        internal static void DataLength(byte[] buf, int off, int len, string msg)
        {
            if (off > (buf.Length - len))
                throw new Exception(msg);
        }

        internal static void OutputLength(byte[] buf, int off, int len, string msg)
        {
            if (off > (buf.Length - len))
                throw new Exception(msg);
        }
    }
}
