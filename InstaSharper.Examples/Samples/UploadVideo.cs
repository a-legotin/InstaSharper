using System;
using System.IO;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.Classes.Models;

namespace InstaSharper.Examples.Samples
{
    internal class UploadVideo : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public UploadVideo(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var mediaVideo = new InstaVideo(Path.GetFullPath(@"c:\somevideo.mp4", 1080, 1080, 3));
            var mediaImage = new InstaImage
            {
                Height = 1080,
                Width = 1080,
                URI = new Uri(Path.GetFullPath(@"c:\someawesomepicture.jpg"), UriKind.Absolute).LocalPath
            };
            var result = await _instaApi.UploadVideoAsync(mediaVideo, mediaImage, "ramtinak");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }
    }
}