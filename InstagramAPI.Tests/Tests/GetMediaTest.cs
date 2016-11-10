using System;
using InstagramAPI.Classes;
using InstagramAPI.Tests.Utils;
using Xunit;

namespace InstagramAPI.Tests.Tests
{
    public class GetMediaTest
    {
        [Theory]
        [InlineData("1379932752706850783")]
        public async void GetMediaByCodeTest(string mediaId)
        {
            //arrange
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            var success = await apiInstance.LoginAsync();
            var media = await apiInstance.GetMediaByCodeAsync(mediaId);
            //assert
            Assert.True(success);
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
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            var success = await apiInstance.LoginAsync();
            var posts = await apiInstance.GetUserPostsAsync(userToFetch);
            //assert
            Assert.True(success);
            Assert.NotNull(posts);
        }
    }
}