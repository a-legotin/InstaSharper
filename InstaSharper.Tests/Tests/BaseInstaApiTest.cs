using System;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    public class BaseInstaApiTest : IDisposable
    {
        private readonly IInstaApi _apiInstance;
        private readonly ITestOutputHelper _output;

        public BaseInstaApiTest(ITestOutputHelper output)
        {
            _output = output;
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            _apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });
        }

        public void Dispose()
        {
            _apiInstance.LogoutAsync();
        }
    }
}