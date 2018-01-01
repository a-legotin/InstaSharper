using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class CollectionTest : IClassFixture<AuthenticatedTestFixture>
    {
        public CollectionTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Theory]
        [InlineData(17913091552048277)]
        public async void GetCollectionByIdTest(long collectionId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var media = await _authInfo.ApiInstance.GetCollectionAsync(collectionId);
            Assert.NotNull(media);
        }

        [Theory]
        [InlineData("New collection")]
        public async void CreateCollectionTest(string collectionName)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var newCollection = await _authInfo.ApiInstance.CreateCollectionAsync(collectionName);
            var collectionList = await _authInfo.ApiInstance.GetCollectionsAsync();

            Assert.NotNull(newCollection.Value);
            Assert.True(newCollection.Value.CollectionName == collectionName);
            Assert.NotNull(collectionList.Value);
        }

        [Theory]
        [InlineData(17913091552048277)]
        public async void AddItemsToCollectionTest(long collectionId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var mediaItems = new[] {"1658893120999767931"};
            var result = await _authInfo.ApiInstance.AddItemsToCollectionAsync(collectionId, mediaItems);

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.Equal(result.Value.CollectionId, collectionId);
        }

        [Theory]
        [InlineData(17913091552048277)]
        public async void DeleteCollectionTest(long collectionId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var media = await _authInfo.ApiInstance.DeleteCollectionAsync(collectionId);
            Assert.True(media.Succeeded);
        }

        [Fact]
        public async void GetCollectionsTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var collections = await _authInfo.ApiInstance.GetCollectionsAsync();
            Assert.NotNull(collections);
        }
    }
}