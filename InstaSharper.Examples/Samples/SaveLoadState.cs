﻿using System;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;

namespace InstaSharper.Examples.Samples
{
    internal class SaveLoadState : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public SaveLoadState(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var result = await _instaApi.GetCurrentUserAsync();
            if (!result.Succeeded)
            {
                Console.WriteLine($"Unable to get current user using current API instance: {result.Info}");
                return;
            }
            Console.WriteLine($"Got current user: {result.Value.UserName} using existing API instance");
            var stream = _instaApi.GetStateDataAsStream();
            var anotherInstance = InstaApiBuilder.CreateBuilder()
                .SetUser(UserSessionData.Empty)
                .SetRequestDelay(RequestDelay.FromSeconds(2,2))
                .Build();
            anotherInstance.LoadStateDataFromStream(stream);
            var anotherResult = await anotherInstance.GetCurrentUserAsync();
            if (!anotherResult.Succeeded)
            {
                Console.WriteLine($"Unable to get current user using current API instance: {result.Info}");
                return;
            }
            Console.WriteLine(
                $"Got current user: {anotherResult.Value.UserName} using new API instance without re-login");
        }
    }
}