using System;
using System.Threading.Tasks;
using InstaSharper.API;

namespace InstaSharper.Examples.Samples
{
    internal class CollectionSample : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public CollectionSample(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            // get all collections of current user
            var collections = await _instaApi.GetCollectionsAsync();
            Console.WriteLine($"Loaded {collections.Value.Items.Count} collections for current user");
            foreach (var instaCollection in collections.Value.Items)
            {
                Console.WriteLine($"Collection: name={instaCollection.CollectionName}, id={instaCollection.CollectionId}");
            }
        }
    }
}