using System;
using System.IO;
using InstaSharper.API;
using InstaSharper.Classes.Models;

namespace InstaSharper.Examples.Samples
{
    internal class UploadPhoto
    {
        private readonly IInstaApi _instaApi;

        public UploadPhoto(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public void DoShow()
        {
            var mediaImage = new MediaImage
            {
                Height = 1080,
                Width = 1080,
                URI = new Uri(Path.GetFullPath(@"c:\someawesomepicture.jpg"), UriKind.Absolute).LocalPath
            };
            var result = _instaApi.UploadPhoto(mediaImage, "someawesomepicture");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }
    }
}