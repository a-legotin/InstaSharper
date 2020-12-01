using System.Net;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Models.Response.System;

namespace InstaSharper.Models.Status
{
    internal sealed class ResponseStatus : ResponseStatusBase
    {
        private ResponseStatus(string errors, string responseStatus)
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
            => new ResponseStatus(response.Message, response.Status);

        public static ResponseStatusBase FromStatusCode(HttpStatusCode statusCode)
            => new ResponseStatus($"Error code: {statusCode}", "Fail");
    }
}