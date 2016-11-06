using System;
using InstagramApi.Classes;
using Xunit;

namespace InstagramApi.Tests
{
    public class InstaApiTest
    {
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
            bool success = apiInstance.Login();

            //assert
            Assert.True(success);
            Assert.True(apiInstance.IsUserAuthenticated);
        }
    }
}