using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;
using InstaSharper.Classes.Models;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class StoryTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _password = System.IO.File.ReadAllText(@"C:\privKey\instasharp.txt");
        private readonly string _username = "thisidlin";

        public StoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyFact]
        private async void GetStoryTrayTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = TestHelpers.Login(apiInstance, _output);
            Assert.True(loginSucceed);
            var result = await apiInstance.GetStoryTrayAsync();
            var stories = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }

        [RunnableInDebugOnlyTheory]
        [InlineData(1129166614)]
        private async void GetUserStoryTest(long userId)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = TestHelpers.Login(apiInstance, _output);
            Assert.True(loginSucceed);
            var result = await apiInstance.GetUserStoryAsync(userId);
            var stories = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }

        [RunnableInDebugOnlyFact]
        public async void UploadStoryImageTest()
        {
            //arrange
            var apiInstance = 
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = _username,
                Password = _password
            });

            //act
            var loginSucceed = TestHelpers.Login(apiInstance, _output);
            Assert.True(loginSucceed);

            var mediaImage = new MediaImage
            {
                Height = 1200,
                Width = 640,
                URI = new Uri(@"C:\privKey\test.jpg", UriKind.Absolute).LocalPath
            };
            var result = await apiInstance.UploadStoryPhotoAsync(mediaImage, "Lake");

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }
    }
}
