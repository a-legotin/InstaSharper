using InstagramAPI.API;
using InstagramAPI.API.Builder;
using InstagramAPI.Classes;

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

        public static IInstaApi GetDefaultInstaApiInstance(UserCredentials user)
        {
            var apiInstance = new InstaApiBuilder()
                .SetUser(user)
                .UseLogger(new TestLogger())
                .Build();
            return apiInstance;
        }
    }
}