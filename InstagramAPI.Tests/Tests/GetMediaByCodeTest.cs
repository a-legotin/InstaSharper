using InstagramAPI.Classes;
using InstagramAPI.Tests.Utils;
using Xunit;

namespace InstagramAPI.Tests.Tests
{
    public class GetMediaByCodeTest
    {
        [Fact]
        public async void GetUserPostsTest()
        {
            //arrange
            var username = "alex_codegarage";
            var password = "3591957P@R";
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            var success = await apiInstance.LoginAsync();
            var media = await apiInstance.GetMediaByCodeAsync("1379932752706850783");
            //assert
            Assert.True(success);
        }
    }
}