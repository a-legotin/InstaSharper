using System;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    public class AuthTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly IInstaApi _apiInstance;

        public AuthTest(ITestOutputHelper output)
        {
            _output = output;
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            _apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });
        }



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

            Assert.False(_apiInstance.IsUserAuthenticated);
            var loginResult = await _apiInstance.LoginAsync();
            Assert.True(loginResult.Succeeded);
            Assert.True(_apiInstance.IsUserAuthenticated);
        }

        public void Dispose()
        {
            _apiInstance.LogoutAsync();
        }
    }
}