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
        public async void LikeUnlikeTest(string mediaId)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            if (!TestHelpers.Login(apiInstance, _output)) throw new Exception("Not logged in");
            //act
            var likeResult = await apiInstance.LikeMediaAsync(mediaId);
            var unLikeResult = await apiInstance.UnLikeMediaAsync(mediaId);
            //assert
            Assert.True(likeResult.Succeeded);
            Assert.True(unLikeResult.Succeeded);
        }
    }
}