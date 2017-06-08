using System;
using InstaSharper.Classes.Models;
using InstaSharper.Tests.Classes;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class StoryTest : IClassFixture<AuthenticatedTestFixture>
    {
        readonly AuthenticatedTestFixture _authInfo;

        public StoryTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [RunnableInDebugOnlyFact]
        private async void GetStoryTrayTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetStoryTrayAsync();
            var stories = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }

        [RunnableInDebugOnlyTheory]
        [InlineData(1129166614)]
        private async void GetUserStoryTest(long userId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetUserStoryAsync(userId);
            var stories = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }

        [RunnableInDebugOnlyFact]
        public async void UploadStoryImageTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var mediaImage = new MediaImage
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
