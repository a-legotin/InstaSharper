using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class FollowersTest
    {
        private readonly ITestOutputHelper _output;

        public FollowersTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("discovery")]
        public async void GetUserFollowersTest(string username)
        {
            var currentUsername = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = currentUsername,
                Password = password
            });
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetUserFollowersAsync(username, 10);
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }

        [RunnableInDebugOnlyFact]
        public async void GetCurrentUserFollwersTest()
        {
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });
            if (!TestHelpers.Login(apiInstance, _output)) return;
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetCurrentUserFollowersAsync();
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }
    }
}