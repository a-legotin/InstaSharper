using System;
using System.Linq;

namespace InstaSharper.Utils
{
    internal static class InstaUtils
    {
        public static string GenerateJazoest(Guid guid) => $"2{guid.ToString().ToCharArray().Sum(ch => (int) ch)}";
    }
}