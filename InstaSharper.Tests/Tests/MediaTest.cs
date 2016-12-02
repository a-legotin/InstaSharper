using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    public class MediaTest
    {
        private readonly ITestOutputHelper output;
        private readonly string password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string username = "alex_codegarage";

        public MediaTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("1379932752706850783")]
        public async void GetMediaByCodeTest(string mediaId)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = username,
                    Password = password
                });
            //act
            output.WriteLine($"Trying to login as user: {username}");
            if (!await TestHelpers.Login(apiInstance, output)) return;
            output.WriteLine($"Getting media by ID: {mediaId}");
            var media = await apiInstance.GetMediaByCodeAsync(mediaId);
            //assert
            Assert.NotNull(media);
        }

        [Theory]
        [InlineData("alex_codegarage")]
        [InlineData("instagram")]
        [InlineData("therock")]
        public async void GetUserMediaListTest(string userToFetch)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = username,
                    Password = password
                });
            var random = new Random(DateTime.Today.Millisecond);
            var pages = random.Next(1, 10);
            //act
            output.WriteLine($"Trying to login as user: {username}");
            if (!await TestHelpers.Login(apiInstance, output)) return;
            output.WriteLine($"Getting posts of user: {userToFetch}");

            var posts = await apiInstance.GetUserMediaAsync(userToFetch, pages);
            //assert
            Assert.NotNull(posts);
            Assert.Equal(userToFetch, posts.Value[random.Next(0, posts.Value.Count)].User.UserName);
        }
    }
}