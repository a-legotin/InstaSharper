using System.Net;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Models.Response.System;

namespace InstaSharper.Models.Status;

internal sealed class ResponseStatus : ResponseStatusBase
{
    private ResponseStatus(string errors,
                           string responseStatus)
    {
        var status = ResponseStatusType.Unknown;
        if (responseStatus == "ok")
            status = ResponseStatusType.OK;
        else if (responseStatus == "fail")
            status = ResponseStatusType.UnExpectedResponse;
        Status = status;
        Message = errors ?? string.Empty;
    }


    public override string Message { get; }

    internal static ResponseStatusBase FromResponse(BadStatusErrorsResponse response)
    {
        return new ResponseStatus($"{response.Message}. {response.ErrorTitle} {response.ErrorBody}".TrimEnd(' '),
            response.Status);
    }

    public static ResponseStatusBase FromStatusCode(HttpStatusCode statusCode)
    {
        return new ResponseStatus($"Error code: {statusCode}", "Fail");
    }
}