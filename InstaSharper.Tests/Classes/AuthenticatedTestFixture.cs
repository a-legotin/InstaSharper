using System;
using System.IO;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;

namespace InstaSharper.Tests.Classes
{
    public class AuthenticatedTestFixture
    {
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string _username = "alex_codegarage";

        public AuthenticatedTestFixture()
        {
            ApiInstance =
                TestHelpers.GetDefaultInstaApiInstance(UserSessionData.ForUsername(_username).WithPassword(_password));
            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    Stream fs = File.OpenRead(stateFile);
                    fs.Seek(0, SeekOrigin.Begin);
                    ApiInstance.LoadStateDataFromStream(fs);
                    if (ApiInstance.IsUserAuthenticated)
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var loginTask = Task.Run(ApiInstance.LoginAsync);

            if (!loginTask.Wait(TimeSpan.FromSeconds(30)))
                throw new Exception($"Unable to login, user: {_username}, password: {_password}.");

            if (!loginTask.Result.Succeeded) return;
            var state = ApiInstance.GetStateDataAsStream();
            using (var fileStream = File.Create(stateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }
        }

        public IInstaApi ApiInstance { get; }

        public string GetUsername()
        {
            return _username;
        }

        public string GetPassword()
        {
            return _password;
        }
    }
}