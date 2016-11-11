using System;
using InstagramAPI.Classes;
using InstagramAPI.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstagramAPI.Tests.Tests
{
    public class LoginTest
    {
        public LoginTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        private readonly ITestOutputHelper output;

        [Fact]
        public async void UserLoginFailTest()
        {
            //arrange
            var username = "alex_codegarage";
            var password = "boombaby!";
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            output.WriteLine("Got API instance");
            //act
            var loginResult = await apiInstance.LoginAsync();
            //assert
            Assert.False(loginResult.Succeeded);
            Assert.False(apiInstance.IsUserAuthenticated);
        }

        [Fact]
        public async void UserLoginSuccessTest()
        {
            //arrange
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            output.WriteLine("Got API instance");
            //act
            var loginResult = await apiInstance.LoginAsync();
            //assert
            Assert.True(loginResult.Succeeded);
            Assert.True(apiInstance.IsUserAuthenticated);
        }
    }
}