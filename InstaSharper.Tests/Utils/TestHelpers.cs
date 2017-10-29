﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Utils
{
    public class TestHelpers
    {
        public static IInstaApi GetDefaultInstaApiInstance(string username)
        {
            var device = AndroidDeviceGenerator.GetByName(AndroidDevices.SAMSUNG_NOTE3);
            var requestMessage = ApiRequestMessage.FromDevice(device);
            var apiInstance = InstaApiBuilder.CreateBuilder()
                .SetUserName(username)
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetApiRequestMessage(requestMessage)
                .Build();
            return apiInstance;
        }

        public static IInstaApi GetDefaultInstaApiInstance(UserSessionData user)
        {
            var device = AndroidDeviceGenerator.GetByName(AndroidDevices.SAMSUNG_NOTE3);
            var requestMessage = ApiRequestMessage.FromDevice(device);
            var apiInstance = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .SetApiRequestMessage(requestMessage)
                .SetRequestDelay(TimeSpan.FromSeconds(2))
                .Build();
            return apiInstance;
        }

        public static IInstaApi GetProxifiedInstaApiInstance(UserSessionData user, InstaProxy proxy)
        {
            var handler = new HttpClientHandler {Proxy = proxy};
            var apiInstance = InstaApiBuilder.CreateBuilder()
                .UseHttpClientHandler(handler)
                .SetUser(user)
                .Build();
            return apiInstance;
        }

        public static async Task<bool> Login(IInstaApi apiInstance, ITestOutputHelper output)
        {
            var loginResult = await apiInstance.LoginAsync();
            if (!loginResult.Succeeded)
            {
                output.WriteLine($"Can't login: {loginResult.Info.Message}");
                return false;
            }
            return true;
        }
    }
}