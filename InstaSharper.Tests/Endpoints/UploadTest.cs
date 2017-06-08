using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class UploadTest
    {
        private readonly ITestOutputHelper _output;

        public UploadTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyFact]
        public async void UploadImage()
        {
            var currentUsername = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = currentUsername,
                Password = password
            });

            if (!await TestHelpers.Login(apiInstance, _output)) return;
            var mediaImage = new MediaImage
            {
                Height = 1080,
                Width = 1080,
                URI = new Uri(@"D:\Dropbox\Public\Inspire.jpg", UriKind.Absolute).LocalPath
            };
            var result = await apiInstance.UploadPhotoAsync(mediaImage, "inspire");
       
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }
    }
}
