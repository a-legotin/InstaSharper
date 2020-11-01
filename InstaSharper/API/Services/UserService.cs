using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Request.User;
using InstaSharper.Models.Response.User;
using LanguageExt;

namespace InstaSharper.API.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserConverters _converters;
        private readonly IUserCredentials _credentials;
        private readonly IDevice _device;
        private readonly IInstaHttpClient _httpClient;
        private readonly ILauncherKeysProvider _launcherKeysProvider;
        private readonly IUserUriProvider _uriProvider;

        public UserService(IUserCredentials credentials,
            IDevice device,
            IUserUriProvider uriProvider,
            IInstaHttpClient httpClient,
            ILauncherKeysProvider launcherKeysProvider,
            IUserConverters converters)
        {
            _credentials = credentials;
            _device = device;
            _uriProvider = uriProvider;
            _httpClient = httpClient;
            _launcherKeysProvider = launcherKeysProvider;
            _converters = converters;
        }

        public async Task<Either<ResponseStatusBase, InstaUserShort>> LoginAsync()
        {
            return (await _httpClient.PostAsync<InstaLoginResponse, LoginRequest>(_uriProvider.Login,
                    await LoginRequest.Build(_device, _credentials, _launcherKeysProvider)))
                .Map(r => _converters.Self.Convert(r.User));
        }
    }
}