using System;
using System.Linq;
using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Request.User;
using InstaSharper.Models.Response.User;
using InstaSharper.Utils.Encryption;
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
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IUserUriProvider _uriProvider;
        private readonly IUserStateService _userStateService;

        public UserService(IUserCredentials credentials,
            IUserUriProvider uriProvider,
            IInstaHttpClient httpClient,
            ILauncherKeysProvider launcherKeysProvider,
            IUserConverters converters,
            IUserStateService userStateService,
            IApiStateProvider apiStateProvider,
            IPasswordEncryptor passwordEncryptor)
        {
            _credentials = credentials;
            _uriProvider = uriProvider;
            _httpClient = httpClient;
            _launcherKeysProvider = launcherKeysProvider;
            _converters = converters;
            _userStateService = userStateService;
            _apiStateProvider = apiStateProvider;
            _passwordEncryptor = passwordEncryptor;
        }

        public async Task<Either<ResponseStatusBase, InstaUserShort>> LoginAsync() 
            => (await _httpClient.PostAsync<InstaLoginResponse, LoginRequest>(_uriProvider.Login,
                await LoginRequest.Build(_apiStateProvider.Device, _credentials, _launcherKeysProvider,
                    _passwordEncryptor)))
            .Map(r =>
            {
                var user = _converters.Self.Convert(r.User);
                _apiStateProvider.SetUser(user);
                return user;
            });

        public async Task<Either<ResponseStatusBase, bool>> LogoutAsync() 
            => (await _httpClient.PostAsync<InstaLogoutResponse, LogoutRequest>(_uriProvider.Logout,
                LogoutRequest.Build(_apiStateProvider.Device, _apiStateProvider.CsrfToken)))
            .Map(r =>
            {
                _apiStateProvider.PerformLogout();
                return r.IsOk();
            });

        public async Task<Either<ResponseStatusBase, InstaUser>> GetUserAsync(string username) 
            => (await _httpClient.GetAsync<InstaSearchUserResponse>(
                _uriProvider.SearchUsers(username),
                SearchUserGetRequest.Build(_apiStateProvider.RankToken)))
            .Map(r
                => _converters.User.Convert(r.Users.FirstOrDefault(user
                    => string.Equals(user.UserName, username, StringComparison.InvariantCultureIgnoreCase))));

        public async Task<Either<ResponseStatusBase, InstaUser[]>> SearchUsersAsync(string query) 
            => (await _httpClient.GetAsync<InstaSearchUserResponse>(
                _uriProvider.SearchUsers(query),
                SearchUserGetRequest.Build(_apiStateProvider.RankToken)))
            .Map(r
                => r.Users.Select(_converters.User.Convert).ToArray());

        public byte[] GetUserSessionAsByteArray() => _userStateService.GetStateDataAsByteArray();
    }
}