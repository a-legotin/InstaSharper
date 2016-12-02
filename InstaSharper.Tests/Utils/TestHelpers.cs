using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
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

        public static bool Login(IInstaApi apiInstance, ITestOutputHelper output)
        {
            var loginResult = apiInstance.Login();
            if (!loginResult.Succeeded)
            {
                output.WriteLine($"Can't login: {loginResult.Message}");
                return false;
            }
            return true;
        }
    }
}