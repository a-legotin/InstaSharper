using System;
using System.Linq;
using InstaSharper.API;
using InstaSharper.Examples.Utils;

namespace InstaSharper.Examples.Samples
{
    internal class Basics
    {
        /// <summary>
        ///     Config values
        /// </summary>
        private static readonly int _maxDescriptionLength = 20;

        private readonly IInstaApi _instaApi;

        public Basics(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public void DoShow()
        {
            // get currently logged in user
            var currentUser = _instaApi.GetCurrentUser().Value;
            Console.WriteLine($"Logged in: username - {currentUser.UserName}, full name - {currentUser.FullName}");

            // get self followers 
            var followers = _instaApi.GetUserFollowersAsync(currentUser.UserName, 5).Result.Value;
            Console.WriteLine($"Count of followers [{currentUser.UserName}]:{followers.Count}");

            // get self user's media, latest 5 pages
            var currentUserMedia = _instaApi.GetUserMedia(currentUser.UserName, 5);
            if (currentUserMedia.Succeeded)
            {
                Console.WriteLine($"Media count [{currentUser.UserName}]: {currentUserMedia.Value.Count}");
                foreach (var media in currentUserMedia.Value)
                    ConsoleUtils.PrintMedia("Self media", media, _maxDescriptionLength);
            }

            //get user time line feed, latest 5 pages
            var userFeed = _instaApi.GetUserTimelineFeed(5);
            if (userFeed.Succeeded)
            {
                Console.WriteLine(
                    $"Feed items (in {userFeed.Value.Pages} pages) [{currentUser.UserName}]: {userFeed.Value.Medias.Count}");
                foreach (var media in userFeed.Value.Medias)
                    ConsoleUtils.PrintMedia("Feed media", media, _maxDescriptionLength);
                //like first 10 medias from user timeline feed
                foreach (var media in userFeed.Value.Medias.Take(10))
                {
                    var likeResult = _instaApi.LikeMedia(media.InstaIdentifier);
                    var resultString = likeResult.Value ? "liked" : "not liked";
                    Console.WriteLine($"Media {media.Code} {resultString}");
                }
            }

            // get tag feed, latest 5 pages
            var tagFeed = _instaApi.GetTagFeed("quadcopter", 5);
            if (tagFeed.Succeeded)
            {
                Console.WriteLine(
                    $"Tag feed items (in {tagFeed.Value.Pages} pages) [{currentUser.UserName}]: {tagFeed.Value.Medias.Count}");
                foreach (var media in tagFeed.Value.Medias)
                    ConsoleUtils.PrintMedia("Tag feed", media, _maxDescriptionLength);
            }
        }
    }
}