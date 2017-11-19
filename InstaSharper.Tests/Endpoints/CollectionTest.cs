using InstaSharper.Tests.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class CollectionTest : IClassFixture<AuthenticatedTestFixture>
    {
        private readonly AuthenticatedTestFixture _authInfo;

        public CollectionTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [Theory]
        [InlineData]
        public async void GetCollections()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var media = await _authInfo.ApiInstance.GetCollectionsAsync();
            Assert.NotNull(media);
        }

        [Theory]
        [InlineData(17896519990103727)]
        public async void GetCollectionById(long collectionId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var media = await _authInfo.ApiInstance.GetCollectionAsync(collectionId);
            Assert.NotNull(media);
        }

        [Theory]
        [InlineData("My beautiful collection")]
        public async void CreateCollection(string collectionName)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var newCollection = await _authInfo.ApiInstance.CreateCollectionAsync(collectionName);
            Assert.NotNull(newCollection.Value);

            //Check the new collection name if is the same [...]
            Assert.True(newCollection.Value.CollectionName == collectionName);

            //Get collectionList
            var collectionList = await _authInfo.ApiInstance.GetCollectionsAsync();

            Assert.NotNull(collectionList.Value);

            //Verify that our new collection is on list
            bool found = false;
            int i = 0;
            while (!found && i < collectionList.Value.Items.Count)
                if (collectionList.Value.Items[i++].CollectionId == newCollection.Value.CollectionId)
                    found = true;

            Assert.True(found);
        }
    }
}
