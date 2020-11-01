using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Models.Request;
using LanguageExt;

namespace InstaSharper.API.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserCredentials _credentials;
        private readonly IDevice _device;
        private readonly IUserUriProvider _uriProvider;
        private readonly IInstaHttpClient _httpClient;
        private readonly ILauncherKeysProvider _launcherKeysProvider;

        public UserService(IUserCredentials credentials,
            IDevice device,
            IUserUriProvider uriProvider,
            IInstaHttpClient httpClient,
            ILauncherKeysProvider launcherKeysProvider)
        {
            _credentials = credentials;
            _device = device;
            _uriProvider = uriProvider;
            _httpClient = httpClient;
            _launcherKeysProvider = launcherKeysProvider;
        }

        public async Task<Either<ResponseStatusBase, LoginResponse>> LoginAsync()
        {
            var response = await _httpClient.PostAsync<LoginResponse, LoginRequest>(_uriProvider.Login,
                await LoginRequest.Build(_device, _credentials, _launcherKeysProvider));
            return response;
        }
    }
}