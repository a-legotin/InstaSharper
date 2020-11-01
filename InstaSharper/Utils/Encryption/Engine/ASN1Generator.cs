using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class Asn1Generator
    {
        protected Asn1Generator(
            Stream outStream) =>
            Out = outStream;

        protected Stream Out { get; }

        public abstract void AddObject(Asn1Encodable obj);

        public abstract Stream GetRawOutputStream();

        public abstract void Close();
    }
}