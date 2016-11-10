using System;
using InstagramAPI.Classes;
using InstagramAPI.Tests.Utils;
using Xunit;

namespace InstagramAPI.Tests.Tests
{
    public class LoginTest
    {
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
            //act
            var success = await apiInstance.LoginAsync();

            //assert
            Assert.False(success);
            Assert.False(apiInstance.IsUserAuthenticated);
        }

        [Fact]
        public void UserLoginSuccessTest()
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
            //act
            var success = apiInstance.Login();

            //assert
            Assert.True(success);
            Assert.True(apiInstance.IsUserAuthenticated);
        }
    }
}