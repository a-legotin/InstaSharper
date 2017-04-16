using System;
using System.Linq;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Examples.Utils;

namespace InstaSharper.Examples
{
    internal class Basics
    {
        /// <summary>
        /// Api instance (one instance per Instagram user)
        /// </summary>
        private static IInstaApi _instaApi;

        /// <summary>
        /// Config values
        /// </summary>
        private static int _maxDescriptionLength = 20;


        private static void Main(string[] args)
        {
            // create user session data and provide login details
            var userSession = new UserSessionData
            {
                UserName = "username",
                Password = "password"
            };
            // create new InstaApi instance using Builder
            _instaApi = new InstaApiBuilder()
                .SetUser(userSession)
                .Build();
            // login
            var logInResult = _instaApi.Login();
            if (!logInResult.Succeeded) { Console.WriteLine($"Unable to login: {logInResult.Info.Message}"); }
            else
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
                    foreach (var media in currentUserMedia.Value) PrintMedia("Self media", media);
                }

                //get user time line feed, latest 5 pages
                var userFeed = _instaApi.GetUserTimelineFeed(5);
                if (userFeed.Succeeded)
                {
                    Console.WriteLine($"Feed items (in {userFeed.Value.Pages} pages) [{currentUser.UserName}]: {userFeed.Value.Medias.Count}");
                    foreach (var media in userFeed.Value.Medias) PrintMedia("Feed media", media);
                    //like first 10 medias from user timeline feed
                    foreach (var media in userFeed.Value.Medias.Take(10))
                    {
                        var likeResult = _instaApi.LikeMedia(media.InstaIdentifier);
                        var resultString = likeResult.Value ? "liked" : "not like";
                        Console.WriteLine($"Media {media.Code} {resultString}");
                    }
                }

                // get tag feed, latest 5 pages
                var tagFeed = _instaApi.GetTagFeed("quadcopter", 5);
                if (tagFeed.Succeeded)
                {
                    Console.WriteLine($"Tag feed items (in {tagFeed.Value.Pages} pages) [{currentUser.UserName}]: {tagFeed.Value.Medias.Count}");
                    foreach (var media in tagFeed.Value.Medias) PrintMedia("Tag feed", media);
                }
                var logoutResult = _instaApi.Logout();
                if (logoutResult.Value) Console.WriteLine("Logout succeed");
            }
            Console.ReadKey();
        }

        private static void PrintMedia(string header, InstaMedia media)
        {
            Console.WriteLine($"{header} [{media.User.UserName}]: {media.Caption?.Text.Truncate(_maxDescriptionLength)}, {media.Code}, likes: {media.LikesCount}, multipost: {media.IsMultiPost}");
        }
    }
}