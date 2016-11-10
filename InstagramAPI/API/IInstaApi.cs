using System.Threading.Tasks;
using InstagramAPI.Classes;

namespace InstagramAPI.API
{
    public interface IInstaApi
    {
        bool IsUserAuthenticated { get; }
        InstaUser GetUser(string username);
        Task<InstaUser> GetUserAsync(string username);
        InstaPostList GetUserPosts(string username);
        Task<InstaPostList> GetUserPostsAsync(string username);
        InstaMedia GetMediaByCode(string postCode);
        Task<InstaMedia> GetMediaByCodeAsync(string postCode);
        bool Login();
        Task<bool> LoginAsync();
        InstaFeed GetUserFeed(int pageCount);
        Task<InstaFeed> GetUserFeedAsync(int pageCount);
    }
}