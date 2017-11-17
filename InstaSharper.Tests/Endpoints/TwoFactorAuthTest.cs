using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class TwoFactorAuthTest
    {
        public TwoFactorAuthTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        public async void User2FaTest()
        {
            var username = "thisasif";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");

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

            Assert.True(loginResult.Value == InstaLoginResult.TwoFactorRequired);
        }

        [Fact]
        public async void User2FaLoginSuccessTest()
        {
            var username = "thisasif";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");

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

            Assert.True(loginResult.Value == InstaLoginResult.TwoFactorRequired);

            System.Threading.Thread.Sleep(20000);

            var content = System.IO.File.ReadAllText(@"C:\tmp\codice.txt");

            var login2FactorAuth = await apiInstance.TwoFactorLoginAsync(content);

            Assert.True(login2FactorAuth.Succeeded);
            Assert.True(apiInstance.IsUserAuthenticated);
        }

        [Fact]
        public async void User2FaLoginFailTest()
        {
            var username = "thisasif";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");

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

            Assert.True(loginResult.Value == InstaLoginResult.TwoFactorRequired);

            var login2FactorAuth = await apiInstance.TwoFactorLoginAsync("666666");

            Assert.False(login2FactorAuth.Succeeded);
        }
    }
}
