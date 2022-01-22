using System;

namespace InstaSharper.Abstractions.Utils;

public interface IRequestDelay
{
    TimeSpan Delay { get; }
}