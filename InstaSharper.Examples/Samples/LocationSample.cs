﻿using System;
using System.Linq;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.Classes;

namespace InstaSharper.Examples.Samples
{
    internal class LocationSample : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public LocationSample(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            // search for related locations near location with latitude = 55.753923, logitude = 37.620940
            // additionaly you can specify search query or just empty string
            var result = await _instaApi.SearchLocation(55.753923, 37.620940, "square");
            Console.WriteLine($"Loaded {result.Value.Count} locations");
            var firstLocation = result.Value?.FirstOrDefault();
            if(firstLocation == null)
                return;
            Console.WriteLine($"Loading feed for location: name={firstLocation.Name}; id={firstLocation.ExternalId}.");

            var locationFeed =
                await _instaApi.GetLocationFeed(long.Parse(firstLocation.ExternalId), PaginationParameters.MaxPagesToLoad(5));

            Console.WriteLine(locationFeed.Succeeded
                ? $"Loaded {locationFeed.Value.Medias?.Count} medias for location, total location medias: {locationFeed.Value.MediaCount}"
                : $"Unable to load location '{firstLocation.Name}' feed");
        }
    }
}