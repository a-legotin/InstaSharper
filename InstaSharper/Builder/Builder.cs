using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Abstractions.Serialization;
using InstaSharper.Abstractions.Utils;
using InstaSharper.API;
using InstaSharper.API.Services;
using InstaSharper.API.Services.User;
using InstaSharper.API.Services.User.Followers;
using InstaSharper.API.UriProviders;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters.Media;
using InstaSharper.Infrastructure.Converters.User;
using InstaSharper.Logging;
using InstaSharper.Models.Device;
using InstaSharper.Models.User;
using InstaSharper.Serialization;
using InstaSharper.Utils;
using InstaSharper.Utils.Encryption;

namespace InstaSharper.Builder;

public class Builder
{
    private IAuthorizationHeaderProvider _authorizationHeaderProvider;
    private IDevice _device;
    private IInstaHttpClient _httpClient;
    private bool _isCompleted;
    private IJsonSerializer _jsonSerializer;
    private ILogger _logger;
    private LogLevel _logLevel;
    private IPasswordEncryptor _passwordEncryptor;
    private IRequestDelay _requestDelay;
    private IStreamSerializer _streamSerializer;
    private IUriProvider _uriProvider;
    private IUserCredentials _userCredentials;
    private byte[] _userSessionBytes;
    private IUserStateService _userStateService;

    public static Builder Create()
    {
        return new Builder();
    }

    public Builder WithDevice(IDevice device)
    {
        _device = device;
        return this;
    }

    public Builder WithUserCredentials(IUserCredentials credentials)
    {
        _userCredentials = credentials;
        return this;
    }

    public Builder WithUserCredentials(string username,
                                       string password)
    {
        _userCredentials = new UserCredentials(username, password);
        return this;
    }

    public Builder WithLogger(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public Builder WithJsonSerializer(IJsonSerializer serializer)
    {
        _jsonSerializer = serializer;
        return this;
    }

    public Builder WithStreamSerializer(IStreamSerializer serializer)
    {
        _streamSerializer = serializer;
        return this;
    }

    public Builder SetLoggingNone()
    {
        _logLevel = LogLevel.None;
        return this;
    }

    public Builder SetLoggingAll()
    {
        _logLevel = LogLevel.All;
        return this;
    }

    public Builder WithUriProvider(IUriProvider provider)
    {
        _uriProvider = provider;
        return this;
    }


    public Builder WithDelay(TimeSpan delay)
    {
        return WithDelay(new DefaultRequestDelay(delay));
    }

    public Builder WithDelay(IRequestDelay delay)
    {
        _requestDelay = delay;
        return this;
    }


    public IInstaApi Build()
    {
        if (_isCompleted)
            throw new InvalidOperationException(
                $"{nameof(Builder)} can't be used twice, please create new {nameof(Builder)} instance");

        _isCompleted = true;
        var credentialsSupplied = !string.IsNullOrEmpty(_userCredentials?.Username)
                                  && !string.IsNullOrEmpty(_userCredentials?.Password);
        var userStateSupplied = _userSessionBytes != null;
        if (!credentialsSupplied && !userStateSupplied)
            throw new ArgumentException("Please supply user credentials or user state");

        _device ??= PredefinedDevices.Xiaomi4Prime;
        _jsonSerializer ??= new JsonSerializer();
        _streamSerializer ??= new StreamSerializer();
        _logger ??= new DebugLogger(_logLevel, _jsonSerializer);
        _uriProvider ??= new UriProvider(new DeviceUriProvider(),
            new UserUriProvider(),
            new UserFollowersUriProvider(),
            new FeedUriProvider());

        var httpHandler = new HttpClientHandler
        {
            UseProxy = false,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        var httpClient = new HttpClient(httpHandler)
        {
            BaseAddress = new Uri(Constants.BASE_URI)
        };

        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
        httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

        _authorizationHeaderProvider = new AuthorizationHeaderProvider();

        _httpClient ??= new InstaHttpClient(httpClient, httpHandler, _logger, _jsonSerializer, _device,
            _authorizationHeaderProvider, _requestDelay);

        _userStateService = new UserStateService(_streamSerializer, (IHttpClientState)_httpClient, _device,
            _authorizationHeaderProvider);

        if (_userSessionBytes != null) _userStateService.LoadStateDataFromByteArray(_userSessionBytes);

        var deviceService = new DeviceService(_uriProvider.Device, _httpClient, _device);

        var userPreviewConverter = new UserShortConverter();
        var friendshipStatusConverter = new InstaFriendshipShortStatusConverter();
        var userConverter = new UserConverter(userPreviewConverter, friendshipStatusConverter);
        var userConverters = new UserConverters(userPreviewConverter, userConverter);
        var captionConverter = new CaptionConverter(userPreviewConverter);
        var locationConverter = new LocationConverter();
        var commentConverter = new CommentConverter(userPreviewConverter);
        var userTagConverter = new UserTagConverter(userPreviewConverter);
        var carouselConverter = new CarouselConverter(userTagConverter);
        var mediaListConverter = new MediaListConverter(new MediaConverter(userConverter, captionConverter,
            locationConverter, userPreviewConverter, commentConverter, carouselConverter, userTagConverter));
        var mediaConverter = new MediaConverters(mediaListConverter);

        var userUriProvider = new UserUriProvider();
        var launcherKeysProvider = new LauncherKeysProvider(deviceService);

        _passwordEncryptor ??= new PasswordEncryptor();

        var userService = new UserService(_userCredentials,
            userUriProvider,
            _httpClient,
            launcherKeysProvider,
            deviceService,
            userConverters,
            _userStateService,
            (IApiStateProvider)_userStateService,
            _passwordEncryptor,
            _authorizationHeaderProvider);

        var followersService = new UserFollowersService(_httpClient, _uriProvider.Followers, userConverters,
            (IApiStateProvider)_userStateService, _logger);
        var feedService = new FeedService(_uriProvider.Feed, _httpClient, mediaConverter);
        var mediaService = new MediaService();
        return new InstaApi(deviceService, userService, followersService, feedService, mediaService);
    }

    public Builder WithUserSession(byte[] session)
    {
        _userSessionBytes = session;
        return this;
    }
}