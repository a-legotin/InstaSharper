using System;
using InstagramApi.Classes;
using InstagramApi.Tests.Utils;
using Xunit;

namespace InstagramApi.Tests.Tests
{
    public class InstaApiTest
    {
        private readonly LoginTest _loginTest = new LoginTest();

        [Fact]
        public void GetUserFeedTest()
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
            apiInstance.Login();
            var feed = apiInstance.GetUserFeed(1);
            //assert
            Assert.NotNull(feed);
        }

        [Fact]
        public void GetUserTest()
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
            apiInstance.Login();
            var user = apiInstance.GetUser(username);
            //assert
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }
    }
}