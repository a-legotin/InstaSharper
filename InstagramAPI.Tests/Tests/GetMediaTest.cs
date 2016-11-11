using System;
using System.Threading.Tasks;
using InstagramAPI.API;
using InstagramAPI.Classes;
using InstagramAPI.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstagramAPI.Tests.Tests
{
    public class GetMediaTest
    {
        private readonly ITestOutputHelper output;
        private readonly string username = "alex_codegarage";
        private readonly string password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        public GetMediaTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("1379932752706850783")]
        public async void GetMediaByCodeTest(string mediaId)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
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
        [InlineData("ladygaga")]
        public async void GetUserPostsTest(string userToFetch)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            output.WriteLine($"Trying to login as user: {username}");
            if (!await TestHelpers.Login(apiInstance, output)) return;
            output.WriteLine($"Getting posts of user: {userToFetch}");
            var posts = await apiInstance.GetUserPostsAsync(userToFetch);
            //assert
            Assert.NotNull(posts);
        }


    }
}