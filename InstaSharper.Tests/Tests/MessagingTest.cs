using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    [Collection("InstaSharper Tests")]
    public class MessagingTest
    {
        public MessagingTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;
        private readonly string _username = "alex_codegarage";
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");

        [Theory]
        [InlineData("340282366841710300949128137443944319108")]
        public async void GetDirectInboxThreadByIdTest(string threadId)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetDirectInboxThreadAsync(threadId);
            var thread = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(thread);
        }

        [Fact]
        public async void GetDirectInboxTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetDirectInboxAsync("", "");
            var inbox = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(inbox);
        }

        [Fact]
        public async void SendMessageTextTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.SendDirectMessageAsync("", "");
            //assert
            Assert.True(result.Succeeded);
        }
    }
}