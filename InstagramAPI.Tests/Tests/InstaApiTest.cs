using System;
using InstagramApi.Classes;
using InstagramApi.Tests.Utils;
using Xunit;

namespace InstagramApi.Tests.Tests
{
    public class InstaApiTest
    {
        [Fact]
        public async void GetUserFeedTest()
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
            bool login = apiInstance.Login();
            var feed = await apiInstance.GetUserFeedAsync(1);
            //assert
            Assert.NotNull(feed);
        }

        [Fact]
        public async void GetUserTest()
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
            await apiInstance.LoginAsync();
            var user = await apiInstance.GetUserAsync(username);
            //assert
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }
    }
}