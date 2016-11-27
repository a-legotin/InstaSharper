using System.Threading.Tasks;
using InstagramAPI.API;
using InstagramAPI.API.Builder;
using InstagramAPI.Classes;
using Xunit.Abstractions;

namespace InstagramAPI.Tests.Utils
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

        public static async Task<bool> Login(IInstaApi apiInstance, ITestOutputHelper output)
        {
            var loginResult = await apiInstance.LoginAsync();
            if (!loginResult.Succeeded)
            {
                output.WriteLine($"Can't login: {loginResult.Message}");
                return false;
            }
            return true;
        }
    }
}