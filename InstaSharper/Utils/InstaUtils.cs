using System;
using System.Linq;

namespace InstaSharper.Utils;

internal static class InstaUtils
{
    public static string GenerateJazoest(Guid guid)
    {
        return $"2{guid.ToString().ToCharArray().Sum(ch => ch)}";
    }
}