﻿using System.Linq;
using InstaSharper.Classes;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class FeedTest : IClassFixture<AuthenticatedTestFixture>
    {
        public FeedTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Theory]
        [InlineData("christmas")]
        public async void GetTagFeedTest(string tag)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetTagFeedAsync(tag, PaginationParameters.MaxPagesToLoad(10));
            var tagFeed = result.Value;
            var anyMediaDuplicate = tagFeed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyStoryDuplicate = tagFeed.Stories.GroupBy(x => x.Id).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
            Assert.False(anyMediaDuplicate);
            Assert.False(anyStoryDuplicate);
        }


        [Theory]
        [InlineData("rock")]
        public async void GetUserTagFeedTest(string username)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetUserTagsAsync(username, PaginationParameters.MaxPagesToLoad(5));
            var tagFeed = result.Value;
            var anyMediaDuplicate = tagFeed.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
            Assert.False(anyMediaDuplicate);
        }

        [Theory]
        [InlineData(267685466)]
        [InlineData(466579064)]
        public async void GetUserReelFeedTest(long userPk)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetUserStoryFeedAsync(userPk);
            var feed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(feed);
        }

        [Fact]
        public async void ExploreTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            var exploreGeed = result.Value;
            var anyMediaDuplicate = exploreGeed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            //assert
            Assert.NotNull(exploreGeed);
            Assert.True(result.Succeeded);
            Assert.False(anyMediaDuplicate);
        }

        [Fact]
        public async void GetFollowingRecentActivityFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult =
                await _authInfo.ApiInstance.GetFollowingRecentActivityAsync(PaginationParameters.MaxPagesToLoad(5));
            var folloowingRecentFeed = getFeedResult.Value;

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(folloowingRecentFeed);
            Assert.True(!folloowingRecentFeed.IsOwnActivity);
        }

        [Fact]
        public async void GetRecentActivityFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult =
                await _authInfo.ApiInstance.GetRecentActivityAsync(PaginationParameters.MaxPagesToLoad(5));
            var ownRecentFeed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(ownRecentFeed);
            Assert.True(ownRecentFeed.IsOwnActivity);
        }

        [Fact]
        public async void GetUserFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult =
                await _authInfo.ApiInstance.GetUserTimelineFeedAsync(PaginationParameters.MaxPagesToLoad(3));
            var feed = getFeedResult.Value;
            var anyDuplicate = feed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyStoryDuplicate = feed.Stories.GroupBy(x => x.Id).Any(g => g.Count() > 1);

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
            Assert.False(anyDuplicate);
            Assert.False(anyStoryDuplicate);
        }

        [Fact]
        public async void GetUserLikeFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getFeedResult = await _authInfo.ApiInstance.GetLikeFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            var feed = getFeedResult.Value;
            var anyDuplicate = feed.GroupBy(x => x.Code).Any(g => g.Count() > 1);

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
            Assert.False(anyDuplicate);
        }
    }
}