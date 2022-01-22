using System;
using InstaSharper.Abstractions.Models.Status;

namespace InstaSharper.Models.Status;

internal class ExceptionalResponseStatus : ResponseStatusBase
{
    private ExceptionalResponseStatus(Exception exception)
    {
        Exception = exception;
        Message = exception.Message;
    }

    public Exception Exception { get; }

    public override string Message { get; }

    internal static ResponseStatusBase FromException(Exception exception)
    {
        return new ExceptionalResponseStatus(exception);
    }
}