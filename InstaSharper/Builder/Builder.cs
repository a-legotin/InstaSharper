using System;
using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Abstractions.Serialization;
using InstaSharper.API;
using InstaSharper.Device;
using InstaSharper.Logging;
using InstaSharper.Models.User;
using InstaSharper.Serialization;

namespace InstaSharper.Builder
{
    public class Builder
    {
        private IDevice _device;
        private ILogger _logger;
        private LogLevel _logLevel;
        private ISerializer _serializer;
        private IUserCredentials _userCredentials;

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

        public Builder WithSerializer(ISerializer serializer)
        {
            _serializer = serializer;
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

        public IInstaApi Build()
        {
            if (string.IsNullOrEmpty(_userCredentials?.Username) || string.IsNullOrEmpty(_userCredentials?.Password))
                throw new ArgumentException("Please supply user credentials");

            _device ??= PredefinedDevices.Xiaomi4Prime;
            _serializer ??= new JsonSerializer();
            _logger ??= new DebugLogger(_logLevel, _serializer);

            return new InstaApi(_device);
        }
    }
}