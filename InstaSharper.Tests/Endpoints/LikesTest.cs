using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class LikesTest : IClassFixture<AuthenticatedTestFixture>
    {
        private readonly AuthenticatedTestFixture _authInfo;

        public LikesTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [Theory]
        [InlineData("1484832969772514291_196754384")]
        public async void LikeUnlikeTest(string mediaId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var likeResult = await _authInfo.ApiInstance.LikeMediaAsync(mediaId);
            var unLikeResult = await _authInfo.ApiInstance.UnLikeMediaAsync(mediaId);
            //assert
            Assert.True(likeResult.Succeeded);
            Assert.True(unLikeResult.Succeeded);
        }
    }
}