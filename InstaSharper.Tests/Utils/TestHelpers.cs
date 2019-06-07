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
                .SetRequestDelay(RequestDelay.FromSeconds(5, 5))
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
            if (loginResult.Succeeded) return true;
            output.WriteLine($"Can't login: {loginResult.Info.Message}");
            return false;
        }
    }
}