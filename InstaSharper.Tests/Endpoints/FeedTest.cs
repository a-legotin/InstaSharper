using System.Linq;
using InstaSharper.Tests.Classes;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class FeedTest : IClassFixture<AuthenticatedTestFixture>
    {
        readonly AuthenticatedTestFixture _authInfo;

        public FeedTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("christmas")]
        public async void GetTagFeedTest(string tag)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetTagFeedAsync(tag, 10);
            var tagFeed = result.Value;
            var anyMediaDuplicate = tagFeed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyStoryDuplicate = tagFeed.Stories.GroupBy(x => x.Id).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
            Assert.False(anyMediaDuplicate);
            Assert.False(anyStoryDuplicate);
        }


        [RunnableInDebugOnlyTheory]
        [InlineData("rock")]
        public async void GetUserTagFeedTest(string username)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetUserTagsAsync(username, 5);
            var tagFeed = result.Value;
            var anyMediaDuplicate = tagFeed.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
            Assert.False(anyMediaDuplicate);
        }

        [RunnableInDebugOnlyFact]
        public async void GetFollowingRecentActivityFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult = await _authInfo.ApiInstance.GetFollowingRecentActivityAsync(5);
            var folloowingRecentFeed = getFeedResult.Value;

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(folloowingRecentFeed);
            Assert.True(!folloowingRecentFeed.IsOwnActivity);
        }

        [RunnableInDebugOnlyFact]
        public async void GetRecentActivityFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult = await _authInfo.ApiInstance.GetRecentActivityAsync(5);
            var ownRecentFeed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(ownRecentFeed);
            Assert.True(ownRecentFeed.IsOwnActivity);
        }

        [RunnableInDebugOnlyFact]
        public async void GetUserFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult = await _authInfo.ApiInstance.GetUserTimelineFeedAsync(5);
            var feed = getFeedResult.Value;
            var anyDuplicate = feed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyStoryDuplicate = feed.Stories.GroupBy(x => x.Id).Any(g => g.Count() > 1);

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
            Assert.False(anyDuplicate);
            Assert.False(anyStoryDuplicate);
        }
    }
}