using System;
using System.Linq;
using InstaSharper.Classes.Models;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class MediaTest : IClassFixture<AuthenticatedTestFixture>
    {
        private readonly AuthenticatedTestFixture _authInfo;

        public MediaTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [Theory]
        [InlineData("1484832969772514291")]
        public async void GetMediaByIdTest(string mediaId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var media = await _authInfo.ApiInstance.GetMediaByIdAsync(mediaId);
            Assert.NotNull(media);
        }

        [Theory]
        [InlineData("1379932752706850783")]
        public async void GetMediaLikersTest(string mediaId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var likers = await _authInfo.ApiInstance.GetMediaLikersAsync(mediaId);
            var anyDuplicate = likers.Value.GroupBy(x => x.Pk).Any(g => g.Count() > 1);
            Assert.NotNull(likers);
            Assert.False(anyDuplicate);
        }

        [Theory]
        [InlineData("1379932752706850783")]
        public async void GetMediaCommentsTest(string mediaId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var comments = await _authInfo.ApiInstance.GetMediaCommentsAsync(mediaId, 3);

            var anyDuplicate = comments.Value.Comments.GroupBy(x => x.Pk).Any(g => g.Count() > 1);

            Assert.NotNull(comments);
            Assert.False(anyDuplicate);
        }

        [Theory]
        [InlineData("alex_codegarage")]
        public async void GetUserMediaListTest(string userToFetch)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var random = new Random(DateTime.Now.Millisecond);

            var posts = await _authInfo.ApiInstance.GetUserMediaAsync(userToFetch, 3);
            var anyDuplicate = posts.Value.GroupBy(x => x.Code).Any(g => g.Count() > 1);

            Assert.NotNull(posts);
            Assert.Equal(userToFetch, posts.Value[random.Next(0, posts.Value.Count)].User.UserName);
            Assert.False(anyDuplicate);
        }

        [Theory]
        [InlineData("1484832969772514291_196754384")]
        public async void PostDeleteCommentTest(string mediaId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var text = "Test comment";
            var postResult = await _authInfo.ApiInstance.CommentMediaAsync(mediaId, text);
            var delResult = await _authInfo.ApiInstance.DeleteCommentAsync(mediaId, postResult.Value.Pk);
            Assert.True(postResult.Succeeded);
            Assert.Equal(text, postResult.Value.Text);
            Assert.True(delResult.Succeeded);
        }

        [Theory]
        [InlineData("1510405963000000025_1414585238", InstaMediaType.Image)]
        public async void DeleteMediaPhotoTest(string mediaId, InstaMediaType mediaType)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var deleteMediaPhoto = await _authInfo.ApiInstance.DeleteMediaAsync(mediaId, mediaType);
            Assert.False(deleteMediaPhoto.Value);
        }

        [Theory]
        [InlineData("1510414591769980888_1414585238", InstaMediaType.Video)]
        public async void DeleteMediaVideoTest(string mediaId, InstaMediaType mediaType)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var deleteMediaVideo = await _authInfo.ApiInstance.DeleteMediaAsync(mediaId, mediaType);
            Assert.True(deleteMediaVideo.Value);
        }

        [Theory]
        [InlineData("1513736003209429255_1414585238", "Hello!")]
        public async void EditMediaTest(string mediaId, string caption)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var editMedia = await _authInfo.ApiInstance.EditMediaAsync(mediaId, caption);
            Assert.True(editMedia.Value);
        }
    }
}