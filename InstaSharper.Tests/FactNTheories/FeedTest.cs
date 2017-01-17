using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.FactNTheories
{
    [Collection("InstaSharper Tests")]
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
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var result = await apiInstance.GetTagFeedAsync(tag);
            var tagFeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
        }


        [Theory]
        [InlineData("rock")]
        public async void GetUserTagFeedTest(string username)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var result = await apiInstance.GetUserTagsAsync(username, 5);
            var tagFeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
        }

        [Fact]
        public async void GetFollowingRecentActivityFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            //no need to perform test if account marked as unsafe
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var getFeedResult = await apiInstance.GetFollowingRecentActivityAsync(5);
            var folloowingRecentFeed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(folloowingRecentFeed);
            Assert.True(!folloowingRecentFeed.IsOwnActivity);
        }

        [Fact]
        public async void GetRecentActivityFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var getFeedResult = await apiInstance.GetRecentActivityAsync(5);
            var ownRecentFeed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(ownRecentFeed);
            Assert.True(ownRecentFeed.IsOwnActivity);
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
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var getFeedResult = await apiInstance.GetUserTimelineFeedAsync(5);
            var feed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
        }
    }
}