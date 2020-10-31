using System.Net.Http;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Serialization;

namespace InstaSharper.Http
{
    internal class InstaHttpClientFactory : IInstaHttpClientFactory
    {
        private readonly ILogger _logger;
        private readonly ISerializer _serializer;

        public InstaHttpClientFactory(ILogger logger, ISerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
        }

        public IInstaHttpClient Construct(HttpClient client) => new InstaHttpClient(client, _logger, _serializer);
    }
}