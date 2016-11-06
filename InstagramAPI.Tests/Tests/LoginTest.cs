using System;
using InstagramApi.Classes;
using InstagramApi.Tests.Utils;
using Xunit;

namespace InstagramApi.Tests.Tests
{
    public class LoginTest
    {
        [Fact]
        public void UserLoginFailTest()
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
            var success = apiInstance.Login();

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