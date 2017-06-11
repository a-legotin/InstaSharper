using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class AuthTest
    {
        public AuthTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        public async void UserLoginFailTest()
        {
            var username = "alex_codegarage";
            var password = "boombaby!";

            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = username,
                    Password = password
                });
            _output.WriteLine("Got API instance");

            var loginResult = await apiInstance.LoginAsync();
            Assert.False(loginResult.Succeeded);
            Assert.False(apiInstance.IsUserAuthenticated);
        }

        [Fact]
        public async void UserLoginSuccessTest()
        {
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");

            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });

            Assert.False(apiInstance.IsUserAuthenticated);
            var loginResult = await apiInstance.LoginAsync();
            Assert.True(loginResult.Succeeded);
            Assert.True(apiInstance.IsUserAuthenticated);
        }
    }
}