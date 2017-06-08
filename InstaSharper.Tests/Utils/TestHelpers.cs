using System.Net.Http;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Helpers;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Utils
{
    public class TestHelpers
    {
        public static IInstaApi GetDefaultInstaApiInstance(string username)
        {
            var apiInstance = new InstaApiBuilder()
                .SetUserName(username)
                .UseLogger(new TestLogger())
                .Build();
            return apiInstance;
        }

        public static IInstaApi GetDefaultInstaApiInstance(UserSessionData user)
        {
            var apiInstance = new InstaApiBuilder()
                .SetUser(user)
                .UseLogger(new TestLogger())
                .Build();
            return apiInstance;
        }

        public static IInstaApi GetProxifiedInstaApiInstance(UserSessionData user, InstaProxy proxy)
        {
            var handler = new HttpClientHandler { Proxy = proxy };
            var apiInstance = new InstaApiBuilder()
                .UseHttpClientHandler(handler)
                .SetUser(user)
                .Build();
            return apiInstance;
        }

        public static bool Login(IInstaApi apiInstance, ITestOutputHelper output)
        {
            var loginResult = apiInstance.Login();
            if (!loginResult.Succeeded)
            {
                output.WriteLine($"Can't login: {loginResult.Info.Message}");
                return false;
            }
            return true;
        }
    }
}