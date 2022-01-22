using System;

namespace InstaSharper.Utils.Encryption.Engine;

public sealed class Times
{
    private static readonly long NanosecondsPerTick = 100L;

    public static long NanoTime()
    {
        return DateTime.UtcNow.Ticks * NanosecondsPerTick;
    }
}