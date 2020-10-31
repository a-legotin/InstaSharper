using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Serialization;
using InstaSharper.Models.Response;
using InstaSharper.Models.Status;
using LanguageExt;

namespace InstaSharper.Http
{
    internal class InstaHttpClient : IInstaHttpClient
    {
        private readonly HttpClient _innerClient;
        private readonly ILogger _logger;
        private readonly ISerializer _serializer;

        public InstaHttpClient(HttpClient innerClient,
            ILogger logger,
            ISerializer serializer)
        {
            _innerClient = innerClient;
            _logger = logger;
            _serializer = serializer;
        }


        public async Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            _innerClient.DefaultRequestHeaders.ConnectionClose = false;
            try
            {
                var responseMessage = await _innerClient.SendAsync(requestMessage);
                var json = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode) return _serializer.Deserialize<T>(json);

                return ResponseStatus.FromResponse(_serializer.Deserialize<BadStatusErrorsResponse>(json));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception);
                return ExceptionalResponseStatus.FromException(exception);
            }
        }
    }
}