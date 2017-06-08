using InstaSharper.Tests.Classes;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class MessagingTest : IClassFixture<AuthenticatedTestFixture>
    {
        readonly AuthenticatedTestFixture _authInfo;

        public MessagingTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("340282366841710300949128137443944319108")]
        public async void GetDirectInboxThreadByIdTest(string threadId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetDirectInboxThreadAsync(threadId);
            var thread = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(thread);
        }

        [RunnableInDebugOnlyFact]
        public async void GetDirectInboxTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetDirectInboxAsync();
            var inbox = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(inbox);
        }

        [RunnableInDebugOnlyFact]
        public async void GetRankedeRecipientsTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetRankedRecipientsAsync();
            Assert.True(result.Succeeded);
        }

        [RunnableInDebugOnlyFact]
        public async void GetRecentRecipientsTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetRecentRecipientsAsync();
            Assert.True(result.Succeeded);
        }
    }
}