using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;

namespace InstaSharper.Examples
{
    class InstaSharperExamples
    {
        private static IInstaApi _instaApi;
        static void Main(string[] args)
        {
            // create user session data and provide login details
            var userSession = new UserSessionData
            {
                UserName = "alex_codegarage",
                Password = "3591957P@R"
            };
            // create new InstaApi instance using Builder
            _instaApi = new InstaApiBuilder()
                .SetUser(userSession)
                .Build();
            // login
            var logInResult = _instaApi.Login();
            if (!logInResult.Succeeded)
            {
                Console.WriteLine($"Unable to login: {logInResult.Message}");
            }
            else
            {
                // get currently logged in user
                var currentUser = _instaApi.GetCurrentUser().Value;
                Console.WriteLine($"Logged in: username - {currentUser.UserName}, full name - {currentUser.FullName}");
                // get followers 
                var followers = _instaApi.GetUserFollowersAsync(currentUser.UserName, 5).Result.Value;
                Console.WriteLine($"There are (is) {followers.Count} followers for user {currentUser.UserName}");
                // get user's media 
                var currentUserMedia = _instaApi.GetUserMedia(currentUser.UserName, 5);
                if (currentUserMedia.Succeeded)
                {
                    Console.WriteLine($"There are (is) {currentUserMedia.Value.Count} media for user {currentUser.UserName}");
                    foreach (var media in currentUserMedia.Value)
                    {
                        Console.WriteLine($"Media: {media.Caption.Text}, {media.Code}, likes: {media.LikesCount}, image link: {media.Images.LastOrDefault()?.Url}");
                    }
                }

                //get user feed, first 5 pages
                var userFeed = _instaApi.GetUserFeed(5);
                if (userFeed.Succeeded)
                {
                    Console.WriteLine($"There are {userFeed.Value.Items.Count} feed items in {userFeed.Value.Pages} pages for user {currentUser.UserName}");
                    foreach (var media in userFeed.Value.Items)
                    {
                        Console.WriteLine($"Feed item - code:{media.Code}, likes: {media.LikesCount}");
                    }
                }
                // get tag feed, first 5 pages
                var tagFeed = _instaApi.GetTagFeed("gm", 5);
                if (userFeed.Succeeded)
                {
                    Console.WriteLine($"There are {tagFeed.Value.Count} tag feed items in {tagFeed.Value.Pages} pages for user {currentUser.UserName}");
                    foreach (var media in tagFeed.Value)
                    {
                        Console.WriteLine($"Tag feed item - code: {media.Code}, likes: {media.LikesCount}");
                    }
                }
                var logoutResult = _instaApi.Logout();
                if (logoutResult.Value) Console.WriteLine("Logout succeed");
            }
            Console.ReadKey();
        }
    }
}
