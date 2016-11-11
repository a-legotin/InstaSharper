using System;
using InstagramAPI.Classes;
using InstagramAPI.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstagramAPI.Tests.Tests
{
    public class InstaApiTest
    {
        public InstaApiTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        private readonly ITestOutputHelper output;
        private readonly string username = "alex_codegarage";
        private readonly string password = Environment.GetEnvironmentVariable("instaapiuserpassword");

        [Fact]
        public async void GetUserFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            if (!await TestHelpers.Login(apiInstance, output)) return;
            var getFeedResult = await apiInstance.GetUserFeedAsync(5);
            var feed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
        }

        [Fact]
        public async void GetUserTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            if (!await TestHelpers.Login(apiInstance, output)) return;
            var getUserResult = await apiInstance.GetUserAsync(username);
            var user = getUserResult.Value;
            //assert
            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }
    }
}