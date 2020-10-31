using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Models.Response;

namespace InstaSharper.Models.Status
{
    internal class ResponseStatus : ResponseStatusBase
    {
        private ResponseStatus(MessageErrorsResponse errors) => Message = string.Join(". ", errors.Errors);


        public override string Message { get; }

        internal static ResponseStatusBase FromResponse(BadStatusErrorsResponse response) =>
            new ResponseStatus(response.Message);
    }
}