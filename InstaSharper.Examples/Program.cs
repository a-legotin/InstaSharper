using System;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Examples.Samples;
using InstaSharper.Logger;

namespace InstaSharper.Examples
{
    public class Program
    {
        /// <summary>
        ///     Api instance (one instance per Instagram user)
        /// </summary>
        private static IInstaApi _instaApi;

        private static void Main(string[] args)
        {
            var result = Task.Run(MainAsync).GetAwaiter().GetResult();
        }

        public static async Task<bool> MainAsync()
        {
            try
            {
                Console.WriteLine("Starting demo of InstaSharper project");
                // create user session data and provide login details
                var userSession = new UserSessionData
                {
                    UserName = "username",
                    Password = "password"
                };

                // create new InstaApi instance using Builder
                _instaApi = new InstaApiBuilder()
                    .SetUser(userSession)
                    .UseLogger(new DebugLogger()) // use logger for requests and debug messages
                    .SetRequestDelay(TimeSpan.FromSeconds(1)) // set delay between requests
                    .Build();

                // login
                Console.WriteLine($"Logging in as {userSession.UserName}");
                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                {
                    Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                }
                else
                {
                    Console.WriteLine("Press 1 to start basic demo samples");
                    Console.WriteLine("Press 2 to start upload photo demo sample");
                    Console.WriteLine("Press 3 to start comment media demo sample");

                    var key = Console.ReadKey();
                    Console.WriteLine(Environment.NewLine);
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            var basics = new Basics(_instaApi);
                            await basics.DoShow();
                            break;
                        case ConsoleKey.D2:
                            var upload = new UploadPhoto(_instaApi);
                            await upload.DoShow();
                            break;
                        case ConsoleKey.D3:
                            var comment = new CommentMedia(_instaApi);
                            await comment.DoShow();
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("Done. Press esc key to exit...");
                    key = Console.ReadKey();
                    return key.Key == ConsoleKey.Escape;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                var logoutResult = Task.Run(() => _instaApi.LogoutAsync()).GetAwaiter().GetResult();
                if (logoutResult.Succeeded) Console.WriteLine("Logout succeed");
            }
            return false;
        }
    }
}
