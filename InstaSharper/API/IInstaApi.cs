using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API
{
    public interface IInstaApi
    {
        #region Properties

        /// <summary>
        ///     Indicates whether user authenticated or not
        /// </summary>
        bool IsUserAuthenticated { get; }

        #endregion

        #region Sync Members

        /// <summary>
        ///     Login using given credentials
        /// </summary>
        /// <returns>True is succeed</returns>
        IResult<bool> Login();

        /// <summary>
        ///     Logout from instagram
        /// </summary>
        /// <returns>True if completed without errors</returns>
        IResult<bool> Logout();

        /// <summary>
        ///     Get user timeline feed
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        IResult<InstaFeed> GetUserTimelineFeed(int maxPages = 0);

        /// <summary>
        ///     Get user explore feed
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns><see cref="InstaFeed" />></returns>
        IResult<InstaFeed> GetExploreFeed(int maxPages = 0);

        /// <summary>
        ///     Get all user media by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        IResult<InstaMediaList> GetUserMedia(string username, int maxPages = 0);

        /// <summary>
        ///     Get media by its id (code)
        /// </summary>
        /// <param name="mediaCode">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        IResult<InstaMedia> GetMediaByCode(string mediaCode);

        /// <summary>
        ///     Get user info by its user name
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        IResult<InstaUser> GetUser(string username);

        /// <summary>
        ///     Get currently logged in user info
        /// </summary>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        IResult<InstaUser> GetCurrentUser();

        /// <summary>
        ///     Get tag feed by tag value
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        IResult<InstaFeed> GetTagFeed(string tag, int maxPages = 0);

        /// <summary>
        ///     Get followers list by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        IResult<InstaUserList> GetUserFollowers(string username, int maxPages = 0);

        /// <summary>
        ///     Get followers list for currently logged in user
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        IResult<InstaUserList> GetCurentUserFollowers(int maxPages = 0);


        /// <summary>
        ///     Get user tags by username
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        IResult<InstaMediaList> GetUserTags(string username, int maxPages = 0);


        /// <summary>
        ///     Get direct inbox threads for current user
        /// </summary>
        /// <returns>
        ///     <see cref="InstaDirectInboxContainer" />
        /// </returns>
        IResult<InstaDirectInboxContainer> GetDirectInbox();

        /// <summary>
        ///     Get direct inbox thread by its id
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        IResult<InstaDirectInboxThread> GetDirectInboxThread(string threadId);

        /// <summary>
        ///     Get recent recipients (threads and users)
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        IResult<InstaRecipients> GetRecentRecipients();

        /// <summary>
        ///     Get ranked recipients (threads and users)
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        IResult<InstaRecipients> GetRankedRecipients();

        /// <summary>
        ///     Get recent activity info
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        IResult<InstaActivityFeed> GetRecentActivity(int maxPages = 0);

        /// <summary>
        ///     Get activity of following
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        IResult<InstaActivityFeed> GetFollowingRecentActivity(int maxPages = 0);

        #endregion

        #region Async Members

        /// <summary>
        ///     Login using given credentials asynchronously
        /// </summary>
        /// <returns>True is succeed</returns>
        Task<IResult<bool>> LoginAsync();

        /// <summary>
        ///     Logout from instagram asynchronously
        /// </summary>
        /// <returns>True if completed without errors</returns>
        Task<IResult<bool>> LogoutAsync();

        /// <summary>
        ///     Get user timeline feed asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(int maxPages = 0);

        /// <summary>
        ///     Get user explore feed asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns><see cref="InstaFeed" />></returns>
        Task<IResult<InstaFeed>> GetExploreFeedAsync(int maxPages = 0);

        /// <summary>
        ///     Get all user media by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get media by its id (code) asynchronously
        /// </summary>
        /// <param name="mediaCode">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        Task<IResult<InstaMedia>> GetMediaByCodeAsync(string mediaCode);

        /// <summary>
        ///     Get user info by its user name asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        Task<IResult<InstaUser>> GetUserAsync(string username);

        /// <summary>
        ///     Get currently logged in user info asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        Task<IResult<InstaUser>> GetCurrentUserAsync();

        /// <summary>
        ///     Get tag feed by tag value asynchronously
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        Task<IResult<InstaFeed>> GetTagFeedAsync(string tag, int maxPages = 0);

        /// <summary>
        ///     Get followers list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        Task<IResult<InstaUserList>> GetUserFollowersAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get followers list for currently logged in user asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        Task<IResult<InstaUserList>> GetCurrentUserFollowersAsync(int maxPages = 0);

        /// <summary>
        ///     Get user tags by username asynchronously
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserTagsAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get direct inbox threads for current user asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaDirectInboxContainer" />
        /// </returns>
        Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync();

        /// <summary>
        ///     Get direct inbox thread by its id asynchronously
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId);

        /// <summary>
        ///     Get recent recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRecentRecipientsAsync();

        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRankedRecipientsAsync();

        /// <summary>
        ///     Get recent activity info asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetRecentActivityAsync(int maxPages = 0);

        /// <summary>
        ///     Get activity of following asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityAsync(int maxPages = 0);

        #endregion
    }
}