using System.Threading.Tasks;
using InstaSharper.Classes.Models;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class MessagingTest : IClassFixture<AuthenticatedTestFixture>
    {
        public MessagingTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Theory]
        [InlineData("340282366841710300949128137443944319108")]
        public async void GetDirectInboxThreadByIdTest(string threadId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetDirectInboxThreadAsync(threadId);
            var thread = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(thread);
        }

        [Theory]
        [InlineData("196754384")]
        public async void SendDirectTextMessageTest(string user)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var text = "This is test ©";
            var result =
                await _authInfo.ApiInstance.SendDirectMessage(user, "340282366841710300949128137443944319108", text);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Count > 0);
        }
        
        [Theory]
        [InlineData("340282366841710300949128137443944319108")]
        public async Task SendDirectLinkMessageThreadTest(string threadId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var message = new InstaMessageLink
            {
                Url = "google.com",
                Text = "This is link description"
            };
            
            var result =
                await _authInfo.ApiInstance.SendLinkMessage(message, threadId);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Count > 0);
        }
        
        [Theory]
        [InlineData(196754384)]
        public async Task SendDirectLinkMessageUserTest(long userPk)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var message = new InstaMessageLink
            {
                Url = "youtube.com",
                Text = "YouTube here"
            };
            
            var result =
                await _authInfo.ApiInstance.SendLinkMessage(message, userPk);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Count > 0);
        }

        [Fact]
        public async void GetDirectInboxTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetDirectInboxAsync();
            var inbox = result.Value;
            Assert.True(result.Succeeded);
            Assert.NotNull(inbox);
        }

        [Fact]
        public async void GetRankedeRecipientsTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetRankedRecipientsAsync();
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void GetRecentRecipientsTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetRecentRecipientsAsync();
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }
    }
}