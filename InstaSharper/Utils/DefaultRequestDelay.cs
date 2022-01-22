using System;
using InstaSharper.Abstractions.Utils;

namespace InstaSharper.Utils;

internal class DefaultRequestDelay : IRequestDelay
{
    public DefaultRequestDelay(TimeSpan delay)
    {
        Delay = delay;
    }

    public TimeSpan Delay { get; }
}