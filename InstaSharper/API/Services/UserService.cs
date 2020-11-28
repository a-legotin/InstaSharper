using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Request.User;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.User;
using LanguageExt;

namespace InstaSharper.API.Services
{
    internal class UserService : IUserService
    {
        private readonly IApiStateProvider _apiStateProvider;
        private readonly IUserConverters _converters;
        private readonly IUserCredentials _credentials;
        private readonly IInstaHttpClient _httpClient;
        private readonly ILauncherKeysProvider _launcherKeysProvider;
        private readonly IUserUriProvider _uriProvider;

        public UserService(IUserCredentials credentials,
            IUserUriProvider uriProvider,
            IInstaHttpClient httpClient,
            ILauncherKeysProvider launcherKeysProvider,
            IUserConverters converters,
            IUserStateService userStateService)
        {
            _credentials = credentials;
            _uriProvider = uriProvider;
            _httpClient = httpClient;
            _launcherKeysProvider = launcherKeysProvider;
            _converters = converters;
            _apiStateProvider = (IApiStateProvider) userStateService;
        }

        public async Task<Either<ResponseStatusBase, InstaUserShort>> LoginAsync()
        {
            return (await _httpClient.PostAsync<InstaLoginResponse, LoginRequest>(_uriProvider.Login,
                    await LoginRequest.Build(_apiStateProvider.Device, _credentials, _launcherKeysProvider)))
                .Map(r =>
                {
                    var user = _converters.Self.Convert(r.User);
                    _apiStateProvider.SetUser(user);
                    return user;
                });
        }
        
        public async Task<Either<ResponseStatusBase, bool>> LogoutAsync()
        {
            return (await _httpClient.PostAsync<InstaLogoutResponse, LogoutRequest>(_uriProvider.Logout,
                    LogoutRequest.Build(_apiStateProvider.Device, _apiStateProvider.CsrfToken)))
                .Map(r =>
                {
                    _apiStateProvider.PerformLogout();
                    return r.IsOk();
                });
        }
    }
}