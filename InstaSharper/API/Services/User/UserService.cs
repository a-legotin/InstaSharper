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
using InstaSharper.Utils;
using InstaSharper.Utils.Encryption;
using LanguageExt;

namespace InstaSharper.API.Services;

internal class UserService : IUserService
{
    private readonly IApiStateProvider _apiStateProvider;
    private readonly IAuthorizationHeaderProvider _authorizationHeaderProvider;
    private readonly IUserConverters _converters;
    private readonly IUserCredentials _credentials;
    private readonly IInstaHttpClient _httpClient;
    private readonly ILauncherKeysProvider _launcherKeysProvider;
    private readonly IDeviceService _deviceService;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IUserUriProvider _uriProvider;
    private readonly IUserStateService _userStateService;

    public UserService(IUserCredentials credentials,
        IUserUriProvider uriProvider,
        IInstaHttpClient httpClient,
        ILauncherKeysProvider launcherKeysProvider,
        IDeviceService deviceService,
        IUserConverters converters,
        IUserStateService userStateService,
        IApiStateProvider apiStateProvider,
        IPasswordEncryptor passwordEncryptor,
        IAuthorizationHeaderProvider authorizationHeaderProvider)
    {
        _credentials = credentials;
        _uriProvider = uriProvider;
        _httpClient = httpClient;
        _launcherKeysProvider = launcherKeysProvider;
        _deviceService = deviceService;
        _converters = converters;
        _userStateService = userStateService;
        _apiStateProvider = apiStateProvider;
        _passwordEncryptor = passwordEncryptor;
        _authorizationHeaderProvider = authorizationHeaderProvider;
    }

    public bool IsAuthenticated => _userStateService.IsAuthenticated;

    public async Task<Either<ResponseStatusBase, InstaUserShort>> LoginAsync()
    {
        return await (await _httpClient.PostAsync<InstaLoginResponse, LoginRequest>(_uriProvider.Login,
                await LoginRequest.Build(_apiStateProvider.Device, _credentials, _launcherKeysProvider,
                    _passwordEncryptor)))
            .MapAsync(async r =>
            {
                var user = _converters.Self.Convert(r.User);
                _apiStateProvider.SetUser(user);
                await ProcessAuthenticationHeaders(r, user);
                return user;
            });
    }

    private async Task ProcessAuthenticationHeaders(InstaLoginResponse r, InstaUserShort user)
    {
        if (r.ResponseHeaders?.TryGetValues(Constants.Headers.IG_SET_USE_AUTH_HEADER, out var useAuthHeader) ==
            true
            && useAuthHeader.FirstOrDefault()?.Equals("true", StringComparison.InvariantCultureIgnoreCase) ==
            true)
            if (r.ResponseHeaders?.TryGetValues(Constants.Headers.IG_SET_AUTHORIZATION, out var authHeader) ==
                true)
                _authorizationHeaderProvider.AuthorizationHeader = authHeader.FirstOrDefault();


        if (r.ResponseHeaders?.TryGetValues(Constants.Headers.SET_WWW_CLAIM, out var wwwHeader) == true)
            _authorizationHeaderProvider.WwwClaimHeader = wwwHeader.FirstOrDefault();
        if (r.ResponseHeaders?.TryGetValues(Constants.Headers.HEADER_X_FB_TRIP_ID, out var fbTripHeader) == true)
            _authorizationHeaderProvider.FbTripHeader = fbTripHeader.FirstOrDefault();
        
        _authorizationHeaderProvider.CurrentUserIdHeader = user.Pk;

        var launcherSync = await _deviceService.LauncherSyncAsync();
        launcherSync.Match(ok =>
        {
            _authorizationHeaderProvider.ShbId = ok.ShbId;
            _authorizationHeaderProvider.ShbTs = ok.ShbTs;
            _authorizationHeaderProvider.Rur = ok.Rur;

        }, fail => { });
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

    public async Task<Either<ResponseStatusBase, InstaUser>> GetUserAsync(string username)
    {
        return (await _httpClient.GetAsync<InstaSearchUserResponse>(
                _uriProvider.SearchUsers(username)))
            .Map(r
                => _converters.User.Convert(r.Users.FirstOrDefault(user
                    => string.Equals(user.UserName, username, StringComparison.InvariantCultureIgnoreCase))));
    }

    public async Task<Either<ResponseStatusBase, InstaUser[]>> SearchUsersAsync(string query)
    {
        return (await _httpClient.GetAsync<InstaSearchUserResponse>(
                _uriProvider.SearchUsers(query)))
            .Map(r
                => r.Users.Select(_converters.User.Convert).ToArray());
    }

    public byte[] GetUserSessionAsByteArray()
    {
        return _userStateService.GetStateDataAsByteArray();
    }

    public void LoadStateDataFromBytes(byte[] stateBytes)
    {
        _userStateService.LoadStateDataFromByteArray(stateBytes);
    }
}