using System;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    public class FollowersTest : IDisposable
    {
        public FollowersTest(ITestOutputHelper output)
        {
            _output = output;
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            _apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });
            TestHelpers.Login(_apiInstance, _output);
        }

        public void Dispose()
        {
            _apiInstance.LogoutAsync();
        }

        private readonly ITestOutputHelper _output;
        private readonly IInstaApi _apiInstance;

        [Theory]
        [InlineData("discovery")]
        public async void GetUserFollowersTest(string username)
        {
            var result = await _apiInstance.GetUserFollowersAsync(username, 10);
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }

        [Fact]
        public async void GetCurrentUserFollwersTest()
        {
            var result = await _apiInstance.GetCurrentUserFollowersAsync();
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }
    }
}