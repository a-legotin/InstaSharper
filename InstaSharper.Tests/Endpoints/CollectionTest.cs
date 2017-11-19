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
        [InlineData(17896519990103727)]
        public async void GetCollectionById(long collectionId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var media = await _authInfo.ApiInstance.GetCollectionAsync(collectionId);
            Assert.NotNull(media);
        }
    }
}
