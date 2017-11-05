using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
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
            if (result)
                return;
            Console.ReadKey();
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
                var device = AndroidDeviceGenerator.GetByName(AndroidDevices.SAMSUNG_NOTE3);
                var requestMessage = ApiRequestMessage.FromDevice(device);
                _instaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .SetApiRequestMessage(requestMessage)
                    .UseLogger(new DebugLogger(LogLevel.Info)) // use logger for requests and debug messages
                    .SetRequestDelay(TimeSpan.FromSeconds(2))
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
                    Console.WriteLine("Press 4 to start stories demo sample");
                    Console.WriteLine("Press 5 to start demo with saving state of API instance");
                    Console.WriteLine("Press 6 to start messaging demo sample");

                    var samplesMap = new Dictionary<ConsoleKey, IDemoSample>
                    {
                        [ConsoleKey.D1] = new Basics(_instaApi),
                        [ConsoleKey.D2] = new UploadPhoto(_instaApi),
                        [ConsoleKey.D3] = new CommentMedia(_instaApi),
                        [ConsoleKey.D4] = new Stories(_instaApi),
                        [ConsoleKey.D5] = new SaveLoadState(_instaApi),
                        [ConsoleKey.D6] = new Messaging(_instaApi)
                    };
                    var key = Console.ReadKey();
                    Console.WriteLine(Environment.NewLine);
                    if (samplesMap.ContainsKey(key.Key))
                        await samplesMap[key.Key].DoShow();
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