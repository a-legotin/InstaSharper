using System;
using System.IO;
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
                URI = new Uri(Path.GetFullPath(@"../../../../assets/image.jpg"), UriKind.Absolute).LocalPath
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

            var result =
                await _authInfo.ApiInstance.UploadPhotosAlbumAsync(new[] {mediaImage, mediaImage1},
                    "Collection of design");

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void UploadVideo()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var mediaVideo = new InstaVideo(Path.GetFullPath(@"../../../../assets/video.mp4"), 640, 480, 3);
            var mediaImage = new InstaImage
            {
                Height = 480,
                Width = 640,
                URI = new Uri(Path.GetFullPath(@"../../../../assets/video_image.jpg"), UriKind.Absolute).LocalPath
            };
            var result = await _authInfo.ApiInstance.UploadVideoAsync(mediaVideo, mediaImage, "mountains");

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }
    }
}