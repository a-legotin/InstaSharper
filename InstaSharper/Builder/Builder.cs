﻿using System;
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
using InstaSharper.API;
using InstaSharper.API.Services;
using InstaSharper.API.UriProviders;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters.User;
using InstaSharper.Logging;
using InstaSharper.Models.Device;
using InstaSharper.Models.User;
using InstaSharper.Serialization;
using InstaSharper.Utils;
using InstaSharper.Utils.Encryption;

namespace InstaSharper.Builder
{
    public class Builder
    {
        private IDevice _device;
        private IInstaHttpClient _httpClient;
        private IJsonSerializer _jsonSerializer;
        private ILogger _logger;
        private LogLevel _logLevel;
        private IPasswordEncryptor _passwordEncryptor;
        private IStreamSerializer _streamSerializer;
        private IUriProvider _uriProvider;
        private IUserCredentials _userCredentials;
        private byte[] _userSessionStream;
        private IUserStateService _userStateService;

        public static Builder Create() => new Builder();

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

        public Builder WithUserCredentials(string username, string password)
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


        public IInstaApi Build()
        {
            if (string.IsNullOrEmpty(_userCredentials?.Username) || string.IsNullOrEmpty(_userCredentials?.Password))
                throw new ArgumentException("Please supply user credentials");

            _device ??= PredefinedDevices.Xiaomi4Prime;
            _jsonSerializer ??= new JsonSerializer();
            _streamSerializer ??= new StreamSerializer();
            _logger ??= new DebugLogger(_logLevel, _jsonSerializer);
            _uriProvider ??= new UriProvider(new DeviceUriProvider(),
                new UserUriProvider());

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

            _httpClient ??= new InstaHttpClient(httpClient, httpHandler, _logger, _jsonSerializer, _device);

            _userStateService = new UserStateService(_streamSerializer, (IHttpClientState) _httpClient, _device);
            if (_userSessionStream != null)
            {
                _userStateService.LoadStateDataFromByteArray(_userSessionStream);
            }

            var deviceService = new DeviceService(_uriProvider.Device, _httpClient, _device);

            var userConverterShort = new UserShortConverter();
            var friendshipStatusConverter = new InstaFriendshipShortStatusConverter();
            var userConverter = new UserConverter(userConverterShort, friendshipStatusConverter);
            var userConverters = new UserConverters(userConverterShort, userConverter);
            var userUriProvider = new UserUriProvider();
            var launcherKeysProvider = new LauncherKeysProvider(deviceService);

            _passwordEncryptor ??= new PasswordEncryptor();

            var userService = new UserService(_userCredentials,
                userUriProvider,
                _httpClient,
                launcherKeysProvider,
                userConverters,
                _userStateService,
                (IApiStateProvider) _userStateService,
                _passwordEncryptor);

            return new InstaApi(deviceService, userService);
        }

        public void WithUserSession(byte[] session)
        {
            _userSessionStream = session;
        }
    }
}