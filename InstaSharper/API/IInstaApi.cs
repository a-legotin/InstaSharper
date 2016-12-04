using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;

namespace InstaSharper.API
{
    public interface IInstaApi
    {
        #region Properties

        bool IsUserAuthenticated { get; }

        #endregion

        #region Sync Members

        IResult<InstaUser> GetUser(string username);
        Task<IResult<InstaUser>> GetUserAsync(string username);
        IResult<InstaMediaList> GetUserMedia(string username, int maxPages = 0);
        Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, int maxPages = 0);
        IResult<InstaMedia> GetMediaByCode(string postCode);
        Task<IResult<InstaMedia>> GetMediaByCodeAsync(string postCode);
        IResult<bool> Login();
        IResult<bool> Logout();

        IResult<InstaUser> GetCurrentUser();
        IResult<InstaUserList> GetCurentUserFollowers(int maxPages = 0);
        IResult<InstaUserList> GetUserFollowers(string username, int maxPages = 0);

        IResult<InstaMediaList> GetTagFeed(string tag, int maxPages = 0);

        #endregion

        #region Async Members

        Task<IResult<bool>> LoginAsync();
        Task<IResult<bool>> LogoutAsync();
        IResult<InstaFeed> GetUserFeed(int maxPages = 0);
        Task<IResult<InstaFeed>> GetUserFeedAsync(int maxPages = 0);
        Task<IResult<InstaUser>> GetCurrentUserAsync();

        Task<IResult<InstaMediaList>> GetTagFeedAsync(string tag, int maxPages = 0);
        Task<IResult<InstaUserList>> GetUserFollowersAsync(string username, int maxPages = 0);
        Task<IResult<InstaUserList>> GetCurrentUserFollowersAsync(int maxPages = 0);

        #endregion
    }
}