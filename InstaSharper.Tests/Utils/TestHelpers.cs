using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Helpers;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Utils
{
    public class TestHelpers
    {
        public static IInstaApi GetDefaultInstaApiInstance(UserSessionData user)
        {
            var apiInstance = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .SetRequestDelay(TimeSpan.FromSeconds(2))
                .SetSignatureKey("b4946d296abf005163e72346a6d33dd083cadde638e6ad9c5eb92e381b35784a")
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