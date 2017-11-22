using System;
using InstaSharper.Classes.Models;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class UploadTest : IClassFixture<AuthenticatedTestFixture>
    {
        public UploadTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Fact]
        public async void UploadImage()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var mediaImage = new InstaImage
            {
                Height = 1080,
                Width = 1080,
                URI = new Uri(@"D:\Dropbox\Public\Inspire.jpg", UriKind.Absolute).LocalPath
            };
            var result = await _authInfo.ApiInstance.UploadPhotoAsync(mediaImage, "inspire");

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void UploadImagesAsAlbumTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var mediaImage = new InstaImage
            {
                Height = 512,
                Width = 512,
                URI = new Uri(@"C:\tmp\1.jpg", UriKind.Absolute).LocalPath
            };

            var mediaImage1 = new InstaImage
            {
                Height = 512,
                Width = 512,
                URI = new Uri(@"C:\tmp\2.jpg", UriKind.Absolute).LocalPath
            };

            var result = await _authInfo.ApiInstance.UploadPhotosAlbumAsync(new[] { mediaImage, mediaImage1}, "Collection of design");

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }
    }
}