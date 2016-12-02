using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    public class FeedTest
    {
        public FeedTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;
        private readonly string _username = "alex_codegarage";
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");

        [Theory]
        [InlineData("christmas")]
        [InlineData("rock")]
        public async void GetTagFeedTest(string tag)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            if (!await TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetTagFeedAsync(tag);
            var tagFeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
        }

        [Fact]
        public async void GetUserFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            if (!await TestHelpers.Login(apiInstance, _output)) return;
            var getFeedResult = await apiInstance.GetUserFeedAsync(5);
            var feed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
        }
    }
}