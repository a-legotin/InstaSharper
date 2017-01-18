using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class MessagingTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string _username = "alex_codegarage";

        public MessagingTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyTheory]
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

        [RunnableInDebugOnlyFact]
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
            var result = await apiInstance.GetDirectInboxAsync();
            var inbox = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(inbox);
        }

        [RunnableInDebugOnlyFact]
        public async void GetRankedeRecipientsTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            var result = await apiInstance.GetRankedRecipientsAsync();
            //assert
            Assert.True(result.Succeeded);
        }

        [RunnableInDebugOnlyFact]
        public async void GetRecentRecipientsTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            var result = await apiInstance.GetRecentRecipientsAsync();
            //assert
            Assert.True(result.Succeeded);
        }
    }
}