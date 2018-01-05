﻿using System;
using InstaSharper.Classes.Models;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class StoryTest : IClassFixture<AuthenticatedTestFixture>
    {
        public StoryTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Theory]
        [InlineData(267685466)]
        private async void GetUserStoryTest(long userId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetUserStoryAsync(userId);
            var story = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(story);
        }

        [Fact]
        private async void GetStoryFeedTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetStoryFeedAsync();
            var stories = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }

        [Fact]
        public async void UploadStoryImageTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var mediaImage = new InstaImage
            {
                Height = 1200,
                Width = 640,
                URI = new Uri(@"C:\test.jpg", UriKind.Absolute).LocalPath
            };
            var result = await _authInfo.ApiInstance.UploadStoryPhotoAsync(mediaImage, "Lake");

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }
    }
}