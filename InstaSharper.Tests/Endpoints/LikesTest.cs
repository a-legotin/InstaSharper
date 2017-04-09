using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class LikesTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string _username = "alex_codegarage";

        public LikesTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("1484832969772514291_196754384")]
        public async void LikeTest(string mediaId)
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
            var result = await apiInstance.LikeMediaAsync(mediaId);
            var exploreGeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(exploreGeed);
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("1484832969772514291_196754384")]
        public async void UnLikeTest(string mediaId)
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
            var result = await apiInstance.UnLikeMediaAsync(mediaId);
            var exploreGeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(exploreGeed);
        }
    }
}